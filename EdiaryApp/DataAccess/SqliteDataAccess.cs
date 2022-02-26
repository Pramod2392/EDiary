using Dapper;
using DataAccess.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class SqliteDataAccess
    {        
        private static string GetConnectionString(string name = "EdiarySqlite")
        {
            string connString =  ConfigurationManager.ConnectionStrings[name].ToString();
            return connString;
        }

        public static List<QuestionModel> GetQuestions()
        {
            try
            {
                using (SqliteConnection conn = new SqliteConnection(GetConnectionString()))
                {
                    var questions = conn.Query<QuestionModel>("SELECT * FROM Questions");
                    return questions.ToList<QuestionModel>();
                }
            }
            catch (Exception ex)
            {
                return new List<QuestionModel>();
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="answers"></param>
        public static void SaveAnswersForADay(List<AnswerModel> answers)
        {
            //Save Data in a report table.
            //Save Data in answers table as a transaction.
            int reportId;
            int CRUDAction;
            try
            {
               Tuple<int,int> tuple =  GetReportId();
                reportId = tuple.Item1;
                CRUDAction = tuple.Item2;

                using (SqliteConnection conn = new SqliteConnection(GetConnectionString()))
                {
                    foreach (AnswerModel answer in answers)
                    {
                        if (CRUDAction.Equals((int)ActionEnum.CRUD.Insert))
                        {
                            conn.Execute("Insert into Answers (Answer, DateModified, QuestionId, ReportId) values (@answer,@dateModified,@questionId,@reportId)",
                            new { answer = answer.answer, dateModified = DateTime.Now.ToLongDateString(), questionId = answer.questionId, reportId = reportId });
                        }
                        else if (CRUDAction.Equals((int)ActionEnum.CRUD.Update))
                        {
                            conn.Execute("Update Answers SET Answer = (@answer) where ReportId = (@reportId) AND QuestionId = (@questionId)",
                                new { answer = answer.answer, reportId = reportId, questionId = answer.questionId });
                        }

                    } 
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the reportId.
        /// </summary>
        /// <returns>Tuple reportId,CRUDAction</returns>
        private static Tuple<int,int> GetReportId()
        {
            int CRUDAction;
            int reportId;
            using (SqliteConnection conn = new SqliteConnection(GetConnectionString()))
            {
                var id = conn.Query<int>("SELECT Id from Reports where ReportDate = (@ReportDate)",
                    new { ReportDate = DateTime.Now.ToShortDateString() });

                if (id.Count() == 0)
                {
                    conn.Execute("Insert into Reports (ReportDate) values (@reportDate) ",
                        new { reportDate = DateTime.Now.ToShortDateString() });
                    CRUDAction = (int)ActionEnum.CRUD.Insert;
                }
                else
                {
                    CRUDAction = (int)ActionEnum.CRUD.Update;
                }

                reportId = conn.Query<int>("SELECT Id FROM Reports where ReportDate = (@reportDate) ",
                    new { reportDate = DateTime.Now.ToShortDateString() }).FirstOrDefault();                

                return new Tuple<int, int>(reportId,CRUDAction);    
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static int GetReportIdForGivenDate(string date)
        {
            using (SqliteConnection conn = new SqliteConnection(GetConnectionString()))
            {
                int id = conn.Query<int>("SELECT Id from Reports where ReportDate = (@ReportDate)",
                    new { ReportDate = date }).FirstOrDefault();                                

                return id;
            }
        }

        public static List<AnswerModel> GetAnswerForGivendate(string date)
        {
            List<AnswerModel> output;
            int reportId = GetReportIdForGivenDate(date);
            using (SqliteConnection conn = new SqliteConnection(GetConnectionString()))
            {                
                output = conn.Query<AnswerModel>("SELECT * FROM Answers where ReportId = (@reportId)", new {@reportId = reportId}).ToList();
            }

            return output;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TextDataAccess : ITextDataAccess
    {
        /// <summary>
        /// 
        /// </summary>
        public void WriteToText(List<string> dataToText)
        {
            string? rootDirectory = ConfigurationManager.AppSettings.Get("EdairyFolder");
            FileInfo FileInfo = new FileInfo($"{rootDirectory}{DateTime.Now.ToShortDateString()}");
            if (!string.IsNullOrWhiteSpace(rootDirectory))
            {
                using (StreamWriter sw = new StreamWriter(FileInfo.FullName))
                {
                    foreach (string data in dataToText)
                    {
                        sw.WriteLine(data);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ReadFromTextModel ReadFromText(DateOnly dateString)
        {
            string? rootDirectory = ConfigurationManager.AppSettings.Get("EdairyFolder");
            FileInfo FileInfo = new FileInfo($"{rootDirectory}{dateString.ToShortDateString()}");
            ReadFromTextModel readFromText;

            if (FileInfo.Exists)
            {
                using (StreamReader reader = new StreamReader(FileInfo.FullName))
                {
                    readFromText = new ReadFromTextModel(true, reader.ReadToEnd());

                }
            }

            else
            {
                readFromText = new ReadFromTextModel(false, "Data does not exist for the given date.");
            }

            return readFromText;
        }
    }
}

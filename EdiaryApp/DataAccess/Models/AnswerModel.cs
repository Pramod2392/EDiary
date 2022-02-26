using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class AnswerModel
    {
        public string answer { get; set; } = string.Empty;

        public int questionId { get; set; }

        public int reportId { get; set; }

        private DateTime _dateModified;

        public DateTime DateModified
        {
            get 
            { 
                return _dateModified; 
            }
            set
            { 
                _dateModified = value; 
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; } = String.Empty;  
    }
}

using ClimbingClub.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingClub.Library
{
    public class Training
    {
        [Key]
        public DateTime TrainingDate { get; set; }
        public virtual Member Member { get; set; }
        [NotMapped]
        public string timeFormat { get { return TrainingDate.Day + "." + TrainingDate.Month + "." + TrainingDate.Year; } }
    }
}

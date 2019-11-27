using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClimbingClub.Library
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Level { get; set; }

        [NotMapped]
        public bool trainedToday { get {
                bool trainingDone = false;
                using(var db=new ApplicationDbContext())
                {
                    trainingDone = db.Trainings.Include(tr => tr.Member).Where(tr => (tr.Member.Id == Id && tr.TrainingDate.Date.Equals(DateTime.Now.Date))).Any();
                }
                return trainingDone;
            } 
        }
    }
}

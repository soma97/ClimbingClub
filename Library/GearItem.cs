using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingClub.Library
{
    public class GearItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Loaning Loaning { get; set; }

        [NotMapped]
        public string Loan { get
            {
                if(Loaning==null)
                {
                    return "Available";
                }
                else
                {
                    return Loaning.Member.Name+" "+Loaning.Member.Surname;
                }
            } }
    }
}

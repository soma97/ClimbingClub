using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingClub.Library
{
    public class GearItemLoaning
    {
        [Key, Column(Order=0)]
        public int IdLoaning { get; set; }
        [Key, Column(Order = 1)]
        public int IdGearItem { get; set; }

        public bool isActiveNow { get; set; }
        public int CountLoaned { get; set; }

        public virtual Loaning Loaning { get; set; }
        public virtual GearItem GearItem { get; set; }
    }
}

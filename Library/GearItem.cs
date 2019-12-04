using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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

        public int CountAvailable { get; set; }

        [NotMapped]
        public List<int> itemsNumber { get
            {
                int sumLoaned = 0;

                using (var db = new ApplicationDbContext())
                {
                    foreach (var x in db.GearLoanings.Where(g => g.IdGearItem == Id && g.isActiveNow == true))
                    {
                        sumLoaned += x.CountLoaned;
                    }
                }
                List<int> list = new List<int>();
                for(int i=0;i<=CountAvailable-sumLoaned;++i)
                {
                    list.Add(i);
                }
                return list;
            } }

        [NotMapped]
        public string Loan { get
            {
                string result = "Available";
                using(var db=new ApplicationDbContext())
                {
                    bool isAvailable = true;
                    int loanId = 0;
                    foreach(var x in db.GearLoanings.Where(gl=>gl.IdGearItem==Id))
                    {
                        if(x.isActiveNow==true)
                        {
                            isAvailable = false;
                            loanId = x.IdLoaning;
                        }
                    }
                    if(isAvailable==false)
                    {
                        result = db.Loanings.Include(l => l.Member).Where(l=>l.Id==loanId).Select(l=> l.Member.Name+" "+l.Member.Surname).FirstOrDefault();
                    }
                }
                return result;
            } }
    }
}

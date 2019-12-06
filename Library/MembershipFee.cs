using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ClimbingClub.Library
{
    public class MembershipFee
    {
        [Key]
        public int Id { get; set; }
        public DateTime Payment { get; set; }
        public int Price { get; set; }
        public Boolean IsMonthly { get; set; }
        public virtual Member Member { get; set; }

        [NotMapped]
        public string timeFormat { get { return IsMonthly ? Payment.ToString("MMM") + "," + Payment.Year : Payment.Day + "." + Payment.Month + "." + Payment.Year; } }
        [NotMapped]
        public string isMonthlyFormat { get { return IsMonthly ? (Application.Current.Resources["Monthly fee"] as string) : (Application.Current.Resources["One training fee"] as string); } }
    }
}

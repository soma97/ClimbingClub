﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ClimbingClub.Library
{
    public class Loaning
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public virtual Member Member { get; set; }
        public virtual User user { get; set; }

        [NotMapped]
        public string returnDateGet { get
            {
                if (ReturnDate.Equals(DateTime.MinValue))
                {
                    string result = (Application.Current.Resources["Loaned"] as string);
                    if (DateTime.Now.CompareTo(ExpectedReturnDate) > 0)
                    {
                        result += "("+ (Application.Current.Resources["overdue"] as string) + ")";
                    }
                    return result;
                }
                else return ReturnDate.Day+"."+ReturnDate.Month+"."+ReturnDate.Year;
            } }
        [NotMapped]
        public string loanDateGet
        {
            get
            {
                return LoanDate.Day + "." + LoanDate.Month + "." + LoanDate.Year;
            }
        }
        [NotMapped]
        public string expectedReturnDateGet
        {
            get
            {
                return ExpectedReturnDate.Day + "." + ExpectedReturnDate.Month + "." + ExpectedReturnDate.Year;
            }
        }
    }
}

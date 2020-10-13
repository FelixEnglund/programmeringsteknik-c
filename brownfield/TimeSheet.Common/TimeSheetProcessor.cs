using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Common.Models;

namespace TimeSheet.Common
{
    public class TimeSheetProcessor
    {
        public static int CalculateTimeForCustomer(List<TimeSheetEntryModels> entries, string customerName)
        {
            int sum = 0;

            foreach (var entry in entries)
            {
                int customerIndex = entry.WorkDone.IndexOf(customerName, StringComparison.OrdinalIgnoreCase);
                if (customerIndex >= 0)
                {
                    sum += entry.HoursWorked;
                }
            }

            return sum;
        }

        public static int CalculateTimeWorked(List<TimeSheetEntryModels> entries) =>
            entries.Sum(x => x.HoursWorked);  //Lambda
        
            
       
       //{
        //    return entries.Sum(x => x.HoursWorked);
           
        //}
    }
}

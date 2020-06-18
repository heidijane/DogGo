using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Utilities
{
    public class TimeUtils
    {
        //converts seconds into a string with the hours and minutes
        public static string SecondsToHoursAndMinutes(int seconds)
        {
            int hours = (seconds % (24 * 3600)) / 3600;
            int minutes = (seconds % (24 * 3600 * 3600)) / 60;

            return $"{(hours < 1 ? "" : $"{ hours}hr ")}{minutes}min";
        }
    }
}

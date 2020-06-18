using System;
using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public List<Walk> Walks { get; set; }

        public int TotalWalkTime
        {
            get
            {
                int TotalWalkTime = 0;
                foreach (Walk walk in Walks)
                {
                    TotalWalkTime += walk.Duration;
                }
                return TotalWalkTime;
            }
        }
    }
}
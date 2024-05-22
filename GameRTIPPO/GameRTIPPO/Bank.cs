using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRTIPPO
{
    internal class Bank
    {
        public int TotalPoints { get; set; } 

        public Bank(int initialPoints)
        {
            TotalPoints = initialPoints;
        }

        public void AddPoints(int points)
        {
            TotalPoints += points; //Каждый игрок делает по ставке (указана в скобках)
        }

        public int TakeAllPoints()
        {
            int points = TotalPoints; 
            TotalPoints = 0;
            return points;
        }
    }
}

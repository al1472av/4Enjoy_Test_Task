using System;
using System.Collections.Generic;

namespace LifeGame.Services.PlayerData
{
    [Serializable]
    public class Data
    {
        public int Lives;
        public int Coins;
        public DateTime QuitTime;
        public List<DateTime> DailyClaimed; 
        
        public Data()
        {
            DailyClaimed = new List<DateTime>();
            Lives = 0;
            Coins = 0;
        }
    }
}
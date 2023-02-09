using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace LifeGame.Services.Timer
{
    public static class Seasons
    {
        private static Season[] _seasons = new[]
        {
            new Season(new[] { 3, 4, 5 }),
            new Season(new[] { 6, 7, 8 }),
            new Season(new[] { 9, 10, 11 }),
            new Season(new[] { 12, 1, 2 }),
        };

        public static int DayInSeason(DateTime dateTime)
        {
            return _seasons.First(t => t.Months.Contains(dateTime.Month)).DayInSeason(dateTime);
        }


        private class Season
        {
            public int[] Months { get; }

            public Season(int[] months)
            {
                Months = months;
            }

            public int DayInSeason(DateTime dateTime)
            {
                int totalDays = 0;

                for (int i = 0; i < Months.Length; i++)
                {
                    if (Months[i] == dateTime.Month)
                    {
                        totalDays += dateTime.Day;
                        break;
                    }

                    totalDays += DateTime.DaysInMonth(dateTime.Day, Months[i]);
                }

                return totalDays;
            }
        }
    }
}
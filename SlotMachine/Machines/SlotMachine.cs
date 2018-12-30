using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SlotMachine.Machines
{
    public class VirtualSlotMachine
    {
        private readonly string[] SymbolDistribution = new string[]
           {
               ProjectContstants.Seven,
               ProjectContstants.Bar3,
               ProjectContstants.Bar2,
               ProjectContstants.Bar2,
               ProjectContstants.Bar,
               ProjectContstants.Bar,
               ProjectContstants.Bar,
               ProjectContstants.Cherry
           };

        public string[][] Slots { get; set; }

        public VirtualSlotMachine(int slotCount)
        {
            Slots = new string[slotCount][];
            Random rnd = new Random();
            for (int i = 0; i<slotCount; i++)
            {
                Slots[i] = SuffleArray(SymbolDistribution, rnd);
            }
        }

        private string[] SuffleArray(string[] arr, Random rnd)
        {
            var tempArr = new List<string>(arr);
            var shuffledArr = new List<string>();
            while (tempArr.Count > 0)
            { 
                var rndItem = rnd.Next(0, tempArr.Count);
                shuffledArr.Add(tempArr[rndItem]);
                tempArr.RemoveAt(rndItem);
                shuffledArr.Add(ProjectContstants.Blank);
            }

            return shuffledArr.ToArray();
        }

        public static int CalcPayout(string[] combination, int creditsPlayed)
        {
           
            if (combination.All(v => v == ProjectContstants.Seven))
            {
                if (creditsPlayed == 3)
                    return 1500;

                return 300 * creditsPlayed;
            }

            if (combination.All(v => v == ProjectContstants.Bar3))
            {
                return 100 * creditsPlayed;
            }

            if (combination.All(v => v == ProjectContstants.Bar2))
            {
                return 50 * creditsPlayed;
            }

            if (combination.All(v => v == ProjectContstants.Bar))
            {
                return 25 * creditsPlayed;
            }

            if (combination.All(v => (v == ProjectContstants.Bar) || (v== ProjectContstants.Bar2) || (v == ProjectContstants.Bar3)))
            {
                return 5 * creditsPlayed;
            }

            var cherryes = combination.Where(s => s == ProjectContstants.Cherry).Count();
            if (cherryes > 0)
            {
                return 2 * cherryes * creditsPlayed;
            }

            return 0;
        }
    }
}
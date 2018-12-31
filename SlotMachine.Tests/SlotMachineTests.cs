using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlotMachine.Machines;
using System.Linq;

namespace SlotMachine.Tests
{
    [TestClass]
    public class SlotMachineTests
    {
        [TestMethod]
        public void TestSlotLengthEquals16()
        {
            var machine = new VirtualSlotMachine(3);
            foreach (var slot in machine.Slots)
                Assert.IsTrue(slot.Length == 16);
        }

        [TestMethod]
        public void TestSlotHasBlankItemBetweenSymbols()
        {
            var machine = new VirtualSlotMachine(3);
            foreach (var slot in machine.Slots)
                for (int i = 0; i<slot.Length;i++)
                {
                    var notBlakSymbol = !(slot[i] == ProjectContstants.Blank);
                    if (notBlakSymbol)
                    {
                        Assert.IsTrue(slot[(i + 1) % slot.Length] == ProjectContstants.Blank);
                    }
                }
        }

        [TestMethod]
        public void TestPayout()
        {
            var combination = new[] {
            ProjectContstants.Bar3,
            ProjectContstants.Bar3,
            ProjectContstants.Bar3
            };
            var money = VirtualSlotMachine.CalcPayout(combination, 2);

            Assert.IsTrue(money == 200);
        }

        [TestMethod]
        public void TestSlotContainsDistributionSymbols()
        {
            string[] SymbolDistribution = new string[]
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
           var machine = new VirtualSlotMachine(3);

            foreach (var slot in machine.Slots)
                Assert.IsFalse(SymbolDistribution.Except(slot).Any());

        }
    }
}

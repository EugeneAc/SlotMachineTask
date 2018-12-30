using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlotMachine.Machines;

namespace SlotMachine.Tests
{
    [TestClass]
    public class TestMachine
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
    }
}

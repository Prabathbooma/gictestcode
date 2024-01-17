using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperApp.Service.Implements;

namespace MineSweeperApp.UnitTest
{
    [TestClass]
    public class MineSweeperAppTest
    {
        //create Mocking instance for service
        MineSweeperServices mineSweeperServices = new MineSweeperServices();

        [TestMethod]
        public void TestMinGridInput()
        {

            var result = mineSweeperServices.IsValidgridSize(2);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void TestMaxGridInput()
        {
            var result = mineSweeperServices.IsValidgridSize(11);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void TestValidGridInput()
        {
            var result = mineSweeperServices.IsValidgridSize(3);
            Assert.AreEqual(result, true);
        }


        [TestMethod]
        public void TestZeroMinesInput()
        {
            var result = mineSweeperServices.IsValidNumberOfMines(3, 0);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void TestInvalidMinesInput()
        {
            var result = mineSweeperServices.IsValidNumberOfMines(3, 4);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void TestValidInputOfSquare()
        {
            var result = mineSweeperServices.IsValidInput("A", 3);
            Assert.AreEqual(result, false);
        }
    }
}
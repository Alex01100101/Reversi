using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reversi;

namespace Tests
{
    [TestClass]
    public class GameGridTests
    {
        [TestMethod]
        public void Constructor_IntialValues_Test()
        {
            //arrange
            //act
            GameGrid gameGrid = new GameGrid(8, 8);

            //assert
            Assert.AreEqual(gameGrid.Rows, 8, "Rows should be 8");
            Assert.AreEqual(gameGrid.Columns, 8, "Columns should be 8");
            Assert.AreEqual(gameGrid[4, 4], 1, "4 4 should be Player 1");
        }
    }
}

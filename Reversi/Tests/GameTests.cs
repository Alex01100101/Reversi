using Reversi;

namespace Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void Reset_InitialValues_Test()
        {
            //arrange
            //act
            Game game = new Game();

            //assert
            Assert.AreEqual(game.GameOver, false, "GameOver should be false");
            Assert.AreEqual(game.Player, 1, "Player should be 1");
            Assert.AreEqual(game.ScoreBlack, 2, "ScoreBlack should be 2");
            Assert.AreEqual(game.ScoreWhite, 2, "ScoreWhite should be 2");
        }
    }
}
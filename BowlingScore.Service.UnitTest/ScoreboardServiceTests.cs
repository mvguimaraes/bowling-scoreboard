using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingScore.Service.UnitTest
{
    [TestClass]
    public class ScoreboardServiceTests
    {
        private ScoreboardService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new ScoreboardService();
        }

        [TestMethod]
        public void RegularGame_CalculatesScore()
        {
            // Frame #1
            _service.Play(1);
            _service.Play(4);

            // Frame #2
            _service.Play(4);
            _service.Play(5);

            // Frame #3 (Spare)
            _service.Play(6);
            _service.Play(4);

            // Frame #4 (Spare)
            _service.Play(5);
            _service.Play(5);

            // Frame #5 (Strike)
            _service.Play(10);

            // Frame #6 (Strike)
            _service.Play(0);
            _service.Play(1);

            // Frame #7 (Spare)
            _service.Play(7);
            _service.Play(3);

            // Frame #8 (Spare)
            _service.Play(6);
            _service.Play(4);

            // Frame #9 (Strike)
            _service.Play(10);

            // Frame #10
            _service.Play(2);
            _service.Play(8);
            _service.Play(6);

            var scoreboard = _service.GetScoreboard();

            Assert.AreEqual(5, scoreboard[1].Score);
            Assert.AreEqual(14, scoreboard[2].Score);
            Assert.AreEqual(29, scoreboard[3].Score);
            Assert.AreEqual(49, scoreboard[4].Score);
            Assert.AreEqual(60, scoreboard[5].Score);
            Assert.AreEqual(61, scoreboard[6].Score);
            Assert.AreEqual(77, scoreboard[7].Score);
            Assert.AreEqual(97, scoreboard[8].Score);
            Assert.AreEqual(117, scoreboard[9].Score);
            Assert.AreEqual(133, scoreboard[10].Score);

            Assert.AreEqual(10, scoreboard.Keys.Count);
        }

        [TestMethod]
        public void Frame10Frame_Regular()
        {
            // Frame #1
            _service.Play(0);
            _service.Play(0);

            // Frame #2
            _service.Play(0);
            _service.Play(0);

            // Frame #3 
            _service.Play(0);
            _service.Play(0);

            // Frame #4 
            _service.Play(0);
            _service.Play(0);

            // Frame #5 
            _service.Play(0);
            _service.Play(0);

            // Frame #6 
            _service.Play(0);
            _service.Play(0);

            // Frame #7 
            _service.Play(0);
            _service.Play(0);

            // Frame #8 
            _service.Play(0);
            _service.Play(0);

            // Frame #9 
            _service.Play(0);
            _service.Play(0);

            // Frame #10
            _service.Play(1);
            _service.Play(2);

            var scoreboard = _service.GetScoreboard();

            Assert.AreEqual(3, scoreboard[10].Score);
            Assert.AreEqual(10, scoreboard.Keys.Count);
            //Assert.IsTrue(_service.Completed);
        }

        [TestMethod]
        public void Frame10Frame_With3Strikes()
        {
            // Frame #1
            _service.Play(0);
            _service.Play(0);

            // Frame #2
            _service.Play(0);
            _service.Play(0);

            // Frame #3 
            _service.Play(0);
            _service.Play(0);

            // Frame #4 
            _service.Play(0);
            _service.Play(0);

            // Frame #5 
            _service.Play(0);
            _service.Play(0);

            // Frame #6 
            _service.Play(0);
            _service.Play(0);

            // Frame #7 
            _service.Play(0);
            _service.Play(0);

            // Frame #8 
            _service.Play(0);
            _service.Play(0);

            // Frame #9 
            _service.Play(0);
            _service.Play(0);

            // Frame #10
            _service.Play(10);
            _service.Play(10);
            _service.Play(10);

            var scoreboard = _service.GetScoreboard();

            Assert.AreEqual(30, scoreboard[10].Score);
            Assert.AreEqual(10, scoreboard.Keys.Count);
        }

        [TestMethod]
        public void Frame10Frame_With2Strikes()
        {
            // Frame #1
            _service.Play(0);
            _service.Play(0);

            // Frame #2
            _service.Play(0);
            _service.Play(0);

            // Frame #3 
            _service.Play(0);
            _service.Play(0);

            // Frame #4 
            _service.Play(0);
            _service.Play(0);

            // Frame #5 
            _service.Play(0);
            _service.Play(0);

            // Frame #6 
            _service.Play(0);
            _service.Play(0);

            // Frame #7 
            _service.Play(0);
            _service.Play(0);

            // Frame #8 
            _service.Play(0);
            _service.Play(0);

            // Frame #9 
            _service.Play(0);
            _service.Play(0);

            // Frame #10
            _service.Play(10);
            _service.Play(10);
            _service.Play(1);

            var scoreboard = _service.GetScoreboard();

            Assert.AreEqual(21, scoreboard[10].Score);
            Assert.AreEqual(10, scoreboard.Keys.Count);
        }

        [TestMethod]
        public void GutterGame_CalculatesScore()
        {
            for (var i = 0; i < 19; i++)
                _service.Play(0);

            var scoreboard = _service.GetScoreboard();

            Assert.AreEqual(0, scoreboard[1].Score);
            Assert.AreEqual(0, scoreboard[2].Score);
            Assert.AreEqual(0, scoreboard[3].Score);
            Assert.AreEqual(0, scoreboard[4].Score);
            Assert.AreEqual(0, scoreboard[5].Score);
            Assert.AreEqual(0, scoreboard[6].Score);
            Assert.AreEqual(0, scoreboard[7].Score);
            Assert.AreEqual(0, scoreboard[8].Score);
            Assert.AreEqual(0, scoreboard[9].Score);
            Assert.AreEqual(0, scoreboard[10].Score);
            Assert.AreEqual(10, scoreboard.Keys.Count);
        }

        [TestMethod]
        public void PerfectGame_CalculatesScore()
        {
            for (var i = 0; i < 12; i++)
                _service.Play(10);

            var scoreboard = _service.GetScoreboard();

            Assert.AreEqual(30, scoreboard[1].Score);
            Assert.AreEqual(60, scoreboard[2].Score);
            Assert.AreEqual(90, scoreboard[3].Score);
            Assert.AreEqual(120, scoreboard[4].Score);
            Assert.AreEqual(150, scoreboard[5].Score);
            Assert.AreEqual(180, scoreboard[6].Score);
            Assert.AreEqual(210, scoreboard[7].Score);
            Assert.AreEqual(240, scoreboard[8].Score);
            Assert.AreEqual(270, scoreboard[9].Score);
            Assert.AreEqual(300, scoreboard[10].Score);
            Assert.AreEqual(10, scoreboard.Keys.Count);
        }

        [TestMethod]
        public void FirstRoll_NotBetween0and10_Throws()
        {
            var exception = Assert.ThrowsException<InvalidOperationException>(() => _service.Play(11));
            Assert.AreEqual("Value must be between 0 and 10", exception.Message);
        }

        [TestMethod]
        public void SecondRoll_PinSumGreaterThen10_Throws()
        {
            _service.Play(7);

            var exception = Assert.ThrowsException<InvalidOperationException>(() => _service.Play(4));
            Assert.AreEqual("Invalid value. You can knock down up to 3 pin(s)", exception.Message);
        }


        [TestMethod]
        public void Completed_IsFalse_Before21Rolls()
        {
            for (var i = 0; i < 19; i++)
                _service.Play(0);

            Assert.IsFalse(_service.Completed);


            _service.Play(0);
            Assert.IsTrue(_service.Completed);
        }

        [TestMethod]
        public void Completed_IsTrue_After21Rolls()
        {
            for (var i = 0; i < 20; i++)
                _service.Play(0);

            Assert.IsTrue(_service.Completed);
        }

        [TestMethod]
        public void Completed_Prevent_NewRolls_ToBeCreated()
        {
            for (var i = 0; i < 20; i++)
                _service.Play(0);

            var exception = Assert.ThrowsException<InvalidOperationException>(() => _service.Play(11));
            Assert.AreEqual("Game over", exception.Message);
        }
    }
}
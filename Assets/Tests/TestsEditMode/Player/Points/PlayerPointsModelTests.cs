using Player.Points.Model;
using UnityEngine;

namespace Tests.TestsEditMode.Player.Points
{
    using NUnit.Framework;

    [TestFixture]
    public class PlayerPointsModelTests
    {
        private PlayerPointsModel _playerPointsModel;

        [SetUp]
        public void SetUp()
        {            
            GameObject gameObject = new GameObject();
            _playerPointsModel = gameObject.AddComponent<PlayerPointsModel>();
            _playerPointsModel.PointsChanged += Action_stub;
        }

        [Test]
        public void PointsPlus_AddsAmountToPoints()
        {
            // Arrange
            int initialPoints = _playerPointsModel.Points;
            int amount = 10;

            // Act
            _playerPointsModel.PointsPlus(amount);

            // Assert
            int expectedPoints = initialPoints + amount;
            int actualPoints = _playerPointsModel.Points;

            
            Assert.AreEqual(expectedPoints, actualPoints);
        }

        [Test]
        public void ResetPoints_SetsPointsToZero()
        {  
            // Arrange
            _playerPointsModel.Points = 100;

            // Act
            _playerPointsModel.ResetPoints();
            
            // Assert
            Assert.AreEqual(0, _playerPointsModel.Points);
        }
        private void Action_stub()
        {
        }
    }

}
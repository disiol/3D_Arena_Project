using Player.Points.Model;
using UnityEngine;

namespace Tests.TestsEditMode.Player.Points
{
    using NUnit.Framework;

    [TestFixture]
    public class PlayerPointsModelTests
    {
        private PlayerPointsModel playerPointsModel;

        [SetUp]
        public void SetUp()
        {            
            GameObject gameObject = new GameObject();
            playerPointsModel = gameObject.AddComponent<PlayerPointsModel>();
            playerPointsModel.PointsChanged += Action_stub;
        }

        [Test]
        public void PointsPlus_AddsAmountToPoints()
        {
            // Arrange
            int initialPoints = playerPointsModel.Points;
            int amount = 10;

            // Act
            playerPointsModel.PointsPlus(amount);

            // Assert
            int expectedPoints = initialPoints + amount;
            int actualPoints = playerPointsModel.Points;

            
            Assert.AreEqual(expectedPoints, actualPoints);
        }

        [Test]
        public void ResetPoints_SetsPointsToZero()
        {  
            // Arrange
            playerPointsModel.Points = 100;

            // Act
            playerPointsModel.ResetPoints();
            
            // Assert
            Assert.AreEqual(0, playerPointsModel.Points);
        }
        private void Action_stub()
        {
        }
    }

}
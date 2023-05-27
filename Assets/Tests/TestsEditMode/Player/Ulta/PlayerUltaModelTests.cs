using NUnit.Framework;
using Player.Ulta.Model;
using UnityEngine;

namespace Tests.TestsEditMode.Player.Ulta
{
    public class PlayerUltaModelTests
    {
        private PlayerUltaModel _playerUltaModel;

        [SetUp]
        public void SetUp()
        {
            GameObject gameObject = new GameObject();

            _playerUltaModel = gameObject.AddComponent<PlayerUltaModel>();
            int minPower = 0;
            int maxPower = 100;
            
            _playerUltaModel.PlayerHealthModelSetTestData(minPower,maxPower);
            _playerUltaModel.PowerChanged += Action_stub;

        }

        [Test]
        public void Increment_IncreasePowerWithinRange()
        {
            // Arrange
            int initialPower = _playerUltaModel.CurrentPower;
            int amount = 5;

            // Act
            _playerUltaModel.Increment(amount);

            // Assert
            int expectedPower = initialPower + amount;
            int actualPower = _playerUltaModel.CurrentPower;
            
            Assert.AreEqual(expectedPower, actualPower);
        }

        [Test]
        public void Increment_IncreasePowerAboveMaxClampsToMax()
        {
            // Arrange 
            int maxPower = _playerUltaModel.MaxPower;
            int amount = maxPower + 5;

            // Act 
            _playerUltaModel.Increment(amount);

            // Assert
            int expectedPower = maxPower;
            int actualPower = _playerUltaModel.CurrentPower;

            Assert.AreEqual(expectedPower, actualPower);
        }

        [Test]
        public void ResetPower_SetCurrentPowerToMin()
        {
            // Arrange 
            _playerUltaModel.Increment(10);
            int initialPower = _playerUltaModel.CurrentPower;

            // Act 
            _playerUltaModel.ResetPower();

            // Assert
            int expectedPower = _playerUltaModel.MinPower;
            int actualPower = _playerUltaModel.CurrentPower;

            Assert.AreEqual(expectedPower, actualPower);
            Assert.AreNotEqual(initialPower, actualPower);
        }
        
        private void Action_stub()
        {
        }
    }
}
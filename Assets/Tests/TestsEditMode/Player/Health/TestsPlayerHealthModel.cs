using NUnit.Framework;
using Player.Health.Model;
using UnityEngine;

namespace Tests.TestsEditMode.Player.Health
{
    public class TestsPlayerHealthModel
    {
        private PlayerHealthModel _playerHealthModel;
        private int _minHealth;
        private int _maxHealth;

        [SetUp]
        public void Setup()
        {
            _playerHealthModel = new GameObject().AddComponent<PlayerHealthModel>();
            _minHealth = 0;
            _maxHealth = 100;
            _playerHealthModel.PlayerHealthModelSetTestData(_minHealth,_maxHealth);

        }
      
        [Test]
        public void Increment_IncreaseHealthWithinBounds()
        {
            // Arrange
            _playerHealthModel.HealthChanged += Action_stub;

            _playerHealthModel.CurrentHealth = 70;

            // Act
            int amount = 30;

            _playerHealthModel.Increment(amount);
            Action_stub();

            // Assert
            Assert.AreEqual(100, _playerHealthModel.CurrentHealth);

            _playerHealthModel.HealthChanged -= Action_stub;
        }


     

        [Test]
        public void Increment_IncreaseHealthAboveMaxHealth_ClampToMaxHealth()
        {
            // Arrange
            _playerHealthModel.HealthChanged += Action_stub;

            _playerHealthModel.CurrentHealth = 90;
            int amount = 20;

            // Act
            _playerHealthModel.Increment(amount);

            // Assert
            Assert.AreEqual(100, _playerHealthModel.CurrentHealth);
            _playerHealthModel.HealthChanged -= Action_stub;

        }

        [Test]
        public void Decrement_DecreaseHealthWithinBounds()
        {
            // Arrange
            _playerHealthModel.HealthChanged += Action_stub;

            _playerHealthModel.CurrentHealth = 70;
            int amount = 20;

            // Act
            _playerHealthModel.Decrement(amount);

            // Assert
            Assert.AreEqual(50, _playerHealthModel.CurrentHealth);
            _playerHealthModel.HealthChanged -= Action_stub;

        }

        [Test]
        public void Decrement_DecreaseHealthBelowMinHealth_ClampToMinHealth()
        {
            // Arrange
            _playerHealthModel.HealthChanged += Action_stub;

            _playerHealthModel.CurrentHealth = 30;
            int amount = 40;

            // Act
            _playerHealthModel.Decrement(amount);

            // Assert
            Assert.AreEqual(0, _playerHealthModel.CurrentHealth);
            _playerHealthModel.HealthChanged -= Action_stub;

        }

        [Test]
        public void Restore_SetHealthToMaxHealth()
        {
            // Arrange
            _playerHealthModel.HealthChanged += Action_stub;
            _playerHealthModel.CurrentHealth = 70;

            // Act
            _playerHealthModel.Restore();

            // Assert
            Assert.AreEqual(100, _playerHealthModel.CurrentHealth);
            _playerHealthModel.HealthChanged -= Action_stub;

        }
        
        private void Action_stub()
        {
        }
    }
}
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests.TestsEditMode
{
    using NUnit.Framework;
    using UnityEngine;
    using Player.Health.Model;

    public class TestsPlayerHealthModel
    {
        [Test]
        public void Increment_IncreaseHealthWithinBounds()
        {
            // Arrange
            PlayerHealthModel playerHealthModel = new GameObject().AddComponent<PlayerHealthModel>();
            playerHealthModel.HealthChanged += Action_stub;

            playerHealthModel.CurrentHealth = 70;

            // Act
            
            playerHealthModel.CurrentHealth = 70;
            int amount = 30;

            // Act
            playerHealthModel.Increment(amount);

            Action_stub();

            // Assert
            Assert.AreEqual(100, playerHealthModel.CurrentHealth);

            playerHealthModel.HealthChanged -= Action_stub;
        }


        private void Action_stub()
        {
        }

        [Test]
        public void Increment_IncreaseHealthAboveMaxHealth_ClampToMaxHealth()
        {
            // Arrange
            PlayerHealthModel playerHealthModel = new GameObject().AddComponent<PlayerHealthModel>();
            playerHealthModel.HealthChanged += Action_stub;

            playerHealthModel.CurrentHealth = 90;
            int amount = 20;

            // Act
            playerHealthModel.Increment(amount);

            // Assert
            Assert.AreEqual(100, playerHealthModel.CurrentHealth);
            playerHealthModel.HealthChanged -= Action_stub;

        }

        [Test]
        public void Decrement_DecreaseHealthWithinBounds()
        {
            // Arrange
            PlayerHealthModel playerHealthModel = new GameObject().AddComponent<PlayerHealthModel>();
            playerHealthModel.HealthChanged += Action_stub;

            playerHealthModel.CurrentHealth = 70;
            int amount = 20;

            // Act
            playerHealthModel.Decrement(amount);

            // Assert
            Assert.AreEqual(50, playerHealthModel.CurrentHealth);
            playerHealthModel.HealthChanged -= Action_stub;

        }

        [Test]
        public void Decrement_DecreaseHealthBelowMinHealth_ClampToMinHealth()
        {
            // Arrange
            PlayerHealthModel playerHealthModel = new GameObject().AddComponent<PlayerHealthModel>();
            playerHealthModel.HealthChanged += Action_stub;

            playerHealthModel.CurrentHealth = 30;
            int amount = 40;

            // Act
            playerHealthModel.Decrement(amount);

            // Assert
            Assert.AreEqual(50, playerHealthModel.CurrentHealth);
            playerHealthModel.HealthChanged -= Action_stub;

        }

        [Test]
        public void Restore_SetHealthToMaxHealth()
        {
            // Arrange
            PlayerHealthModel playerHealthModel = new GameObject().AddComponent<PlayerHealthModel>();
            playerHealthModel.HealthChanged += Action_stub;

            playerHealthModel.CurrentHealth = 70;

            // Act
            playerHealthModel.Restore();

            // Assert
            Assert.AreEqual(100, playerHealthModel.CurrentHealth);
            playerHealthModel.HealthChanged -= Action_stub;

        }
    }
}
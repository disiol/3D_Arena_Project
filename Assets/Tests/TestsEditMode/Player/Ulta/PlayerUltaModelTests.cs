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
            int initialPower = _playerUltaModel.CurrentPower;
            int amount = 5;

            _playerUltaModel.Increment(amount);

            int expectedPower = initialPower + amount;
            int actualPower = _playerUltaModel.CurrentPower;

            Assert.AreEqual(expectedPower, actualPower);
        }

        [Test]
        public void Increment_IncreasePowerAboveMaxClampsToMax()
        {
            int maxPower = _playerUltaModel.MaxPower;
            int amount = maxPower + 5;

            _playerUltaModel.Increment(amount);

            int expectedPower = maxPower;
            int actualPower = _playerUltaModel.CurrentPower;

            Assert.AreEqual(expectedPower, actualPower);
        }

        [Test]
        public void ResetPower_SetCurrentPowerToMin()
        {
            _playerUltaModel.Increment(10);
            int initialPower = _playerUltaModel.CurrentPower;

            _playerUltaModel.ResetPower();

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
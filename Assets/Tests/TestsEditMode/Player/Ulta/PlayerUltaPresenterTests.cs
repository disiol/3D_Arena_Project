using Player.Ulta.Model;
using Player.Ulta.Presenter;

namespace Tests.TestsEditMode.Player.Ulta
{
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    [TestFixture]
    public class PlayerUltaPresenterTests
    {
        private PlayerUltaPresenter _playerUltaPresenter;
        private PlayerUltaModel _playerUltaModel;
        private Slider _powerIndicatorSlider;
        private TextMeshProUGUI _powerText;
        private GameObject _ultaButton;

        [SetUp]
        public void SetUp()
        {
            // Create a new instance of the presenter and set up the required components
            GameObject presenterObject = new GameObject();
            _playerUltaPresenter = presenterObject.AddComponent<PlayerUltaPresenter>();

            _playerUltaModel = presenterObject.AddComponent<PlayerUltaModel>();
            int minPower = 0;
            int maxPower = 100;

            _playerUltaModel.PlayerHealthModelSetTestData(minPower, maxPower);
            _playerUltaModel.PowerChanged += Action_stub;


            _powerIndicatorSlider = presenterObject.AddComponent<Slider>();
            _powerText = presenterObject.AddComponent<TextMeshProUGUI>();
            _ultaButton = new GameObject();

            _playerUltaPresenter.PlayerUltaPresenterSetTestData(_playerUltaModel, _powerIndicatorSlider, _powerText,
                _ultaButton);

        }


        [Test]
        public void PowerPlas_IncrementsPowerInModel()
        {
            // Arrange
            int initialPower = _playerUltaModel.CurrentPower;
            int incrementAmount = 10;

            // Act
            _playerUltaPresenter.PowerPlas(incrementAmount);

            // Assert
            Assert.AreEqual(initialPower + incrementAmount, _playerUltaModel.CurrentPower);
        }

        [Test]
        public void Reset_ResetsPowerInModel()
        {
            // Arrange
            _playerUltaModel.Increment(50);

            // Act
            _playerUltaPresenter.Reset();

            // Assert
            Assert.AreEqual(0, _playerUltaModel.CurrentPower);
        }

        [Test]
        public void UpdateView_UpdatesSliderAndText()
        {
            // Arrange

            _playerUltaModel.Increment(50);
            _powerIndicatorSlider.value = 0.5f;
            _powerText.text = "50";
            
            // Act
            _playerUltaPresenter.UpdateView();
            
            // Assert
            Assert.AreEqual(0.5f, _powerIndicatorSlider.value);
            Assert.AreEqual("50", _powerText.text);
        }

        [Test]
        public void UpdateView_SetsUltaButtonActiveWhenPowerIsMax()
        {
            // Arrange
            _playerUltaModel.Increment(100);
            _ultaButton.SetActive(false);
            
            // Act
            _playerUltaPresenter.UpdateView();

            // Assert
            Assert.IsTrue(_ultaButton.activeSelf);
        }

        [Test]
        public void UpdateView_SetsUltaButtonInactiveWhenPowerIsNotMax()
        {
            // Arrange
            _playerUltaModel.Increment(50);
            _ultaButton.SetActive(true);

            // Act
            _playerUltaPresenter.UpdateView();
            
            // Assert
            Assert.IsFalse(_ultaButton.activeSelf);
        }
        
        private void Action_stub()
        {
        }
    }
}
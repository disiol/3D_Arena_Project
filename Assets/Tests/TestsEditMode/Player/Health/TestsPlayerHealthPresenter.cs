using NUnit.Framework;
using Player.Health.Model;
using Player.Health.Presenter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tests.TestsEditMode.Player.Health
{
    
        public class TestsPlayerHealthPresenter
        {
            private PlayerHealthPresenter _playerHealthPresenter;
            private PlayerHealthModel _playerHealthModel;
            private Slider _healthSlider;
            private TextMeshProUGUI _healthText;
            private int _minHealth;
            private int _maxHealth;

            [SetUp]
            public void Setup()
            {
                // Create a GameObject to attach the PlayerUltaPresenter script
                GameObject gameObject = new GameObject();
                _playerHealthPresenter = gameObject.AddComponent<PlayerHealthPresenter>();


                // Create a PlayerUltaModel instance
                _playerHealthModel = gameObject.AddComponent<PlayerHealthModel>();
                _minHealth = 0;
                _maxHealth = 100;
                _playerHealthModel.PlayerHealthModelSetTestData(_minHealth, _maxHealth);


                // Create a Slider and Text for the UI elements
                GameObject sliderObject = new GameObject();
                _healthSlider = sliderObject.AddComponent<Slider>();

                GameObject textObject = new GameObject();
                _healthText = textObject.AddComponent<TextMeshProUGUI>();

                // Set the references in the presenter script
                _playerHealthPresenter.PlayerHealthPresenterSetTestData(_playerHealthModel, _healthSlider, _healthText);
                
                _playerHealthModel.HealthChanged += Action_stub;

            }

            [Test]
            public void UpdateView_UpdatesHealthSliderAndText()
            {
                // Arrange
                _playerHealthModel.CurrentHealth = 50;

               // Act
                _playerHealthPresenter.UpdateView();

                // Assert that the healthSlider value is set correctly
                Assert.AreEqual(0.5f, _healthSlider.value);

                // Assert that the healthText text is set correctly
                Assert.AreEqual("50", _healthText.text);
            }

            [Test]
            public void Damage_ReducesHealth()
            {
                // Arrange
                _playerHealthModel.CurrentHealth = 100;

                // Act
                _playerHealthPresenter.Damage(20);
                _playerHealthPresenter.UpdateView();

                // Assert that the current health is reduced by 20
                Assert.AreEqual(80, _playerHealthModel.CurrentHealth);
            }

            [Test]
            public void Heal_IncreasesHealth()
            {
                // Arrange 
                _playerHealthModel.CurrentHealth = 50;

               // Act Call the Heal method with a heal amount of 30
                _playerHealthPresenter.Heal(30);

                // Assert that the current health is increased by 30
                Assert.AreEqual(80, _playerHealthModel.CurrentHealth);
            }

            [Test]
            public void Reset_RestoresHealth()
            {
                // Arrange
                _playerHealthModel.CurrentHealth = 50;

                // Act
                _playerHealthPresenter.Reset();

                // Assert that the current health is restored to the maximum health
                Assert.AreEqual(100, _playerHealthModel.CurrentHealth);
            }

            private void Action_stub()
            {
            }
        }
    
}
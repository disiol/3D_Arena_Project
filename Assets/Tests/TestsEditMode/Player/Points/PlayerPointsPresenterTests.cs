using NUnit.Framework;
using Player.Points.Model;
using Player.Points.Presenter;
using TMPro;
using UnityEngine;

namespace Tests.TestsEditMode.Player.Points
{
  
    public class PlayerPointsPresenterTests
    {
        private PlayerPointsPresenter _playerPointsPresenter;
        private PlayerPointsModel _playerPointsModel;
        private TextMeshProUGUI _pointsText;

        [SetUp]
        public void Setup()
        {
            // Create a new instance of the presenter
            GameObject presenterObject = new GameObject();
            _playerPointsPresenter = presenterObject.AddComponent<PlayerPointsPresenter>();

            // Create a new instance of the _playerPointsModel
            _playerPointsModel = presenterObject.AddComponent<PlayerPointsModel>();
            _playerPointsModel.PointsChanged += Action_stub;

            // Create a new instance of the TextMeshProUGUI component
            GameObject textObject = new GameObject();
            _pointsText = textObject.AddComponent<TextMeshProUGUI>();

            // Set the _playerPointsModel and pointsText fields in the presenter
            _playerPointsPresenter.PlayerUltaPresenterSetTestData(_playerPointsModel, _pointsText);
        }

    

        [Test]
        public void PointsPlus_IncrementsPointsModel()
        {
            // Arrange
            int initialPoints = _playerPointsModel.Points;

            // Act
            _playerPointsPresenter.PointsPlus(10);

            // Assert
            Assert.AreEqual(initialPoints + 10, _playerPointsModel.Points);
        }

        [Test]
        public void Reset_ResetsPointsModel()
        {
            // Arrange
            _playerPointsModel.Points = 100;

            // Act
            _playerPointsPresenter.Reset();

            // Assert
            Assert.AreEqual(0, _playerPointsModel.Points);
        }

        [Test]
        public void UpdateView_UpdatesPointsText()
        {
            // Arrange
            _playerPointsModel.Points = 50;

            // Act
            _playerPointsPresenter.UpdateView();

            // Assert
            Assert.AreEqual("50", _pointsText.text);
        }
        
        private void Action_stub()
        {
            
        }
    }

}
using Player.Points.Model;
using Player.Ulta.Model;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

namespace Player.Ulta.Presenter
{
    public class PlayerPointsPresenter : MonoBehaviour
    {
        [Header("Model")] [SerializeField] private PlayerPointsModel playerPointsModel;
        [SerializeField] private TextMeshProUGUI pointsText;

#if UNITY_EDITOR

        public void PlayerUltaPresenterSetTestData(PlayerPointsModel playerHealthModel,
            TextMeshProUGUI pointsText)
        {
            this.playerPointsModel = playerHealthModel;
            this.pointsText = pointsText;
        }
#endif


        private void Awake()
        {
            CheckSerializeFieldToNull();
        }


        private void Start()
        {
            if (playerPointsModel != null)
            {
                playerPointsModel.PointsChanged += OnPlayerPointsModelChanged;
            }
        }

        private void OnDestroy()
        {
            if (playerPointsModel != null)
            {
                playerPointsModel.PointsChanged -= OnPlayerPointsModelChanged;
            }
        }

        //    • За Синего - 50
       // • За Красного - 15
        public void PointsPlus(int amount)
        {
            playerPointsModel?.PointsPlus(amount);
        }


        // send reset to the model
        public void Reset()
        {
            playerPointsModel?.ResetPoints();
        }

        public void UpdateView()
        {
            if (playerPointsModel == null)
                return;

            FormatTheDataForView();

        }

        private void FormatTheDataForView()
        {
            if (pointsText != null)
            {
                pointsText.text = playerPointsModel.Points.ToString();
            }

        }

     

        private void CheckSerializeFieldToNull()
        {
            if (playerPointsModel == null)
            {
                Debug.LogWarning(
                    "PlayerPointsPresenter Presenter needs a PlayerPointsModel to present please make sure one is set in The Inspector",
                    gameObject);
                enabled = false;
            }
            
            if (pointsText == null)
            {
                Debug.LogWarning(
                    "PlayerPointsModel Presenter needs a HealthText to Update please make sure one is set in The Inspector",
                    gameObject);
                enabled = false;
            }
            
        }

        private void OnPlayerPointsModelChanged()
        {
            UpdateView();
        }
    }
}
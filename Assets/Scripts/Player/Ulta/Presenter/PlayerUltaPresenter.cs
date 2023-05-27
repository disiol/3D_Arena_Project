using Player.Ulta.Model;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

namespace Player.Ulta.Presenter
{
    public class PlayerUltaPresenter : MonoBehaviour
    {
        [Header("Model")] [SerializeField] private PlayerUltaModel playerUltaModel;

        [Header("View")] [SerializeField] private Slider powerIndicatorSlider;
        [SerializeField] private TextMeshProUGUI powerText;
        [SerializeField] private GameObject ultaButton;

#if UNITY_EDITOR

        public void PlayerUltaPresenterSetTestData(PlayerUltaModel playerHealthModel, Slider healthSlider,
            TextMeshProUGUI powerText, GameObject ultaButton)
        {
            this.playerUltaModel = playerHealthModel;
            this.powerIndicatorSlider = healthSlider;
            this.powerText = powerText;
            this.ultaButton = ultaButton;
        }
#endif


        private void Awake()
        {
            CheckSerializeFieldToNull();
        }


        private void Start()
        {
            if (playerUltaModel != null)
            {
                playerUltaModel.PowerChanged += OnPlayerHealthModelChanged;
            }
        }

        private void OnDestroy()
        {
            if (playerUltaModel != null)
            {
                playerUltaModel.PowerChanged -= OnPlayerHealthModelChanged;
            }
        }

        // Сила - 50\100
        public void PowerPlas(int amount)
        {
            playerUltaModel?.Increment(amount);
        }


        // send reset to the model
        public void Reset()
        {
            playerUltaModel?.ResetPower();
        }

        public void UpdateView()
        {
            if (playerUltaModel == null)
                return;

            FormatTheDataForView();
            ShowUltaButton();

        }

        private void FormatTheDataForView()
        {
            if (powerIndicatorSlider != null && playerUltaModel.MaxPower != 0)
            {
                powerIndicatorSlider.value = (float)playerUltaModel.CurrentPower / (float)playerUltaModel.MaxPower;
            }

            if (powerText != null)
            {
                powerText.text = playerUltaModel.CurrentPower.ToString();
            }

        }

        private void ShowUltaButton()
        {
            ultaButton.SetActive(playerUltaModel.CurrentPower.Equals(playerUltaModel.MaxPower));
        }

        private void CheckSerializeFieldToNull()
        {
            if (playerUltaModel == null)
            {
                Debug.LogWarning(
                    "PlayerPointsPresenter Presenter needs a PlayerPointsModel to present please make sure one is set in The Inspector",
                    gameObject);
                enabled = false;
            }

            if (powerIndicatorSlider == null)
            {
                Debug.LogWarning(
                    "PlayerPointsPresenter Presenter needs a Slider to Update please make sure one is set in The Inspector",
                    gameObject);
                enabled = false;
            }

            if (powerText == null)
            {
                Debug.LogWarning(
                    "PlayerPointsModel Presenter needs a HealthText to Update please make sure one is set in The Inspector",
                    gameObject);
                enabled = false;
            }

            if (ultaButton == null)
            {
                Debug.LogWarning(
                    "PlayerPointsModel Presenter needs a ultaButton to Update please make sure one is set in The Inspector",
                    gameObject);
                enabled = false;
            }
        }

        private void OnPlayerHealthModelChanged()
        {
            UpdateView();
        }
    }
}
using Player.Health.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Health.Presenter
{
    public class PlayerHealthPresenter : MonoBehaviour
    {
        [Header("Model")] 
        [SerializeField] private PlayerHealthModel playerHealthModel;

        [Header("View")]
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI healthText;

#if UNITY_EDITOR

        public void PlayerHealthPresenterSetTestData(PlayerHealthModel playerHealthModel, Slider healthSlider,
            TextMeshProUGUI healthText)
        {
            this.playerHealthModel = playerHealthModel;
            this.healthSlider = healthSlider;
            this.healthText = healthText;
        }
#endif


        private void Awake()
        {
            CheckSerializeFieldToNull();
        }


        private void Start()
        {
            if (playerHealthModel != null)
            {
                playerHealthModel.HealthChanged += OnPlayerHealthModelChanged;
            }

            Reset();
        }

        private void OnDestroy()
        {
            if (playerHealthModel != null)
            {
                playerHealthModel.HealthChanged -= OnPlayerHealthModelChanged;
            }
        }

        // send damage to the model
        public void Damage(int amount)
        {
            playerHealthModel?.Decrement(amount);
        }

        public void Heal(int amount)
        {
            playerHealthModel?.Increment(amount);
        }

        // send reset to the model
        public void Reset()
        {
            playerHealthModel?.Restore();
        }

        public void UpdateView()
        {
            if (playerHealthModel == null)
                return;

            FormatTheDataForView();
        }

        private void FormatTheDataForView()
        {
            // format the data for view
            if (healthSlider != null && playerHealthModel.MaxHealth != 0)
            {
                healthSlider.value = (float)playerHealthModel.CurrentHealth / (float)playerHealthModel.MaxHealth;
            }

            if (healthText != null)
            {
                healthText.text = playerHealthModel.CurrentHealth.ToString();
            }
        }

        private void CheckSerializeFieldToNull()
        {
            if (playerHealthModel == null)
            {
                Debug.LogWarning(
                    "PlayerUltaModel Presenter needs a PlayerUltaModel to present please make sure one is set in The Inspector",
                    gameObject);
                enabled = false;
            }

            if (healthSlider == null)
            {
                Debug.LogWarning(
                    "PlayerUltaModel Presenter needs a Slider to Update please make sure one is set in The Inspector",
                    gameObject);
                enabled = false;
            }

            if (healthText == null)
            {
                Debug.LogWarning(
                    "PlayerUltaModel Presenter needs a HealthText to Update please make sure one is set in The Inspector",
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
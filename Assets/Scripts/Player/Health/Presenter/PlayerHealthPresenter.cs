using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player.Health.Presenter
{
    // The Presenter. This listens for View changes in the user interface and the manipulates the Model (PlayerHealthModel)
    // in response. The Presenter updates the View when the Model changes.

    public class PlayerHealthPresenter : MonoBehaviour
    {
        [FormerlySerializedAs("health")] [Header("Model")] [SerializeField] Model.PlayerHealthModel playerHealthModel;

        [Header("View")] [SerializeField] Slider healthSlider;
        [SerializeField] Text healthText;

        private void Awake()
        {
            if (playerHealthModel == null)
            {
                Debug.LogWarning(
                    "PlayerHealthModel Presenter needs a PlayerHealthModel to present please make sure one is set in The Inspector",
                    gameObject);
                enabled = false;
            }

            if (healthSlider == null)
            {
                Debug.LogWarning(
                    "PlayerHealthModel Presenter needs a Slider to Update please make sure one is set in The Inspector",
                    gameObject);
                enabled = false;
            }

            if (healthText == null)
            {
                Debug.LogWarning(
                    "PlayerHealthModel Presenter needs a HealthText to Update please make sure one is set in The Inspector",
                    gameObject);
                enabled = false;
            }
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

        // listen for model changes and update the view
        public void OnPlayerHealthModelChanged()
        {
            UpdateView();
        }
    }
}
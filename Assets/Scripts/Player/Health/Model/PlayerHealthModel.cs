using System;
using UnityEngine;

namespace Player.Health.Model
{
    public class PlayerHealthModel : MonoBehaviour
    {
#if UNITY_EDITOR
        public void PlayerHealthModelSetTestData(int minHealth, int maxHealth)
        {
            this.minHealth = minHealth;
            this.maxHealth = maxHealth;
        }
#endif
       

        public event Action HealthChanged;

        [SerializeField] private int minHealth;
        [SerializeField] private int maxHealth;
        private int _currentHealth;

        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public int MinHealth => minHealth;
        public int MaxHealth => maxHealth;

        public void Increment(int amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, minHealth, maxHealth);
            UpdateHealth();
        }

        public void Decrement(int amount)
        {
            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, minHealth, maxHealth);
            UpdateHealth();
        }

        // max the health value
        public void Restore()
        {
            _currentHealth = maxHealth;
            UpdateHealth();
        }

        // invokes the event
        public void UpdateHealth()
        {
            HealthChanged.Invoke();
        }
    }
}
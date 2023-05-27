using System;
using UnityEngine;

namespace Player.Ulta.Model
{
    public class PlayerUltaModel : MonoBehaviour
    {
#if UNITY_EDITOR
        public void PlayerHealthModelSetTestData(int minPower, int maxPower)
        {
            this.minPower = minPower;
            this.maxPower = maxPower;
        }
#endif


        public event Action PowerChanged;

        [SerializeField] private int minPower;
        [SerializeField] private int maxPower;
        private int _currentPower;

        public int CurrentPower
        {
            get => _currentPower;
            set => _currentPower = value;
        }

        public int MinPower => minPower;
        public int MaxPower => maxPower;

        public void Increment(int amount)
        {
            _currentPower += amount;
            _currentPower = Mathf.Clamp(_currentPower, minPower, maxPower);
            UpdatePower();
        }
        

        public void ResetPower()
        {
            _currentPower = minPower;
            UpdatePower();
        }

        public void KillAllEnemies()
        {
            //TODO при срабатывании которой все враги умрут
            Debug.Log("PlayerPointsModel KillAllEnemies");
            
        }

        // invokes the event
        public void UpdatePower()
        {
            PowerChanged.Invoke();
        }
    }
}
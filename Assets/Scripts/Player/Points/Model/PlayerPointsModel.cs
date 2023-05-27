using System;
using UnityEngine;

namespace Player.Points.Model
{
    public class PlayerPointsModel : MonoBehaviour
    {
        public event Action PointsChanged;


        private int _points;

        public int Points
        {
            get => _points;
            set => _points = value;
        }

        public void PointsPlus(int amount)
        {
            _points += amount;
            UpdatePoints();
        }
        public void ResetPoints()
        {
            _points = 0;
        }

        // invokes the event
        public void UpdatePoints()
        {
            PointsChanged.Invoke();
        }

       
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player.Movement
{
    public class PlayerCameraController : MonoBehaviour
    {
        [Header("PlayerCameraController")]
        
         [SerializeField] private  float yAngleMin = 0.0f;
         [SerializeField] private  float yAngleMax = 50.0f;

         [SerializeField] private Transform lookAt;
         [SerializeField] private Transform camTransform;
         [FormerlySerializedAs("_distanceY")]
         [Header("distance")] 
         [SerializeField]  private int distanceX;
         [SerializeField]  private int distanceY;
         [SerializeField] private float distanceZ;

        private float _currentX = 0.0f;
        private float _currentY = 45.0f;
        private Vector2 _lukInput;

        public Vector2 LukInput
        {
            set => _lukInput = value;
        }
        private void Update()
        {
            _currentX += _lukInput.x;
            _currentY += _lukInput.y;

            _currentY = Mathf.Clamp(_currentY, yAngleMin, yAngleMax);
        }

        private void LateUpdate()
        {
            Vector3 dir = new Vector3(distanceX, distanceY, -distanceZ);
            Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
           
            camTransform.position = lookAt.position + rotation * dir;
            camTransform.LookAt(lookAt.position);
        }
        
       
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class PlayerController : MonoBehaviour
    {
        public float movementSpeed = 5f;
        public float turnSpeed = 200f;
        public GameObject projectilePrefab;
        public float projectileForce = 10f;
        public float edgeThreshold = 1f;

        private Vector2 _movementInput;
        private Vector2 _lukInput;

        private bool _isAttacking;

        private PlayerActionsMap _playerActionsMap;

        private void Start()
        {
            _playerActionsMap = new PlayerActionsMap();
            _playerActionsMap.Player.Move.performed += OnMovement;
            _playerActionsMap.Player.Move.canceled += OnMovement; 
            _playerActionsMap.Player.Luk.performed += OnLuk;
            _playerActionsMap.Player.Luk.canceled += OnLuk;
            _playerActionsMap.Player.Fire.started += OnAttack;
            _playerActionsMap.Player.Fire.canceled += OnAttack;
            _playerActionsMap.Enable();        }

        private void OnDisable()
        {
            _playerActionsMap.Disable();

        }

        private void Update()
        {
        
            
            // Movement controls
            float movementX = _movementInput.x;
            float movementY = _movementInput.y;

            transform.Translate(Vector3.forward * movementY * movementSpeed * Time.deltaTime);
           
            transform.Rotate(Vector3.up * movementX * turnSpeed * Time.deltaTime);

            // Projectile launch
            if (_isAttacking)
            {
                LaunchProjectile();
            }

            // Detect proximity to the edge
            if (IsNearEdge())
            {
                MoveToRandomPosition();
            }
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>();
        } 
        public void OnLuk(InputAction.CallbackContext context)
        {
            _lukInput = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _isAttacking = true;
            }
            else if (context.canceled)
            {
                _isAttacking = false;
            }
        }

        private void LaunchProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileForce, ForceMode.Impulse);
        }

        private bool IsNearEdge()
        {
            // Replace with your own logic to determine proximity to the edge
            // You can use the player's position and the platform's boundaries

            // For example, assuming the platform is a square with a size of 10 units:
            float platformSize = 10f;
            Vector3 platformCenter = Vector3.zero; // Replace with actual center position of the platform

            float distanceToEdge = Vector3.Distance(transform.position, platformCenter) - platformSize / 2f;
            return distanceToEdge < edgeThreshold;
        }

        private void MoveToRandomPosition()
        {
            // Replace with your own logic to generate a random position within the platform's bounds
            // You can use the platform's collider or defined boundaries to restrict the random position

            Vector3 randomPosition = Vector3.zero; // Replace with actual random position calculation

            transform.position = randomPosition;
        }
    }
}

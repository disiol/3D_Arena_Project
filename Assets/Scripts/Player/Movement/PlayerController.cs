using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Player.Movement
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float turnSpeed = 200f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileForce = 10f;

      


        private Rigidbody _rb;

        private Vector2 _movementInput;
        private Vector2 _lukInput;

        private bool _isAttacking;


        private PlayerActionsMap _playerActionsMap;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _playerActionsMap = new PlayerActionsMap();
            _playerActionsMap.Player.Move.performed += OnMovement;
            _playerActionsMap.Player.Move.canceled += OnMovement;
            _playerActionsMap.Player.Luk.performed += OnLuk;
            _playerActionsMap.Player.Luk.canceled += OnLuk;
            _playerActionsMap.Player.Fire.started += OnAttack;
            _playerActionsMap.Player.Fire.canceled += OnAttack;
            _playerActionsMap.Enable();
            
            _rigidbody = gameObject.GetComponent<Rigidbody>();

        }

        private void OnDisable()
        {
            _playerActionsMap.Player.Move.performed -= OnMovement;
            _playerActionsMap.Player.Move.canceled -= OnMovement;
            _playerActionsMap.Player.Luk.performed -= OnLuk;
            _playerActionsMap.Player.Luk.canceled -= OnLuk;
            _playerActionsMap.Player.Fire.started -= OnAttack;
            _playerActionsMap.Player.Fire.canceled -= OnAttack;
            _playerActionsMap.Disable();
        }

        private void Update()
        {
            // Movement controls

            OnLuk();


            // Projectile launch
            if (_isAttacking)
            {
                LaunchProjectile();
            }

            // Detect proximity to the edge
            // if (IsAtEdge())
            // {
            //     MovePlayerToSafeLocation();
            // }
        }

        // private void FixedUpdate()
        // {
        //     Movement();
        //
        // }

        private void OnLuk()
        {
            float lukInputX = _lukInput.x;
            transform.Rotate(Vector3.up * lukInputX * turnSpeed * Time.deltaTime);
        }

        private void Movement()
        {
            Debug.Log("PlayerController Movement  before movement transform = "+ transform.position);

            Vector3 direction = new Vector3(_movementInput.x, 0, _movementInput.y).normalized;

            _rigidbody.MovePosition(transform.position + transform.TransformDirection(direction)* movementSpeed * Time.fixedDeltaTime);
            Debug.Log("PlayerController Movement transform = "+ transform.position);

        }

        public void OnMovement(InputAction.CallbackContext context)
        {
           
            _movementInput = context.ReadValue<Vector2>().normalized;
            
            Debug.Log("PlayerController OnMovement _movementInput = "+_movementInput);

            Movement();


        }

        public void OnLuk(InputAction.CallbackContext context)
        {
            _lukInput = context.ReadValue<Vector2>().normalized;
            
            Debug.Log("PlayerController OnLuk _movementInput = "+_lukInput);

           
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

        private bool IsAtEdge()
        {
            // Check if the player is close to the edge

            //TODO
            // if (platform != null)
            // {
            //     float distanceToEdge = Vector3.Distance(transform.position, platform.position);
            //     return distanceToEdge <= edgeDistance;
            // }

            return false;
        }

        private void MovePlayerToSafeLocation()
        {
            //TODO
            // Vector3 randomPosition = GetRandomPositionAwayFromEnemies();
            // transform.position = randomPosition;
        }

        // private Vector3 GetRandomPositionAwayFromEnemies()
        // {
            //TODO
            // Vector3 randomPosition = Vector3.zero;
            // bool isPositionValid = false;
            //
            // while (!isPositionValid)
            // {
            //     // Generate a random position within the platform boundaries
            //     var position = platform.position;
            //     var localScale = platform.localScale;
            //
            //     float randomX = Random.Range(position.x - localScale.x / 2f, position.x + localScale.x / 2f);
            //     float randomZ = Random.Range(position.z - localScale.z / 2f, position.z + localScale.z / 2f);
            //     randomPosition = new Vector3(randomX, transform.position.y, randomZ);
            //
            //     // Check if the random position is far enough from all enemies
            //     bool isPositionSafe = true;
            //     foreach (Transform enemy in enemies)
            //     {
            //         float distanceToEnemy = Vector3.Distance(randomPosition, enemy.position);
            //         if (distanceToEnemy <= enemyRadius)
            //         {
            //             isPositionSafe = false;
            //             break;
            //         }
            //     }
            //
            //     isPositionValid = isPositionSafe;
            // }
            //
            // return randomPosition;
        // }
    }
}
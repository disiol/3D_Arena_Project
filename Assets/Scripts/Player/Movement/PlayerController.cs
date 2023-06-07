using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Player.Movement
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float turnSpeed = 200f;
        
        [Header("Bullet")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletForce = 10f;


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
        private void FixedUpdate()
        {
            Movement();
        }

        private void OnLuk()
        {
            Debug.Log("PlayerController OnLuk  before rotation rotation = "+ transform.rotation);
            float lukInputX = _lukInput.x;
           
           
            transform.Rotate(Vector3.up * lukInputX * turnSpeed * Time.deltaTime);
           
           // _playerCameraController.LukInput = _lukInput;
            Debug.Log("PlayerController OnLuk   rotation = "+ transform.rotation);

        }

       

        private void Movement()
        {

            Vector3 direction = new Vector3(_movementInput.x, 0, _movementInput.y).normalized;

            _rigidbody.MovePosition(transform.position +
                                    transform.TransformDirection(direction) * movementSpeed * Time.fixedDeltaTime);
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>().normalized;

            Debug.Log("PlayerController OnMovement _movementInput = " + _movementInput);
        }

        public void OnLuk(InputAction.CallbackContext context)
        {
            _lukInput = context.ReadValue<Vector2>().normalized;

            Debug.Log("PlayerController OnLuk _lukInput = " + _lukInput);
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
            GameObject projectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
        }

      
    }
}
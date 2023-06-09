using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Player.Movement;
using UnityEngine.InputSystem;
using Camera = UnityEngine.Camera;
using Object = UnityEngine.Object;

namespace Tests.TestsPlayMode.Movement
{
    public class PlayerControllerTests : InputTestFixture

    {
        private GameObject _playerObject;
        private PlayerController _playerController;
        private PlayerActionsMap _playerActionsMap;
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();
        private Rigidbody _rigidbody;

        [SetUp]
        public override void Setup()
        {
            _playerObject = new GameObject("Player");
            var root = new GameObject();
            // Attach a camera to our root game object.
            root.AddComponent(typeof(Camera));
            // Get a reference to the camera.
            var camera = root.GetComponent<Camera>();
            // Set the camera's background color to white.
            // Add our game object (with the camera included) to the scene by instantiating it.
            root = GameObject.Instantiate(root);

            _rigidbody = _playerObject.AddComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            _playerController = _playerObject.AddComponent<PlayerController>();
            _playerActionsMap = new PlayerActionsMap();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(_playerObject);
            _playerActionsMap.Disable();
        }

        [UnityTest]
        public IEnumerator Player_Forward()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            _playerActionsMap.Player.Move.performed += _playerController.OnMovement;
            _playerActionsMap.Player.Move.canceled += _playerController.OnMovement;
            _playerActionsMap.Player.Move.Enable();
            _playerActionsMap.Enable();


            float playerControllerMovementSpeed =
                (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "movementSpeed");
            var position = _playerObject.transform.position;

            Vector3 direction = new Vector3(0, 0, 1.0f);


            Vector3 expectedPosition = position +
                                       _playerObject.transform.TransformDirection(direction) *
                                       playerControllerMovementSpeed * Time.fixedTime;
            Vector3 expectedPositionNormalized = expectedPosition.normalized;


            Gamepad gamepad = InputSystem.AddDevice<Gamepad>();

            // Act
            Set(gamepad.leftStick, new Vector2(0.0f, 1.0f));

            yield return new WaitForSeconds(0.1f);


            var newPosition = _playerObject.transform.position;

            // Assert
            Assert.AreEqual(expectedPositionNormalized, newPosition.normalized,
                "Player_Left. Mover object moved from " + position + " to " + expectedPositionNormalized);
        }
        
        [UnityTest]
        public IEnumerator Player_Back()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            _playerActionsMap.Player.Move.performed += _playerController.OnMovement;
            _playerActionsMap.Player.Move.canceled += _playerController.OnMovement;
            _playerActionsMap.Player.Move.Enable();
            _playerActionsMap.Enable();


            float playerControllerMovementSpeed =
                (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "movementSpeed");
            var position = _playerObject.transform.position;

            Vector3 direction = new Vector3(0, 0, -1.0f);


            Vector3 expectedPosition = position +
                                       _playerObject.transform.TransformDirection(direction) *
                                       playerControllerMovementSpeed * Time.fixedTime;
            Vector3 expectedPositionNormalized = expectedPosition.normalized;


            Gamepad gamepad = InputSystem.AddDevice<Gamepad>();

            // Act
            Set(gamepad.leftStick, new Vector2(0.0f, -1.0f));

            yield return new WaitForSeconds(0.1f);


            var newPosition = _playerObject.transform.position;

            // Assert
            Assert.AreEqual(expectedPositionNormalized, newPosition.normalized,
                "Player_Left. Mover object moved from " + position + " to " + expectedPositionNormalized);
        }
        [UnityTest]
        public IEnumerator Player_Right()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            _playerActionsMap.Player.Move.performed += _playerController.OnMovement;
            _playerActionsMap.Player.Move.canceled += _playerController.OnMovement;
            _playerActionsMap.Player.Move.Enable();
            _playerActionsMap.Enable();


            float playerControllerMovementSpeed =
                (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "movementSpeed");
            var position = _playerObject.transform.position;

            Vector3 direction = new Vector3(1.0f, 0, 0.0f);


            Vector3 expectedPosition = position +
                                       _playerObject.transform.TransformDirection(direction) *
                                       playerControllerMovementSpeed * Time.fixedTime;
            Vector3 expectedPositionNormalized = expectedPosition.normalized;


            Gamepad gamepad = InputSystem.AddDevice<Gamepad>();

            // Act
            Set(gamepad.leftStick, new Vector2(1.0f, 0.0f));

            yield return new WaitForSeconds(0.1f);


            var newPosition = _playerObject.transform.position;

            // Assert
            Assert.AreEqual(expectedPositionNormalized, newPosition.normalized,
                "Player_Left. Mover object moved from " + position + " to " + expectedPositionNormalized);
        }   [UnityTest]
        public IEnumerator Player_Left()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            _playerActionsMap.Player.Move.performed += _playerController.OnMovement;
            _playerActionsMap.Player.Move.canceled += _playerController.OnMovement;
            _playerActionsMap.Player.Move.Enable();
            _playerActionsMap.Enable();


            float playerControllerMovementSpeed =
                (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "movementSpeed");
            var position = _playerObject.transform.position;

            Vector3 direction = new Vector3(-1.0f, 0, 0.0f);


            Vector3 expectedPosition = position +
                                       _playerObject.transform.TransformDirection(direction) *
                                       playerControllerMovementSpeed * Time.fixedTime;
            Vector3 expectedPositionNormalized = expectedPosition.normalized;


            Gamepad gamepad = InputSystem.AddDevice<Gamepad>();

            // Act
            Set(gamepad.leftStick, new Vector2(-1.0f, 0.0f));

            yield return new WaitForSeconds(0.1f);


            var newPosition = _playerObject.transform.position;

            // Assert
            Assert.AreEqual(expectedPositionNormalized, newPosition.normalized,
                "Player_Left. Mover object moved from " + position + " to " + expectedPositionNormalized);
        }

        [UnityTest]
        public IEnumerator RotatesPlayerRight()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;


            _playerActionsMap.Player.Luk.performed += _playerController.OnLuk;
            _playerActionsMap.Player.Luk.canceled += _playerController.OnLuk;
            _playerActionsMap.Player.Luk.Enable();
            _playerActionsMap.Enable();

            Gamepad gamepad = InputSystem.AddDevice<Gamepad>();

            Quaternion initialRotation = _playerObject.transform.rotation;

            var lukInputX = 1.0f;


            // Act
            Set(gamepad.rightStick, new Vector2(lukInputX, 0.0f));

            yield return new WaitForSeconds(0.1f);

            // Assert


            Quaternion finalRotation = _playerObject.transform.rotation;

            Assert.Greater(finalRotation.normalized.y, initialRotation.normalized.y, "RotatesPlayerRight");
        }  
        
        [UnityTest]
        public IEnumerator RotatesPlayerLeft()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;


            _playerActionsMap.Player.Luk.performed += _playerController.OnLuk;
            _playerActionsMap.Player.Luk.canceled += _playerController.OnLuk;
            _playerActionsMap.Player.Luk.Enable();
            _playerActionsMap.Enable();

            Gamepad gamepad = InputSystem.AddDevice<Gamepad>();

            Quaternion initialRotation = _playerObject.transform.rotation;

            var lukInputX = -1.0f;


            // Act
            Set(gamepad.rightStick, new Vector2(lukInputX, 0.0f));

            yield return new WaitForSeconds(0.1f);

            // Assert


            Quaternion finalRotation = _playerObject.transform.rotation;

            Assert.Less(finalRotation.normalized.y, initialRotation.normalized.y, "RotatesPlayerLeft");
        }

        [UnityTest]
        public IEnumerator Fire_Started_SetsIsAttackingToTrue()
        {
            _playerActionsMap.Enable();

            _playerActionsMap.Player.Fire.started += _playerController.OnAttack;
            _playerActionsMap.Player.Fire.Enable();
            _playerActionsMap.Player.Fire.ReadValue<float>();

            yield return null;

            bool isAttacking =
                (bool)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "_isAttacking");
            Assert.IsFalse(isAttacking);
        }

        [UnityTest]
        public IEnumerator Fire_Canceled_SetsIsAttackingToFalse()
        {
            _playerActionsMap.Enable();

            _playerActionsMap.Player.Fire.canceled += _playerController.OnAttack;
            _playerActionsMap.Player.Fire.Enable();
            _playerActionsMap.Player.Fire.ReadValue<float>();

            yield return null;

            bool isAttacking =
                (bool)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "_isAttacking");
            Assert.IsFalse(isAttacking);
        }

        [UnityTest]
        public IEnumerator IsAttacking_DefaultValue_IsFalse()
        {
            yield return null;

            bool isAttacking =
                (bool)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "_isAttacking");
            Assert.IsFalse(isAttacking);
        }


       
    }
}
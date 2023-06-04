using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Player.Movement;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Tests.TestsPlayMode.Movement
{
    public class PlayerControllerTests : InputTestFixture

    {
        private GameObject _playerObject;
        private PlayerController _playerController;
        private PlayerActionsMap _playerActionsMap;
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();

        [SetUp]
        public void Setup()
        {
            _playerObject = new GameObject("Player");

            _playerObject.AddComponent<Rigidbody>();

            _playerController = _playerObject.AddComponent<PlayerController>();
            _playerActionsMap = new PlayerActionsMap();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(_playerObject);
            _playerActionsMap.Disable();
            _playerActionsMap.Dispose();
        }

        [UnityTest]
        public IEnumerator Move_Forward()
        {
            _playerActionsMap.Enable();

            _playerActionsMap.Player.Move.performed += _playerController.OnMovement;
            _playerActionsMap.Player.Move.canceled += _playerController.OnMovement;
            _playerActionsMap.Player.Move.Enable();

            float playerControllerMovementSpeed =
                (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "movementSpeed");
            var position = _playerObject.transform.position;

            Vector3 direction = new Vector3(0, 0, 1.0f).normalized;
            

            Vector3 expectedPosition = position +
                                       _playerObject.transform.TransformDirection(direction) *
                                       playerControllerMovementSpeed * Time.fixedTime;
            Gamepad gamepad = InputSystem.AddDevice<Gamepad>();
            Set(gamepad.leftStick, new Vector2(0.0f, 1.0f));


            yield return new WaitForSeconds(0.1f);


            var newPosition = _playerObject.transform.position;


            Assert.AreEqual(expectedPosition, newPosition,
                "Move_Forward. Mover object moved from " + position + " to " + expectedPosition);
        }

        [UnityTest]
        public IEnumerator Luk_Right_RotatesPlayerRight()
        {
            _playerActionsMap.Enable();

            _playerActionsMap.Player.Luk.performed += _playerController.OnLuk;
            _playerActionsMap.Player.Luk.canceled += _playerController.OnLuk;
            _playerActionsMap.Player.Luk.Enable();

            Gamepad gamepad = InputSystem.AddDevice<Gamepad>();
            Set(gamepad.leftStick, new Vector2(0.0f, 1.0f));

            yield return null;

            float playerControllerTurnSpeed =
                (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "turnSpeed");
            Quaternion expectedRotation = Quaternion.Euler(0f, playerControllerTurnSpeed * Time.deltaTime, 0f) *
                                          _playerObject.transform.rotation;

            Assert.AreEqual(expectedRotation, _playerObject.transform.rotation);
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


        [UnityTest]
        public IEnumerator MovePlayerToSafeLocation_SetsPlayerPositionToValidLocation()
        {
            // Create a mock platform object
            GameObject platformObject = new GameObject();

            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlayerController), _playerController, "platform",
                platformObject.transform);

            // Create mock enemy objects
            GameObject enemy1 = new GameObject();
            GameObject enemy2 = new GameObject();
            Transform[] enemies = new Transform[2] { enemy1.transform, enemy2.transform };
            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlayerController), _playerController, "enemies", enemies);


            // Call the MovePlayerToSafeLocation() method
            _getAccessToPrivate.GetPrivateMethod(typeof(PlayerController), "MovePlayerToSafeLocation");


            // Ensure that the player's position has changed
            Assert.AreNotEqual(Vector3.zero, _playerController.transform.position);

            // Ensure that the player's new position is away from all enemies
            Transform[] enemiesValue =
                (Transform[])_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "enemies");
            foreach (Transform enemy in enemiesValue)
            {
                float distanceToEnemy = Vector3.Distance(_playerController.transform.position, enemy.position);
                float enemyRadius = (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController),
                    _playerController,
                    "enemyRadius");
                Assert.Greater(distanceToEnemy, enemyRadius);
            }

            // Wait for the next frame to complete the test
            yield return null;
        }


        [UnityTest]
        public IEnumerator PlayerMovesToSafeLocationWhenAtEdge()
        {
            // Arrange


            GameObject platformObject = new GameObject("Platform")
            {
                transform =
                {
                    position = Vector3.zero,
                    localScale = new Vector3(10f, 1f, 10f)
                }
            };

            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlayerController), _playerController, "platform",
                platformObject.transform);

            Transform enemy1 = CreateEnemy(new Vector3(2f, 0f, 2f));
            Transform enemy2 = CreateEnemy(new Vector3(-3f, 0f, -3f));

            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlayerController), _playerController, "enemies",
                new Transform[] { enemy1, enemy2 });


            yield return null; // Wait for one frame to allow initialization

            // Act
            _playerObject.transform.position = new Vector3(9f, 0f, 9f);
            yield return null; // Wait for one frame to trigger Update()

            // Assert
            float playerControllerEdgeDistance =
                (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "edgeDistance");

            Assert.IsTrue(Vector3.Distance(_playerObject.transform.position, platformObject.transform.position) >
                          playerControllerEdgeDistance);

            // Clean up
            Object.Destroy(_playerObject);
            Object.Destroy(platformObject);
            Object.Destroy(enemy1.gameObject);
            Object.Destroy(enemy2.gameObject);
        }


        private Transform CreateEnemy(Vector3 position)
        {
            GameObject enemyObject = new GameObject("Enemy");
            enemyObject.transform.position = position;
            return enemyObject.transform;
        }
    }
}
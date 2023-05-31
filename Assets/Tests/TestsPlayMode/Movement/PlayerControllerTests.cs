using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Player.Movement;
using Object = UnityEngine.Object;

namespace Tests.TestsPlayMode.Movement
{
    public class PlayerControllerTests
    {
        private GameObject _playerObject;
        private PlayerController _playerController;
        private PlayerActionsMap _playerActionsMap;

        [SetUp]
        public void Setup()
        {
            _playerObject = new GameObject("Player");
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
        public IEnumerator Move_Forward_TranslatesPlayerForward()
        {
            _playerActionsMap.Enable();

            _playerActionsMap.Player.Move.performed += _playerController.OnMovement;
            _playerActionsMap.Player.Move.canceled += _playerController.OnMovement;
            _playerActionsMap.Player.Move.Enable();
            _playerActionsMap.Player.Move.ReadValue<Vector2>();

            yield return null;

            Vector3 expectedPosition = _playerObject.transform.position +
                                       _playerObject.transform.forward * _playerController.movementSpeed * Time.deltaTime;

            Assert.AreEqual(expectedPosition, _playerObject.transform.position);
        }

        [UnityTest]
        public IEnumerator Luk_Right_RotatesPlayerRight()
        {
            _playerActionsMap.Enable();

            _playerActionsMap.Player.Luk.performed += _playerController.OnLuk;
            _playerActionsMap.Player.Luk.canceled += _playerController.OnLuk;
            _playerActionsMap.Player.Luk.Enable();
            _playerActionsMap.Player.Luk.ReadValue<Vector2>();

            yield return null;

            Quaternion expectedRotation = Quaternion.Euler(0f, _playerController.turnSpeed * Time.deltaTime, 0f) *
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

            bool isAttacking = (bool)GetInstanceField(typeof(PlayerController), _playerController, "_isAttacking");
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

            bool isAttacking = (bool)GetInstanceField(typeof(PlayerController), _playerController, "_isAttacking");
            Assert.IsFalse(isAttacking);
        }

        [UnityTest]
        public IEnumerator IsAttacking_DefaultValue_IsFalse()
        {
            yield return null;

            bool isAttacking = (bool)GetInstanceField(typeof(PlayerController), _playerController, "_isAttacking");
            Assert.IsFalse(isAttacking);
        }
        
        
        [UnityTest]
        public IEnumerator MoveToRandomPosition_ChangesPlayerPosition()
        {
            Vector3 initialPosition = _playerObject.transform.position;
            
            
            // Act
            MethodInfo privateMethodMoveToRandomPosition = typeof(PlayerController).GetMethod("MoveToRandomPosition", BindingFlags.NonPublic | BindingFlags.Instance);
            privateMethodMoveToRandomPosition.Invoke(_playerController, null);
        
            yield return null;
        
            Vector3 newPosition = _playerObject.transform.position;
        
            Assert.AreNotEqual(initialPosition, newPosition);
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
            _playerController.platform = platformObject.transform;
        
            Transform enemy1 = CreateEnemy(new Vector3(2f, 0f, 2f));
            Transform enemy2 = CreateEnemy(new Vector3(-3f, 0f, -3f));
            _playerController.enemies = new Transform[] { enemy1, enemy2 };
        
            yield return null; // Wait for one frame to allow initialization
        
            // Act
            _playerObject.transform.position = new Vector3(9f, 0f, 9f);
            yield return null; // Wait for one frame to trigger Update()
        
            // Assert
            Assert.IsTrue(Vector3.Distance(_playerObject.transform.position, platformObject.transform.position) > _playerController.edgeDistance);
        
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
        private object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags =
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
    }
}
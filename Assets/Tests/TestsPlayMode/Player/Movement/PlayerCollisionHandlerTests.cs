using System.Collections;
using Moq;
using Player.Movement;

namespace Tests.TestsPlayMode.Player.Movement
{
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;

    public class PlayerCollisionHandlerTests
    {
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();

        [UnityTest]
        public IEnumerator OnCollisionEnter_WhenCollidedWithTagLimit_ShouldSetRandomPosition()
        {
            // Arrange
            // Create a test platform
            GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
            platform.transform.position = Vector3.zero;
            platform.transform.localScale = new Vector3(10f, 1f, 10f);

            // Create test enemies
            GameObject enemy1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            enemy1.transform.position = new Vector3(-2f, 0f, 0f);
            enemy1.tag = "Enemy";

            GameObject enemy2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            enemy2.transform.position = new Vector3(2f, 0f, 0f);
            enemy2.tag = "Enemy";


            // Create the player object and attach the PlayerCollisionHandler script
            GameObject player = new GameObject("Player");
            PlayerCollisionHandler collisionHandler = player.AddComponent<PlayerCollisionHandler>();


            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlayerCollisionHandler), collisionHandler,
                "platformCollider", platform.GetComponent<Collider>());


            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlayerCollisionHandler), collisionHandler,
                "enemies", new GameObject[] { enemy1, enemy2 });


            // Set the player's initial position to a dangerous position near the enemies
            player.transform.position = new Vector3(0f, 0f, 0f);

            //Act
            // Trigger a collision with the "limit" object
            GameObject limit = GameObject.CreatePrimitive(PrimitiveType.Cube);
            limit.gameObject.tag = "limit";
            
            // Wait for one frame update
            yield return new WaitForSeconds(0.1f);
            // Assert

            // Check if the player has been teleported to a safe position

            bool isPositionNearEnemies = (bool)_getAccessToPrivate.GetPrivateMethod(typeof(PlayerCollisionHandler),
                collisionHandler,"IsPositionNearEnemies" ,new object[] { player.transform.position });

            Assert.IsFalse(isPositionNearEnemies);

            // Cleanup the test objects
            Object.Destroy(player);
            Object.Destroy(platform);
            Object.Destroy(enemy1);
            Object.Destroy(enemy2);
        }
    }
}
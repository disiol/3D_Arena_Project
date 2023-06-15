using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
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
        public IEnumerator OnCollisionEnter_CollisionWithTagLimit_ShouldMovePlayerToRandomPositionAwayFromEnemies()
        {
            // Arrange
            GameObject player = new GameObject();
            PlayerCollisionHandler collisionHandler = player.AddComponent<PlayerCollisionHandler>();

            Collider platformCollider = player.AddComponent<BoxCollider>();
            platformCollider.bounds.SetMinMax(new Vector3(0f, 0f, 0f), new Vector3(10f, 0f, 10f));

            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlayerCollisionHandler), collisionHandler,
                "platformCollider", platformCollider);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlayerCollisionHandler), collisionHandler,
                "platformRadius", 2f);

            GameObject enemy1 = new GameObject();
            GameObject enemy2 = new GameObject();

            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlayerCollisionHandler), collisionHandler,
                "enemies", new[] { enemy1, enemy2 });
           
            GameObject collisionObject = new GameObject();
            collisionObject.AddComponent<BoxCollider>();
            collisionObject.tag = "limit";
            collisionObject.transform.position = new Vector3(10f, 0f, 10f);
            

            // Act
        
            player.transform.position = new Vector3(10f, 0f, 10f);
        


            yield return new WaitForSeconds(0.1f);

            // Assert
            Vector3 playerPosition = player.transform.position;
            Vector3 platformCenter = platformCollider.bounds.center;
            Vector3 platformExtents = platformCollider.bounds.extents;

            Assert.IsTrue(playerPosition.x >= platformCenter.x - platformExtents.x &&
                          playerPosition.x <= platformCenter.x + platformExtents.x);
           
            Assert.IsTrue(playerPosition.z >= platformCenter.z - platformExtents.z &&
                          playerPosition.z <= platformCenter.z + platformExtents.z);


            GameObject[] enemies = (GameObject[])_getAccessToPrivate.GetPrivateFieldValue(
                typeof(PlayerCollisionHandler),
                collisionHandler,
                "enemies");
            ;
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(playerPosition, enemy.transform.position);

                float platformRadius = 5f;

                Assert.Greater(distance, platformRadius);
            }
        }
    }
}
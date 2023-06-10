using UnityEngine;

namespace Player.Movement
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        public Collider platformCollider;
        public float platformRadius = 1f;
        public GameObject[] enemies;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("limit"))
            {
                Vector3 randomPosition = GetRandomPositionAwayFromEnemies();
                transform.position = randomPosition;
            }
        }

        private Vector3 GetRandomPositionAwayFromEnemies()
        {
            Vector3 platformCenter = platformCollider.bounds.center;
            Vector3 platformExtents = platformCollider.bounds.extents;
            float randomX;
            float randomZ;

            // Loop until a random position away from enemies is found
            while (true)
            {
                randomX = Random.Range(platformCenter.x - platformExtents.x, platformCenter.x + platformExtents.x);
                randomZ = Random.Range(platformCenter.z - platformExtents.z, platformCenter.z + platformExtents.z);
                Vector3 randomPosition = new Vector3(randomX, transform.position.y, randomZ);

                // Check if the random position is away from enemies
                bool isAwayFromEnemies = true;
              
                foreach (GameObject enemy in enemies)
                {
                    float distance = Vector3.Distance(randomPosition, enemy.transform.position);
                    if (distance < platformRadius)
                    {
                        isAwayFromEnemies = false;
                        break;
                    }
                }

                if (isAwayFromEnemies)
                {
                    return randomPosition;
                }
            }
        }
    }

}

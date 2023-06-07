using UnityEngine;

namespace Player.Movement
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        public Collider platformCollider;
        public float platformRadius = 1f;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider == platformCollider)
            {
                Vector3 randomPosition = GetRandomPosition();
                transform.position = randomPosition;
            }
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 platformCenter = platformCollider.bounds.center;
            Vector3 platformExtents = platformCollider.bounds.extents;
            float randomX = Random.Range(platformCenter.x - platformExtents.x, platformCenter.x + platformExtents.x);
            float randomZ = Random.Range(platformCenter.z - platformExtents.z, platformCenter.z + platformExtents.z);
            Vector3 randomPosition = new Vector3(randomX, transform.position.y, randomZ);
            return randomPosition;
        }
    }
}

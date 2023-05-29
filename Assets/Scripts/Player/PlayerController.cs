using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float movementSpeed = 5f;
        public float rotationSpeed = 180f;
        public GameObject projectilePrefab;
        public float projectileForce = 10f;
        public float edgeThreshold = 1f;
        public float randomPlacementRange = 10f;
        public LayerMask enemyLayer;

        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // Movement controls
            float moveInput = Input.GetAxis("Vertical");
            float turnInput = Input.GetAxis("Horizontal");

            // Move the player
            Vector3 movement = transform.forward * moveInput * movementSpeed * Time.deltaTime;
            _rb.MovePosition(_rb.position + movement);

            // Rotate the player
            float rotation = turnInput * rotationSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);
            _rb.MoveRotation(_rb.rotation * turnRotation);

            // Projectile launch
            if (Input.GetButtonDown("Fire1"))
            {
                LaunchProjectile();
            }

            // Detect proximity to the edge
            if (Mathf.Abs(transform.position.x) >= edgeThreshold || Mathf.Abs(transform.position.z) >= edgeThreshold)
            {
                MoveToRandomPosition();
            }
        }

        private void LaunchProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.AddForce(transform.forward * projectileForce, ForceMode.Impulse);
        }

        private void MoveToRandomPosition()
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-randomPlacementRange, randomPlacementRange),
                transform.position.y,
                Random.Range(-randomPlacementRange, randomPlacementRange)
            );

            transform.position = randomPosition;
        }

        private void FixedUpdate()
        {
            // Enemy detection and avoidance
            Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, 5f, enemyLayer);
            if (nearbyEnemies.Length > 0)
            {
                // Implement your avoidance behavior here
            }
        }
    }
}

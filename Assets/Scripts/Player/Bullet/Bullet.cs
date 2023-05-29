using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Bullet
{
    public class Bullet : MonoBehaviour
{
    [SerializeField] private int bluePowerPoints = 50;
   [SerializeField] private int redPowerPoints = 15;
    
    [SerializeField] private float ricochetChanceLowHealth = 0.1f;
    [SerializeField] private float ricochetChanceMaxHealth = 1.0f;
    
    [SerializeField] private float healthReplenishSmall = 0.25f;
    [SerializeField] private float healthReplenishHalf = 0.5f;

    private bool _hasHitTarget = false;
    private bool _isLowHealth = false;
    private int _currentHealth = 100;

    private ObjectPool _bulletPool;

    public void SetObjectPool(ObjectPool pool)
    {
        _bulletPool = pool;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasHitTarget)
            return;

        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Renderer>().material.color == Color.blue)
            {
                // Blue enemy hit
                IncreasePoints(bluePowerPoints);
            }
            else if (other.GetComponent<Renderer>().material.color == Color.red)
            {
                // Red enemy hit
                IncreasePoints(redPowerPoints);
            }

            _hasHitTarget = true;

            if (_isLowHealth)
            {
                float ricochetChance = Mathf.Lerp(ricochetChanceLowHealth, 
                    ricochetChanceMaxHealth, 
                    (float)_currentHealth / 100f);
                if (Random.value <= ricochetChance)
                {
                    // Ricochet to nearest enemy
                    Ricochet();
                    return;
                }
            }

            // Check if secondary ricochet shell hit
            if (Random.value <= 0.5f)
            {
                // Replenish health by half
                ReplenishHealth(healthReplenishHalf);
            }
            else
            {
                // Replenish health by a small amount
                ReplenishHealth(healthReplenishSmall);
            }

            // return the bullet   to the object pool
            _bulletPool.ReturnObject(gameObject);
        }
    }

    private void IncreasePoints(int points)
    {
        // Increase power points
        // You can implement your own logic here
    }

    private void ReplenishHealth(float amount)
    {
        // Replenish player health by the given amount
        // You can implement your own logic here
    }

    private void Ricochet()
    {
        // Find the nearest enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        // Check if a nearest enemy is found
        if (nearestEnemy != null)
        {
            // Calculate the direction towards the nearest enemy
            Vector3 direction = nearestEnemy.transform.position - transform.position;
            direction.Normalize();

            // Calculate the ricochet position based on the direction
            Vector3 ricochetPosition = transform.position + direction * 5f;

            // Set the ricochet position and activate the bullet
            transform.position = ricochetPosition;
            gameObject.SetActive(true);
        }
        else
        {
            // If no nearest enemy is found, deactivate the bullet and return to the object pool
            gameObject.SetActive(false);
            _bulletPool.ReturnObject(gameObject);
        }
    }
}

}
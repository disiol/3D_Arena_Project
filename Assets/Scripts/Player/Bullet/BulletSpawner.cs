
using UnityEngine.UI;

namespace Player.Bullet
{
    using UnityEngine;

    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool bulletPool;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Button buttonFire ;

     

        private void SpawnBullet()
        {
            GameObject bulletObject = bulletPool.GetObject();

            if (bulletObject != null)
            {
                bulletObject.transform.position = spawnPoint.position;
                bulletObject.transform.rotation = spawnPoint.rotation;
                bulletObject.SetActive(true);

                // Set the object pool reference on the bullet
                Bullet bulletComponent = bulletObject.GetComponent<Bullet>();
                bulletComponent.SetObjectPool(bulletPool);
            }
        }
    }

}
using UnityEngine;

using UnityEngine;
using System.Collections.Generic;
using Player.Bullet;

public class ObjectPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 10;

    private Queue<GameObject> inactiveObjects = new Queue<GameObject>();

    private void Start()
    {
        // Create the initial pool of objects
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.SetObjectPool(this);
            inactiveObjects.Enqueue(bullet);
        }
    }

    public GameObject GetObject()
    {
        if (inactiveObjects.Count > 0)
        {
            GameObject bullet = inactiveObjects.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            // If the pool is empty, you can choose to dynamically create new objects here
            Debug.LogWarning("Object pool is empty!");
            return null;
        }
    }

    public void ReturnObject(GameObject bullet)
    {
        bullet.SetActive(false);
        inactiveObjects.Enqueue(bullet);
    }
}

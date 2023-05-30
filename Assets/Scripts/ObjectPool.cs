using UnityEngine;

using UnityEngine;
using System.Collections.Generic;
using Player.Bullet;

public class ObjectPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 10;

    private Queue<GameObject> _inactiveObjects = new();

    private void Start()
    {
        // Create the initial pool of objects
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.SetObjectPool(this);
            _inactiveObjects.Enqueue(bullet);
        }
    }

    public GameObject GetObject()
    {
        if (_inactiveObjects.Count > 0)
        {
            GameObject bullet = _inactiveObjects.Dequeue();
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
        _inactiveObjects.Enqueue(bullet);
    }
}

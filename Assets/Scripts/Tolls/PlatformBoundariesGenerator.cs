using UnityEngine;
using UnityEngine.Serialization;

namespace Tolls
{
    public class PlatformBoundariesGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject boundaryPrefab;
        [SerializeField] private float spacing = 1f;
        [SerializeField] private float platformRadius;
        [SerializeField] private Transform centerPoint;

        void Start()
        {
            CreateBoundaries();
        }

        void CreateBoundaries()
        {
            int numBoundaries = Mathf.CeilToInt(2f * Mathf.PI * platformRadius / spacing);
            float angleIncrement = 360f / numBoundaries;

            for (int i = 0; i < numBoundaries; i++)
            {
                float angle = i * angleIncrement;
                Vector3 boundaryPosition = centerPoint.position +
                                           Quaternion.Euler(0f, angle, 0f) * (Vector3.forward * platformRadius);

                GameObject boundaryObject = Instantiate(boundaryPrefab, boundaryPosition, Quaternion.identity);
                boundaryObject.transform.SetParent(transform);
            }
        }
    }
}
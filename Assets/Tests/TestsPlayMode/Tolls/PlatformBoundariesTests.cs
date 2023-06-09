using System.Collections;
using NUnit.Framework;
using Tolls;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.TestsPlayMode
{
    public class PlatformBoundariesTests
    {
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();

        [UnityTest]
        public IEnumerator BoundariesAreCreated()
        {
            // Create an instance of the PlatformBoundariesGenerator script
            PlatformBoundariesGenerator platformBoundariesGenerator =
                new GameObject().AddComponent<PlatformBoundariesGenerator>();


            // Set up the necessary properties for testing
            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlatformBoundariesGenerator), platformBoundariesGenerator,
                "boundaryPrefab", new GameObject());

            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlatformBoundariesGenerator), platformBoundariesGenerator,
                "spacing", 1f);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlatformBoundariesGenerator), platformBoundariesGenerator,
                "platformRadius", 5f);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(PlatformBoundariesGenerator), platformBoundariesGenerator,
                "centerPoint", new GameObject().transform);


            // Call the CreateBoundaries method
            _getAccessToPrivate.GetPrivateMethod(typeof(PlatformBoundariesGenerator), "CreateBoundaries");

            // Wait for one frame to allow Unity to process the Instantiate call
            yield return null;

            // Perform assertions to verify that the boundaries are created as expected

            // Check if the number of boundaries created matches the expected value
            float platformRadius = (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlatformBoundariesGenerator),
                platformBoundariesGenerator, "platformRadius");
            float spacing = (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlatformBoundariesGenerator),
                platformBoundariesGenerator, "spacing");

            int expectedNumBoundaries =
                Mathf.CeilToInt(2f * Mathf.PI * platformRadius / spacing);
            Assert.AreEqual(expectedNumBoundaries, platformBoundariesGenerator.transform.childCount);

            // Check if all the boundaries have the correct parent (the script's GameObject)
            foreach (Transform child in platformBoundariesGenerator.transform)
            {
                Assert.AreEqual(platformBoundariesGenerator.transform, child.parent);
            }
        }
    }
}
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        UpdateSafeArea();
    }

    // Update is called once per frame
    private void UpdateSafeArea()
    {
        var safeArea = Screen.safeArea;
        var myRectTransform = GetComponent<RectTransform>();

        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
      
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        myRectTransform.anchorMin = anchorMin;
        myRectTransform.anchorMax = anchorMax;
    }
}
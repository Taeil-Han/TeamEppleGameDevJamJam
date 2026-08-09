using UnityEngine;

public class AspectRatioLock : MonoBehaviour
{
    [SerializeField] float targetAspectWidth = 16f;
    [SerializeField] float targetAspectHeight = 9f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        Debug.Log("AspectRatioLock Start() running. Screen: " + Screen.width + "x" + Screen.height);
        ApplyAspectRatio();
        Debug.Log("Camera rect set to: " + cam.rect);
    }

    void ApplyAspectRatio()
    {
        float targetAspect = targetAspectWidth / targetAspectHeight;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1f)
        {
            Rect rect = cam.rect;
            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1f - scaleHeight) / 2f;
            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1f / scaleHeight;
            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0;
            cam.rect = rect;
        }
    }
}

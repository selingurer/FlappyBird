using UnityEngine;

public class Helper
{
    public static float GetScreenCenterX()
    {
        Camera cam = Camera.main;
        Vector3 centerWorldPos = cam.ViewportToWorldPoint(
            new Vector3(0.5f, 0.5f, cam.nearClipPlane)
        );

        return centerWorldPos.x;
    }

    public static float GetScreenHeight()
    {
        Camera cam = Camera.main;

        if (cam == null)
        {
            Debug.LogError("Main Camera not found!");
            return 0f;
        }

        return cam.orthographicSize * 2f;
    }
}
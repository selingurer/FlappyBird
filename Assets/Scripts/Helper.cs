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
}
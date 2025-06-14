using UnityEngine;

public class zoomScript : MonoBehaviour
{
    public static bool disableZoom = false;
    public float zoom;
    private float minZoom = 0.25f;
    private float maxZoom = 100;
    private float vel = 0;
    private float zoomMultiplier = 25;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        zoom = cam.orthographicSize;
    }

    void Update()
    {
        if (!disableZoom)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (zoom > 50)
            {
                zoomMultiplier = 100;
            }
            else
            {
                zoomMultiplier = 25;
            }
            zoom -= scroll * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref vel, 0.1f);
        }
    }
}

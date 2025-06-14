using UnityEngine;

public class textPosScriptCompound : MonoBehaviour
{
    public GameObject nucleus;
    private Camera cam;
    private float zoom;
    private Vector3 pos;
    private Vector3 posFinal;
    private zoomScript zoomScript;
    private void Start()
    {
        cam = Camera.main;
        zoomScript = cam.GetComponent<zoomScript>();
        zoom = zoomScript.zoom;
    }

    void Update()
    {
        pos = cam.WorldToScreenPoint(nucleus.transform.position);
        posFinal = new Vector3(pos.x, pos.y + 10);
        zoom = zoomScript.zoom;
        if (zoom < 5.4f)
        {
            transform.localScale = new Vector3(0.22f + (zoom - 5.0f) / 75.0f, 0.22f + (zoom - 5.0f) / 75.0f);
        }
        else
        {
            posFinal = new Vector3(-10000, -10000);
        }
        transform.position = posFinal;
    }
}

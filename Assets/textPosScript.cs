using TMPro;
using UnityEngine;

public class textPosScript : MonoBehaviour
{
    public GameObject nucleus;
    private Camera cam;
    float zoom;
    private Vector3 pos;
    private Vector3 posFinal;
    public string text;
    private zoomScript zoomScript;
    private void Start()
    {
        cam = Camera.main;
        TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();
        zoomScript = cam.GetComponent<zoomScript>();
        zoom = zoomScript.zoom;
        text = nucleus.GetComponent<electronSpawnScript>().atomLookUp[nucleus.GetComponent<electronSpawnScript>().numElectrons];
        nucleus.GetComponent<nucleusScript>().text = text;
        textMeshPro.SetText(text);
    }

    void Update()
    {
        pos = cam.WorldToScreenPoint(nucleus.transform.position);
        posFinal = new Vector3(pos.x, pos.y+10);
        zoom = zoomScript.zoom;
        if (zoom < 5.4f)
        {
            transform.localScale = new Vector3(0.22f + (zoom - 5.0f)/75.0f, 0.22f + (zoom - 5.0f)/75.0f);
        }

        // Too far zoomed out
        else
        {
            posFinal = new Vector3(-10000, -10000);
        }
        transform.position = posFinal;
    }
}

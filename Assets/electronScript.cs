using UnityEngine;

public class electronScript : MonoBehaviour
{
    public GameObject nucleus;
    private float velocityT;
    public Vector3 intendedPosition;
    private float zoom;
    private Camera cam;
    private bool paused = false;
    private GameObject pauser;
    private zoomScript zoomScript;
    private simulationPauser simulationPauser;

    void Start()
    {
        pauser = GameObject.FindGameObjectWithTag("Pauser");
        simulationPauser = pauser.GetComponent<simulationPauser>();
        cam = Camera.main;
        zoomScript = cam.GetComponent<zoomScript>();
        zoom = zoomScript.zoom;
    }

    void Update()
    {
        paused = simulationPauser.isPaused;
        if (!paused)
        {
            Vector3 dir = Vector3.Normalize(new Vector3(gameObject.transform.position.x - nucleus.transform.position.x, gameObject.transform.position.y - nucleus.transform.position.y));

            // Multiply by the radius for the actual tangential velocity but it looks cooler this way
            velocityT = Mathf.PI / 40;
            intendedPosition += (Vector3)Vector2.Perpendicular(dir) * velocityT * Time.deltaTime;

            transform.position = nucleus.transform.position + intendedPosition;
        }

        zoom = zoomScript.zoom;
        if (zoom < 20) {
            transform.localScale = new Vector3(zoom / 100, zoom / 100);
        }
        else if (zoom >= 20)
        {
            transform.localScale = new Vector3(0, 0);
        }
    }
}

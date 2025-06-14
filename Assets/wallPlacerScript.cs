using UnityEngine;

public class wallPlacerScript : MonoBehaviour
{
    [SerializeField] private GameObject previewWall;
    [SerializeField] private GameObject wall;
    private bool on = false;
    private Camera cam;
    private Vector3 rotation;
    public void placeWall()
    {
        zoomScript.disableZoom = true;
        rotation = new Vector3(0, 0, 0);
        previewWall.SetActive(true);
        GameObject.FindGameObjectWithTag("Pauser").GetComponent<simulationPauser>().isPaused = true;
        on = true;
    }

    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if (on)
        {
            // Rotation via scrollwheel
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput > 0)
            {
                previewWall.transform.Rotate(0, 0, 6);
                rotation += new Vector3(0, 0, 6);
            }
            else if (scrollInput < 0)
            {
                previewWall.transform.Rotate(0, 0, -6);
                rotation += new Vector3(0, 0, -6);
            }

            Vector3 mousePosReal = cam.ScreenToWorldPoint(Input.mousePosition);
            previewWall.transform.position = new Vector3(mousePosReal.x, mousePosReal.y, 0);
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
                GameObject thisWall = Instantiate(wall, new Vector3(pos.x, pos.y, 0), transform.rotation);
                thisWall.transform.localEulerAngles = rotation;
                previewWall.SetActive(false);
                GameObject.FindGameObjectWithTag("Pauser").GetComponent<simulationPauser>().isPaused = false;
                on = false;
                zoomScript.disableZoom = false;
                previewWall.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}

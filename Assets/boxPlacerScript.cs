using UnityEngine;

public class boxPlacer : MonoBehaviour
{
    [SerializeField] private GameObject previewBox;
    [SerializeField] private GameObject box;
    private bool on = false;
    private Camera cam;
    public void placeBox()
    {
        previewBox.SetActive(true);
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
            Vector3 mousePosReal = cam.ScreenToWorldPoint(Input.mousePosition);
            previewBox.transform.position = new Vector3(mousePosReal.x, mousePosReal.y, 0);
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(box, new Vector3(pos.x, pos.y, 0), transform.rotation);
                previewBox.SetActive(false);
                GameObject.FindGameObjectWithTag("Pauser").GetComponent<simulationPauser>().isPaused = false;
                on = false;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
public class periodicTableHighlightScript : MonoBehaviour
{
    private Color normalColor;
    private GameObject[] atoms;
    public GameObject nucleus;
    public GameObject nucleusSpawner;
    private atomSpawnScript atomSpawnScript;
    private Image image;
    public int numElectrons;
    public GameObject previewNucleus;
    private Camera cam;
    private Vector3 mousePosReal;
    private Vector3 pos;
    private GameObject thisAtom;
    private bool secondMode = false;
    private nucleusScript atomNucleusScript;
    private void Start()
    {
        cam = Camera.main;
        normalColor = GetComponent<Image>().color;
        atomSpawnScript = nucleusSpawner.GetComponent<atomSpawnScript>();
        image = GetComponent<Image>();
    }
    private void Update()
    {
        mousePosReal = cam.ScreenToWorldPoint(Input.mousePosition);
        previewNucleus.transform.position = new Vector3(mousePosReal.x, mousePosReal.y, 0);
        if (secondMode)
        {
            if (Input.GetMouseButtonUp(0))
            {
                pos = cam.ScreenToWorldPoint(Input.mousePosition);
                thisAtom = Instantiate(nucleus, new Vector3(pos.x, pos.y, 0), transform.rotation);
                atoms = GameObject.FindGameObjectsWithTag("Atom");
                thisAtom.GetComponent<electronSpawnScript>().numElectrons = numElectrons;
                
                foreach (GameObject atom in atoms)
                {
                    atomNucleusScript = atom.GetComponent<nucleusScript>();
                    atomNucleusScript.uL = true;
                }
                previewNucleus.SetActive(false);
                

                atomSpawnScript.destroyTiles = true;
                secondMode = false;
            }
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(), Input.mousePosition))
        {
            if (Input.GetMouseButtonUp(0))
            {
                atomSpawnScript.end = true;
                previewNucleus.SetActive(true);
                secondMode = true;
            }
            image.color = normalColor + new Color(0.2f, 0.2f, 0.2f);
        }
        else
        {
            image.color = normalColor;
        }
    }
}

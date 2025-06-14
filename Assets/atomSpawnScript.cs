using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class atomSpawnScript : MonoBehaviour
{
    public GameObject nucleus;
    [SerializeField] private GameObject symbol;
    [SerializeField] private GameObject atomMenu;
    private TextMeshProUGUI symbolText;
    private GameObject PeriodicTable;
    private GameObject pauser;
    private simulationPauser simulationPauser;
    private GameObject[] atomNames;
    private List<GameObject> symbols =  new List<GameObject>();
    private List<GameObject> tiles = new List<GameObject>();
    private List<periodicTableHighlightScript> highlightScripts = new List<periodicTableHighlightScript>();
    public bool end = false;
    public bool destroyTiles = false;
    private GameObject bg;
    [SerializeField] private GameObject previewNucleus;
    private Image bgimage;
    private GameObject imageGameObject;
    private periodicTableHighlightScript imageHighlightScript;
    private electronSpawnScript electronSpawnScript;
    private Image image;
    private GameObject text;
    private atomMenuScript atomMenuScript;
    private bool wasActive;
    private bool wasPaused;

    private void Start()
    {
        atomMenuScript = atomMenu.GetComponent<atomMenuScript>();
        electronSpawnScript = nucleus.GetComponent<electronSpawnScript>();
        symbolText = symbol.GetComponent<TextMeshProUGUI>();
        pauser = GameObject.FindGameObjectWithTag("Pauser");
        simulationPauser = pauser.GetComponent<simulationPauser>();
        PeriodicTable = GameObject.FindGameObjectWithTag("PeriodicTable");

    }

    private void Update()
    {
        if (end)
        {
            foreach (GameObject tile in tiles)
            {
                tile.transform.position = new Vector3(2000, 2000);
            }
            foreach (GameObject symbolItem in symbols)
            {
                Destroy(symbolItem);
            }
            symbols.Clear();
            Destroy(bg);
            if (!wasPaused)
            {
                pauser.GetComponent<simulationPauser>().isPaused = false;
            }
            foreach (GameObject atomName in atomNames)
            {
                atomName.SetActive(true);
            }
            end = false;
            
        }
        if (destroyTiles)
        {
            foreach (GameObject tile in tiles)
            {
                Destroy(tile);
            }
            highlightScripts.Clear();
            tiles.Clear();
            destroyTiles = false;
            if (wasActive)
            {
                atomMenuScript.activate();
                wasActive = false;
            }
        }
    }

    public void spawnAtom()
    {
        if (atomMenuScript.isActive)
        {
            wasActive = true;
            atomMenuScript.activate();
        }
        if (pauser.GetComponent<simulationPauser>().isPaused == true)
        {
            wasPaused = true;
        }
        atomNames = GameObject.FindGameObjectsWithTag("AtomName");
        
        foreach (GameObject atomName in atomNames)
        {
            atomName.SetActive(false);
        }
        
        bg = new GameObject();
        
        bgimage = bg.AddComponent<Image>();
        bgimage.color = new Color(0, 0, 0);
        
        bg.transform.localScale = new Vector3(Screen.width, Screen.height, 1);
        bg.transform.SetParent(PeriodicTable.transform);
        simulationPauser.isPaused = true;
        for (int i = 9; i > 0; i--)
        {
            for (int j = 0; j < 18; j++)
            {
                if (!(j < 4 && i-1 < 2) && !((j > 1 && j < 12) && i-1 > 5) && !((j > 0 && j < 17) && i-1 == 8))
                {
                    imageGameObject = new GameObject();
                    image = imageGameObject.AddComponent<Image>();
                    image.color = Random.ColorHSV(0f, 1f, 0.8f, 0.8f, 0.5f, 0.3f);

                    imageGameObject.transform.localScale = new Vector3(Screen.width / 1800, Screen.width / 1800, 1);
                    imageGameObject.transform.position = new Vector3(j * Screen.width / 18, i * Screen.width / 18, 0) + new Vector3(imageGameObject.transform.localScale.x / 2, imageGameObject.transform.localScale.y / 2 - (Screen.height - (Screen.width / 18) * 9) / 200f) * 100;

                    imageGameObject.transform.SetParent(PeriodicTable.transform);

                    imageHighlightScript = imageGameObject.AddComponent<periodicTableHighlightScript>();
                    imageHighlightScript.nucleus = nucleus;
                    imageHighlightScript.nucleusSpawner = gameObject;
                    imageHighlightScript.previewNucleus = previewNucleus;
                    tiles.Add(imageGameObject);
                    highlightScripts.Add(imageHighlightScript);
                }
            }
        }


        
        for (int i = 0; i < tiles.Count; i++)
        {
            if (i > 56 && i < 75)
            {
                symbolText.SetText(electronSpawnScript.atomLookUp[i + 15]);
                highlightScripts[i].numElectrons = i + 15;
            }
            else if (i > 74 && i < 90)
            {
                symbolText.SetText(electronSpawnScript.atomLookUp[i + 29]);
                highlightScripts[i].numElectrons = i + 29;
            }
            else if (i > 89 && i < 104)
            {
                symbolText.SetText(electronSpawnScript.atomLookUp[i - 32]);
                highlightScripts[i].numElectrons = i - 32;
            }
            else if (i > 103 && i < 119)
            {
                symbolText.SetText(electronSpawnScript.atomLookUp[i - 14]);
                highlightScripts[i].numElectrons = i - 14;
            }
            else
            {
                symbolText.SetText(electronSpawnScript.atomLookUp[i + 1]);
                highlightScripts[i].numElectrons = i + 1;
            }
            text = Instantiate(symbol, tiles[i].transform.position, Quaternion.identity);
            symbols.Add(text);
            text.transform.SetParent(PeriodicTable.transform);
        }

        
    }
}

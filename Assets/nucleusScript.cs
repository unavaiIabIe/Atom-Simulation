using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class nucleusScript : MonoBehaviour
{
    [SerializeField] private GameObject compound;
    public List<GameObject> electrons = new List<GameObject>();
    public float charge = 0;
    public List<GameObject> atomsAndCompounds;
    private static List<GameObject> universalAtomsAndCompounds;
    private float zoom;
    public GameObject atomName;
    public GameObject compoundName;
    private GameObject canvas;
    private Camera cam;
    private Vector3 finalForce;
    private Vector3 distance;
    public bool uL = false;
    private bool paused = false;
    private GameObject pauser;
    private simulationPauser simulationPauser;
    public float compoundKey = 0;
    public GameObject thisAtom;
    public string text;
    public bool secondInCompound = false;
    public int valenceNum;
    public int compoundNetCharge;
    public int group;
    private nucleusScript atomNucleusScript;
    private nucleusScript thisSpecAtomNucleusScript;
    private nucleusScript thisCompoundNucleusScript;
    private electronSpawnScript electronSpawnScript;
    private bool atomIsCompound;
    private zoomScript zoomScript;
    public bool attract = false;
    private bool found = false;
    public bool noble;
    [SerializeField] private List<string> nameParts = new List<string>();
    [SerializeField] private List<int> corNums = new List<int>();
    [SerializeField] private List<int> groupNums = new List<int>();
    private List<List<GameObject>> newCompounds;
    private static float universalCompoundKey;
    private bool lS = true;
    public bool isCompound;
    [SerializeField] private GameObject atomInfo;
    public GameObject info;
    private GameObject atomMenu;
    public static float currentY = 479;
    private TMP_Text posXText;
    private TMP_Text posYText;
    private int posXLength;
    private int posYLength;
    public  int disableCompoundFormation = -1; // Unused
    public List<GameObject> appendedInfos = new List<GameObject>();
    private Vector3 initialSpeed = Vector3.zero;


    void Start()
    {
        atomMenu = GameObject.FindGameObjectWithTag("atomMenu");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        info = Instantiate(atomInfo, new Vector2(79.71997f, currentY), Quaternion.identity);
        info.GetComponent<nucleusDeleterScript>().nucleus = gameObject;
        currentY -= 41.0163f;
        info.transform.SetParent(atomMenu.transform, false);
        
        electronSpawnScript = GetComponent<electronSpawnScript>();
        valenceNum = -1;
        pauser = GameObject.FindGameObjectWithTag("Pauser");
        simulationPauser = pauser.GetComponent<simulationPauser>();

        GetComponent<Rigidbody2D>().velocity = initialSpeed;
        
        if (electronSpawnScript != null)
        {
            isCompound = false;
            thisAtom = Instantiate(atomName, Vector3.zero, Quaternion.identity, canvas.transform);
            thisAtom.GetComponent<textPosScript>().nucleus = gameObject;
            charge = (float)electronSpawnScript.numElectrons;        
        }
        else
        {
            isCompound = true;
            thisAtom = Instantiate(compoundName, Vector3.zero, Quaternion.identity, canvas.transform);
            thisAtom.GetComponent<textPosScriptCompound>().nucleus = gameObject;
            thisAtom.GetComponent<TextMeshProUGUI>().SetText(text);
            info.transform.GetChild(3).gameObject.GetComponent<TMP_Text>().SetText(text);
        }
        posXText = info.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
        posYText = info.transform.GetChild(5).gameObject.GetComponent<TMP_Text>();
        cam = Camera.main;
        zoomScript = cam.GetComponent<zoomScript>();

        
    }

    void lateStart()
    {
        this.gameObject.tag = "temp";
        foreach (GameObject atom in GameObject.FindGameObjectsWithTag("Atom"))
        {
            atomsAndCompounds.Add(atom);
        }
        this.gameObject.tag = "Atom";
        universalAtomsAndCompounds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Atom"));
        newCompounds = compoundCheck.checkEnvironment(universalAtomsAndCompounds, gameObject);
        universalCompoundKey = Time.time;
        foreach (List<GameObject> compound in newCompounds)
        {
            foreach (GameObject atom in compound)
            {
                atom.GetComponent<nucleusScript>().compoundKey = universalCompoundKey;
            }
        }
        if (!isCompound)
        {
            info.transform.GetChild(3).gameObject.GetComponent<TMP_Text>().SetText(thisAtom.GetComponent<textPosScript>().text);
        }
    }

    void updateList()
    {
        this.gameObject.tag = "temp";
        List<GameObject> newAtomsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Atom"));
        this.gameObject.tag = "Atom";

        for (int i = 0; i < atomsAndCompounds.Count; i++)
        {
            GameObject atom = atomsAndCompounds[i];
            if (!newAtomsList.Contains(atom))
            {
                atomsAndCompounds.Remove(atom);
            }
        }
        foreach (GameObject atom in newAtomsList)
        {
            if (!atomsAndCompounds.Contains(atom))
            {
                appendedInfos.Add(atom.GetComponent<nucleusScript>().info);
                atomsAndCompounds.Add(atom);
            }
        }
    }

    public void updateInfos(int mode)
    {
        foreach (GameObject info in appendedInfos)
        {
            if (info != null)
            {
                info.transform.position += mode * new Vector3(0, 41.0163f);
            }
        }
    }

    void Update()
    {
        posXLength = transform.position.x.ToString().Length;
        posYLength = transform.position.y.ToString().Length;
        if (transform.position.x < 0)
        {
            if (posXLength > 6)
            {
                posXLength = 6;
            }
            posXText.SetText(((transform.position.x * 100) / 100.0f).ToString().Substring(0, posXLength));
        }
        else {
            if (posXLength > 5)
            {
                posXLength = 5;
            }
            posXText.SetText(((transform.position.x * 100) / 100.0f).ToString().Substring(0, posXLength));
        }
        if (transform.position.y < 0)
        {
            if (posYLength > 6)
            {
                posYLength = 6;
            }
            posYText.SetText(((transform.position.y * 100) / 100.0f).ToString().Substring(0, posYLength));
        }
        else
        {
            if (posYLength > 5)
            {
                posYLength = 5;
            }
            posYText.SetText(((transform.position.y * 100) / 100.0f).ToString().Substring(0, posYLength));
        }
        if (lS)
        {
            lateStart();
            lS = false;
        }

        paused = simulationPauser.isPaused;
        if (uL)
        {
            updateList();
            uL = false;
        }

        if (!paused)
        {
            finalForce = Vector3.zero;
            for (int k = 0; k < atomsAndCompounds.Count; k++)
            {
                GameObject atom = atomsAndCompounds[k];
                if (atom != null)
                {
                    atomNucleusScript = atom.GetComponent<nucleusScript>();
                    atomIsCompound = atomNucleusScript.isCompound;
                    if (compoundKey != 0 && atomNucleusScript.compoundKey == compoundKey)
                    {
                        found = true;
                        if (Vector3.Distance(transform.position, atom.transform.position) < 1)
                        {
                            currentY += 2 * 41.0163f;
                            updateInfos(1);
                            atomNucleusScript.updateInfos(1);
                            Destroy(info);
                            Destroy(atomNucleusScript.info);
                            found = true;
                            GameObject thisCompound = Instantiate(compound, gameObject.transform.position + (atom.transform.position - gameObject.transform.position) / 2, Quaternion.identity);
                            thisCompoundNucleusScript = thisCompound.GetComponent<nucleusScript>();
                            thisCompoundNucleusScript.charge = charge + atomNucleusScript.charge;
                            thisCompoundNucleusScript.compoundKey = compoundKey;
                            int thisCompoundNetCharge = 0;
                            if (isCompound && atomIsCompound)
                            {
                                thisCompoundNetCharge = (valenceNum + atomNucleusScript.valenceNum) % 8;
                                if (thisCompoundNetCharge > 4)
                                {
                                    thisCompoundNetCharge -= 8;
                                }
                            }
                            else if (isCompound && !atomIsCompound)
                            {
                                thisCompoundNetCharge = (atomNucleusScript.compoundNetCharge + valenceNum) % 8;
                            }
                            else if (!isCompound && atomIsCompound)
                            {
                                thisCompoundNetCharge = (compoundNetCharge + atomNucleusScript.valenceNum) % 8;
                            }
                            else
                            {
                                thisCompoundNetCharge = (compoundNetCharge + atomNucleusScript.compoundNetCharge) % 8;
                            }
                            thisCompoundNucleusScript.compoundNetCharge = thisCompoundNetCharge;


                            if (nameParts.Count == 0 && atomNucleusScript.nameParts.Count == 0)
                            {
                                if (text.Equals(atomNucleusScript.text))
                                {
                                    thisCompoundNucleusScript.text = text + "2";
                                    thisCompoundNucleusScript.nameParts.Add(text);
                                    thisCompoundNucleusScript.corNums.Add(2);
                                    thisCompoundNucleusScript.groupNums.Add(group);
                                }
                                else if (secondInCompound)
                                {
                                    thisCompoundNucleusScript.text = atomNucleusScript.text + text;
                                    thisCompoundNucleusScript.nameParts.Add(atomNucleusScript.text);
                                    thisCompoundNucleusScript.corNums.Add(1);
                                    thisCompoundNucleusScript.groupNums.Add(atomNucleusScript.group);
                                    thisCompoundNucleusScript.nameParts.Add(text);
                                    thisCompoundNucleusScript.corNums.Add(1);
                                    thisCompoundNucleusScript.groupNums.Add(group);
                                }
                                else
                                {
                                    thisCompoundNucleusScript.text = text + atomNucleusScript.text;
                                    thisCompoundNucleusScript.nameParts.Add(text);
                                    thisCompoundNucleusScript.corNums.Add(1);
                                    thisCompoundNucleusScript.groupNums.Add(group);
                                    thisCompoundNucleusScript.nameParts.Add(atomNucleusScript.text);
                                    thisCompoundNucleusScript.corNums.Add(1);
                                    thisCompoundNucleusScript.groupNums.Add(atomNucleusScript.group);
                                }
                            }

                            else
                            {
                                nucleusScript scriptOne;
                                nucleusScript scriptTwo;
                                if (!isCompound)
                                {
                                    nameParts.Add(text);
                                    corNums.Add(1);
                                    groupNums.Add(group);
                                }
                                else if (!atomIsCompound)
                                {
                                    atomNucleusScript.nameParts.Add(atomNucleusScript.text);
                                    atomNucleusScript.corNums.Add(1);
                                    atomNucleusScript.groupNums.Add(atomNucleusScript.group);
                                }
                                if (atomIsCompound)
                                {
                                    scriptOne = this;
                                    scriptTwo = atomNucleusScript;
                                }
                                else
                                {
                                    scriptOne = atomNucleusScript;
                                    scriptTwo = this;
                                }
                                thisCompoundNucleusScript.nameParts.Clear();
                                thisCompoundNucleusScript.groupNums.Clear();
                                thisCompoundNucleusScript.corNums.Clear();
                                for (int i = 0; i < scriptTwo.nameParts.Count; i++)
                                {
                                    thisCompoundNucleusScript.nameParts.Insert(i, scriptTwo.nameParts[i]);
                                    thisCompoundNucleusScript.groupNums.Insert(i, scriptTwo.groupNums[i]);
                                    thisCompoundNucleusScript.corNums.Insert(i, scriptTwo.corNums[i]);
                                }
                                for (int j = 0; j < scriptOne.nameParts.Count; j++)
                                {
                                    bool found = false;
                                    for (int i = 0; i < thisCompoundNucleusScript.nameParts.Count; i++)
                                    {
                                        if (thisCompoundNucleusScript.nameParts[i].Equals(scriptOne.nameParts[j]))
                                        {
                                            thisCompoundNucleusScript.corNums[i] += scriptOne.corNums[j];
                                            found = true;
                                        }
                                    }

                                    if (!found)
                                    {
                                        bool placed = false;
                                        for (int i = 0; i < scriptTwo.nameParts.Count; i++)
                                        {
                                            if (scriptOne.groupNums[j] == scriptTwo.groupNums[i])
                                            {
                                                if (String.Compare(scriptOne.nameParts[j], scriptTwo.nameParts[i]) == -1)
                                                {
                                                    thisCompoundNucleusScript.nameParts.Insert(i + 1, scriptOne.nameParts[j]);
                                                    thisCompoundNucleusScript.corNums.Insert(i + 1, scriptOne.corNums[j]);
                                                    thisCompoundNucleusScript.groupNums.Insert(i + 1, scriptOne.groupNums[j]);
                                                    placed = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    thisCompoundNucleusScript.nameParts.Insert(i, scriptOne.nameParts[j]);
                                                    thisCompoundNucleusScript.corNums.Insert(i, scriptOne.corNums[j]);
                                                    thisCompoundNucleusScript.groupNums.Insert(i, scriptOne.groupNums[j]);
                                                    placed = true;
                                                    break;
                                                }
                                            }
                                            else if (scriptOne.groupNums[j] < scriptTwo.groupNums[i])
                                            {
                                                thisCompoundNucleusScript.nameParts.Insert(i, scriptOne.nameParts[j]);
                                                thisCompoundNucleusScript.corNums.Insert(i, scriptOne.corNums[j]);
                                                thisCompoundNucleusScript.groupNums.Insert(i, scriptOne.groupNums[j]);
                                                placed = true;
                                                break;
                                            }
                                        }
                                        if (!placed)
                                        {
                                            thisCompoundNucleusScript.nameParts.Add(scriptOne.nameParts[j]);
                                            thisCompoundNucleusScript.corNums.Add(scriptOne.corNums[j]);
                                            thisCompoundNucleusScript.groupNums.Add(scriptOne.groupNums[j]);
                                        }
                                    }
                                }


                                String netText = "";
                                for (int i = 0; i < thisCompoundNucleusScript.nameParts.Count; i++)
                                {
                                    if (thisCompoundNucleusScript.corNums[i] == 1)
                                    {
                                        netText += thisCompoundNucleusScript.nameParts[i];
                                    }
                                    else
                                    {
                                        netText += thisCompoundNucleusScript.nameParts[i] + thisCompoundNucleusScript.corNums[i];
                                    }
                                }
                                thisCompoundNucleusScript.text = netText;
                            }
                            thisCompoundNucleusScript.initialSpeed = GetComponent<Rigidbody2D>().velocity + atom.GetComponent<Rigidbody2D>().velocity;
                            foreach (GameObject electron in atomNucleusScript.electrons)
                            {
                                Destroy(electron);
                            }
                            atomsAndCompounds.Remove(atomNucleusScript.thisAtom);
                            Destroy(atomNucleusScript.thisAtom);
                            atomsAndCompounds.Remove(atom);
                            foreach (GameObject thisSpecAtom in atomsAndCompounds)
                            {
                                if (thisSpecAtom != null)
                                {
                                    thisSpecAtomNucleusScript = thisSpecAtom.GetComponent<nucleusScript>();
                                    thisSpecAtomNucleusScript.atomsAndCompounds.Remove(atom);
                                    thisSpecAtomNucleusScript.atomsAndCompounds.Remove(gameObject);
                                    thisSpecAtomNucleusScript.atomsAndCompounds.Add(thisCompound);
                                }
                                if (thisSpecAtom.GetComponent<nucleusScript>().appendedInfos.Contains(info))
                                {
                                    thisSpecAtom.GetComponent<nucleusScript>().appendedInfos.Remove(info);
                                }
                            }
                            Destroy(atom);
                            foreach (GameObject electron in electrons)
                            {
                                Destroy(electron);
                            }

                            Destroy(thisAtom);
                            Destroy(gameObject);
                        }
                    }
                    Vector3 oppDirP = Vector3.Normalize(new Vector3(transform.position.x - atom.transform.position.x, transform.position.y - atom.transform.position.y));
                    float repulsiveForceP = (float)chargeScript.ChargeScript(gameObject, atom) * Mathf.Pow(10, 7);
                    finalForce += new Vector3(oppDirP.x * (float)repulsiveForceP, oppDirP.y * (float)repulsiveForceP);
                }
            }

            if (compoundKey != 0 && !found)
            {
                compoundKey = 0;
            }

            distance = finalForce * Time.deltaTime;
            GetComponent<Rigidbody2D>().AddForce(distance);
        }

        found = false;

        zoom = zoomScript.zoom;

        if (zoom > 10 && zoom < 20)
        {
            transform.localScale = new Vector3(0.16f + 23 * (zoom - 10) / 125, 0.16f + 23 * (zoom - 10) / 125);
        }
        else if (zoom >= 20)
        {
            transform.localScale = new Vector3(2, 2);
        }
        else
        {
            transform.localScale = new Vector3(0.16f, 0.16f);
        }
    }
}

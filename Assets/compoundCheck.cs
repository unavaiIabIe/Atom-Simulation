using System.Collections.Generic;
using UnityEngine;

public class compoundCheck : MonoBehaviour
{
    public static List<List<GameObject>> checkEnvironment(List<GameObject> atomsAndCompounds, GameObject newAtom)
    {
        List<List<GameObject>> futureCompounds = new List<List<GameObject>>();
        // Two atom
        for (int i = 0; i < atomsAndCompounds.Count; i++)
        {
            GameObject atomTwo = atomsAndCompounds[i];
            if (!ReferenceEquals(atomTwo, newAtom))
            {
                List<GameObject> potentialCompound = new List<GameObject>() { newAtom, atomTwo };
                if (willFormCompound(potentialCompound))
                {
                    futureCompounds.Add(potentialCompound);
                    atomsAndCompounds.Remove(atomTwo);
                }
            }
        }
        // Three atom
        for (int i = 0; i < atomsAndCompounds.Count; i++)
        {
            GameObject atomTwo = atomsAndCompounds[i];
            if (!ReferenceEquals(atomTwo, newAtom))
            {
                for (int j = 0; j < atomsAndCompounds.Count; j++)
                {
                    GameObject atomThree = atomsAndCompounds[j];
                    if (!ReferenceEquals(atomThree, newAtom) &&  i != j)
                    {
                        List<GameObject> potentialCompound = new List<GameObject>() { newAtom, atomTwo, atomThree };
                        if (willFormCompound(potentialCompound))
                        {
                            futureCompounds.Add(potentialCompound);
                            atomsAndCompounds.Remove(atomTwo);
                            atomsAndCompounds.Remove(atomThree);
                        }
                    }
                }
            }
        }
        // Four atom
        for (int i = 0; i < atomsAndCompounds.Count; i++)
        {
            GameObject atomTwo = atomsAndCompounds[i];
            if (!ReferenceEquals(atomTwo, newAtom))
            {
                for (int j = 0; j < atomsAndCompounds.Count; j++)
                {
                    GameObject atomThree = atomsAndCompounds[j];
                    if (!ReferenceEquals(atomThree, newAtom) && i != j)
                    {
                        for (int k = 0; k < atomsAndCompounds.Count; k++)
                        {
                            GameObject atomFour = atomsAndCompounds[k];
                            if (!ReferenceEquals(atomFour, newAtom) && k != i && k != j)
                            {
                                List<GameObject> potentialCompound = new List<GameObject>() { newAtom, atomTwo, atomThree, atomFour };
                                if (willFormCompound(potentialCompound))
                                {
                                    futureCompounds.Add(potentialCompound);
                                    atomsAndCompounds.Remove(atomTwo);
                                    atomsAndCompounds.Remove(atomThree);
                                    atomsAndCompounds.Remove(atomFour);
                                }
                            }
                        }
                    }
                }
            }
        }
        // Five atom
        for (int i = 0; i < atomsAndCompounds.Count; i++)
        {
            GameObject atomTwo = atomsAndCompounds[i];
            if (!ReferenceEquals(atomTwo, newAtom))
            {
                for (int j = 0; j < atomsAndCompounds.Count; j++)
                {
                    GameObject atomThree = atomsAndCompounds[j];
                    if (!ReferenceEquals(atomThree, newAtom) && i != j)
                    {
                        for (int k = 0; k < atomsAndCompounds.Count; k++)
                        {
                            GameObject atomFour = atomsAndCompounds[k];
                            if (!ReferenceEquals(atomFour, newAtom) && k != i && k != j)
                            {
                                for (int l = 0; l < atomsAndCompounds.Count; l++)
                                {
                                    GameObject atomFive = atomsAndCompounds[l];
                                    if (!ReferenceEquals(atomFive, newAtom) && l != i && l != j && l != k)
                                    {
                                        List<GameObject> potentialCompound = new List<GameObject>() { newAtom, atomTwo, atomThree, atomFour, atomFive };
                                        if (willFormCompound(potentialCompound))
                                        {
                                            futureCompounds.Add(potentialCompound);
                                            atomsAndCompounds.Remove(newAtom);
                                            atomsAndCompounds.Remove(atomTwo);
                                            atomsAndCompounds.Remove(atomThree);
                                            atomsAndCompounds.Remove(atomFour);
                                            atomsAndCompounds.Remove(atomFive);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        // Not worth spending processing power to go further
        return futureCompounds;
    }
    private static bool willFormCompound(List<GameObject> atoms)
    {
        int totalValence = 0;
        foreach (GameObject atom in atoms)
        {
            nucleusScript atomNucleusScript = atom.GetComponent<nucleusScript>();
            if (atomNucleusScript.noble)
            {
                return false;
            }
            if (atomNucleusScript.disableCompoundFormation == atoms.Count)
            {
                return false;
            }
            if (atomNucleusScript.isCompound)
            {
                totalValence += atomNucleusScript.compoundNetCharge;
            }
            else
            {
                totalValence += atomNucleusScript.valenceNum;
            }
        }
        if (totalValence % 2 != 0)
        {
            return false;
        }
        foreach (GameObject atom in atoms)
        {
            atom.GetComponent<nucleusScript>().disableCompoundFormation = atoms.Count;
        }
        return true;
    }
}
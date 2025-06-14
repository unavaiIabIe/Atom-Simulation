using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class electronSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject Electron;
    private float d = 0.15f;
    public int numElectrons = 0;
    private int[] electronsPerShell = new int[8];
    private electronScript thisElectronScript;
    private nucleusScript nucleusScript;
    // Aufbau
    private int[,] pattern = { { 1, 2 }, { 2, 2 }, { 2, 6 }, { 3, 2 }, { 3, 6 }, { 4, 2 }, { 3, 10 }, { 4, 6 }, { 5, 2 }, { 4, 10 }, { 5, 6 }, { 6, 2 }, { 4, 14 }, { 5, 10 }, { 6, 6 }, { 7, 2 }, { 5, 14 }, { 6, 10 }, { 7, 6 }};
    public Dictionary<int, string> atomLookUp = new Dictionary<int, string>()
        {
            {1, "H"},
            {2, "He" },
            {3, "Li" },
            {4, "Be" },
            {5, "B" },
            {6, "C" },
            {7, "N" },
            {8, "O" },
            {9, "F" },
            {10, "Ne" },
            {11, "Na" },
            {12, "Mg" },
            {13, "Al" },
            {14, "Si" },
            {15, "P" },
            {16, "S" },
            {17, "Cl" },
            {18, "Ar" },
            {19, "K" },
            {20, "Ca" },
            {21, "Sc" },
            {22, "Ti" },
            {23, "V" },
            {24, "Cr" },
            {25, "Mn" },
            {26, "Fe" },
            {27, "Co" },
            {28, "Ni" },
            {29, "Cu" },
            {30, "Zn" },
            {31, "Ga" },
            {32, "Ge" },
            {33, "As" },
            {34, "Se" },
            {35, "Br" },
            {36, "Kr" },
            {37, "Rb" },
            {38, "Sr" },
            {39, "Y" },
            {40, "Zr" },
            {41, "Nb" },
            {42, "Mo" },
            {43, "Tc" },
            {44, "Ru" },
            {45, "Rh" },
            {46, "Pd" },
            {47, "Ag" },
            {48, "Cd" },
            {49, "In" },
            {50, "Sn" },
            {51, "Sb" },
            {52, "Te" },
            {53, "I" },
            {54, "Xe" },
            {55, "Cs" },
            {56, "Ba" },
            {57, "La" },
            {58, "Ce" },
            {59, "Pr" },
            {60, "Nd" },
            {61, "Pm" },
            {62, "Sm" },
            {63, "Eu" },
            {64, "Gd" },
            {65, "Tb" },
            {66, "Dy" },
            {67, "Ho" },
            {68, "Er" },
            {69, "Tm" },
            {70, "Yb" },
            {71, "Lu" },
            {72, "Hf" },
            {73, "Ta" },
            {74, "W" },
            {75, "Re" },
            {76, "Os" },
            {77, "Ir" },
            {78, "Pt" },
            {79, "Au" },
            {80, "Hg" },
            {81, "Tl" },
            {82, "Pb" },
            {83, "Bi" },
            {84, "Po" },
            {85, "At" },
            {86, "Rn" },
            {87, "Fr" },
            {88, "Ra" },
            {89, "Ac" },
            {90, "Th" },
            {91, "Pa" },
            {92, "U" },
            {93, "Np" },
            {94, "Pu" },
            {95, "Am" },
            {96, "Cm" },
            {97, "Bk" },
            {98, "Cf" },
            {99, "Es" },
            {100, "Fm" },
            {101, "Md" },
            {102, "No" },
            {103, "Lr" },
            {104, "Rf" },
            {105, "Db" },
            {106, "Sg" },
            {107, "Bh" },
            {108, "Hs" },
            {109, "Mt" },
            {110, "Ds" },
            {111, "Rg" },
            {112, "Cn" },
            {113, "Nh" },
            {114, "Fl" },
            {115, "Mc" },
            {116, "Lv" },
            {117, "Ts" },
            {118, "Og" }
        };
    private Dictionary<int, int> groupLookUp = new Dictionary<int, int>()
        {
            {1, 1},
            {2, 18},
            {3,  1},
            {4, 2},
            {5, 13},
            {6, 14},
            {7, 15},
            {8, 16},
            {9, 17},
            {10, 18},
            {11, 1},
            {12, 2},
            {13, 13},
            {14, 14},
            {15, 15},
            {16, 16},
            {17, 17},
            {18, 18},
            {19, 1},
            {20, 2},
            {21, 3},
            {22, 4},
            {23, 5},
            {24, 6},
            {25, 7},
            {26, 8},
            {27, 9},
            {28, 10},
            {29, 11},
            {30, 12},
            {31, 13},
            {32, 14},
            {33, 15},
            {34, 16},
            {35, 17},
            {36, 18},
            {37, 1},
            {38, 2},
            {39, 3},
            {40, 4},
            {41, 5},
            {42, 6},
            {43, 7},
            {44, 8},
            {45, 9},
            {46, 10},
            {47, 11},
            {48, 12},
            {49, 13},
            {50, 14},
            {51, 15},
            {52, 16},
            {53, 17},
            {54, 18},
            {55, 1},
            {56, 2},
            {57, 3},
            {58, 3},
            {59, 3},
            {60, 3},
            {61, 3},
            {62, 3},
            {63, 3},
            {64, 3},
            {65, 3},
            {66, 3},
            {67, 3},
            {68, 3},
            {69, 3},
            {70, 3},
            {71, 3},
            {72, 4},
            {73, 5},
            {74, 6},
            {75, 7},
            {76, 8},
            {77, 9},
            {78, 10},
            {79, 11},
            {80, 12},
            {81, 13},
            {82, 14},
            {83, 15},
            {84, 16},
            {85, 17},
            {86, 18},
            {87, 1},
            {88, 2},
            {89, 3},
            {90, 3},
            {91, 3},
            {92, 3},
            {93, 3},
            {94, 3},
            {95, 3},
            {96, 3},
            {97, 3},
            {98, 3},
            {99, 3},
            {100, 3},
            {101, 3},
            {102, 3},
            {103, 3},
            {104, 4},
            {105, 5},
            {106, 6},
            {107, 7},
            {108, 8},
            {109, 9},
            {110, 10},
            {111, 1},
            {112, 12},
            {113, 13},
            {114, 14},
            {115, 15},
            {116, 16},
            {117, 17},
            {118, 18}
        };

    void Start()
    {
        nucleusScript = GetComponent<nucleusScript>();
        int total = 0;
        int i = 0;
        int num = 0;
        while (total < numElectrons)
        {
            if (!(total + pattern[i, 1] >= numElectrons))
            {
                num = pattern[i, 1];
            }
            else
            {
                num = numElectrons - total;
            }
            electronsPerShell[pattern[i, 0]] += num;
            total += num;
            i++;

        }
        for (int j = 0; j < 8; j++)
        {
            for (int k = 0; k < electronsPerShell[j]; k++)
            {
                Vector3 pos = new Vector3(gameObject.transform.position.x + Mathf.Cos(k * (2 * Mathf.PI / electronsPerShell[j]))
                    * d * (j + 1), gameObject.transform.position.y + Mathf.Sin(k * (2 * Mathf.PI / electronsPerShell[j])) * d * (j + 1));
                GameObject thisElectron = Instantiate(Electron, pos, transform.rotation);
                thisElectronScript = thisElectron.GetComponent<electronScript>();
                thisElectronScript.intendedPosition = pos - gameObject.transform.position;
                thisElectronScript.nucleus = gameObject;
                nucleusScript.electrons.Add(thisElectron);
            }
        }
        nucleusScript.charge = numElectrons;
        for (int l = 0; l < electronsPerShell.Length; l++)
        {
            if (l == electronsPerShell.Length-1)
            {
                nucleusScript.valenceNum = electronsPerShell[l];
                break;
            }
            else if (electronsPerShell[l+1] == 0)
            {
                nucleusScript.valenceNum = electronsPerShell[l];
                break;
            }
        }
       nucleusScript.group = groupLookUp[numElectrons];
       if (nucleusScript.group == 18)
        {
            nucleusScript.noble = true;
        }
    }
}

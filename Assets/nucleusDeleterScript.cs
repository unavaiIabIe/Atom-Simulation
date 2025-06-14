using UnityEngine;

public class nucleusDeleterScript : MonoBehaviour
{
    public GameObject nucleus;
    private nucleusScript nucScript;
    private void Start()
    {
        nucScript = nucleus.GetComponent<nucleusScript>();
    }
    public void deleteNucleus()
    {
        foreach (GameObject electron in nucScript.electrons)
        {
            Destroy(electron);
        }
        nucScript.updateInfos(1);
        foreach (GameObject atom in nucScript.atomsAndCompounds)
        {
            if (atom != null && atom.GetComponent<nucleusScript>().appendedInfos.Contains(nucScript.info))
            {
                atom.GetComponent<nucleusScript>().appendedInfos.Remove(nucScript.info);
            }
        }
        Destroy(nucScript.thisAtom);
        Destroy(nucleus);
        nucleusScript.currentY += 41.0163f;
        Destroy(gameObject);
    }
}

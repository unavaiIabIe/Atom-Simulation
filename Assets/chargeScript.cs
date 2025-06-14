using UnityEngine;

public class chargeScript : MonoBehaviour
{
    public static double ChargeScript(GameObject particleOne, GameObject particleTwo)
    {
        double k = (9 * Mathf.Pow(10, 9));
        double e = Mathf.Pow((float)1.60217733 * Mathf.Pow(10, -19), 2);
        double cOne = Mathf.Pow(10, 20);
        double cTwo = Mathf.Pow(10, -25);

        float chargeOne;
        float chargeTwo;
        nucleusScript particleOneNucleusScript = particleOne.GetComponent<nucleusScript>();
        nucleusScript particleTwoNucleusScript = particleTwo.GetComponent<nucleusScript>();
        chargeOne = particleOneNucleusScript.charge;
        chargeTwo = particleTwoNucleusScript.charge;

        float r = Mathf.Sqrt(Mathf.Pow(particleTwo.transform.position.x - particleOne.transform.position.x, 2) + Mathf.Pow(particleTwo.transform.position.y - particleOne.transform.position.y, 2));

        //Coulomb's Law (with added constants cOne and cTwo)
        double final =  ((k * (chargeOne * chargeTwo * e) + cTwo) / Mathf.Pow(r, 2)) * cOne;

        if (particleOneNucleusScript.compoundKey == particleTwoNucleusScript.compoundKey && particleOneNucleusScript.compoundKey != 0)
        {
            return -final;
        }

        return final;
    }
}

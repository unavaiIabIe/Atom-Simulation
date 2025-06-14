using UnityEngine;

public class simulationPauser : MonoBehaviour
{
    public bool isPaused = false;
    public void pauseSimulation()
    {
        if (isPaused == false)
        {
            isPaused = true;
        }
        else
        {
            isPaused = false;
        }
    }
}

using UnityEngine;

public class cameraMoveScript : MonoBehaviour
{
    private Vector3 Origin;
    private Vector3 Difference;
    private bool drag = false;

    void LateUpdate()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (drag == false)
            {
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }
        if (drag)
        {
            Camera.main.transform.position = Origin - Difference;
        }
    }
}

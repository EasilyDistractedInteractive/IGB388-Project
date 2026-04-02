using UnityEngine;

public class LeverExample : MonoBehaviour
{
    private void Start()
    {
        SetToGreen();
    }

    public void SetToRed ()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void SetToGreen()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
}

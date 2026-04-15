using UnityEngine;
public class LightCandle : MonoBehaviour
{
    public GameObject visualToShow;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Flame")
        {
            visualToShow.SetActive(true);
        }
    }
}

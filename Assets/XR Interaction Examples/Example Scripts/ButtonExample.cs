using UnityEngine;

public class ButtonExample : MonoBehaviour
{
    public GameObject objectToSpawn;

    public void Spawn ()
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}

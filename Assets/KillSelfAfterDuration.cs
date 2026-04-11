using UnityEngine;

public class KillSelfAfterDuration : MonoBehaviour
{
    public float duration;

    void Start()
    {
        Destroy(gameObject, duration);
    }

}

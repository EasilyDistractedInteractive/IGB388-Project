using UnityEngine;

public class Grill : MonoBehaviour
{
    [Tooltip("The smoke for when the grill has no ingredients on it")]
    public GameObject grillInactiveSmoke;

    [Tooltip("The smoke for when the grill has ingredients on it")]
    public GameObject grillActiveSmoke;

    public int IngredientsInTrigger;
    public AudioClip grillingNoise;
    public AudioSource grillAudioSource;

    void Start()
    {
        IngredientsInTrigger = 0; // Or however many cubes at start
        grillInactiveSmoke.SetActive(true);
    }

    void OnTriggerEnter(Collider col)
    {
        // check for entering cubes
        if (col.tag == "Ingredient")
        {
            IngredientsInTrigger++;
            grillInactiveSmoke.SetActive(false);
            grillActiveSmoke.SetActive(true);
            grillAudioSource.PlayOneShot(grillingNoise);
            Debug.Log("Played cook audio");
        }
    }

    void OnTriggerExit(Collider col)
    {
        // check for exiting cubes
        if (col.tag == "Ingredient")
        {
            IngredientsInTrigger--; // Could use `--cubesInTrigger` inside the if, but this is more readable
            grillInactiveSmoke.SetActive(true);
            grillActiveSmoke.SetActive(false);
            grillAudioSource.Stop();
            Debug.Log("Audio Stopped");
        }
    }
}
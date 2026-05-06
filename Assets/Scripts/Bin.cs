using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Bin : MonoBehaviour
{
    List<GameObject> binObjects;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ingredient") || collision.transform.CompareTag("Bomb"))
        {
            binObjects.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ingredient") || collision.transform.CompareTag("Bomb"))
        {
            binObjects.Remove(collision.gameObject);
        }
    }

    public void EmptyBin()
    {
        foreach (GameObject binObject in binObjects)
        {
            Destroy(binObject);
        }

        binObjects.Clear();
    }
}
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Bin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ingredient") || collision.transform.CompareTag("Bomb"))
        {
            Destroy(collision.gameObject);
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ResetUtensil : MonoBehaviour
{
    public XRSocketInteractor socketInteractor;

    private XRGrabInteractable currentUtensil;
    private Rigidbody rb;

    private void Awake()
    {
        currentUtensil = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {
            ResetUtensilPosition();
        }
    }

    public void ResetUtensilPosition()
    {
        if (currentUtensil.isSelected)
        {
            currentUtensil.interactionManager.SelectExit(currentUtensil.firstInteractorSelecting, currentUtensil);
        }

        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;


        transform.position = socketInteractor.transform.position;
        transform.rotation = socketInteractor.transform.rotation;


        currentUtensil.interactionManager.SelectEnter(
            (IXRSelectInteractor)socketInteractor, 
            (IXRSelectInteractable)currentUtensil
        );
    }


}


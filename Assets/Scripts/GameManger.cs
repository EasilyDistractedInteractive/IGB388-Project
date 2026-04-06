using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class GameManger : MonoBehaviour
{

    public XRSocketInteractor cuttingBoard;
    public GameObject currentObjectOnBoard;


    public void Update()
    {
      
        IXRSelectInteractable boardInteractable = cuttingBoard.GetOldestInteractableSelected();
        if(boardInteractable != null)
        {
            currentObjectOnBoard = boardInteractable.transform.gameObject;
        }

        IngredientLogic ingredientOnCuttingBoard = currentObjectOnBoard.GetComponent<IngredientLogic>();
        ingredientOnCuttingBoard.setIsOnCuttingBoardTrue();



    }
}

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

    public XRSocketInteractor sink;
    public GameObject currentObjectInSink;

    public List<GameObject> ingredientsList = new List<GameObject>();
    public GameObject ingredientSpawnPoint;


    public void Update()
    {
      
        IXRSelectInteractable boardInteractable = cuttingBoard.GetOldestInteractableSelected();
        if(boardInteractable != null)
        {
            currentObjectOnBoard = boardInteractable.transform.gameObject;

            IngredientLogic ingredientOnCuttingBoard = currentObjectOnBoard.GetComponent<IngredientLogic>();
            ingredientOnCuttingBoard.setIsOnCuttingBoardTrue();
        }

        //currentObjectOnBoard = null;

        IXRSelectInteractable sinkInteractable = sink.GetOldestInteractableSelected();
        if(sinkInteractable != null)
        {
            currentObjectInSink = sinkInteractable.transform.gameObject;

            IngredientLogic ingredientInSink = currentObjectInSink.GetComponent<IngredientLogic>();
            ingredientInSink.setIsInSinkTrue();
        }

        //currentObjectInSink = null;

    }


    /// <summary>
    /// Testing function to spawn random ingredient when slop lever pulled. Goo goo ga ga
    /// By Maximus K
    /// </summary>
    public void SpawnRandomIngredient()
    {
        Debug.Log("Spawning random ingredient");
        Instantiate(ingredientsList[Random.Range(0, ingredientsList.Count)], ingredientSpawnPoint.transform.position, ingredientSpawnPoint.transform.rotation);
    }
}

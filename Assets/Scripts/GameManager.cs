using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GameManager : MonoBehaviour
{
    public XRSocketInteractor cuttingBoard;
    public GameObject currentObjectOnBoard;
    IngredientLogic ingredientOnCuttingBoard;

    public XRSocketInteractor sink;
    public GameObject currentObjectInSink;
    IngredientLogic ingredientInSink;

    public List<GameObject> ingredientsList = new List<GameObject>();
    public GameObject ingredientSpawnPoint;

    public GameObject tapWaterParticles;

    public bool tapOn;

    float cleanRate = 100f;

    [SerializeField] public AudioClip prepCompleted;

    [SerializeField] public AudioClip[] cutIngredientClips;
    [SerializeField] public AudioClip squishedIngredientClip;


    public void Update()
    {
      
        IXRSelectInteractable boardInteractable = cuttingBoard.GetOldestInteractableSelected();
        if(boardInteractable != null)
        {
            currentObjectOnBoard = boardInteractable.transform.gameObject;

            ingredientOnCuttingBoard = currentObjectOnBoard.GetComponent<IngredientLogic>();
            ingredientOnCuttingBoard.setIsOnCuttingBoardTrue();
        }

        //currentObjectOnBoard = null;

        IXRSelectInteractable sinkInteractable = sink.GetOldestInteractableSelected();
        if(sinkInteractable != null)
        {
            currentObjectInSink = sinkInteractable.transform.gameObject;

            ingredientInSink = currentObjectInSink.GetComponent<IngredientLogic>();
            ingredientInSink.setIsInSinkTrue();

            if(tapOn == true)
            {
                ingredientInSink.Wash(cleanRate);
            }
        }

        //currentObjectInSink = null;

        

    }

    public void SinkWaterOn()
    {
        tapWaterParticles.SetActive(true);
        tapOn = true;
    }

    public void SinkWaterOff()
    {
        tapWaterParticles.SetActive(false);
        tapOn = false;
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

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GameManager : MonoBehaviour
{
    public Chef chef;
    public OrderHandler orderHandler;
    public Timer gameTimer;

    [Header("Cutting Board")]

    public XRSocketInteractor cuttingBoardSocket;
    public GameObject currentObjectOnBoard;
    IngredientLogic ingredientOnCuttingBoard;

    [Header("Sink")]

    public XRSocketInteractor sinkSocket;
    public GameObject currentObjectInSink;
    IngredientLogic ingredientInSink;

    public GameObject tapWaterParticles;

    public bool tapOn;

    float cleanRate = 100f;

    [Header("Slop Dispensers")]

    public XRSocketInteractor SlopADispenserSocket;
    public GameObject SlopACurrentObject;
    IngredientLogic IngredientOnSlopA;
    public GameObject SlopAParticles;

    public XRSocketInteractor SlopBDispenserSocket;
    public GameObject SlopBCurrentObject;
    IngredientLogic IngredientOnSlopB;
    public GameObject SlopBParticles;

    public XRSocketInteractor SlopCDispenserSocket;
    public GameObject SlopCCurrentObject;
    IngredientLogic IngredientOnSlopC;
    public GameObject SlopCParticles;

    //public List<GameObject> ingredientsList = new List<GameObject>();
    //public GameObject ingredientSpawnPoint;

    [Header("Sound files")]

    [SerializeField] public AudioClip prepCompleted;

    [SerializeField] public AudioClip[] cutIngredientClips;
    [SerializeField] public AudioClip squishedIngredientClip;

    public void Update()
    {

        IXRSelectInteractable slopAInteractable = SlopADispenserSocket.GetOldestInteractableSelected();
        if(slopAInteractable != null)
        {
            SlopACurrentObject = slopAInteractable.transform.gameObject;

            IngredientOnSlopA = SlopACurrentObject.GetComponent<IngredientLogic>();
        }

        IXRSelectInteractable slopBInteractable = SlopBDispenserSocket.GetOldestInteractableSelected();
        if(slopBInteractable != null)
        {
            SlopBCurrentObject = slopBInteractable.transform.gameObject;

            IngredientOnSlopB = SlopBCurrentObject.GetComponent<IngredientLogic>();
        }

        IXRSelectInteractable slopCInteractable = SlopCDispenserSocket.GetOldestInteractableSelected();
        if(slopCInteractable != null)
        {
            SlopCCurrentObject = slopCInteractable.transform.gameObject;

            IngredientOnSlopC = SlopCCurrentObject.GetComponent<IngredientLogic>();
        }

        

      
        IXRSelectInteractable boardInteractable = cuttingBoardSocket.GetOldestInteractableSelected();
        if(boardInteractable != null)
        {
            currentObjectOnBoard = boardInteractable.transform.gameObject;

            ingredientOnCuttingBoard = currentObjectOnBoard.GetComponent<IngredientLogic>();
            ingredientOnCuttingBoard.setIsOnCuttingBoardTrue();
        }

        //currentObjectOnBoard = null;

        IXRSelectInteractable sinkInteractable = sinkSocket.GetOldestInteractableSelected();
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

    public void SlopAActivate()
    {
        IngredientOnSlopA.SetSlopA();
        SlopAParticles.SetActive(true);
    }

    public void SlopBActivate()
    {
        IngredientOnSlopB.SetSlopB();
        SlopBParticles.SetActive(true);
    }

    public void SlopCActivate()
    {
        IngredientOnSlopC.SetSlopC();
        SlopCParticles.SetActive(true);
    }


    public void SlopADeactivate()
    {
        SlopAParticles.SetActive(false);
    }

    public void SlopBDeactivate()
    {
        SlopBParticles.SetActive(false);
    }
    
    public void SlopCDeactivate()
    {
        SlopCParticles.SetActive(false);
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


    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    /// <summary>
    /// Testing function to spawn random ingredient when slop lever pulled. Goo goo ga ga
    /// By Maximus K
    /// </summary>
    //public void SpawnRandomIngredient()
    //{
    //    Debug.Log("Spawning random ingredient");
    //    Instantiate(ingredientsList[Random.Range(0, ingredientsList.Count)], ingredientSpawnPoint.transform.position, ingredientSpawnPoint.transform.rotation);
    //}
}

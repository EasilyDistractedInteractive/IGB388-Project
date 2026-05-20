using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;
using Unity.XR.CoreUtils;

public class GameManager : MonoBehaviour
{
    public Chef chef;
    public OrderHandler orderHandler;
    public Timer gameTimer;
    public TutorialManager tutorialManager;

    public Slider tableHeightSlider;
    public Transform tableTransform;

    public Slider playerHeightSlider;
    public XROrigin playerOrigin;

    [Header("Cutting Board")]

    public XRSocketInteractor cuttingBoardSocket;
    public GameObject currentObjectOnBoard;
    IngredientLogic ingredientOnCuttingBoard;

    [Header("Sink")]

    public XRSocketInteractor sinkSocket;
    public GameObject currentObjectInSink;
    IngredientLogic ingredientInSink;

    public GameObject tapWaterParticles;
    float cleanRate = 100f;
    public bool tapOn;

    [Header("Grill")]
    public XRSocketInteractor grillSocket;
    public GameObject currentObjectOnGrill;
    IngredientLogic ingredientOnGrill;

    float cookRate = 25f;
    

    

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

    [SerializeField] public AudioClip grillSizzling;

    [SerializeField] public AudioClip grillingComplete;

    float tableHeight;
    float playerHeight = 1.7f;

    public GameObject heightAdjustSliders;
    public GameObject heightAdjustButtons;

    public enum HeightAdjustMode
    {
        sliders,
        buttons,     
    }

    public HeightAdjustMode heightAdjustMode;

    float playerHeightAdjustRate = 0.1f;
    float tableHeightAdjustRate = 0.1f;


    public void Update()
    {
        


        if(heightAdjustMode == HeightAdjustMode.sliders)
        {
            heightAdjustSliders.SetActive(true);
            heightAdjustButtons.SetActive(false);
            tableHeight = tableHeightSlider.value;
            playerHeight = playerHeightSlider.value;
        }

        if(heightAdjustMode == HeightAdjustMode.buttons)
        {
            heightAdjustSliders.SetActive(false);
            heightAdjustButtons.SetActive(true);

            if(playerHeight >= 2f)
            {
                playerHeight = 2f;
            }

            if(playerHeight <= 1.4f)
            {
                playerHeight = 1.4f;
            }

            if(tableHeight >= 0.675f)
            {
                tableHeight = 0.675f;
            }

            if(tableHeight <= -0.675f)
            {
                tableHeight = -0.675f;
            }
        }

        tableTransform.position = new Vector3(0,tableHeight,0);
        playerOrigin.CameraYOffset = playerHeight;


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


        //Handles grill logic
        IXRSelectInteractable grillInteractable = grillSocket.GetOldestInteractableSelected();
        if (grillInteractable != null)
        {
            currentObjectOnGrill = grillInteractable.transform.gameObject;

            ingredientOnGrill = currentObjectOnGrill.GetComponent<IngredientLogic>();
            ingredientOnGrill.setIsOnGrillTrue();

            ingredientOnGrill.Cook(cookRate);
        }
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

    public void playerHeightIncrease()
    {
        playerHeight += playerHeightAdjustRate;
    }

    public void playerHeightDecrease()
    {
        playerHeight -= playerHeightAdjustRate;
    }



    public void tableHeightIncrease()
    {
        tableHeight += tableHeightAdjustRate;
    }

    public void tableHeightDecrease()
    {
        tableHeight -= tableHeightAdjustRate;
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

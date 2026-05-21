using UnityEngine;
using UnityEngine.UI;

public class IngredientLogic : MonoBehaviour
{
    public GameManager Manager;
    public Ingredient ingredient;
    GameObject ingredientModel;

    public string ingredientName;

    public bool isDirty;
    public bool isSliced;
    public bool isCooked;
    int slices = 0;

    public bool isOnCuttingBoard;
    public int framesOffCuttingBoard;

    public bool isOnSlopA;
    public int framesOffSlopA;

    public bool isOnSlopB;
    public int framesOffSlopB;

    public bool isOnSlopC;
    public int framesOffSlopC;

    public bool isInSink;
    public int framesOutOfSink;
    public float cleanliness = 0;

    public bool isOnGrill;
    public int framesOffGrill;
    public float cookedness = 0;

    [SerializeField] private AudioSource ingredientAudioSource;

    public Sprite ingredientIcon;

    bool playedCookedDing = false;
    bool playedGrillSizzle = false;
    bool playedSinkSploosh = false;
    


    public enum state
    {
        Clean_Sliced_Cooked,
        Clean_Sliced_Raw,
        Clean_Unsliced_Cooked,
        Clean_Unsliced_Raw,
        Dirty_Sliced_Cooked,
        Dirty_Sliced_Raw,
        Dirty_Unsliced_Cooked,
        Dirty_Unsliced_Raw,       
    }

    public enum slopStates
    {
        Slop_Empty,
        Slop_A_Full,
        Slop_B_Full,
        Slop_C_Full
    }

    [HideInInspector] public int statesCount = 8; //Update if more states are added
    [HideInInspector] public int slopStatesCount = 4; //Update if more states are added

    [HideInInspector] public state currentState = state.Dirty_Unsliced_Raw;

    [HideInInspector] public slopStates currentSlopState = slopStates.Slop_Empty;

    public state currentModel = state.Dirty_Unsliced_Raw;

    public TutorialManager tutorialManager;


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        tutorialManager = Manager.tutorialManager;

        isSliced = false;
        isDirty = true;
        isCooked = false;

        if(isDirty)
        {
            cleanliness = 0f;
        }

        instantiateCurrentModel();

        //ingredientAudioSource = GetComponentInChildren<AudioSource>();

        
        
    }

    public void SetGrabbedBool()
    {
        if(tutorialManager != null)
        {
            tutorialManager.ingredientGrabbed = true;
        }
    }

    void instantiateCurrentModel()
    {
        Destroy(ingredientModel);
        if(currentState == state.Dirty_Unsliced_Raw)
        {
            ingredientModel = Instantiate (ingredient.model_Dirty_Unsliced_Raw, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Dirty_Unsliced_Raw;
        }
        if(currentState == state.Clean_Unsliced_Raw)
        {
            ingredientModel = Instantiate (ingredient.model_Clean_Unsliced_Raw, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Clean_Unsliced_Raw;
        }
        if(currentState == state.Clean_Sliced_Raw)
        {
            ingredientModel = Instantiate (ingredient.model_Clean_Sliced_Raw, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Clean_Sliced_Raw;
            ingredientAudioSource.PlayOneShot(Manager.prepCompleted);
        }
        if(currentState == state.Dirty_Sliced_Raw)
        {
            ingredientModel = Instantiate (ingredient.model_Dirty_Sliced_Raw, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Dirty_Sliced_Raw;
            ingredientAudioSource.PlayOneShot(Manager.prepCompleted);
        }


        if(currentState == state.Dirty_Unsliced_Cooked)
        {
            ingredientModel = Instantiate (ingredient.model_Dirty_Unsliced_Cooked, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Dirty_Unsliced_Cooked;
        }
        if(currentState == state.Clean_Unsliced_Cooked)
        {
            ingredientModel = Instantiate (ingredient.model_Clean_Unsliced_Cooked, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Clean_Unsliced_Cooked;
        }
        if(currentState == state.Clean_Sliced_Cooked)
        {
            ingredientModel = Instantiate (ingredient.model_Clean_Sliced_Cooked, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Clean_Sliced_Cooked;
            ingredientAudioSource.PlayOneShot(Manager.prepCompleted);
        }
        if(currentState == state.Dirty_Sliced_Cooked)
        {
            ingredientModel = Instantiate (ingredient.model_Dirty_Sliced_Cooked, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Dirty_Sliced_Cooked;
            ingredientAudioSource.PlayOneShot(Manager.prepCompleted);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(slices >= 3)
        {
            isSliced = true;
            if(tutorialManager != null)
            {
                tutorialManager.ingredientCut = true;
            }
        }

        if(ingredient.isSlopBowl == false)
        {
            if(isDirty == true && isSliced == false && isCooked == false)
            {
                currentState = state.Dirty_Unsliced_Raw;
            }
            if(isDirty == false && isSliced == false && isCooked == false)
            {
                currentState = state.Clean_Unsliced_Raw;
            }
            if(isDirty == false && isSliced == true && isCooked == false)
            {
                currentState = state.Clean_Sliced_Raw;
            }
            if(isDirty == true && isSliced == true && isCooked == false)
            {
                currentState = state.Dirty_Sliced_Raw;
            }




            if(isDirty == true && isSliced == false && isCooked == true)
            {
                currentState = state.Dirty_Unsliced_Cooked;
            }
            if(isDirty == false && isSliced == false && isCooked == true)
            {
                currentState = state.Clean_Unsliced_Cooked;
            }
            if(isDirty == false && isSliced == true && isCooked == true)
            {
                currentState = state.Clean_Sliced_Cooked;
            }
            if(isDirty == true && isSliced == true && isCooked == true)
            {
                currentState = state.Dirty_Sliced_Cooked;
            }

            
        }

        if(currentState != currentModel)
        {
            instantiateCurrentModel();
        }

        if(framesOffCuttingBoard >= 2)
        {
            isOnCuttingBoard = false;
        }

        if(framesOutOfSink >= 2)
        {
            isInSink = false;
        }

        if(cleanliness >= 100f)
        {
            isDirty = false;
            if(tutorialManager != null)
            {
                tutorialManager.ingredientWashed = true;
            }
        }



        if (cookedness >= 100f)
        {
            isCooked = true;
            if(playedCookedDing == false)
            {
                ingredientAudioSource.PlayOneShot(Manager.grillingComplete);
                playedCookedDing = true;
            }
            if (tutorialManager != null)
            {
                tutorialManager.ingredientCooked = true;
            }
        }

    

    }

    void LateUpdate()
    {
        framesOffCuttingBoard += 1;
        framesOutOfSink += 1;
        framesOffGrill++;
    }

    public void setIsOnCuttingBoardTrue()
    {
        isOnCuttingBoard = true;
        framesOffCuttingBoard = 0;
    }

    public void setIsInSinkTrue()
    {
        isInSink = true;
        framesOutOfSink = 0;

        if(playedSinkSploosh == false)
        {
            ingredientAudioSource.PlayOneShot(Manager.sinkSploosh);
            playedSinkSploosh = true;
        }
    }

    public void setIsOnGrillTrue()
    {
        isOnGrill = true;
        framesOffGrill = 0;
        if(playedGrillSizzle == false)
        {
            ingredientAudioSource.PlayOneShot(Manager.grillSizzling);
            playedGrillSizzle = true;
        }
    }

    public void Wash(float cleanRate)
    {
        if(isInSink == true)
        {
            cleanliness += cleanRate * Time.deltaTime;
        }
    }

    public void Cook(float cookRate)
    {
        if(isOnGrill == true)
        {
            cookedness += cookRate * Time.deltaTime;
        }
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Utensil" && isOnCuttingBoard == true)
        {
            Debug.Log("Slice");
            slices += 1;

            if (!isSliced)
            {
                Instantiate(ingredient.cutParticleEffect, transform.position, transform.rotation);
                //print("GOOOP");

                ingredientAudioSource.PlayOneShot(Manager.cutIngredientClips[Random.Range(0, Manager.cutIngredientClips.Length)]);

                ingredientAudioSource.PlayOneShot(Manager.squishedIngredientClip);
                //Debug.Log("Playing cut sound");
            }
        }


        if(collision.gameObject.name == "DestructionCollider")
        {
            Destroy(gameObject, 2f);
        }
    
    }

    public void SetSlopA()
    {
        if(ingredient.isSlopBowl == true)
        {
            currentState = state.Clean_Unsliced_Raw;
            currentSlopState = slopStates.Slop_A_Full;
            print("SlopA!");
        }
    }

    public void SetSlopB()
    {
        if(ingredient.isSlopBowl == true)
        {
            currentState = state.Clean_Sliced_Raw;
            currentSlopState = slopStates.Slop_B_Full;
            print("SlopB!");
        }
    }

    public void SetSlopC()
    {
        if(ingredient.isSlopBowl == true)
        {
            currentState = state.Dirty_Sliced_Raw;
            currentSlopState = slopStates.Slop_C_Full;
            print("SlopC!");
        }
    }

}

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


    public enum state
    {
        Dirty_Unsliced,
        Clean_Unsliced,
        Clean_Sliced,
        Dirty_Sliced
    }

    [HideInInspector] public int statesCount = 4; //Update if more states are added

    [HideInInspector] public state currentState = state.Dirty_Unsliced;
    public state currentModel = state.Dirty_Unsliced;

    public TutorialManager tutManager;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSliced = false;
        isDirty = true;
        isCooked = false;

        if(isDirty)
        {
            cleanliness = 0f;
        }

        instantiateCurrentModel();

        ingredientAudioSource = GetComponentInChildren<AudioSource>();

        Manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        tutManager = Manager.chef.tutManager;
        
    }

    public void SetGrabbedBool()
    {
        tutManager.ingredientGrabbed = true;
    }

    void instantiateCurrentModel()
    {
        Destroy(ingredientModel);
        if(currentState == state.Dirty_Unsliced)
        {
            ingredientModel = Instantiate (ingredient.modelDirty_Unsliced, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Dirty_Unsliced;
        }
        if(currentState == state.Clean_Unsliced)
        {
            ingredientModel = Instantiate (ingredient.modelClean_Unsliced, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Clean_Unsliced;
        }
        if(currentState == state.Clean_Sliced)
        {
            ingredientModel = Instantiate (ingredient.modelClean_Sliced, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Clean_Sliced;
            ingredientAudioSource.PlayOneShot(Manager.prepCompleted);
        }
        if(currentState == state.Dirty_Sliced)
        {
            ingredientModel = Instantiate (ingredient.modelDirty_Sliced, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Dirty_Sliced;
            ingredientAudioSource.PlayOneShot(Manager.prepCompleted);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(slices >= 3)
        {
            isSliced = true;
            tutManager.ingredientCut = true;
        }

        if(ingredient.isSlopBowl == false)
        {
            if(isDirty == true && isSliced == false)
            {
                currentState = state.Dirty_Unsliced;
            }
            if(isDirty == false && isSliced == false)
            {
                currentState = state.Clean_Unsliced;
            }
            if(isDirty == false && isSliced == true)
            {
                currentState = state.Clean_Sliced;
            }
            if(isDirty == true && isSliced == true)
            {
                currentState = state.Dirty_Sliced;
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
            tutManager.ingredientWashed = true;
        }

        if (cookedness >= 100f)
        {
            isCooked = true;
            tutManager.ingredientCooked = true;
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
    }

    public void setIsOnGrillTrue()
    {
        isOnGrill = true;
        framesOffGrill = 0;
    }

    public void Wash(float cleanRate)
    {
        if(isInSink == true)
        {
            cleanliness += cleanRate * Time.deltaTime;
        }
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Knife" && isOnCuttingBoard == true)
        {
            Debug.Log("Slice");
            slices += 1;

            if (!isSliced)
            {
                Instantiate(ingredient.cutParticleEffect, transform.position, transform.rotation);
                print("GOOOP");

                ingredientAudioSource.PlayOneShot(Manager.cutIngredientClips[Random.Range(0, Manager.cutIngredientClips.Length)]);

                ingredientAudioSource.PlayOneShot(Manager.squishedIngredientClip);
                Debug.Log("Playing cut sound");
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
            currentState = state.Clean_Unsliced;
            print("SlopB!");
        }
    }

    public void SetSlopB()
    {
        if(ingredient.isSlopBowl == true)
        {
            currentState = state.Clean_Sliced;
            print("SlopB!");
        }
    }

    public void SetSlopC()
    {
        if(ingredient.isSlopBowl == true)
        {
            currentState = state.Dirty_Sliced;
            print("SlopB!");
        }
    }

}

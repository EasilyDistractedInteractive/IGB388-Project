using UnityEngine;

public class IngredientLogic : MonoBehaviour
{
    public Ingredient ingredient;
    GameObject ingredientModel;
    GameObject ingredientModelSliced;

    public bool isDirty;
    public bool isSliced;
    int slices = 0;

    public bool isOnCuttingBoard;

    public enum state
    {
        Dirty_Unsliced,
        Clean_Unsliced,
        Clean_Sliced,
        Dirty_Sliced
    }

    state currentState = state.Dirty_Unsliced;
    public state currentModel = state.Dirty_Unsliced;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSliced = false;
        isDirty = false;

        instantiateCurrentModel();

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
        }
        if(currentState == state.Dirty_Sliced)
        {
            ingredientModel = Instantiate (ingredient.modelDirty_Sliced, gameObject.transform.position , Quaternion.identity);
            ingredientModel.transform.parent = gameObject.transform;
            currentModel = state.Dirty_Sliced;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(slices >= 3)
        {
            isSliced = true;
        }

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

        if(currentState != currentModel)
        {
            instantiateCurrentModel();
        }

    }



    public void setIsOnCuttingBoardTrue()
    {
        isOnCuttingBoard = true;
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Knife" && isOnCuttingBoard == true)
        {
            Debug.Log("Slice");
            slices += 1;
        }
    }
}

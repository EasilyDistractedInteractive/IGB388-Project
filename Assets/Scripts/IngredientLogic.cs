using UnityEngine;

public class IngredientLogic : MonoBehaviour
{
    public Ingredient ingredient;
    GameObject ingredientModelBase;
    GameObject ingredientModelSliced;

    public bool isSliced;
    int slices = 0;

    public bool isOnCuttingBoard;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSliced = false;
        ingredientModelBase = Instantiate (ingredient.modelBase, gameObject.transform.position , Quaternion.identity);
        ingredientModelSliced = Instantiate (ingredient.modelSliced, gameObject.transform.position , Quaternion.identity);

        ingredientModelBase.transform.parent = gameObject.transform;
        
        ingredientModelSliced.transform.parent = gameObject.transform;

        ingredientModelBase.SetActive(true);
        ingredientModelSliced.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(slices >= 3)
        {
            isSliced = true;
        }
        if(isSliced == false)
        {
            ingredientModelBase.SetActive(true);
            ingredientModelSliced.SetActive(false);
        }

        if(isSliced == true)
        {
            ingredientModelBase.SetActive(false);
            ingredientModelSliced.SetActive(true);
        }
    }

    //void LateUpdate()
    //{
    //    isOnCuttingBoard = false;
    //}

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

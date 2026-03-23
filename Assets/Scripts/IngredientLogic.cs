using UnityEngine;

public class IngredientLogic : MonoBehaviour
{
    public Ingredient ingredient;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject ingredientModel = Instantiate (ingredient.model, gameObject.transform.position , Quaternion.identity);

        ingredientModel.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.XR.Content.Interaction;

public class OrderChecker : MonoBehaviour
{
    //List<Ingredient> submittedIngredients = new List<Ingredient>();
    IngredientLogic submittedIngredient;
    OrderHandler orderHandler;
    [SerializeField] private GameObject orderCorrectEffect;

    void Start()
    {
        orderHandler = FindAnyObjectByType<OrderHandler>();
    }

    //Currently only built to handle single ingredient orders
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            submittedIngredient = other.GetComponentInParent<IngredientLogic>();
            IngredientsCheck(submittedIngredient, other.transform.parent.gameObject);
        }
    }

    private void IngredientsCheck(IngredientLogic submittedIngredient, GameObject ingredientObject)
    {
        Destroy(ingredientObject, 1.0f);

        Order currentOrder = orderHandler.currentOrder;
        //Checks if the ingredient is of the correct type
        if (submittedIngredient.ingredientName == currentOrder.ingredient.ingredientName)
        {
            //Compares the state of the submitted ingredient with the required state
            if (submittedIngredient.currentState.ToString() == currentOrder.requiredPrepMethod.ToString())
            {
                orderHandler.OrderComplete();
                GameObject juiceEffect = Instantiate(orderCorrectEffect);

                //Destroys juice effect
                Destroy(juiceEffect, 2);
            }
        }
    }
}
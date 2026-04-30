using UnityEngine;
using System.Collections.Generic;
using System;

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
            IngredientsCheck(submittedIngredient);
            Destroy(other.gameObject);
        }
    }

    private void IngredientsCheck(IngredientLogic submittedIngredient)
    {
        Order currentOrder = orderHandler.currentOrder;
        //Checks if the ingredient is of the correct type
        if (submittedIngredient.ingredientName == currentOrder.ingredient.ingredientName)
        {
            //Compares the state of the submitted ingredient with the required state
            if (submittedIngredient.currentState.ToString() == currentOrder.requiredPrepMethod.ToString())
            {
                orderHandler.OrderComplete();
                GameObject juiceEffect = Instantiate(orderCorrectEffect);

                //Destroys the 
                Destroy(juiceEffect, 2);
                Destroy(submittedIngredient.gameObject);
            }
        }
    }
}
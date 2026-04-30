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
        Debug.Log(other.tag);
        if (other.CompareTag("Ingredient"))
        {
            submittedIngredient = other.GetComponentInParent<IngredientLogic>();
            IngredientsCheck(submittedIngredient);
            Destroy(other.gameObject);
        }
    }

    private void IngredientsCheck(IngredientLogic submittedIngredient)
    {
        Debug.Log("Checking ingredient");
        Order currentOrder = orderHandler.currentOrder;
        if (submittedIngredient.ingredientName == currentOrder.ingredient.ingredientName)
        {
            Debug.Log($"{submittedIngredient.currentState} {currentOrder.requiredPrepMethod}");
            //Compares the state of the submitted ingredient with the required state
            if (submittedIngredient.currentState.ToString() == currentOrder.requiredPrepMethod.ToString())
            {
                orderHandler.OrderComplete();
                GameObject juiceEffect = Instantiate(orderCorrectEffect);

                Destroy(juiceEffect, 2);
            }
        }
    }
}
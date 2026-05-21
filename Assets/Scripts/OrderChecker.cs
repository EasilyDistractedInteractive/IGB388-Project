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

    List<Order> validOrders;

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
            Debug.Log($"Order submitted {submittedIngredient.name}");
            IngredientsCheck(submittedIngredient, other.transform.parent.gameObject);
        }
    }

    private void IngredientsCheck(IngredientLogic submittedIngredient, GameObject ingredientObject)
    {
        Destroy(ingredientObject);

        validOrders = new List<Order>();

        //Gets all the orders with the same ingredient type as the submitted order
        foreach (Order order in orderHandler.currentOrders)
        {
            if (submittedIngredient.ingredientName == order.ingredient.ingredientName)
            {
                validOrders.Add(order);
            }
        }

        //Slop bowl Logic
        if (submittedIngredient.ingredient.isSlopBowl)
        {
            foreach (Order order in validOrders)
            {
                Debug.Log($"Submitted ingredient is {submittedIngredient.currentSlopState}, required ingredient is {order.requiredSlopState}");
                if (submittedIngredient.currentSlopState.ToString() == order.requiredSlopState.ToString())
                {
                    OrderCorrect(order);
                }
            }
        }

        else
        {
            //Compares the required state of each orders required ingredient with the state of the submitted ingredient
            foreach (Order order in validOrders)
            {
                if (submittedIngredient.currentState.ToString() == order.requiredPrepMethod.ToString())
                {
                    OrderCorrect(order);
                }
            }
        }
    }

    void OrderCorrect(Order order)
    {
        order.orderComplete = true;
        orderHandler.OrderComplete(order);

        //Spawns and destroys the added juice effect
        GameObject juiceEffect = Instantiate(orderCorrectEffect);
        Destroy(juiceEffect, 2);
    }
}
using UnityEngine;
using System.Collections.Generic;
using System;

public class OrderChecker : MonoBehaviour
{
    //List<Ingredient> submittedIngredients = new List<Ingredient>();
    Ingredient submittedIngredient;
    OrderHandler orderHandler;
    [SerializeField] private GameObject orderCorrectEffect;

    void Start()
    {
        orderHandler = FindAnyObjectByType<OrderHandler>();
    }

    //Currently only built to handle single ingredient orders, will expand after prototype submission
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Ingredient"))
        {
            submittedIngredient = other.GetComponentInParent<IngredientLogic>().ingredient;
            IngredientsCheck(submittedIngredient);
            Destroy(other.gameObject);
        }
    }

    private void IngredientsCheck(Ingredient submittedIngredient)
    {
        if (submittedIngredient.name == orderHandler.currentOrder.ingredients[0].name)
        {
            orderHandler.OrderComplete();
            GameObject juiceEffect = Instantiate(orderCorrectEffect);

            Destroy(juiceEffect, 2);
        }
    }
}
using UnityEngine;
using System.Collections.Generic;
using System;

public class OrderChecker : MonoBehaviour
{
    //List<Ingredient> submittedIngredients = new List<Ingredient>();
    Ingredient submittedIngredient;
    OrderHandler orderHandler;
    GameObject orderCorrectEffect;

    void Start()
    {
        orderHandler = FindAnyObjectByType<OrderHandler>();
    }

    //Currently only built to handle single ingredient orders, will expand after prototype submission
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ingredient")
        {
            submittedIngredient = other.transform.GetComponent<Ingredient>();
        }
    }

    private void IngredientsCheck()
    {
        if (submittedIngredient.name == orderHandler.currentOrder.ingredients[0].name)
        {
            orderHandler.OrderComplete();
            GameObject juiceEffect = Instantiate(orderCorrectEffect);
            Destroy(juiceEffect, 2);
        }
    }
}
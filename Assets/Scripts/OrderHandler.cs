using UnityEngine;
using System;
using System.Collections.Generic;

public class OrderHandler : MonoBehaviour
{
    public List<Order> orderQueue;
    public Order currentOrder;

    [Tooltip("The available ingredients")]
    public Ingredient[] ingredientPool;

    float ingredientMaxComplexity = 0;

    void Start()
    {
        //Finding the most complex individual ingredient
        foreach (Ingredient ingredient in ingredientPool)
        {
            if (ingredient.ingredientComplexity > ingredientMaxComplexity) { ingredientMaxComplexity = ingredient.ingredientComplexity; }
        }
    }

    void UpdateOrderQueue(bool popOrder)
    {

    }

    //Technically can scale infinitely but can be hardcapped if need be
    public void GenerateOrder(float orderComplexity)
    {
        float tempOrderComplexity = 0;
        //bool orderValid = false;

        int ingredientCount = Mathf.RoundToInt(orderComplexity / ingredientMaxComplexity);
        if (ingredientCount == 0) { ingredientCount = 1; }

        Order newOrder = new Order();
        newOrder.ingredients = new Ingredient[ingredientCount];

        for (int i = 0; i < ingredientCount; i++)
        {
            Ingredient tempIng = ingredientPool[UnityEngine.Random.Range(0, ingredientPool.Length)];
            tempOrderComplexity += tempIng.ingredientComplexity;
            newOrder.ingredients[i] = tempIng;
        }
        Debug.Log($"New order: Complexity range is {orderComplexity * 0.75} to {orderComplexity * 1.25}; ingredients are:");
        foreach (var ingredient in newOrder.ingredients)
        {
            Debug.Log (ingredient.name);
        }
        orderQueue.Add(newOrder);
        if (currentOrder == null) { currentOrder = newOrder; };
    }

    public void OrderComplete()
    {
        UpdateOrderQueue(currentOrder);
        //Add function to add score for completed order
    }
}
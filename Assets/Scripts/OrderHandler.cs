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
        int ingredientCount = 1;
        float tempOrderComplexity = 0;
        bool orderValid = false;

        while (orderComplexity > ingredientMaxComplexity * ingredientCount) ingredientCount++;

        Order newOrder = new Order();
        newOrder.ingredients = new Ingredient[ingredientCount];

        while (!orderValid)
        {
            for (int i = 0; i < ingredientCount; i++)
            {
                Ingredient tempIng = ingredientPool[UnityEngine.Random.Range(0, ingredientPool.Length)];
                tempOrderComplexity += tempIng.ingredientComplexity;
                newOrder.ingredients[i] = tempIng;
            }
            if ((orderComplexity * 0.75 <= tempOrderComplexity) && (tempOrderComplexity <= orderComplexity * 1.25)) { orderValid = true; break; }
        }


        orderQueue.Add(currentOrder);
    }
}
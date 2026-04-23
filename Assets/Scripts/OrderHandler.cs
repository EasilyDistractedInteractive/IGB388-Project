using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class OrderHandler : MonoBehaviour
{
    public Queue<Order> orderQueue = new Queue<Order>();
    public Order currentOrder;

    [Tooltip("The available ingredients")]
    public IngredientLogic[] ingredientPool;

    public TMP_Text textObject;

    float ingredientMaxComplexity = 0;

    public Image ingredientIcon;

    void Start()
    {
        //Finding the most complex individual ingredient
        foreach (IngredientLogic tempIngredient in ingredientPool)
        {
            if (tempIngredient.ingredient.ingredientComplexity > ingredientMaxComplexity) { ingredientMaxComplexity = tempIngredient.ingredient.ingredientComplexity; }
        }
    }

    void UpdateOrderQueue()
    {
        orderQueue.Dequeue();
        if (orderQueue.Count != 0) currentOrder = orderQueue.Peek();
    }

    //Technically can scale infinitely but can be hardcapped if need be
    public void GenerateOrder(float orderComplexity)
    {
        float tempOrderComplexity = 0;
        //bool orderValid = false;

        int ingredientCount = Mathf.RoundToInt(orderComplexity / ingredientMaxComplexity);
        if (ingredientCount == 0) { ingredientCount = 1; }

        //Order tempOrder = new Order();

        Ingredient[] tempIngredients = new Ingredient[ingredientCount];

        for (int i = 0; i < ingredientCount; i++)
        {
            IngredientLogic tempIng = ingredientPool[UnityEngine.Random.Range(0, ingredientPool.Length)];
            tempOrderComplexity += tempIng.ingredient.ingredientComplexity;
            tempIngredients[i] = tempIng.ingredient;
        }

        orderQueue.Enqueue(new Order { ingredients = tempIngredients, orderComplexity = tempOrderComplexity});
        if (orderQueue.Count == 1) { currentOrder = orderQueue.Peek(); };
        textObject.text = currentOrder.ingredients[0].ToString();
        ingredientIcon.sprite = currentOrder.ingredients[0].associatedObject.ingredientIcon;
    }

    public void OrderComplete()
    {
        UpdateOrderQueue();
        //Add function to add score for completed order
    }
}
using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class OrderHandler : MonoBehaviour
{
    public Queue<Order> orderQueue = new Queue<Order>();
    public Order currentOrder;

    [Tooltip("The available ingredients")]
    public IngredientLogic[] ingredientPool;

    float ingredientMaxComplexity = 0;

    public TMP_Text[] docketTexts;

    int orderCounter = 0;
    int currentOrderCount = 0;

    public GameObject[] dockets;

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
        currentOrderCount--;
        if (orderQueue.Count != 0) currentOrder = orderQueue.Peek();

        //Updating the queued dockets so the leftmost docket is the first remaining one to have been generated
        for (int i = 0; i < docketTexts.Length-1; i++)
        {
            docketTexts[i].text = docketTexts[i+1].text;
        }

        //If there are less current orders than there are dockets, makes the last dockets invisible
        for (int i = dockets.Length; i > currentOrderCount; i--)
        {
            dockets[i].SetActive(false);
        }
    }

    //Technically can scale infinitely but can be hardcapped if need be
    public void GenerateOrder(float orderComplexity)
    {
        float tempOrderComplexity = 0;

        int ingredientCount = Mathf.RoundToInt(orderComplexity / ingredientMaxComplexity);
        if (ingredientCount == 0) { ingredientCount = 1; }

        Ingredient[] tempIngredients = new Ingredient[ingredientCount];

        for (int i = 0; i < ingredientCount; i++)
        {
            IngredientLogic tempIng = ingredientPool[UnityEngine.Random.Range(0, ingredientPool.Length)];
            tempOrderComplexity += tempIng.ingredient.ingredientComplexity;
            tempIngredients[i] = tempIng.ingredient;
        }

        orderQueue.Enqueue(new Order { ingredients = tempIngredients, orderComplexity = tempOrderComplexity});
        orderCounter++;

        if (orderQueue.Count == 1) { currentOrder = orderQueue.Peek(); };
        if (orderQueue.Count > dockets.Length)
        {
            dockets[orderQueue.Count].SetActive(true);
            docketTexts[orderQueue.Count].text = $"Order #{orderCounter}\n {tempIngredients[0]}\n Sliced"; //Not fully built out, needs to be expanded, just temp for testing
        }
    }

    public void OrderComplete()
    {
        UpdateOrderQueue();
        //Add function to add score for completed order
    }
}
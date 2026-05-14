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

    public GameObject docketPrefab;
    public GameObject docketCanvas;

    public int[] docketPositions;
    public int docketY;
    int docketMax = 6;

    int orderCounter = 0;
    int currentOrderCount = 0;

    public List<GameObject> dockets; //public for testing

    public Chef chef;

    private void Start()
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

        GameObject tempDocket = dockets[0];
        dockets.Remove(dockets[0]);
        Destroy(tempDocket);
        
        //Updating the queued dockets so the leftmost docket is the first remaining one to have been generated
        for (int i = 0; i < dockets.Count; i++)
        {
            RectTransform rt = dockets[i].GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(docketPositions[i], docketY);
            if (dockets[i].activeSelf == false && i < docketMax) { dockets[i].SetActive(true); }
        }
    }

    //Technically can scale infinitely but can be hardcapped if need be
    public void GenerateOrder(float orderComplexity)
    {
        float tempOrderComplexity = 0;

        int ingredientCount = Mathf.RoundToInt(orderComplexity / ingredientMaxComplexity);
        if (ingredientCount == 0) { ingredientCount = 1; }

        IngredientLogic tempIng = ingredientPool[UnityEngine.Random.Range(0, ingredientPool.Length)];
        tempOrderComplexity += tempIng.ingredient.ingredientComplexity;

        int orderState = UnityEngine.Random.Range(0, tempIng.statesCount);

        orderQueue.Enqueue(new Order { ingredient = tempIng.ingredient, orderComplexity = tempOrderComplexity, requiredPrepMethod = (Order.prepMethod)orderState });
        orderCounter++;

        if (orderQueue.Count == 1) { currentOrder = orderQueue.Peek(); };
        GameObject newDocket = Instantiate(docketPrefab, docketCanvas.transform);
        dockets.Add(newDocket);
        TMP_Text docketText = newDocket.GetComponentInChildren<TMP_Text>();
        docketText.text = $"Order #{orderCounter}\n{tempIng.ingredientName}\n{currentOrder.prepMethodNames[orderState]}";

        if (dockets.Count <= docketMax)
        {
            RectTransform rt = newDocket.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(docketPositions[dockets.Count-1], docketY, 0);
            newDocket.SetActive(true);
        }
    }

    public void OrderComplete()
    {
        UpdateOrderQueue();
        //Add function to add score for completed order

        chef.incrementChefMood(5);

    }
}
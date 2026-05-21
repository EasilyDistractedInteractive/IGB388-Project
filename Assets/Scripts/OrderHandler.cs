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
    public List<Order> currentOrders = new List<Order>();

    [Tooltip("The available ingredients")]
    public IngredientLogic[] ingredientPool;

    float ingredientMaxComplexity = 0;

    public GameObject docketPrefab;

    public GameObject[] docketPositions;

    int orderCounter = 0;

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

    public Queue<Order> RemoveFromQueue(Queue<Order> myQueue, Order itemToRemove)
    {
        //Filters out the item and creates a new queue from the result
        Queue<Order> newQueue = new(myQueue.Where(x => x != itemToRemove));
        return newQueue;
    }

    void UpdateOrderQueue(Order order)
    {
        orderQueue = RemoveFromQueue(orderQueue, order);

        GameObject tempDocket = null;

        foreach (GameObject docket in dockets)
        {
            if (docket.GetComponentInChildren<Docket>().docketOrder.orderComplete)
            {
                tempDocket = docket;
                break;
            }
        }
        
        if (tempDocket != null)
        {
            dockets.Remove(dockets[0]);
            Destroy(tempDocket);
            currentOrders.Remove(order);

            //Updating the queued dockets so the leftmost docket is the first remaining one to have been generated
            for (int i = 0; i < dockets.Count; i++)
            {
                dockets[i].transform.parent = docketPositions[i].transform;
                dockets[i].transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

                if (dockets[i].activeSelf == false && i < docketPositions.Length) 
                {
                    currentOrders.Add(dockets[i].GetComponentInChildren<Docket>().docketOrder);
                    dockets[i].transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                    dockets[i].SetActive(true);
                }
            }
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

        int orderSlopState;
        int orderState;

        Order tempOrder = new Order();

        if (tempIng.ingredient.isSlopBowl)
        {
            //Generates a slop state that is not empty
            orderSlopState = UnityEngine.Random.Range(1, tempIng.slopStatesCount);
            tempOrder = new Order { ingredient = tempIng.ingredient, orderComplexity = tempOrderComplexity, requiredSlopState = (Order.SlopStates)orderSlopState };
            tempOrder.isSlopBowl = true;
            tempOrder.orderComplete = false;

            orderQueue.Enqueue(tempOrder);
            orderCounter++;

            if (orderQueue.Count <= docketPositions.Length) currentOrders.Add(tempOrder);
        }

        else
        {
            orderState = UnityEngine.Random.Range(0, tempIng.statesCount);
            tempOrder = new Order { ingredient = tempIng.ingredient, orderComplexity = tempOrderComplexity, requiredPrepMethod = (Order.PrepMethod)orderState };
            tempOrder.isSlopBowl = false;
            tempOrder.orderComplete = false;

            orderQueue.Enqueue(tempOrder);
            orderCounter++;

            if (orderQueue.Count <= docketPositions.Length) currentOrders.Add(tempOrder);
        }

        GameObject newDocket = Instantiate(docketPrefab);
        dockets.Add(newDocket);
        Docket newDocketReference = newDocket.GetComponentInChildren<Docket>();
        newDocketReference.docketOrder = tempOrder;
        newDocketReference.DocketSetup();

        TMP_Text docketText = newDocketReference.orderNumberText;
        docketText.text = $"Order #{orderCounter}";

        if (dockets.Count <= docketPositions.Length)
        {
            newDocket.transform.parent = docketPositions[dockets.Count - 1].transform;
            newDocket.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), new Quaternion(0,0,0,0));
            newDocket.SetActive(true);
        }
    }

    public void OrderComplete(Order order)
    {
        UpdateOrderQueue(order);

        //Add function to add score for completed order

        chef.incrementChefMood(5);

    }
}
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.XR.Content.Interaction;

public class OrderChecker : MonoBehaviour
{
    //List<Ingredient> submittedIngredients = new List<Ingredient>();
    IngredientLogic submittedIngredient;
    OrderHandler orderHandler;
    [SerializeField] private ParticleSystem orderCorrectEffect;

    [SerializeField] public AudioSource chefAudioSource;
    [SerializeField] public AudioClip orderCorrectAudio;
    [SerializeField] public AudioClip incorrectOrderAudio;

    [SerializeField] public TutorialManager tutManager;


    bool playedCorrectSound = false;
    bool correctOrderFound = false;

    List<Order> validOrders;

    Chef chef;

    GameManager gameManager;

    void Start()
    {
        orderHandler = FindAnyObjectByType<OrderHandler>();
        chef = orderHandler.chef;
        gameManager = FindAnyObjectByType<GameManager>();
    }

    //Currently only built to handle single ingredient orders
    private void OnTriggerEnter(Collider other)
    {
        correctOrderFound = false;
        if (other.CompareTag("Ingredient"))
        {
            if (!tutManager.tutComplete) { chef.tutManager.TutorialPhase(5, chef.tutManager.ingredientSubmitted); }

            submittedIngredient = other.GetComponentInParent<IngredientLogic>();
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
                if (submittedIngredient.currentSlopState.ToString() == order.requiredSlopState.ToString())
                {
                    OrderCorrect(order);
                    correctOrderFound = true;
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
                    correctOrderFound = true;
                }
            }
        }
        if (!correctOrderFound)
        {
            chefAudioSource.PlayOneShot(incorrectOrderAudio);
        }
    }

    void OrderCorrect(Order order)
    {
        order.orderComplete = true;
        gameManager.ordersCompleted++;
        gameManager.UpdateScore(order.orderScore); //Incrementing the player's score by the amount of points the order is worth
        orderHandler.OrderComplete(order);

        //Plays the attached juice effect
        orderCorrectEffect.Play();
        chefAudioSource.PlayOneShot(orderCorrectAudio);

        //StartCoroutine(DeactivateEffect(orderCorrectEffect, 2));
    }

    IEnumerator DeactivateEffect(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        effect.SetActive(false);
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Scriptable Objects/Order")]
public class Order : ScriptableObject
{
    [Tooltip("The ingredients in this order")]
    public Ingredient[] ingredients; //Depending on if we want to exclusively have one ingredient per order this can be changed to a single object

    [Tooltip("The time the player has to complete the order before they are reprimanded by the chef")]
    public float timeLimit;

    [Tooltip("The bonus value the order has, 0 by default")]
    public float orderBonusValue = 0;

    [Tooltip("The overall difficulty of an order, based on time limit and ingredient complexity")] //Formula needs to be tuned with playtesting
    [HideInInspector] public float orderComplexity;
}

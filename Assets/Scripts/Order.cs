using UnityEngine;

public class Order
{
    [Tooltip("The ingredients in this order")]
    public Ingredient ingredient; //Depending on if we want to exclusively have one ingredient per order this can be changed to a single object

    public enum prepMethod
    {
        Dirty_Unsliced,
        Clean_Unsliced,
        Clean_Sliced,
        Dirty_Sliced
    }

    public prepMethod requiredPrepMethod;

    public string[] prepMethodNames = { "None", "Clean", "Clean + Cut", "Cut"};

    [Tooltip("The time the player has to complete the order before they are reprimanded by the chef")]
    public float timeLimit;

    [Tooltip("The bonus value the order has, 0 by default")]
    public float orderBonusValue = 0;

    [Tooltip("The overall difficulty of an order, based on time limit and ingredient complexity")] //Formula needs to be tuned with playtesting
    [HideInInspector] public float orderComplexity;
}

using UnityEngine;

public class Order
{
    [Tooltip("The ingredients in this order")]
    public Ingredient ingredient;

    public enum PrepMethod
    {
        Clean_Sliced_Cooked,
        Clean_Sliced_Raw,
        Clean_Unsliced_Cooked,
        Clean_Unsliced_Raw,
        Dirty_Sliced_Cooked,
        Dirty_Sliced_Raw,
        Dirty_Unsliced_Cooked,
        Dirty_Unsliced_Raw
    }

    public enum SlopStates
    {
        Slop_Empty,
        Slop_A_Full,
        Slop_B_Full,
        Slop_C_Full
    }

    public bool isSlopBowl;

    public bool orderComplete;

    public GameObject attachedDocket;

    public PrepMethod requiredPrepMethod;
    public SlopStates requiredSlopState;

    [Tooltip("The time the player has to complete the order before they are reprimanded by the chef")]
    public float timeLimit;

    [Tooltip("The bonus value the order has, 0 by default")]
    public float orderBonusValue = 0;

    [Tooltip("The overall difficulty of an order, based on time limit and ingredient complexity")] //Formula needs to be tuned with playtesting
    [HideInInspector] public float orderComplexity;
}

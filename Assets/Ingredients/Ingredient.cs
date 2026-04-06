using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    [Tooltip("The base model for this ingredient")]
    public GameObject modelBase;

    [Tooltip("The sliced model for this ingredient. To be displayed after the object has been cut up")]
    public GameObject modelSliced;

    [Tooltip("The ingredient's name")]
    public string ingredientName;

    [Tooltip("How difficult the ingredient is to prepare")] //Needs to be tuned with playtesting
    public float ingredientComplexity;

    //public string ingredientDescription; //Not in use yet but could be good to add for final submission to increase alien ingredient vibes
}
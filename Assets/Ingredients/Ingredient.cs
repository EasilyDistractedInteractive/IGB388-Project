using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public bool isSlopBowl;

    public GameObject model_Clean_Sliced_Cooked;
    public GameObject model_Clean_Sliced_Raw;
    public GameObject model_Clean_Unsliced_Cooked;
    public GameObject model_Clean_Unsliced_Raw;
    public GameObject model_Dirty_Sliced_Cooked;
    public GameObject model_Dirty_Sliced_Raw;
    public GameObject model_Dirty_Unsliced_Cooked;
    public GameObject model_Dirty_Unsliced_Raw;

    

    public GameObject cutParticleEffect;

    public IngredientLogic associatedObject;

    [Tooltip("The ingredient's name")]
    public string ingredientName;

    [Tooltip("How difficult the ingredient is to prepare")] //Needs to be tuned with playtesting
    public float ingredientComplexity;

    //public string ingredientDescription; //Not in use yet but could be good to add for final submission to increase alien ingredient vibes

    
    
    
    
    
    
    
    
}
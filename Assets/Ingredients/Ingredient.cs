using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public GameObject model;
    public string ingredientName;

}

using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    public int IngredientsInTrigger;

    void Start() 
    {
        IngredientsInTrigger = 0; // Or however many cubes at start
    }

    void OnTriggerEnter(Collider col) 
    {
        // check for entering cubes
        if (col.tag == "Ingredient")
        {
            IngredientsInTrigger++;
            
        }
    }

    void OnTriggerExit(Collider col) 
    {
        // check for exiting cubes
        if (col.tag == "Ingredient") 
        {
            IngredientsInTrigger--; // Could use `--cubesInTrigger` inside the if,
                                       // but this is more readable
                

        }
    }

    void Update()
    {
        print(IngredientsInTrigger);
    }
}

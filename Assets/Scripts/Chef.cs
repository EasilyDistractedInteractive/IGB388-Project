using UnityEngine;

public class Chef : MonoBehaviour
{
    private int nextOrderTimer = 3; //Set to 3 for now for testing purposes
    [SerializeField] private int nextOrderInterval;

    private int moodCheckTimer = 10;
    [SerializeField] private int moodCheckInterval;

    private int chefMood;

    public int orderComplexity; //Public for testing, hide later

    public OrderHandler orderHandler;

    void Update()
    {
        if (Time.time > nextOrderTimer)
        {
            orderHandler.GenerateOrder(orderComplexity);
            nextOrderTimer += nextOrderInterval;
        }
        if (Time.time > moodCheckTimer)
        {
            MoodCheck();
            moodCheckTimer += moodCheckInterval;
        }
    }

    public int ChefMood
    {
        get { return chefMood; }
        set 
        { 
            chefMood = value;
            MoodCheck();
        } 
    }
    public enum Moods { Frustrated, Exasperated, Neutral, Satisfied, Happy };
    public Moods mood = Moods.Neutral;
    void MoodCheck()
    {
        switch (chefMood)
        {
            case <= 20:
                mood = Moods.Frustrated;
                break;
            case <= 40:
                mood = Moods.Exasperated;
                break;
            case <= 60:
                mood = Moods.Neutral;
                break;
            case <= 80:
                mood = Moods.Satisfied;
                break;
            case > 80:
                mood = Moods.Happy;
                break;
        }
    }

    void PrintOrder()
    {
        Order newOrder = orderHandler.currentOrder;
        for (int i = 0; i < newOrder.ingredients.Length; i++)
        {
            Debug.Log(newOrder.ingredients[i]);
        }
    }
}
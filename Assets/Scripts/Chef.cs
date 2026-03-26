using UnityEngine;

public class Chef : MonoBehaviour
{
    private int nextOrderTimer;

    private int chefMood;

    public int orderComplexity;

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
}
using System;
using UnityEngine;

public class Chef : MonoBehaviour
{
    private float nextOrderTimer; //Set to 3 for now for testing purposes
    [SerializeField] private float nextOrderInterval;

    private float moodCheckTimer;
    [SerializeField] private float moodCheckInterval;

    private int chefMood;

    public int orderComplexity; //Public for testing, hide later

    public OrderHandler orderHandler;

    void Start()
    {
        nextOrderTimer = Time.time + nextOrderInterval;
        moodCheckTimer = Time.time + moodCheckInterval;
    }

    void Update()
    {
        if (Time.time > nextOrderTimer)
        {
            orderHandler.GenerateOrder(orderComplexity);
            Instantiate(orderHandler.orderQueue.Peek().ingredients[0].associatedObject, transform.position, Quaternion.identity); //Spawns ingredients for testing, remove when integrating
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

    [SerializeField] VoiceLines[] voiceLines = new VoiceLines[4];

    [Serializable]
    struct VoiceLines
    {
        public Moods voiceLinesMood;
        public VoiceLine[] orderSubmittedVoiceLines;
        public VoiceLine[] orderLateVoiceLines;
        public VoiceLine[] orderIncorrectVoiceLines;
    }
    
    [Serializable]
    public struct VoiceLine
    {
        public string voiceLineText;
        public AudioClip voiceLineAudio;
    }

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

    public void OrderCompleteInteraction()
    {

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
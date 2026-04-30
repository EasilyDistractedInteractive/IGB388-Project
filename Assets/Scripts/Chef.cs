using System;
using TMPro;
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

    public GameObject speechBubble;
    public TMP_Text speechBubbleText;
    AudioSource chefAudioSource;

    void Start()
    {
        nextOrderTimer = Time.time + nextOrderInterval;
        moodCheckTimer = Time.time + moodCheckInterval;
        currentLinePool = voiceLines[1];
        chefAudioSource = GetComponent<AudioSource>();
    }

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
    public enum Moods { Frustrated, Neutral, Happy };
    public Moods mood = Moods.Neutral;

    [SerializeField] VoiceLines[] voiceLines;

    VoiceLines currentLinePool;

    [Serializable]
    struct VoiceLines
    {
        [SerializeField] public Moods voiceLinesMood;
        [SerializeField] public VoiceLine[] orderReceivedVoiceLines;
        [SerializeField] public VoiceLine[] orderLateVoiceLines;
        [SerializeField] public VoiceLine[] orderIncorrectVoiceLines;
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
            case <= 30:
                mood = Moods.Frustrated;
                break;
            case <= 70:
                mood = Moods.Neutral;
                break;
            case <= 100:
                mood = Moods.Happy;
                break;
        }
    }

    public void PlayVoiceline(VoiceLine[] voiceLinePool)
    {
        VoiceLine selectedLine = voiceLinePool[UnityEngine.Random.Range(0, voiceLinePool.Length)];

        speechBubble.SetActive(true);
        speechBubbleText.text = selectedLine.voiceLineText;
        chefAudioSource.PlayOneShot(selectedLine.voiceLineAudio);
    }

    public void OrderCompleteInteraction()
    {

    }
}
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Chef : MonoBehaviour
{
    private float nextOrderTimer; //Set to 3 for now for testing purposes
    [SerializeField] private float nextOrderInterval;

    private float moodCheckTimer;
    [SerializeField] private float moodCheckInterval;

    private float chefMood = 50;

    public int orderComplexity; //Public for testing, hide later

    public OrderHandler orderHandler;

    public Dialogue chefDialogue;

    [HideInInspector] public AudioSource chefAudioSource;

    public Animator chefAnim;

    public TextMeshProUGUI moodText;

    [HideInInspector] public GameManager manager;

    Timer gameTimer;

    public TutorialManager tutManager;

    public bool replay = true; //Bool to check if game is being replayed, will disable tutorial if true

    public bool gameActive;

    void Start()
    {
        nextOrderTimer = Time.time + nextOrderInterval;
        moodCheckTimer = Time.time + moodCheckInterval;
        currentLinePool = voiceLines[1];
        chefAudioSource = GetComponent<AudioSource>();
        manager = FindAnyObjectByType<GameManager>();
        gameTimer = manager.gameTimer;

        if (!replay) StartCoroutine(tutManager.Tutorial());
    }

    void Update()
    {
        //moodText.text = "Chef Mood: " + chefMood;

        if (gameActive)
        {
            if (Time.time > nextOrderTimer)
            {
                orderHandler.GenerateOrder(orderComplexity);
                incrementChefMood(-3);
                nextOrderTimer += nextOrderInterval;
            }

            if (Time.time > moodCheckTimer)
            {
                MoodCheck();
                moodCheckTimer += moodCheckInterval;
            }
        }

        if(mood == Moods.Disappointed)
        {
            chefAnim.SetInteger("Emotion", -1);
        }
        if(mood == Moods.Neutral)
        {
            chefAnim.SetInteger("Emotion", 0);
        }
        if(mood == Moods.Excited)
        {
            chefAnim.SetInteger("Emotion", 1);
        }
        //print(chefMood);
    }

    public float ChefMood
    {
        get { return chefMood; }
        set 
        { 
            chefMood = value;
            MoodCheck();
        } 
    }

    public enum Moods { Disappointed, Neutral, Excited };
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

    public void incrementChefMood(float amount )
    {
        chefMood += amount;
    }

    void MoodCheck()
    {
        switch (chefMood)
        {
            case <= 30:
                mood = Moods.Disappointed;
                break;
            case <= 65:
                mood = Moods.Neutral;
                break;
            case <= 80:
                mood = Moods.Excited;
                break;
        }
    }

    public void OrderCompleteInteraction()
    {

    }
}
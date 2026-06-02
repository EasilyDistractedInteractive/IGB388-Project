using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Chef : MonoBehaviour
{
    [HideInInspector] public float nextOrderTimer; //Set to 3 for now for testing purposes
    public float nextOrderInterval;

    [HideInInspector] public float moodCheckTimer;
    public float moodCheckInterval;

    [HideInInspector] public float chefDialogueTimer;
    public float chefDialogueInterval;

    private float chefMood = 50;

    public int orderComplexity; //Public for testing, hide later

    public OrderHandler orderHandler;

    public Dialogue chefDialogue;

    public AudioSource chefAudioSource;

    [SerializeField] public AudioClip excitedVoice;
    [SerializeField] public AudioClip neutralVoice;
    [SerializeField] public AudioClip disappointedVoice;

    public Animator chefAnim;

    [HideInInspector] public GameManager manager;

    Timer gameTimer;

    public TutorialManager tutManager;

    public bool replay; //Bool to check if game is being replayed, will disable tutorial if true

    public bool gameActive;

    public OrderChecker orderChecker;

    
    void Start()
    {
        currentLinePool = voiceLines[1].miscVoiceLines;
        //chefAudioSource = GetComponent<AudioSource>();
        manager = FindAnyObjectByType<GameManager>();
        gameTimer = manager.gameTimer;

        if (!replay)
        {
            tutManager.TutorialPhase(0, tutManager.tutStarted);
        }
    }

    void Update()
    {
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

            if (Time.time > chefDialogueTimer)
            {
                PlayDialogue();
                chefDialogueTimer += chefDialogueInterval;
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

    VoiceLine[] currentLinePool;

    [Serializable]
    struct VoiceLines
    {
        [SerializeField] public Moods voiceLinesMood;
        [SerializeField] public VoiceLine[] orderReceivedVoiceLines; //Unused as of now, same as below 2
        [SerializeField] public VoiceLine[] orderLateVoiceLines;
        [SerializeField] public VoiceLine[] orderIncorrectVoiceLines;
        [SerializeField] public VoiceLine[] miscVoiceLines;
    }
    
    [Serializable]
    public struct VoiceLine
    {
        public string voiceLineText;
        public AudioClip voiceLineAudio;
    }

    public void incrementChefMood(float amount)
    {
        chefMood += amount;
    }

    void MoodCheck()
    {
        switch (chefMood)
        {
            case <= 30:
                mood = Moods.Disappointed;
                chefAudioSource.PlayOneShot(disappointedVoice);
                currentLinePool = voiceLines[0].miscVoiceLines;
                break;

            case <= 65:
                mood = Moods.Neutral;
                chefAudioSource.PlayOneShot(neutralVoice);
                currentLinePool = voiceLines[1].miscVoiceLines;
                break;

            case <= 80:
                mood = Moods.Excited;
                chefAudioSource.PlayOneShot(excitedVoice);
                currentLinePool = voiceLines[2].miscVoiceLines;
                break;
        }
    }

    public void PlayDialogue()
    {
        int voiceLineSelection = UnityEngine.Random.Range(0, currentLinePool.Length);
        VoiceLine line = currentLinePool[voiceLineSelection];
        chefDialogue.TimedDialogue(line.voiceLineText, line.voiceLineAudio, chefAudioSource, 8);
    }
}
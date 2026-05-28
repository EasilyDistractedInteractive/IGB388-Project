using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [Tooltip("The time for the level in seconds")]
    [SerializeField] float remainingTime;

    [HideInInspector] public bool timerRunning = false;

    GameManager manager;

    public AudioSource clockAudioSource;
    public AudioClip tickTock;

    bool gameEndTimer = false;

    private void Start()
    {
        UpdateClock(remainingTime);
        manager = FindAnyObjectByType<GameManager>();
    }


    void Update()
    {
        if (remainingTime > 0 && timerRunning)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            countdownText.color = Color.red;
            manager.GameOver();
        }
        if(remainingTime <= 30 && gameEndTimer == false)
        {
            gameEndTimer = true;
            clockAudioSource.PlayOneShot(tickTock);
        }

        UpdateClock(remainingTime);
    }

    void UpdateClock(float remainingTime)
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

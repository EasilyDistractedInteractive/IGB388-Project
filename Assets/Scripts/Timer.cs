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

    private void Start()
    {
        UpdateClock(remainingTime);
        manager = FindAnyObjectByType<GameManager>();
    }


    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            countdownText.color = Color.red;
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

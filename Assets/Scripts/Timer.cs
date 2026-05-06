using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Timer : MonoBehaviour
{
    public bool playCountdown;
    public int minutes;
    public int seconds;
    public TextMeshProUGUI countdownTimer;

    void Start()
    {
        StartCoroutine(countdown());
    }


    IEnumerator countdown()
    {
        while (playCountdown == true)
        {
            if(seconds > 0)
            {
                seconds--;
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                seconds = 60;
                minutes--;
            }

            countdownTimer.text = string.Format("{00:00}:{01:00}", minutes, seconds);

            if(minutes == 0 && seconds == 0)
            {
                playCountdown = false;
            }
        }

        yield return null;
    }
}

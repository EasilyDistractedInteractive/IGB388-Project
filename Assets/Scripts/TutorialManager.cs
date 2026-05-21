using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Chef;

public class TutorialManager : MonoBehaviour
{
    int tutorialProgression = 0;
    int tutorialCompletionAmt = 5;

    [SerializeField] private Chef chef;

    [SerializeField] public Chef.VoiceLine[] tutorialVoiceLines;

    public void TutorialPhase(int voiceLineInt)
    {
        if (voiceLineInt < tutorialVoiceLines.Length)
        {
            Chef.VoiceLine voiceLine = tutorialVoiceLines[voiceLineInt];

            chef.chefDialogue.ActivateDialogue(voiceLine.voiceLineText, voiceLine.voiceLineAudio, chef.chefAudioSource);
        }

        tutorialProgression++;
        if (tutorialProgression >= tutorialCompletionAmt)
        {
            chef.chefDialogue.gameObject.SetActive(false);
            chef.manager.gameTimer.timerRunning = true;
            chef.gameActive = true;
            chef.nextOrderTimer = Time.time + chef.nextOrderInterval;
        }
    }

    private void Update()
    {
        if (tutorialProgression >= tutorialCompletionAmt)
        {
            chef.chefDialogue.gameObject.SetActive(false);
            chef.manager.gameTimer.timerRunning = true;
            chef.gameActive = true;
            chef.nextOrderTimer = Time.time + chef.nextOrderInterval;
        }
    }
}
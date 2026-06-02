using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Chef;

public class TutorialManager : MonoBehaviour
{
    public bool ingredientGrabbed = false;

    public bool ingredientCut = false;

    public bool ingredientWashed = false;

    public bool ingredientCooked = false;

    public bool ingredientSubmitted = false;

    public bool slopDispensed = false;

    public bool tutComplete = false;

    [SerializeField] private Chef chef;

    [SerializeField] public Chef.VoiceLine[] tutorialVoiceLines;

    List<int> completedSteps = new List<int>();

    public bool tutStarted = false;

    public void TutorialPhase(int voiceLineInt, bool associatedBool)
    {
        if (voiceLineInt == 4) { chef.orderChecker.gameObject.SetActive(true); }

        if (voiceLineInt == 6) { EndTutorial(); }

        if (!associatedBool && !tutComplete && !completedSteps.Contains(voiceLineInt))
        {
            completedSteps.Add(voiceLineInt);
            //Debug.Log(voiceLineInt);
            if (voiceLineInt < tutorialVoiceLines.Length)
            {
                Chef.VoiceLine voiceLine = tutorialVoiceLines[voiceLineInt];

                chef.chefDialogue.ActivateDialogue(voiceLine.voiceLineText, voiceLine.voiceLineAudio, chef.chefAudioSource);
            }
        }
    }

    public void EndTutorial()
    {
        chef.orderChecker.gameObject.SetActive(true);
        chef.chefDialogue.gameObject.SetActive(false);
        chef.manager.gameTimer.timerRunning = true;
        chef.gameActive = true;
        chef.nextOrderTimer = Time.time;
        chef.moodCheckTimer = Time.time + chef.moodCheckInterval;
        chef.chefDialogueTimer = Time.time + chef.chefDialogueInterval;
        tutComplete = true;
    }
}
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public bool ingredientGrabbed = false;

    public bool ingredientCut = false;

    public bool ingredientWashed = false;

    public bool ingredientCooked = false;

    public bool ingredientSubmitted = false;

    bool[] tutorialProgressionSteps;

    [SerializeField] private Chef chef;

    private void Start()
    {
        tutorialProgressionSteps = new bool[] { ingredientGrabbed, ingredientCut, ingredientWashed, ingredientCooked, ingredientSubmitted};
    }

    public IEnumerator Tutorial()
    {
        for (int i = 0; i < chef.tutorialVoiceLines.Length; i++)
        {
            Chef.VoiceLine voiceLine = chef.tutorialVoiceLines[i];
            bool nextStep = tutorialProgressionSteps[i];

            chef.chefDialogue.ActivateDialogue(voiceLine.voiceLineText, voiceLine.voiceLineAudio, chef.chefAudioSource);

            yield return new WaitUntil(() => nextStep);
        }

        chef.manager.gameTimer.timerRunning = true;
        chef.gameActive = true;
    }
}
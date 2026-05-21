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

    bool[] tutorialProgressionSteps;

    [SerializeField] private Chef chef;

    [SerializeField] public Chef.VoiceLine[] tutorialVoiceLines;

    private void Start()
    {
        tutorialProgressionSteps = new bool[] { ingredientGrabbed, ingredientCut, ingredientWashed, ingredientCooked, ingredientSubmitted};
    }

    public IEnumerator Tutorial()
    {
        Debug.Log("Tutorial Beginning");
        for (int i = 0; i < tutorialVoiceLines.Length; i++)
        {
            Chef.VoiceLine voiceLine = tutorialVoiceLines[i];
            bool nextStep = tutorialProgressionSteps[i];

            chef.chefDialogue.ActivateDialogue(voiceLine.voiceLineText, voiceLine.voiceLineAudio, chef.chefAudioSource);

            yield return new WaitUntil(() => tutorialProgressionSteps[i] == true);
        }

        chef.manager.gameTimer.timerRunning = true;
        chef.gameActive = true;
    }
}
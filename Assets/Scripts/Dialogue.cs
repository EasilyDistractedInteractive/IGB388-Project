using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject speechBubble;
    public TMP_Text speechBubbleText;

    public void ActivateDialogue(string dialogueText, AudioClip dialogueAudio, AudioSource dialogueAudioSource)
    {
        speechBubbleText.text = dialogueText;
        if (dialogueAudio != null && dialogueAudioSource != null)
        {
            dialogueAudioSource.PlayOneShot(dialogueAudio);
        }
    }
}
using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject speechBubble;
    public TMP_Text speechBubbleText;

    public void ActivateDialogue(string dialogueText, AudioClip dialogueAudio, AudioSource dialogueAudioSource)
    {
        this.gameObject.SetActive(true);
        speechBubbleText.text = dialogueText;
        
        if (dialogueAudio != null && dialogueAudioSource != null)
        {
            dialogueAudioSource.PlayOneShot(dialogueAudio);
        }
    }

    public void TimedDialogue(string dialogueText, AudioClip dialogueAudio, AudioSource dialogueAudioSource, float dialogueTimeFrame)
    {
        StartCoroutine(DisableAfterWait(dialogueTimeFrame));
        this.gameObject.SetActive(true);
        speechBubbleText.text = dialogueText;

        if (dialogueAudio != null && dialogueAudioSource != null)
        {
            dialogueAudioSource.PlayOneShot(dialogueAudio);
        }
    }

    public IEnumerator DisableAfterWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }
}
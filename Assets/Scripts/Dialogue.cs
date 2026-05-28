using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject speechBubble;
    public TMP_Text speechBubbleText;

    public void ActivateDialogue(string dialogueText, AudioClip dialogueAudio, AudioSource dialogueAudioSource)
    {
        //Debug.Log("Dialogue Active");
        this.gameObject.SetActive(true);
        speechBubbleText.text = dialogueText;
        //dialogueAudioSource.PlayOneShot(dialogueAudio);
        
        if (dialogueAudio != null && dialogueAudioSource != null)
        {
            dialogueAudioSource.PlayOneShot(dialogueAudio);
        }
        
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonControls : MonoBehaviour
{
    public Animator buttonAnimator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        buttonAnimator.SetBool("Pressed", true);
        StartCoroutine(ResetButton());
    }

    private IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(0.5f);
        buttonAnimator.SetBool("Pressed", false);
    }
}

using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class DialExample : MonoBehaviour
{
    public XRKnob dial;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (dial == null || animator == null) return;
        animator.speed = dial.value;    
    }
}

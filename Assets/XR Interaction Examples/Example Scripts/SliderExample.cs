using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class SliderExample : MonoBehaviour
{
    public XRSlider slider;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator == null || slider == null) return;
        animator.speed = slider.value;
    }
}

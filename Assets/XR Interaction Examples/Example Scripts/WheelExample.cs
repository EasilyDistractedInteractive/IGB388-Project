using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class WheelExample : MonoBehaviour
{
    public XRKnob wheel;

    ParticleSystem.EmissionModule module;

    private void Start()
    {
        module = GetComponent<ParticleSystem>().emission;
        
    }

    private void Update()
    {
        if (wheel == null) return;
        module.rateOverTimeMultiplier = wheel.value;
    }
}

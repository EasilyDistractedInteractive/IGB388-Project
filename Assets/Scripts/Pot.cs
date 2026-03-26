using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class Pot : MonoBehaviour
{
    [HideInInspector] public XRGrabInteractable grabbable;
    [HideInInspector] public Rigidbody rigid;
    [HideInInspector] public Vector3 velocity;
    [SerializeField] private float forceToBreak = 1.0f;
    [SerializeField] private GameObject potSmashEffect;
    private Vector3 lastPosition;
    void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        UpdateVelocity();
    }
    private void OnTriggerEnter(Collider other)
    {
        Pot collidingPot = other.gameObject.GetComponent<Pot>();
        // Check all the guard conditions.
        if (collidingPot == null) return;
        if (!grabbable.isSelected) return;
        if (!collidingPot.grabbable.isSelected) return;
        if (velocity.magnitude + collidingPot.velocity.magnitude < forceToBreak) return;

        // Trigger the Controller Haptic Feedback
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftController.SendHapticImpulse(0, 1.0f, 0.1f);
        rightController.SendHapticImpulse(0, 1.0f, 0.1f);
        // Trigger the particle effects.
        Instantiate(potSmashEffect, transform.position, Quaternion.identity);
        Instantiate(potSmashEffect, collidingPot.transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
    private void UpdateVelocity()
    {
        Vector3 currentPosition = transform.position;
        velocity = (currentPosition - lastPosition) / Time.fixedDeltaTime;
        lastPosition = currentPosition;
    }
}
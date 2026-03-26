using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class VRInputExample : MonoBehaviour
{
    // Example showing a yes/no input.
    public InputActionReference xButton;
    // Example showing a float input (0=unpressed, 1=fully pressed, otherwise partly pressed).
    public InputActionReference leftTrigger;
    // Example showing a Vector2 input (x=left right movement, y = up/down movement).
    public InputActionReference rightJoystick;

void Update()
    {
        if (xButton.action.triggered)
        {
            Debug.Log("The player pressed the X button. Do something!");
            UnityEngine.XR.InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            leftController.SendHapticImpulse(0, 0.5f, 0.1f);
        }
        float leftTriggerAmount = leftTrigger.action.ReadValue<float>();
        if (leftTriggerAmount > 0.1f)
        {
            Debug.Log("Left trigger is pressed: " + leftTriggerAmount);
        }
        Vector2 rightJoystickAmount = rightJoystick.action.ReadValue<Vector2>();
        if (rightJoystickAmount.magnitude > 0.1f)
        {
            Debug.Log("Right joystick is pressed: " + rightJoystickAmount);
        }
    }
}
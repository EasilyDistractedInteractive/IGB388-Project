using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class JoystickExample : MonoBehaviour
{
    public XRJoystick joystick;

    void Update()
    {
        if (joystick == null) return;
        transform.LookAt(transform.position + new Vector3(joystick.value.x, 0, joystick.value.y));
    }
}

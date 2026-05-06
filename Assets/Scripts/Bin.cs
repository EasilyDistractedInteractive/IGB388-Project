using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Bin : MonoBehaviour
{
    // have function that destroys objects within the collider that have interaction layers set to ingredients and bomb

    public InteractionLayerMask destroyableInteractionLayers;

}

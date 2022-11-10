using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gets the position and rotation of the headset
/// </summary>
public class FollowHMD : MonoBehaviour
{
    /// <summary>
    /// The current position/rotation of the headset
    /// </summary>
    public Transform HMD;
    public float xOffset;

    // Update is called once per frame
    /// <summary>
    /// Sets the position and rotation of the object to that of the headset, and offsets the position based on the current xOffset amount
    /// </summary>
    void Update()
    {
        transform.position = HMD.position;
        transform.rotation = HMD.rotation;

        transform.position += transform.right * xOffset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Awareness widget shows the past and future alerts in the video player timeline.
/// Moves with user's head, so it stays fixed to the bottom of the screen. This widget
/// is intended to always be in view to the user when the video is playing.
/// </summary>
public class AwarenessWidget : MonoBehaviour
{
    private IAppInfo appInfo;
    /// <summary>
    /// Variable to store the current position of the awareness widget
    /// </summary>
    [SerializeField] private Vector3 canvasPos;
    /// <summary>
    /// Variable to store the current rotation of the awareness widget
    /// </summary>
    [SerializeField] private Vector3 canvasRot;

    // Start is called before the first frame update
    void Start()
    {
        appInfo = transform.parent.GetComponentInParent<IAppInfo>();
    }

    /// <summary>
    /// If the headset is not null, gets current position/rotation and 
    /// changes position/rotation of awareness widget to match that.
    /// </summary>
    void Update()
    {
        if (appInfo.GetHead() != null && !transform.IsChildOf(appInfo.GetHead()))
        {
            transform.parent = appInfo.GetHead();
            transform.GetComponent<RectTransform>().localPosition = canvasPos;
            transform.GetComponent<RectTransform>().localEulerAngles = canvasRot;
        }
    }
}

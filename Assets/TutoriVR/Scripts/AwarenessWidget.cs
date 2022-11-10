using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Awareness widget shows the past and future alerts in the timeline.
/// Moves with user's head, so it stays fixed to the bottom of the screen
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

    // Update is called once per frame
    /// <summary>
    /// If there is a user currently, get the current position/rotation of the
    /// head and change position/rotation of the awareness widget to match that.
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

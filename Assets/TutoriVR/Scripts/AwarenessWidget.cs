using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Awareness widget shows the past and future alerts in the timeline
//Moves with user's head, so it stays fixed to the bottom of the screen
public class AwarenessWidget : MonoBehaviour
{
    //variables to store the current position and rotation fo the awareness widget
    private IAppInfo appInfo;
    [SerializeField] private Vector3 canvasPos;
    [SerializeField] private Vector3 canvasRot;

    // Start is called before the first frame update
    void Start()
    {
        appInfo = transform.parent.GetComponentInParent<IAppInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        //if there is a user currently, get the current position/rotation of the
        //head and change position/rotation of the awareness widget to match that
        if (appInfo.GetHead() != null && !transform.IsChildOf(appInfo.GetHead()))
        {
            transform.parent = appInfo.GetHead();
            transform.GetComponent<RectTransform>().localPosition = canvasPos;
            transform.GetComponent<RectTransform>().localEulerAngles = canvasRot;
        }
    }
}

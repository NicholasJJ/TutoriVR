using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evereal.VRVideoPlayer;

/// <summary>
/// Rotates bar based on where ray selected on rotate bar
/// </summary>
public class RotateBar : BarBase, IRunnable
{
    private IAppInfo appInfo;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        appInfo = transform.parent.parent.parent.GetComponentInParent<IAppInfo>();
        //Debug.Log(appInfo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    /// <summary>
    /// For the perspective thumbnail widget, sets the field of view of the game
    /// based on the requested rotation around the handstroke and updates the
    /// progress bar for how much rotation has occurred accordingly
    /// </summary>
    /// <param name="currentPoint"></param>
    

    public void Run(Vector3 currentPoint)
    {
        float currentWidth = Vector3.Distance(startPoint.position, currentPoint);
        float progress = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
        float ydegree = 360.0f * progress;
        Debug.Log(ydegree);
        target.transform.localEulerAngles = new Vector3(target.transform.localEulerAngles.x, ydegree, target.transform.localEulerAngles.z);
        SetProgress(progress);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evereal.VRVideoPlayer;

/// <summary>
/// Zoombar increases/decreases field of view of camera depending on where selected
/// </summary>
public class ZoomBar : BarBase, IRunnable
{
    private IAppInfo appInfo;
    public GameObject cam;
    // Start is called before the first frame update

    /// <summary>
    /// Set the default field of view when the application starts
    /// </summary>
    void Start()
    {
        appInfo = transform.parent.parent.parent.GetComponentInParent<IAppInfo>();
        cam.GetComponent<Camera>().fieldOfView = 45.0f;
        SetProgress(.25f);
        //Debug.Log(appInfo);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Change the field of view based on the current width and progress bar in OpenBrush
    /// </summary>
    /// <param name="currentPoint"></param>
    public void Run(Vector3 currentPoint)
    {
        float currentWidth = Vector3.Distance(startPoint.position, currentPoint);
        float progress = Mathf.Clamp(currentWidth / progressBarWidth, 0f, 1f);
        float fov = 179.0f * progress;
        if (fov < 15.0f)
        {
            fov = 15.0f;
        }
        Debug.Log(fov);
        cam.GetComponent<Camera>().fieldOfView = fov;
        SetProgress(progress);

    }
}

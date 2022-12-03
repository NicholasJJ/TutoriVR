using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evereal.VRVideoPlayer;

/// <summary>
/// Zoombar increases/decreases field of view of camera showing the 3d recorded
/// brush strokes on the perspective thumbnail.
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
    /// Change the field of view by selecting the zoom bar based on where the
    /// current point is located on it.
    /// </summary>
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

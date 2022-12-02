using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles centering of tutoriVR widgets when unused button status is held
/// after which the widgets remain in the same place.
/// </summary>
public class SetPlayerPositionRecreation : MonoBehaviour, IRunnableHold
{
    private IAppInfo appInfo;
    private bool held;

    /// <summary>
    /// Initializes with appInfo set and held set to false.
    /// </summary>
    void Start()
    {
        appInfo = GetComponentInParent<IAppInfo>();

        held = false;
        // transform.position = new Vector3 (1,0,-80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// When unused button status is held, the tutori widgets are centered based
    /// on the position of the right controller and remain in the same position
    /// starting from the next frame.
    /// </summary>
    /// <returns>yield return null in order pause execution in the current
    /// frame after centering the widgets</returns>
    IEnumerator Held()
    {
        //Debug.Log("held 1");
        while (appInfo.GetUnusedButtonStatus() == ButtonStatus.Held)
        {
            transform.parent.parent = appInfo.GetRightController();
            yield return null;
            transform.parent.parent = GameObject.Find("TutoriWidgets").transform;
        }
    }

    // IEnumerator UnHeld()
    // {
    //     while (appInfo.GetUnusedButtonStatus() != ButtonStatus.Held)
    //     {
    //         transform.parent = GameObject.Find("TutoriWidgets").transform;
    //         yield return null;
    //     }
    // }

    //public void Run(Vector3 currentpoint)
    //{
    //    held = !held;
    //    // appInfo = GetComponentInParent<IAppInfo>();
    //    // if (rController == null) rController = appInfo.GetRightController();
    //    // if (lController == null) lController = appInfo.GetLeftController();
    //    if (held)
    //    {
    //        transform.parent.parent = appInfo.GetRightController();
    //    } else
    //    {
    //        transform.parent.parent = GameObject.Find("TutoriWidgets").transform;
    //    }

    //    // yield return new WaitForSeconds(5);

    //}

    /// <summary>
    /// Starts Coroutine on held meaning that the function will be called every frame,
    /// unless execution is paused when a yield return null is executed. 
    /// </summary>
    public void RunHold(Vector3 currentpoint)
    {

        Debug.Log("rstat held");
        StartCoroutine(Held());

    }
}

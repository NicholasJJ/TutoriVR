using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Evereal.VRVideoPlayer;


// [RequireComponent(typeof(Camera))]
/// <summary>
/// 
/// </summary>
public class SetPlayerPos : MonoBehaviour, IRunnableHold
{

    // [SerializeField] RecordingEvent Event;
    // public Text txt;
    private IAppInfo appInfo;
    private bool held;
    //public GameObject videoPlayer;

    // private IAppInfo appInfo;

    // [SerializeField] VideoCapture VC;
    /// <summary>
    /// On start, held is set to false.
    /// </summary>
    void Start()
    {
        appInfo = GetComponentInParent<IAppInfo>();
 
        held = false;
        // transform.position = new Vector3 (1,0,-80);
    }

    void Update()
    {
        //Debug.Log(appInfo.GetUnusedButtonStatus());
    }


    ///*IEnumerator EnableEffect()*/
    //    {
    //        // Debug.Log("EnableEffect 1");
    //        //  txt.text="4s";
    //        transform.parent.parent =GameObject.Find("TutoriWidgets").transform;
    //        gameObject.GetComponent<Renderer>().material = stopButton;   
    //        // Debug.Log("EnableEffect 2");
    //       //RealMethod();
    //    }


    /// <summary>
    /// When unused button status is held, the video player is centered based
    /// on the position of the right controller and remains in the same position
    /// starting from the next frame.
    /// </summary>
    /// <returns>yield return null in order to pause execution in the current
    /// frame after centering the video player</returns>
    IEnumerator Held()
    {
        Debug.Log("held 1");
        while (appInfo.GetUnusedButtonStatus() == ButtonStatus.Held)
        {
            if (transform.parent.GetComponent<VRVideoPlayer>().stereoMode == StereoMode.NONE)
            {
                transform.parent.parent = appInfo.GetRightController();
                yield return null;
                transform.parent.parent = GameObject.Find("VRVideoPlayer_UI (1)").transform;
            } else if (transform.parent.GetComponent<VRVideoPlayer>().stereoMode == StereoMode.LEFT_RIGHT)
            {
                transform.parent.parent = appInfo.GetRightController();
                yield return null;
                transform.parent.parent = GameObject.Find("TutoriWidgets").transform;
            }
        }
    }

    // IEnumerator UnHeld()
    // {
    //     while (appInfo.GetUnusedButtonStatus() != ButtonStatus.Held)
    //     {
    //         transform.parent.parent = GameObject.Find("TutoriWidgets").transform;
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

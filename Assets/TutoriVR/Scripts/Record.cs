using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;


// [RequireComponent(typeof(Camera))]
/// <summary>
/// Creates a record button relative to the left controller
/// Can activate/deactivate its children, including start/stop recording buttons
/// </summary>
public class Record : MonoBehaviour, IRunnable
{
    [SerializeField] Material recordButton;
    [SerializeField] Material stopButton;
    [SerializeField] RecordingEvent Event;

    private IAppInfo appInfo;
    private bool currentstate;

    // [SerializeField] VideoCapture VC;
    // Start is called before the first frame update
    /// <summary>
    /// Creates the record button with its children active
    /// Set position and rotation based on that of the record button
    /// </summary>
    void Start()
    {
        appInfo = GetComponentInParent<IAppInfo>();
        transform.parent = appInfo.GetLeftController();
        transform.localPosition = appInfo.GetRecordButtonPosition();
        transform.localEulerAngles = appInfo.GetRecordButtonEulerAngles();
        SetChildrenActive(false);
        currentstate = false;
        gameObject.GetComponent<Renderer>().material = recordButton;   
    }

    /// <summary>
    /// Set the record button's parent as the left controller 
    /// Update position and rotation based on record button position/rotation
    /// </summary>
    void Update()
    {
        if (transform.parent == null)
        {
            transform.parent = appInfo.GetLeftController();
            transform.localPosition = appInfo.GetRecordButtonPosition();
            transform.localEulerAngles = appInfo.GetRecordButtonEulerAngles();
        }
        Debug.LogError(transform.parent);
    }
    
    /// <summary>
    /// Makes all the children active/inactive
    /// </summary>
    /// <param name="setting">Setting for whether the children should be active or not</param>
    private void SetChildrenActive(bool setting)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(setting);
        }
    }

    /// <summary>
    /// Display appropriate options for starting or stopping the recording based on the current settings
    /// </summary>
    /// <param name="currentpoint"></param>
    public void Run(Vector3 currentpoint)
    {
        SetChildrenActive(!currentstate);
        currentstate = !currentstate;
        if (currentstate==false)
        {
         
        gameObject.GetComponent<Renderer>().material = recordButton;   
        }
        else
        {gameObject.GetComponent<Renderer>().material = stopButton;   

        }
        // Event.Raise();
        // if (!Event.isRecording())
        // {
        //     VideoCaptureCtrl.instance.StopCapture();
        //     gameObject.GetComponent<Renderer>().material = recordButton;
        //     Debug.Log("Save & Export");
        // }
        // else
        // {
        //     RecordingEventListener.recordID = Time.time.ToString();
        //     PathConfig.SaveFolder = RecordingEventListener.ExportPath();
        //     VC.customPathFolder = RecordingEventListener.ExportPath();
        //     VideoCaptureCtrl.instance.StartCapture();
        //     // VideoPlayer.instance.SetRootFolder();
        //     // VideoPlayer.instance.NextVideo();
        //     // VideoPlayer.instance.PlayVideo();
        //     gameObject.GetComponent<Renderer>().material = stopButton;
        //     Debug.Log("Start Recording");
        // }
    }
}

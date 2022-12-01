using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// All implementations of RecordingEventListener will contain a list of listeners
/// that will record information during a recording that needs to be specified in
/// the implementation (not in this class). After the recording is over, all of the
/// information recording will be saved as JSON files to the same folder in /CaptureAt + RecordID
/// in order to differentiate between different recordings. RecordID is specified before
/// by the user before the recording begins.
/// </summary>
public abstract class RecordingEventListener : MonoBehaviour
{
    public RecordingEvent Event;
    public IAppInfo appInfo;
    public static string recordID;

    /// <summary>
    /// Writes the information in argument "json" to the file in the /CaptureAt +
    /// RecordID folder
    /// </summary>
    /// <param name="name"></param>
    /// <param name="json"></param>
    public static void ExportJson(string name, string json) 
    {
        string d = Application.persistentDataPath + "/CaptureAt" + recordID;
        if (!Directory.Exists(d)) Directory.CreateDirectory(d);
        string p = d + "/" + name + ".json";
        Debug.Log(p);
        //if (!File.Exists(p)) File.Create(p);
        StreamWriter writer = new StreamWriter(p);
        writer.Write(json);
        writer.Close();
    }

    /// <summary>
    /// Creates /CaptureAt folder based on the set recordID if doesn't exist
    /// </summary>
    /// <returns></returns>
    public static string ExportPath() 
    { 
        string d = Application.persistentDataPath + "/CaptureAt" + recordID + "/";
        if (!Directory.Exists(d)) Directory.CreateDirectory(d);
        return d;
    }

    /// <summary>
    /// Adds self to the list of listeners in Recording Event
    /// </summary>
    private void OnEnable()
    {
        appInfo = GameObject.Find("TutoriWidgets").GetComponent<IAppInfo>();
        Event.RegisterListener(this);
    }

    /// <summary>
    /// Removes self from the list of listeners in Recording Event
    /// </summary>
    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnRecordRaised()
    {
        Debug.Log("Record Raised");
        if (Event.isRecording())
        {
            StartRecording();
        } else
        {
            EndRecording();
        }
    }

    private void Update()
    {
        //Debug.Log(Event.isRecording());
        if (Event.isRecording())
        {
            DuringRecord();
        }
    }

    public abstract void StartRecording();

    public abstract void DuringRecord();

    public abstract void EndRecording();
}

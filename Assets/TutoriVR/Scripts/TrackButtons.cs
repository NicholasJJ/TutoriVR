using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// List of button information to be added during the recording. Must
/// be serializable in order to convert this class and the ButtonInstances
/// stored in the list to JSON properly.
/// </summary>
[Serializable]
public class buttonLedger
{
    public List<ButtonInstance> ledger;
    public buttonLedger()
    {
        ledger = new List<ButtonInstance>();
    }
}

/// <summary>
/// Will record controller + scene center information during every frame and save
/// it to the folder specified by the RecordingEventListener class. The JSON
/// file containing the controller + scene center information will be called "inputs".
/// </summary>
public class TrackButtons : RecordingEventListener
{
    private buttonLedger captures; //dictionary: frame to button instance class
    private ButtonInstance binstance;
    private float recordingStartTime;
    [SerializeField] GameObject tracker;

    public void fStart()
    {
        //captures = new Dictionary<int, ButtonInstance>();
    }

    public void fUpdate()
    {
        //Debug.Log("HERE!!!");
       // binstance = new ButtonInstance();
        //captures[Time.frameCount] = binstance.createInstance();
        //Debug.Log(Time.frameCount + " " + captures[Time.frameCount].brushtype);
    }

    /// <summary>
    /// Creates ledger for recording controller + scene information and sets the time
    /// to current time.
    /// </summary>
    public override void StartRecording()
    {
        captures = new buttonLedger();
        recordingStartTime = Time.time;
    }

    /// <summary>
    /// Creates a copy of the information regarding controllers and scene.
    /// Adds copy to the ledger + time stamp (time since recording began)
    /// </summary>
    public override void DuringRecord()
    {
        binstance = new ButtonInstance();
        binstance.createInstance(appInfo, Time.time - recordingStartTime,tracker);
        captures.ledger.Add(binstance);
        //Debug.Log(JsonUtility.ToJson(binstance));
    }

    /// <summary>
    /// Converts all stored information to JSON and writes it into the file "inputs"
    /// in the /CaptureAt + RecordID folder that was specified by RecordingEventListener
    /// </summary>
    public override void EndRecording()
    {
        string buttonJSON = JsonUtility.ToJson(captures);
        Debug.Log(buttonJSON);
        ExportJson("inputs", buttonJSON);
    }
}

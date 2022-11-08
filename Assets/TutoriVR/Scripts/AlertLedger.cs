using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

//Adding alerts with time and color into the time progress bar, and convert to JSON

//Initialize the time and color for that  alert
[Serializable]
public class Alert
{
    public float time;
    public ColoredAlert color;
    public Alert(float t, ColoredAlert c)
    {
        this.time = t;
        this.color = c;
    }
}

//Initializes a list containing all alerts for that video and stores the total time of the video
[Serializable]
public class ListLedger
{
    public List<Alert> alertList;
    public float totalTime;
    public ListLedger()
    {
        alertList = new List<Alert>();
    }
}

/* AlertLedger class with options for recording alert times, 
starting the video recording again, and converting the video to JSON upon completion */
[CreateAssetMenu]
public class AlertLedger : ScriptableObject
{
    private float recordingStartTime;
    //private List<(float, ColoredAlert)> recordedAlerts;
    [SerializeField] RecordingEvent Event;
    public ListLedger recordedAlerts = new ListLedger();
    public void Restart()
    {
        recordedAlerts = new ListLedger();
        recordingStartTime = Time.time;
    }

    //Creates a new Alert at the current time with a specified color if the recording is currently happening
    public void RecordNewAlert(ColoredAlert color)
    {
        if (Event.isRecording())
        {
            recordedAlerts.alertList.Add(new Alert(Time.time - recordingStartTime, color));
        }
    }

    //returns the alerts and total time of the video as a JSON
    public string toJSON()
    {
        Debug.Log("recorded alerts count is " + recordedAlerts.alertList.Count);
        for (int i = 0; i < recordedAlerts.alertList.Count; i++)
        {
            Debug.Log(recordedAlerts.alertList[i]);
        }
        recordedAlerts.totalTime = Time.time - recordingStartTime;
        return JsonUtility.ToJson(recordedAlerts);
    }
}

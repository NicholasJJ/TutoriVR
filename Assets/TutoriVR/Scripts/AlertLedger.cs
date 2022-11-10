﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

//Adding alerts with time and color into the time progress bar, and convert to JSON

/// <summary>
/// Alerts are specific points on the video progress bar that the tutor wants to highlight.
/// They contain two attributes, the time that they occur at and the color that they will show up as on the timeline.
/// </summary>
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

/// <summary>
/// Initializes a list containing all alerts for that video and stores the total time of the video
/// </summary>
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

/// <summary>
/// AlertLedger class contains options for recording alert times, starting the video recording again, and converting the video to JSON upon completion.
/// </summary>
[CreateAssetMenu]
public class AlertLedger : ScriptableObject
{
    private float recordingStartTime;
    //private List<(float, ColoredAlert)> recordedAlerts;
    [SerializeField] RecordingEvent Event;
    public ListLedger recordedAlerts = new ListLedger();

    /// <summary>
    /// Restarts the recording
    /// </summary>
    public void Restart()
    {
        recordedAlerts = new ListLedger();
        recordingStartTime = Time.time;
    }

    /// <summary>
    /// Creates a new Alert at the current time with a specified color if the recording is currently happening
    /// </summary>
    /// <param name="color"></param>
    public void RecordNewAlert(ColoredAlert color)
    {
        if (Event.isRecording())
        {
            recordedAlerts.alertList.Add(new Alert(Time.time - recordingStartTime, color));
        }
    }

    /// <summary>
    /// Gets each of the alert times and the total time of the video
    /// </summary>
    /// <returns> Returns the alerts and total time of the video as a JSON </returns>
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Recording Event stores RecordingEventListeners that can all be raised. Able to add and
/// remove listeners from the list.
/// </summary>
[CreateAssetMenu]
public class RecordingEvent : ScriptableObject
{
    private List<RecordingEventListener> listeners = new List<RecordingEventListener>();
    private bool recording = false;

    /// <summary>
    /// sets recording to false
    /// </summary>
    private void OnEnable()
    {
        recording = false;
    }

    /// <summary>
    /// Calls onRecordRaised for all of the listeners added to the list "listeners".
    /// </summary>
    public void Raise()
    {
        recording = !recording;
        foreach (RecordingEventListener listener in listeners)
        {
            listener.OnRecordRaised();
        }
    }

    public void RegisterListener(RecordingEventListener l)
    {
        listeners.Add(l);
    }

    public void UnregisterListener(RecordingEventListener l)
    {
        listeners.Remove(l);
    }

    public bool isRecording() => recording;

    
}

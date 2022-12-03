using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores the different colors of recording bookmark alerts
/// </summary>
public enum ColoredAlert
{
    Red,
    Blue,
    Yellow
}

/// <summary>
/// Records alert when run in AlertLedger
/// </summary>
public class SendRecordAlert : MonoBehaviour, IRunnable
{
    [SerializeField] AlertLedger recorder;
    [SerializeField] ColoredAlert alertColor;

    /// <summary>
    /// Records the alert with the chosen color of the alert
    /// </summary>
    public void Run(Vector3 currentpoint)
    {
        recorder.RecordNewAlert(alertColor);
    }

}

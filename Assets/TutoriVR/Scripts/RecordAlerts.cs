using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Handles bookmark recording and export
/// </summary>
public class RecordAlerts : RecordingEventListener
{
    [SerializeField] private AlertLedger ledger;

    private void Start()
    {
        SetChildrenActive(false);
    }

    /// <summary>
    /// AlertLedger gets rid of all bookmarks and sets its children to active
    /// </summary>
    public override void StartRecording()
    {
        ledger.Restart();
        SetChildrenActive(true);
    }

    public override void DuringRecord()
    {
        
    }

    /// <summary>
    /// Exports all the bookmarks to json and makes all of them inactive
    /// </summary>
    public override void EndRecording()
    {
        string alertJSON = ledger.toJSON();
        Debug.Log(alertJSON);
        SetChildrenActive(false);
        ExportJson("alerts", alertJSON);
    }
    
    /// <summary>
    /// Makes all the bookmarks active/inactive depending on the setting
    /// </summary>
    /// <param name="setting"></param>
    private void SetChildrenActive(bool setting)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(setting);
        }
    }
}

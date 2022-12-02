﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Toggles the perspective widget active state
/// Perspective widget is used to understand stroke structure and controller interactions
/// The created stroke can be rotated 360 degrees around the stroke and zoomed in/out of
/// </summary>
public class ReconstructionButton : MonoBehaviour, IRunnable
{
    public GameObject perspective_widget;
    private bool currentstate;
    // Start is called before the first frame update

    /// <summary>
    /// Initializes the perspective widget to be off
    /// </summary>
    void Start()
    {

        currentstate = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// Toggles whether the perspective widget is on or off based on its current state
    /// </summary>
    /// <param name="currentPoint"></param>
    public void Run(Vector3 currentPoint)
    {
        currentstate = !currentstate;
        if (currentstate == false)
        {
            perspective_widget.SetActive(false);
        }
        else
        {
            perspective_widget.SetActive(true);

        }
    }
}

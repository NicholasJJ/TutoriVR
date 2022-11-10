using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maintains variable for whether awareness widget is active or not
/// </summary>
public class AlertsButton : MonoBehaviour, IRunnable
{
    //store the awareness widget and whether it is active
    public GameObject awareness_widget;
    private bool currentstate;

    // Start is called before the first frame update
    void Start()
    {
        currentstate = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Toggles whether the awareness widget is on or off
    /// </summary>
    /// <param name="currentPoint"></param>
    public void Run(Vector3 currentPoint)
    {
        currentstate = !currentstate;
        if (currentstate == false)
        {
            awareness_widget.SetActive(false);
        }
        else
        {
            awareness_widget.SetActive(true);

        }
    }

   }

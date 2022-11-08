using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Maintains variable for whether awareness widget is active or not
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


    //Toggle the active state for the awareness widget
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

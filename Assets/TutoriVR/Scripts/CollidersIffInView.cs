using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enables the collider component if the object is in view based on the current point of view
/// </summary>
public class CollidersIffInView : MonoBehaviour
{
    private IAppInfo appInfo;
    Transform h;
    /// <summary>
    /// Gets the information about the application before the first frame starts
    /// </summary>
    void Start()
    {
        appInfo = GetComponentInParent<IAppInfo>();
    }

    /// <summary>
    /// Find the collider and enable it if in view
    /// </summary>
    void Update()
    {
        //if the collider already exists and it is in view, enable it
        if (h != null)
        {
            Vector3 headToSelf = transform.position - h.position;
            float angle = Vector3.Angle(headToSelf, transform.forward) % 360;
            //Debug.Log(angle);
            SetCollidersEnabled(angle < 90 || angle > 270);
        } 
        //otherwise find the collider (TutoriWidgets) and get the information about the head
        else
        {
            appInfo = GameObject.Find("TutoriWidgets").GetComponent<IAppInfo>();
            Debug.Log("appinfo = " + appInfo);
            h = appInfo.GetHead();
        }
    }

    /// <summary>
    /// Changes whether the object is enabled based on whether it is in view
    /// </summary>
    /// <param name="setting">Variable indicating whether the object should be enabled for collision or not</param>
    void SetCollidersEnabled(bool setting)
    {
        if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = setting;
        //foreach (Collider c in GetComponentsInChildren<Collider>())
        //{
        //    c.enabled = setting;
        //}
    }
}

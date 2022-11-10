using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enables the collider component if angle from head means object in view
public class CollidersIffInView : MonoBehaviour
{
    private IAppInfo appInfo;
    Transform h;
    // Start is called before the first frame update
    void Start()
    {
        appInfo = GetComponentInParent<IAppInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the collider already exists enable it if in view
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

    //change whether it is enabled based on if the collider is in view
    void SetCollidersEnabled(bool setting)
    {
        if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = setting;
        //foreach (Collider c in GetComponentsInChildren<Collider>())
        //{
        //    c.enabled = setting;
        //}
    }
}

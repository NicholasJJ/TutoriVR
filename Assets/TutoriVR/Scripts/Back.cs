using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour, IRunnable
{
    private IAppInfo appInfo;
    private bool currentstate;

    // Start is called before the first frame update
    void Start()
    {
            // appInfo = GetComponentInParent<IAppInfo>();
            // transform.parent = appInfo.GetLeftController();
            // transform.localPosition = appInfo.GetRecordButtonPosition();
            // transform.localEulerAngles = appInfo.GetRecordButtonEulerAngles();
            // currentstate = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Run(Vector3 currentpoint)
    {
        Debug.Log("Pressed Back");
        transform.parent.gameObject.GetComponent<Renderer>().enabled = true;

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).gameObject.SetActive(false);
        }
    }
}

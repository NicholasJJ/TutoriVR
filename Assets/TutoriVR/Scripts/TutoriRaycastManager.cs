using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonStatus
{
    Down,
    Held,
    Up,
    None
}

/// <summary>
/// Deals with the interaction of the controller ray with the world's gameobjects. Can
/// run and change the color of gameobjects.
/// </summary>
public class TutoriRaycastManager : MonoBehaviour
{
    [SerializeField] IAppInfo appInfo;
    [SerializeField] LineRenderer lLine;
    [SerializeField] LineRenderer rLine;
    private Transform rController;
    private Transform lController;
    private ButtonStatus rStat;
    private ButtonStatus lStat;

    private Dictionary<GameObject, Color> regColor = new Dictionary<GameObject, Color>();
    // Start is called before the first frame update
    void Start()
    {
        appInfo = GetComponent<IAppInfo>();
        //rController = appInfo.GetRightController().transform;
        //lController = appInfo.GetLeftController().transform;
    }

    /// <summary>
    /// Updates class variables to contain reference the transform of the controllers and calls checkRay.
    /// Also updates the color of objects that were selected and their colors stored in regColor. Removes
    /// objects from regColor that are not selected.
    /// </summary>
    void Update()
    {
        if (rController == null) rController = appInfo.GetRightController().transform;
        if (lController == null) lController = appInfo.GetLeftController().transform;
        rStat = appInfo.GetUnusedButtonStatus();
        //lStat = appInfo.GetLeftTriggerStatus();
        GameObject o1 = checkRay(rController, rStat, rLine);
        //GameObject o2 = checkRay(lController, lStat, lLine);

        List<GameObject> toBeRemoved = new List<GameObject>();
        foreach (GameObject o in regColor.Keys)
        {
            //if (!o.Equals(o1) && !o.Equals(o2))
            //{
            //    o.GetComponent<Renderer>().material.color = regColor[o];
            //    toBeRemoved.Add(o);
            //}
            if (!o.Equals(o1))
            {
                o.GetComponent<Renderer>().material.color = regColor[o];
                toBeRemoved.Add(o);
            }
        }
        foreach (GameObject o in toBeRemoved)
        {
            regColor.Remove(o);
        }
    }

    /// <summary>
    /// Finds object that is being hit by controller ray. If the object can be run, the object's new color is
    /// made slightly dimmer (even more so if controller is selecting that object) and saved in regColor. 
    /// Calls the object's run function if button status is held/up.
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="status"></param>
    /// <param name="line"></param>
    /// <returns>Gameobject that is being hit by the ray (can be null)</returns>
    GameObject checkRay(Transform controller, ButtonStatus status, LineRenderer line)
    {
        RaycastHit hit;
        Vector3 dir = controller.rotation * appInfo.GetRaycastForwardRotation();
        Debug.DrawRay(controller.position, dir, Color.red);
        if (Physics.Raycast(controller.position, dir, out hit))
        {
            if (hit.collider.gameObject.GetComponent<IRunnable>() != null || hit.collider.gameObject.GetComponent<IRunnableHold>() != null)
            {
                line.SetPosition(0, controller.position);
                line.SetPosition(1, hit.point);
                line.enabled = true;
                GameObject obj = hit.collider.gameObject;
                if (obj.GetComponent<Renderer>() != null)
                {
                    Color c = obj.GetComponent<Renderer>().material.color;
                    if (!regColor.ContainsKey(obj))
                    {
                        regColor.Add(obj, c);
                    }
                    Color d = new Color(Mathf.Max(0, regColor[obj].r - .2f), Mathf.Max(0, regColor[obj].g - .2f), Mathf.Max(0, regColor[obj].b - .2f), regColor[obj].a);
                    obj.GetComponent<Renderer>().material.color = d;
                    if (status == ButtonStatus.Held)
                    {
                        d = new Color(Mathf.Max(0, regColor[obj].r - .4f), Mathf.Max(0, regColor[obj].g - .4f), Mathf.Max(0, regColor[obj].b - .4f), regColor[obj].a);
                        obj.GetComponent<Renderer>().material.color = d;
                    }
                }

                if (status == ButtonStatus.Held)
                {
                    Debug.Log(obj.name + "  " + obj.GetComponent<IRunnable>());
                    obj.GetComponent<IRunnableHold>().RunHold(hit.point);
                }
                else if (status == ButtonStatus.Up)
                {
                    Debug.Log(obj.name + "  " + obj.GetComponent<IRunnable>());
                    obj.GetComponent<IRunnable>().Run(hit.point);
                }
                return obj;
            }
        }
        else
        {
            line.enabled = false;
        }
        return null;
    }
}

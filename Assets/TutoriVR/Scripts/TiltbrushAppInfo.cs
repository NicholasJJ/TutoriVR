using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Valve.VR;
using System;
using UnityEngine.XR;

/// <summary>
/// AdditionalInfo will store information regarding the brush tool/type/color.
/// Needs to be serializable as it will be converted to JSON.
/// </summary>
[Serializable]
public struct additionalInfo
{
    public TiltBrush.BaseTool.ToolType tool;
    public TiltBrush.BrushDescriptor brushtype;
    public Color brushcolor;
}

/// <summary>
/// TiltbrushAppInfo stores the pointers to the controllers and the scene and updates
/// them if they are somehow lost. Also updates the status of the controllers based on
/// input detected.
/// </summary>
public class TiltbrushAppInfo : MonoBehaviour, IAppInfo
{
    private Transform leftController;
    private Transform rightController;
    private Transform sceneTransform;
    [SerializeField] private Transform head;
    // private RaycastHit rightHit; 
    private ButtonStatus rightTriggerStatus;
    private ButtonStatus leftTriggerStatus;
    private ButtonStatus unusedButtonStatus;
    // public SteamVR_Action_Single triggerAction;
    // public SteamVR_Action_Boolean uAction;
    [SerializeField] private Vector3 recButtonPos;
    [SerializeField] private Vector3 recButtonRot;
    // [SerializeField] private TiltBrush.VrSdk vrSdk; 

    [SerializeField] private Vector3 raycastForwardRotation; 

    /// <summary>
    /// Sets sceneTransform to be equal to the transform of the gameobject "SceneParent"
    /// </summary>
    void Start()
    {
        // TiltBrush.SketchControlsScript.m_Instance.
        // leftController = GameObject.Find("Controller (wand)").transform;
        // rightController = GameObject.Find("Controller (brush)").transform;
        sceneTransform = GameObject.Find("SceneParent").transform;
    }

    /// <summary>
    /// Every frame, updates the pointers to controllers if they are null and the parent of their transform is not 
    /// UnityXRUnitializedControls(Clone) AND updates the status of the triggers and buttons based on the input
    /// from the controllers.
    /// </summary>
    void Update()
    {
        Debug.Log("lft controller: " + leftController);
        if (rightController == null &&
            !GameObject.Find("Controller (brush)").transform.parent.Equals(null) &&
            !GameObject.Find("Controller (brush)").transform.parent.name.Equals("UnityXRUninitializedControls(Clone)"))
        {
            rightController = GameObject.Find("Controller (brush)").transform;
            Debug.Log("parent is: " + rightController.parent.name);

        }
        if (leftController == null &&
            !GameObject.Find("Controller (brush)").transform.parent.Equals(null) &&
            !GameObject.Find("Controller (brush)").transform.parent.name.Equals("UnityXRUninitializedControls(Clone)"))
        {
            leftController = GameObject.Find("Controller (wand)").transform;
            Debug.Log("parent is: "  + leftController.parent.name);
        }
        if (head == null && GameObject.Find("(RenderWrapper Camera)") != null) head = GameObject.Find("(RenderWrapper Camera)").transform;
        Debug.DrawRay(rightController.position, rightController.forward, Color.red);
        // bool rtVal = triggerAction.GetAxis(SteamVR_Input_Sources.RightHand) > 0.99f;
        // bool ltVal = triggerAction.GetAxis(SteamVR_Input_Sources.LeftHand) > 0.99f;
        // bool uval = uAction.GetState(SteamVR_Input_Sources.RightHand);
        bool ltVal = getControllerWithName(TiltBrush.InputManager.ControllerName.Wand).GetVrInput(TiltBrush.VrInput.Trigger);
        bool rtVal = getControllerWithName(TiltBrush.InputManager.ControllerName.Brush).GetVrInput(TiltBrush.VrInput.Trigger);
        bool uVal = getControllerWithName(TiltBrush.InputManager.ControllerName.Brush).GetVrInput(TiltBrush.VrInput.Button01);
        rightTriggerStatus = UpdatedButtonStatus(rightTriggerStatus, rtVal);
        leftTriggerStatus = UpdatedButtonStatus(leftTriggerStatus, ltVal);
        unusedButtonStatus = UpdatedButtonStatus(unusedButtonStatus, uVal);
        //Debug.Log(uval);
        //Debug.Log(unusedButtonStatus);
    }

    /// <summary>
    /// Finds controller with the specified name. Returns null if the name does not exist 
    /// in TiltBrush Input manager controllers.
    /// </summary>
    private TiltBrush.ControllerInfo getControllerWithName(TiltBrush.InputManager.ControllerName name) {
        foreach (var c in TiltBrush.InputManager.Controllers) {
            if (c.Behavior.ControllerName.Equals(name)) return c;
        }
        return null;
    }

    /// <summary>
    /// Determines what the new status of the controller is depending on its current state
    /// and whether or not the trigger/button was just pressed.
    /// </summary>
    /// <returns>New status of the controller</returns>
    private ButtonStatus UpdatedButtonStatus(ButtonStatus current, bool isPressed)
    {
        if (isPressed)
        {
            if (current == ButtonStatus.None) 
                return ButtonStatus.Down;
            return ButtonStatus.Held;
        }
        else
        {
            if (current == ButtonStatus.Held || current == ButtonStatus.Down)
            {
                return ButtonStatus.Up;
            } 
            else
            {
                return ButtonStatus.None;
            }
        }
    }

    public Transform GetLeftController() => leftController;
    public Transform GetRightController() => rightController;

    public Transform GetSceneRootTransform() => sceneTransform;

    public ButtonStatus GetRightTriggerStatus() => rightTriggerStatus;
    public ButtonStatus GetLeftTriggerStatus() => leftTriggerStatus;

    public Transform GetHead() => head;

    public Vector3 GetRecordButtonPosition() => recButtonPos;

    public Vector3 GetRecordButtonEulerAngles() => recButtonRot;

    public ButtonStatus GetUnusedButtonStatus() => unusedButtonStatus;

    /// <summary>
    /// Creates instance of the additional info struct and fills it with information regarding
    /// the brush tool, its color, and its type. Converts instance into JSON.
    /// </summary>
    /// <returns>JSON of additional info regarding the brush</returns>
    public string GetSerializedAdditionalInfo()
    {
        additionalInfo a = new additionalInfo();
        a.brushtype = null;
        if (TiltBrush.BrushController.m_Instance != null)
        {
            a.brushtype = TiltBrush.BrushController.m_Instance.ActiveBrush;
        }
        a.tool = GameObject.Find("SketchSurface").GetComponent<TiltBrush.SketchSurfacePanel>().ActiveToolType;
        // a.brushcolor = TiltBrush.ColorController.trackColor;
        a.brushcolor = TiltBrush.App.BrushColor.CurrentColor;
        return JsonUtility.ToJson(a);
    }

    /// <returns>Current color of the BrushController</returns>
    public Color GetColor()
    {
        return GameObject.Find("App").GetComponent<TiltBrush.BrushColorController>().CurrentColor;
    }

    public Vector3 GetRaycastForwardRotation() => raycastForwardRotation;
}

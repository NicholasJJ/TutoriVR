﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Valve.VR;
using System;
using UnityEngine.XR;

[Serializable]
public struct additionalInfo
{
    public TiltBrush.BaseTool.ToolType tool;
    public TiltBrush.BrushDescriptor brushtype;
    public Color brushcolor;
}

public class TiltbrushAppInfo : MonoBehaviour, IAppInfo
{
    private Transform leftController;
    private Transform rightController;
    private Transform sceneTransform;
    [SerializeField] private Transform head;
    private RaycastHit rightHit;
    private ButtonStatus rightTriggerStatus;
    private ButtonStatus leftTriggerStatus;
    private ButtonStatus unusedButtonStatus;
    // public SteamVR_Action_Single triggerAction;
    // public SteamVR_Action_Boolean uAction;
    [SerializeField] private Vector3 recButtonPos;
    [SerializeField] private Vector3 recButtonRot;
    [SerializeField] private TiltBrush.VrSdk vrSdk;
    // Start is called before the first frame update
    void Start()
    {
        // TiltBrush.SketchControlsScript.m_Instance.
        leftController = GameObject.Find("Controller (wand)").transform;
        rightController = GameObject.Find("Controller (brush)").transform;
        sceneTransform = GameObject.Find("SceneParent").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightController == null) rightController = GameObject.Find("Controller (brush)").transform;
        if (leftController == null) leftController = GameObject.Find("Controller (wand)").transform;
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

    private TiltBrush.ControllerInfo getControllerWithName(TiltBrush.InputManager.ControllerName name) {
        foreach (var c in TiltBrush.InputManager.Controllers) {
            if (c.Behavior.ControllerName.Equals(name)) return c;
        }
        return null;
    }

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

    public Color GetColor()
    {
        return GameObject.Find("App").GetComponent<TiltBrush.BrushColorController>().CurrentColor;
    }
}

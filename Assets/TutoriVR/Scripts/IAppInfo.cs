using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Includes functions for getting status of the headset, triggers, and the controllers.
/// Also has functions for position and rotation of the record button, and the color of the drawing tools' lines.
/// </summary>
public interface IAppInfo
{
    Transform GetLeftController();

    Transform GetRightController();

    Vector3 GetRaycastForwardRotation();

    Transform GetSceneRootTransform();

    Transform GetHead();

    ButtonStatus GetRightTriggerStatus();

    ButtonStatus GetLeftTriggerStatus();

    ButtonStatus GetUnusedButtonStatus();

    Vector3 GetRecordButtonPosition();

    Vector3 GetRecordButtonEulerAngles();

    string GetSerializedAdditionalInfo();

    Color GetColor(); //Color of the trail in the 3d controller recreation

}

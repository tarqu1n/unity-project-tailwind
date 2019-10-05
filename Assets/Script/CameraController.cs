﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    protected Vector3 _LocalRotation;
    protected Vector3 _LocalPosition;

    protected float _CameraDistance = 10f;
    
    [Header("Speeds")]
    public float MouseSensitivity = 4f;
    public float ScrollSensitvity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;
    public float PanDampening = 10f;

    [Header("Constraints")]
    public float UpperVerticalRotationLimit = 90f;
    public float LowerVerticalRotationLimit = 30f;
    public float MaxZoomDistance = 50f;
    public float MinZoomDistance = 3f;

    public bool CameraDisabled = false;

    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;

        this._XForm_Parent.rotation = Quaternion.Euler(LowerVerticalRotationLimit, 0, 0);
        _LocalRotation.y = LowerVerticalRotationLimit;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            CameraDisabled = !CameraDisabled;

        if (!CameraDisabled && Input.GetMouseButton(1))
        {
            //Rotation of the Camera based on Mouse Coordinates
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                //Clamp the y Rotation to horizon and not flipping over at the top
                if (_LocalRotation.y < LowerVerticalRotationLimit)
                    _LocalRotation.y = LowerVerticalRotationLimit;
                else if (_LocalRotation.y > UpperVerticalRotationLimit)
                    _LocalRotation.y = UpperVerticalRotationLimit;
            }
        }


        if (!CameraDisabled && Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                Vector3 forward = this._XForm_Parent.transform.forward;
                Vector3 right= this._XForm_Parent.transform.right;

                forward.y = 0f;
                right.y = 0f;
                forward.Normalize();
                right.Normalize();

                float x = -Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime * PanDampening;
                float z = -Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime * PanDampening;

                // move parent position with mouse
                this._XForm_Parent.position += (forward * z) + (right * x);
            }
        }

        //Zooming Input from our Mouse Scroll Wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;
            ScrollAmount *= (this._CameraDistance * 0.3f);
            this._CameraDistance += ScrollAmount * -1f;
            this._CameraDistance = Mathf.Clamp(this._CameraDistance, MinZoomDistance, MaxZoomDistance);
        }
        

        //Actual Camera Rig Transformations
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

        if (this._XForm_Camera.localPosition.z != this._CameraDistance * -1f)
        {
            this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
        }
    }
}
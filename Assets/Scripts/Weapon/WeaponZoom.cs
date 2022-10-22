using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] RigidbodyFirstPersonController fpsController;
    [SerializeField] float zoomInFOV = 16f;
    [SerializeField] float zoomOutFOV = 60f;
    [SerializeField] float zoomInSensitivity = 0.5f;
    [SerializeField] float zoomOutSensitivity = 2f;


    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            ZoomIn();
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            ZoomOut();
        }
        
    }

    private void OnDisable()
    {
        ZoomOut();
    }

    private void ZoomOut()
    {
        mainCamera.fieldOfView = zoomOutFOV;
        fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
        fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
    }

    private void ZoomIn()
    {
        mainCamera.fieldOfView = zoomInFOV;
        fpsController.mouseLook.XSensitivity = zoomInSensitivity;
        fpsController.mouseLook.YSensitivity = zoomInSensitivity;
    }
}

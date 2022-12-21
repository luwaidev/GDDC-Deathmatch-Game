using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private bool zoom = false;
    private float FOV = 60;
    public float zoomSpeed = 40f;
    public float camSpeed = 200f;

    private float initialMoveSpeed;

    private void Awake()
    {
        vcam = GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();
        initialMoveSpeed = gameObject.GetComponent<PlayerController>().moveSpeed;
    }

    private void OnZoom(InputValue value)
    {
        if (zoom)
        {
            zoom = false;
        }
        else
        {
            zoom = true;
        }
    }

    private void Update()
    {
        if (zoom)
        {
            if (FOV > 30)
            {
                FOV -= zoomSpeed * Time.deltaTime;
                vcam.m_Lens.FieldOfView = FOV;
            }
            else
            {
                vcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = camSpeed / 4;
                vcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = camSpeed / 4;
                gameObject.GetComponent<PlayerController>().moveSpeed = initialMoveSpeed / 4;
            }
        }
        else
        {
            if (FOV < 60)
            {
                FOV += zoomSpeed * Time.deltaTime;
                vcam.m_Lens.FieldOfView = FOV;
            }
            else
            {
                vcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = camSpeed;
                vcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = camSpeed;
                gameObject.GetComponent<PlayerController>().moveSpeed = initialMoveSpeed;
            }
        }
    }
}

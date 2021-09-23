using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMove : MonoBehaviour
{
    public CinemachineFreeLook camera;
    public int X_MaxSpeed;
    public int Y_MaxSpeed;

    private int fieldOfViewMax;
    private int fieldOfViewMin;


    private void Start()
    {
        fieldOfViewMax = 60;
        fieldOfViewMin = 30;
    }

    public void XAxisMaxSpeedChange() 
    {
        camera.m_XAxis.m_MaxSpeed = X_MaxSpeed;
    }

    public void YAxisMaxSpeedChange()
    {
        camera.m_YAxis.m_MaxSpeed = Y_MaxSpeed;
    }

    private void Update()
    {
        var mouseScroll = Input.GetAxis("Mouse ScrollWheel");


        if (mouseScroll > 0.0f && camera.m_Lens.FieldOfView > fieldOfViewMin)
        {
            camera.m_Lens.FieldOfView -= 2;
        }
        else if(mouseScroll < 0.0f && camera.m_Lens.FieldOfView < fieldOfViewMax)
        {
            camera.m_Lens.FieldOfView += 2;
        }
    }


}

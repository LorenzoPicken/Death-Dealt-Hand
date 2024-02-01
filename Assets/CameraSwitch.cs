using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.CompilerServices;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera fpsCam;
    [SerializeField] CinemachineVirtualCamera tableCam;
    bool isFPS = true;

    // Start is called before the first frame update
    void Start()
    {
        fpsCam.Priority = 1;
        tableCam.Priority = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SwitchCam();
        }
    }

    private void SwitchCam()
    {
        isFPS = !isFPS;
        if (isFPS == true) 
        {
            fpsCam.Priority = 0;
            tableCam.Priority = 1;
        }
        else
        {
            fpsCam.Priority = 1;
            tableCam.Priority = 0;
        }
    }
}

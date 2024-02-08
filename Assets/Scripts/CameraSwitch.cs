using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.CompilerServices;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]  CinemachineVirtualCamera fpsCam;
    [SerializeField]  CinemachineVirtualCamera tableCam;
    
    // Update is called once per frame
    public void SwitchToHand()
    {
        fpsCam.Priority = 1;
        tableCam.Priority = 0;
    }

    public void SwitchToTable()
    {
        fpsCam.Priority = 0;
        tableCam.Priority = 1;
    }
}

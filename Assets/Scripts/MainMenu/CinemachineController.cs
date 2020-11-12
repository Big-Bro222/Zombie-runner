using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineController:MonoBehaviour
{

    private static CinemachineController cinemachineController;

    public static CinemachineController Instance { get { return cinemachineController; } }


    private void Awake()
    {
        if (cinemachineController != null && cinemachineController != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            cinemachineController = this;
        }
    }
    // Start is called before the first frame update
    public void SetupCamera(int index)
    {
        CinemachineVirtualCamera[] virtualCameras = GetComponentsInChildren<CinemachineVirtualCamera>();
        for(int i = 0; i < virtualCameras.Length; i++)
        {
            if (i != index)
            {
                virtualCameras[i].Priority = 10;
            }
            else
            {
                virtualCameras[i].Priority = 11;
            }
        }
    }
}

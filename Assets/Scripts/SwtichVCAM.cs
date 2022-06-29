using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class SwtichVCAM : MonoBehaviour
{
    private CinemachineFreeLook freeCamera;

    // Start is called before the first frame update
    private void Awake()
    {
        freeCamera = GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            freeCamera.Priority = 0;
        }
        else
        {
            freeCamera.Priority = 10;
        }
    }
}


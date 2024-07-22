using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    /// <summary>
    /// LookAt and CameraForward work same and LookInverted and CameraInverted also work
    /// same, we just did this to lear a thing by different methods
    
    /// </summary>
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        Cameraforward,
        CameraInverted,
    }

    [SerializeField] Mode mode;

    private void LateUpdate()
    {
         switch(mode)
         {
             case Mode.LookAt:
                 transform.LookAt(Camera.main.transform);
                 break;
             case Mode.LookAtInverted:
                 Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                 transform.LookAt(transform.position);
                 break;
            case Mode.Cameraforward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraInverted:
                transform.forward = -Camera.main.transform.forward;
                break;

     
        }

     }
    }


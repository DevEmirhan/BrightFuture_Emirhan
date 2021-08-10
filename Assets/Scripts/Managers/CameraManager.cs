using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private List<CinemachineVirtualCamera> cmCameras = new List<CinemachineVirtualCamera>();
    public CinemachineVirtualCamera currentCamera;

    public void ActivateCamera(int CameraIndex)
    {
        foreach (var camera in cmCameras)
        {
            camera.gameObject.SetActive(false);
        }
        cmCameras[CameraIndex].gameObject.SetActive(true);
        currentCamera = cmCameras[CameraIndex];
    }
}

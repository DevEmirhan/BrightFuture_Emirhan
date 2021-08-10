using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Bindings")]
    [SerializeField] private CinemachineVirtualCamera gameCam;

    [Space(5)][Header("Arrangements")]
    [SerializeField] private float minDistanceForMove = 15f;
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private float panSpeed = 20f;
    [SerializeField] private float zoomSpeed = 20f;

    [Space(5)][Header("Movement Limitations")]
    [SerializeField] private float horizontalLimit = 50f;
    [SerializeField] private float verticalUpperLimit = -20f;
    [SerializeField] private float verticalLowerLimit = 30f;
    [SerializeField] private float heightMin = 10f;
    [SerializeField] private float heightMax = 100f;

    private bool isActive = false;
    private Vector3 pos;
    private Vector2 beganPos, currPos, deltaPos;
    public float Sensitivity = 1f;


    private void Start()
    {
        InitializePlayer();
    }
    public void InitializePlayer()
    {
        pos = gameCam.transform.position;
        isActive = true;
    }



    void Update()
    {
        if (!isActive) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            beganPos = Input.mousePosition;
            currPos = beganPos;
        }
        if (Input.GetMouseButton(0))
        {
            currPos = Input.mousePosition;
            deltaPos = (currPos - beganPos)*Sensitivity;
        }

        if (Input.GetMouseButtonUp(0))
        {
            deltaPos = Vector3.zero;
        }


        if(deltaPos.y > minDistanceForMove || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (deltaPos.y < -minDistanceForMove || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (deltaPos.x > minDistanceForMove || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (deltaPos.x < -minDistanceForMove || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * zoomSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -horizontalLimit, horizontalLimit);
        pos.y = Mathf.Clamp(pos.y, heightMin, heightMax);
        pos.z = Mathf.Clamp(pos.z, verticalLowerLimit, verticalUpperLimit);

        gameCam.transform.position = pos;
    }
}

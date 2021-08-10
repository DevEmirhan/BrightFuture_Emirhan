using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum PlayMode
{
    Movement,
    Placement
}
public class PlayerController : Singleton<PlayerController>
{
    [Header("Bindings")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private CinemachineVirtualCamera gameCam;

    [Space(5)][Header("Arrangements")]
    [SerializeField] private float minDistanceForMove = 15f;
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private float panSpeed = 20f;
    [SerializeField] private float zoomSpeed = 20f;
    [SerializeField] private float minWaterLevel;

    [Space(5)][Header("Movement Limitations")]
    [SerializeField] private float horizontalLimit = 50f;
    [SerializeField] private float verticalUpperLimit = -20f;
    [SerializeField] private float verticalLowerLimit = 30f;
    [SerializeField] private float heightMin = 10f;
    [SerializeField] private float heightMax = 100f;


    private bool isActive = false;
    private PlayMode playMode = PlayMode.Movement;
    private GameObject currentObject = null;

    private Vector3 pos;
    private Vector2 beganPos, currPos, deltaPos;
    public float Sensitivity = 1f;
    private bool isObjectDragged = false;

    private void Start()
    {
        InitializePlayer();
    }
    public void InitializePlayer()
    {
        pos = gameCam.transform.position;
        currentObject = null;
        playMode = PlayMode.Movement;
        isActive = true;
    }



    void Update()
    {
        if (!isActive) { return; }
        if (playMode == PlayMode.Movement)
        {
            if (Input.GetMouseButtonDown(0))
            {
                beganPos = Input.mousePosition;
                currPos = beganPos;
            }
            if (Input.GetMouseButton(0))
            {
                currPos = Input.mousePosition;
                deltaPos = (currPos - beganPos) * Sensitivity;
            }

            if (Input.GetMouseButtonUp(0))
            {
                deltaPos = Vector3.zero;
            }


            if (deltaPos.y > minDistanceForMove || Input.mousePosition.y >= Screen.height - panBorderThickness)
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
        else if (playMode == PlayMode.Placement)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo) && currentObject != null)
                {
                    if (hitInfo.point.y >= minWaterLevel)
                    {
                        currentObject.transform.position = hitInfo.point;
                        currentObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                        isObjectDragged = true;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isObjectDragged)
                {
                    PlaceObjectToGround();
                }

            }

        }
  
    }
    public void ActivateDecorationItem(int ItemIndex)
    {
        GameObject newObject = Instantiate(PrefabHolder.Instance.decorationObjects[ItemIndex]);
        newObject.transform.position = Vector3.down * 20;
        currentObject = newObject;
        isObjectDragged = false;
        playMode = PlayMode.Placement;
    }
    public void PlaceObjectToGround()
    {
        playMode = PlayMode.Movement;
        isObjectDragged = false;
        currentObject = null;
    }

}

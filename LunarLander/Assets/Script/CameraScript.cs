using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript cameraScript;
    [SerializeField] private GameObject Player;
    [SerializeField] private float MoveLeftAndRight = 10;
    [SerializeField] private float MoveUpAndDown = 6;
    [SerializeField] private float MoveLeftAndRightSize = 10;
    [SerializeField] private float MoveUpAndDownSize = 6;
    private bool Zoom;
    private Vector3 CameraStart;

    private void Awake()
    {
        if(!cameraScript) cameraScript = this;
        CameraStart = transform.position;
    }

    private void Update()
    {
        if (Player)
        {
            if (!Zoom)
            {
                MoveCameraNoSize();
            }
            else
            {
                MoveCameraWithSize();
            }
           
        }
        
    }
    public void MoveCameraNoSize()
    {
        if ((gameObject.transform.position.x - Player.transform.position.x) > MoveLeftAndRight)
        {
            gameObject.transform.position = new Vector3(Player.transform.position.x + MoveLeftAndRight, transform.position.y, transform.position.z);
        }
        else if ((gameObject.transform.position.x - Player.transform.position.x) < - MoveLeftAndRight)
        {
            gameObject.transform.position = new Vector3(Player.transform.position.x - MoveLeftAndRight, transform.position.y, transform.position.z);
        }
        if ((gameObject.transform.position.y - Player.transform.position.y) < - MoveUpAndDown)
        {
            gameObject.transform.position = new Vector3(transform.position.x, Player.transform.position.y - MoveUpAndDown,  transform.position.z);
        }
        else if ((gameObject.transform.position.y - Player.transform.position.y) > MoveUpAndDown)
        {
            if (transform.position.y < 1)
            {
                transform.position = new Vector3(transform.position.x, CameraStart.y, transform.position.z);
            }
            else
            {
                gameObject.transform.position = new Vector3(transform.position.x, Player.transform.position.y + MoveUpAndDown, transform.position.z);
            }
            
        }
    }
    public void MoveCameraWithSize()
    {
        if ((gameObject.transform.position.x - Player.transform.position.x) > MoveLeftAndRightSize)
        {
            gameObject.transform.position = new Vector3(Player.transform.position.x + MoveLeftAndRightSize, transform.position.y, transform.position.z);
        }
        else if ((gameObject.transform.position.x - Player.transform.position.x) < -MoveLeftAndRightSize)
        {
            gameObject.transform.position = new Vector3(Player.transform.position.x - MoveLeftAndRightSize, transform.position.y, transform.position.z);
        }
        if ((gameObject.transform.position.y - Player.transform.position.y) < -MoveUpAndDownSize)
        {
            gameObject.transform.position = new Vector3(transform.position.x, Player.transform.position.y - MoveUpAndDownSize, transform.position.z);
        }
        else if ((gameObject.transform.position.y - Player.transform.position.y) > MoveUpAndDownSize)
        {
            if (transform.position.y < -4)
            {
                transform.position = new Vector3(transform.position.x, -4-40f, transform.position.z);
            }
            else
            {
                gameObject.transform.position = new Vector3(transform.position.x, Player.transform.position.y + MoveUpAndDownSize, transform.position.z);
            }

        }
    }

    public void ZoomCameraMag()
    {
        GetComponent<Camera>().orthographicSize = 3.5f;
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - 3, transform.position.z);
        Zoom = true;
        
    }
    
    public void ZoomCameraMin()
    {
        GetComponent<Camera>().orthographicSize = 8;
        transform.position = new Vector3(transform.position.x, CameraStart.y, transform.position.z);
        Zoom = false;
    }
}

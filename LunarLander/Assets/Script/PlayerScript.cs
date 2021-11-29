using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _rotation;
    [SerializeField] private Transform Right;
    [SerializeField] private GameObject _Obstacle;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private GameObject LimitLeft;
    [SerializeField] private GameObject LimitRight;
    [SerializeField] private GameObject Platform;
    private Rigidbody2D _rigidBody;
    private bool CameraZoom;
    private bool Disable;
    private bool carriege;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        carriege = false;
    }

    private void Update()
    {
        ControlDistance();
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, LimitLeft.transform.position.x, LimitRight.transform.position.x), Mathf.Clamp(transform.position.y, -30, LimitLeft.transform.position.y), transform.position.z);
    }
    void FixedUpdate()
    {
        InputPlayer();
    }

    private void InputPlayer()
    {
        if (!Disable && ManagerGame.fuel > 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(new Vector3(0, 0, _rotation));
                if (transform.rotation.z > 0.7f)
                {
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(new Vector3(0, 0, -_rotation));
                if (transform.rotation.z < -0.7f)
                {
                    transform.eulerAngles = new Vector3(0, 0, 270);
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                ApplyForce(Right, _force);
            }
            else if (!Input.GetKey(KeyCode.W) || !Input.GetKeyDown(KeyCode.W))
            {
                GetComponentInChildren<Animator>().SetBool("StartThruster", false);
            }
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("StartThruster", false);
        }
        
    }

    private void ControlDistance()
    {
        if ((transform.position.y - _Obstacle.transform.position.y) < 2 && CameraZoom == false)
        {
            CameraZoom = true;
            CameraScript.cameraScript.ZoomCameraMag();

        }
        else if ((transform.position.y - _Obstacle.transform.position.y) > 4 && CameraZoom == true)
        {
            CameraZoom = false;
            CameraScript.cameraScript.ZoomCameraMin();
        }
        if ((transform.position.y - Platform.transform.position.y) < 3 )
        {
            if (!carriege)
            {
                carriege = true;
                GetComponentsInChildren<Animator>()[1].SetBool("undercarriage", true);
            }            
        }
        else if ((transform.position.y - Platform.transform.position.y) > 3)
        {
            carriege = false;
            GetComponentsInChildren<Animator>()[1].SetBool("undercarriage", false);
        }
    }

    private void ApplyForce(Transform thrusterTransform, float thrustPower)
    {

        var direction = transform.position - thrusterTransform.position;
         _rigidBody.AddForceAtPosition(direction.normalized * thrustPower, thrusterTransform.position);
        GetComponentInChildren<Animator>().SetBool("StartThruster", true);
        ManagerGame.managerGame.DisplayFuel(0.5f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Save") && collision.relativeVelocity.magnitude < 0.5f  && ManagerGame.fuel > 0)
        {
            collision.gameObject.GetComponent<PlatformPoints>().AddScore();
            Disable = true;
            GetComponentInChildren<Animator>().SetBool("StartThruster", false);
            ManagerGame.managerGame.StopTimer = true;
            

        }
        else
        {
            ManagerGame.managerGame.StopTimer = true;
            ManagerGame.managerGame.Lose(25);
            Destroy(gameObject);
            Instantiate(Explosion).transform.position = transform.position;
            
        }
        
    }
}

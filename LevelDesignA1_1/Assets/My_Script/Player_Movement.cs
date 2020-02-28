using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float sensitivity = 55;

    public Transform playerBody;
    public Camera playerCam;

    private float xRotation;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    //public float sensitivityX = 15F;
    //public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    Rigidbody rb;

    public float playerSpeed = 2f;

    public float counterMovement = 0.175f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(value: xRotation, min: -90, max: 90);

        //playerCam.transform.localRotation = Quaternion.Euler(xRotation, y: 0, z: 0);
        //playerBody.Rotate(eulers: Vector3.up * mouseX);

        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X");

            rotationY += Input.GetAxis("Mouse Y");
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * 0, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y");
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCrouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopCrouch();
        }

        Movement();
    }

    private void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rb.AddRelativeForce(new Vector3(h, 0, v) * playerSpeed);
    }

    private void StartCrouch()
    {
        transform.localScale = new Vector3(x: 1, y: 0.5f, z: 1);
    }

    private void StopCrouch()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.localScale = new Vector3(x: 1, y: 1, z: 1);
    }
}

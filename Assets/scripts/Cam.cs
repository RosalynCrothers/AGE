using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{

    public float senX;
    public float senY;

    private float rotX;
    private float rotY;

    public Transform camOrient;
    private Vector3 camPos;



        void Start()
        {
            camPos = this.transform.position;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {

            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senY;

            rotY += mouseX;
            rotX -= mouseY;

            rotX = Mathf.Clamp(rotX, -90f, 90f);

            transform.rotation = Quaternion.Euler(rotX, rotY, 0);
            camOrient.rotation = Quaternion.Euler(0, rotY, 0);
        }
}

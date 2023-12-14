using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    
    public float p_Speed;
    public Transform p_Orient;
    public Vector3 p_Dir;
    public Rigidbody p_RB;

    private int goUp;

    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        p_RB = GetComponent<Rigidbody>();
        p_RB.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    void FixedUpdate()
    {
        Movement();
        goUp = 0;
    }

    private void Inputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.Space))
        {
            goUp = 1;
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            goUp = -1;
        }

    }

    private void Movement()
    {
        Vector3 verDir = new Vector3(0.0f, goUp * p_Speed, 0.0f);
        p_Dir = p_Orient.forward * verticalInput + p_Orient.right * horizontalInput;
        p_RB.AddForce(p_Dir.normalized * p_Speed, ForceMode.Impulse);
        p_RB.AddForce(verDir, ForceMode.Impulse);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 500;
    public float rotationSpeed;
    public float dashForce;

    public float dashAmount;
    public float dashTime;
    private float _dashTime;
    public bool canDash;

    private bool dashTimer = false;
    private Rigidbody rb;


    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 lastDir;

    public GameObject visuals;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _dashTime = dashTime;
    }


    void Update()
    {
        GetInput();
        Rotate();
        DashTimer();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;

        if (moveInput.magnitude != 0)
        {
            lastDir = moveInput;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            dashTimer = true;
        }

    }
    private void Dash()
    {
        dashAmount--;
        rb.velocity = Vector3.zero;
        rb.AddForce(lastDir * dashForce, ForceMode.Impulse);
        canDash = false; 
    }

    private void DashTimer() 
    {
        if (dashTimer)
        {
            if (_dashTime > 0)
            {
                _dashTime -= Time.deltaTime;
                Dash();
            }
            else
            {
                _dashTime = dashTime;
                canDash = true;
                dashTimer = false;
            }
        }
    }

    private void Move()
    {
        rb.velocity = moveVelocity * Time.fixedDeltaTime;
    }

    private void Rotate()
    {
        Quaternion newRotation = Quaternion.LookRotation(moveVelocity, Vector3.up);
        if (moveVelocity.magnitude > 0)
        {
            visuals.transform.rotation = Quaternion.Lerp(visuals.transform.rotation,
                newRotation, Time.fixedDeltaTime * rotationSpeed);
        }
    }
}

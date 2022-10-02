using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 500;
    public float sprintSpeed;
    private float speed;
    
    public float rotationSpeed;
    public float gravity;
    [Header("Sprint")]
    public float regainSprintTime;
    public float sprintTime;
    private float _sprintTime;
    public bool canSprint;


    private bool sprintTimer = false;
    private CharacterController _characterController;


    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 rotation;

    public GameObject visuals;

    void Start()
    {
        _sprintTime = sprintTime;
        speed = moveSpeed;
        _characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        GetInput();
        Rotate();
        SprintTimer();
        _characterController.Move(moveVelocity * Time.deltaTime);
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprintTimer = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && sprintTimer)
        {
            speed = moveSpeed;
            canSprint = true;
            sprintTimer = false;
        }

        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), gravity, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        rotation = new Vector3(moveInput.x, 0, moveInput.z);
    }

    private void SprintTimer() 
    {
        if (sprintTimer)
        {
            if (_sprintTime > 0)
            {
                _sprintTime -= Time.deltaTime;
                speed = sprintSpeed;
                canSprint = false;
            }
            else
            {
                speed = moveSpeed;
                StartCoroutine(SpringReplenish());
            }
        }
    }

    private void Rotate()
    {
        Quaternion newRotation = Quaternion.LookRotation(rotation, Vector3.up);
        if (moveVelocity.magnitude > 0)
        {
            visuals.transform.rotation = Quaternion.Lerp(visuals.transform.rotation,
                newRotation, Time.fixedDeltaTime * rotationSpeed);
        }
    }

    private IEnumerator SpringReplenish()
    {
        yield return new WaitForSecondsRealtime(regainSprintTime);
        _sprintTime = sprintTime;
        
        canSprint = true;
        sprintTimer = false;
    }
}

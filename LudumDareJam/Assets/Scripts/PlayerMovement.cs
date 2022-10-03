using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("UI")] 
    public Image staminaBar;
    
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


    private bool sprintGoing = false;
    private CharacterController _characterController;


    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 rotation;

    public GameObject visuals;

    void Start()
    {
        Time.timeScale = 1f;
        _sprintTime = sprintTime;
        speed = moveSpeed;
        _characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        GetInput();
        Rotate();
        SprintTimer();
        StaminaFill();
        _characterController.Move(moveVelocity * Time.deltaTime);
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprintGoing = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && sprintGoing)
        {
            speed = moveSpeed;
            sprintGoing = false;
        }
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), gravity, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        rotation = new Vector3(moveInput.x, 0, moveInput.z);
    }

    private void SprintTimer() 
    {
        if (sprintGoing)
        {
            if (_sprintTime > 0)
            {
                _sprintTime -= Time.deltaTime;
                speed = sprintSpeed;
            }
            else
            {
                speed = moveSpeed;
            }
        }
        
        if(_sprintTime < sprintTime)
            _sprintTime += Time.deltaTime / regainSprintTime;
    }

    private void StaminaFill()
    {
        staminaBar.fillAmount = _sprintTime / sprintTime;
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
    
}

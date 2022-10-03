using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Respawn")] 
    public Transform respawnPosition;

    [Header("Packager")] 
    public PackageManager pckmn;
    
    [Header("UI")] 
    public Image staminaBar;

    [Header("Move")] 
    public Animator anim;
    public float moveSpeed = 500;
    public float sprintSpeed;
    private float speed;

    public float rotationSpeed;
    public float gravity;
    [Header("Sprint")]
    public float regainSprintTime;
    public float sprintTime;
    private float _sprintTime;
    private float throwingCoolDown = 5f;

    private float slowedSpeed = 5f;
    private float slowedSprint = 40f;
    private float oldSpeed;
    private float oldSprint;

    private Vector3 previousPosition;

    private bool sprintGoing = false;
    private CharacterController _characterController;


    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 rotation;

    public GameObject visuals;

    void Start()
    {
        Time.timeScale = 1f;
        oldSpeed = moveSpeed;
        oldSprint = sprintSpeed;
        _sprintTime = sprintTime;
        speed = moveSpeed;
        _characterController = GetComponent<CharacterController>();
        previousPosition = transform.position;
    }
    
    void Update()
    {
        GetInput();
        Rotate();
        SprintTimer();
        StaminaFill();
        _characterController.Move(moveVelocity * Time.deltaTime);
        throwingCoolDown += Time.deltaTime;
        previousPosition = transform.position;
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprintGoing = true;
            anim.SetBool("Run", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && sprintGoing)
        {
            anim.SetBool("Run", false);
            speed = moveSpeed;
            sprintGoing = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && throwingCoolDown >= 5f)
        {
            GameObject throwedItem = Instantiate(pckmn.currentObjectInHands.GetComponent<Item>().itemToThrow, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), Quaternion.identity);
            throwedItem.GetComponent<Rigidbody>().velocity = new Vector3(visuals.transform.forward.x * 15f, 5f, visuals.transform.forward.z * 15f);
            Dog.GetToy(throwedItem.transform);
            Destroy(throwedItem, 5f);
            throwingCoolDown = 0f;
        }
        
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), gravity, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        
        if(Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
            anim.SetBool("Walk",true);
        else
            anim.SetBool("Walk",false);
        
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

    private Coroutine currentCour;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallingZone"))
            transform.position = respawnPosition.position;

        if (other.CompareTag("DOG") && currentCour == null)
            currentCour = StartCoroutine(slowness());
        
    }

    private IEnumerator slowness()
    {
        moveSpeed = slowedSpeed;
        sprintSpeed = slowedSprint;
        yield return new WaitForSeconds(3f);
        moveSpeed = oldSpeed;
        sprintSpeed = oldSprint;
        currentCour = null;
    }
}

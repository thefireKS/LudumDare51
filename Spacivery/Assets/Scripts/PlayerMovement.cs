using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Respawn")] 
    [SerializeField] private Transform respawnPosition;

    [Header("Move")] 
    [SerializeField] private Animator anim;
    [SerializeField] private float moveSpeed = 500;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed; 
    [SerializeField] private GameObject player_GFX;
    
    private Vector2 moveInput;
    private Vector3 moveVelocity;
    private Vector3 rotation;
    
    private Rigidbody rigidbody;
    
    private void Start()
    {
        Time.timeScale = 1f;

        rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(moveVelocity.x * Time.fixedDeltaTime, rigidbody.velocity.y,moveVelocity.z * Time.fixedDeltaTime);
    }

    public void GetInput(InputAction.CallbackContext _callbackContext)
    {
        moveInput = _callbackContext.ReadValue<Vector2>();
        var input = moveInput.normalized;
        moveVelocity = new Vector3(input.x,0, input.y) * moveSpeed;
        
        if (input != Vector2.zero)
        {
            anim.SetBool("Walk",true);
            rotation = new Vector3(input.x, 0, input.y);
        }
        else
            anim.SetBool("Walk",false);
    }

    private void Rotate()
    {
        Quaternion newRotation = Quaternion.LookRotation(rotation, Vector3.up);
        if (moveVelocity.magnitude > 0)
        {
            player_GFX.transform.rotation = Quaternion.Lerp(player_GFX.transform.rotation,
                newRotation, Time.fixedDeltaTime * rotationSpeed);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallingZone"))
            transform.position = respawnPosition.position;
    }
}

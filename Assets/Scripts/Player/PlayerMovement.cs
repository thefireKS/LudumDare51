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

    [Header("Parameters")] [SerializeField]
    private PlayerParameters playerParameters;
    
    [Header("Rotation")]
    [SerializeField] private GameObject player_GFX;

    [HideInInspector] 
    public float moveSpeed;
    
    private Vector2 moveInput;
    private Vector3 moveVelocity;
    private Vector3 rotation;
    
    private Rigidbody _rigidbody;
    
    private void Start()
    {
        Time.timeScale = 1f;
        
        moveSpeed = playerParameters.moveSpeed;
        
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        if(Time.timeScale == 0f)
            return;
        
        _rigidbody.velocity = new Vector3(moveVelocity.x * Time.fixedDeltaTime, _rigidbody.velocity.y,moveVelocity.z * Time.fixedDeltaTime);
    }

    public void GetInput(InputAction.CallbackContext _callbackContext)
    {
        if(Time.timeScale == 0f)
            return;
        
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
                newRotation, Time.fixedDeltaTime * playerParameters.rotationSpeed);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallingZone"))
            transform.position = respawnPosition.position;
    }
}

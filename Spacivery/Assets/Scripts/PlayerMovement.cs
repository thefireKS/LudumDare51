using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Respawn")] 
    public Transform respawnPosition;

    [Header("Move")] 
    public Animator anim;
    public float moveSpeed = 500;

    public float rotationSpeed;
    public float gravity;
    
    private CharacterController _characterController;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 rotation;

    public GameObject visuals;

    void Start()
    {
        Time.timeScale = 1f;
        _characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        GetInput();
        Rotate();
        _characterController.Move(moveVelocity * Time.deltaTime);
    }

    private void GetInput()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), gravity, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
        
        if(Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
            anim.SetBool("Walk",true);
        else
            anim.SetBool("Walk",false);
        
        if(moveInput.x != 0 || moveInput.z != 0)
            rotation = new Vector3(moveInput.x, 0, moveInput.z);
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallingZone"))
            transform.position = respawnPosition.position;
    }
}

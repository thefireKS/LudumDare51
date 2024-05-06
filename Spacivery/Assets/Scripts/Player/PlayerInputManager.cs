using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
    }
}

using UnityEngine;

public class FPS : MonoBehaviour
{
    private void Awake()
    {
        #if UNITY_ANDROID
        Application.targetFrameRate = 60;
        #endif
    }
}

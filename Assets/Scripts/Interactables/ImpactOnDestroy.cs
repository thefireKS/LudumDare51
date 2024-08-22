using UnityEngine;

public class ImpactOnDestroy : MonoBehaviour
{
    [SerializeField] private GameObject VSFX_Prefab;

    private void OnDestroy()
    {
        if(gameObject.scene.isLoaded)
            Instantiate(VSFX_Prefab, transform.position, transform.rotation);
    }
}

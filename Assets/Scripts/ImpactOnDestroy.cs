using UnityEngine;

public class ImpactOnDestroy : MonoBehaviour
{
    [SerializeField] private GameObject VSFX_Prefab;

    private void OnDestroy()
    {
        Instantiate(VSFX_Prefab, transform.position, transform.rotation);
    }
}

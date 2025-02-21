using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] protected InteractableItemsParameters itemsParameters;
    [Space(5)]
    [SerializeField] protected GameObject VSFX_Prefab;

    protected int scoreAmount;
    public static Action<int> AddScore;

    protected virtual void OnDestroy()
    {
        if(gameObject.scene.isLoaded)
            Instantiate(VSFX_Prefab, transform.position, transform.rotation);
    }
}

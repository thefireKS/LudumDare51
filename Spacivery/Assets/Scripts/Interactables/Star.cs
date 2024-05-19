using UnityEngine;

public class Star : Collectable
{
    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;
        
        AddScore?.Invoke(scoreAmount);
        Destroy(gameObject);
    }
}

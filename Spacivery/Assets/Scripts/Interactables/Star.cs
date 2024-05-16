using UnityEngine;

public class Star : Collectable
{
    private void OnTriggerEnter(Collider collided)
    {
        if (!collided.CompareTag("Player")) return;
        
        addScore?.Invoke(scoreAmount);
        Destroy(gameObject);
    }
}

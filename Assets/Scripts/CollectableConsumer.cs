using UnityEngine;

[RequireComponent (typeof(Collider))]
public class CollectableConsumer : MonoBehaviour
{
    private float _itemsCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
            _itemsCollected++;
        }
    }
}

using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Item : MonoBehaviour, ICollectable, IHoldable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Collect()
    {
        Destroy(gameObject);
    }

    public void PickUp()
    {
        _rigidbody.isKinematic = true;
    }

    public void PutDown()
    {
        _rigidbody.isKinematic = false;
    }
}

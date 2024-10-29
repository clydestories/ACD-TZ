using UnityEngine;

public class Item : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private InputReader _input;
    [SerializeField] private Camera _camera;

    private Vector2 _screenCenter = new Vector2(0.5f, 0.5f);
    private IHoldable _holdable;

    private void OnEnable()
    {
        _input.Interacted += Interact;
    }

    private void OnDisable()
    {
        _input.Interacted += Interact;
    }

    private void Interact()
    {
        Ray ray = _camera.ViewportPointToRay(_screenCenter);

        if (_holdable as Item != null)
        {
            PutHoldableDown();
            return;
        }

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out IHoldable holdable))
            {
                PickHoldableUp(holdable);
            }
        }
    }

    private void PickHoldableUp(IHoldable holdable)
    {
        _holdable = holdable;
        
        if (_holdable is Item)
        {
            Item item = holdable as Item;
            item.transform.parent = _container;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        _holdable.PickUp();
    }

    private void PutHoldableDown()
    {
        if (_holdable is Item)
        {
            Item item = _holdable as Item;
            item.transform.parent = null;
        }

        _holdable.PutDown();
        _holdable = null;
    }
}

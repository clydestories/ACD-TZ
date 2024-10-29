using DG.Tweening;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _pickUpDistance;
    [Header("Dependencies")]
    [SerializeField] private Transform _container;
    [SerializeField] private InputReader _input;
    [SerializeField] private Camera _camera;

    private Vector2 _screenCenter = new Vector2(0.5f, 0.5f);
    private IHoldable _holdable;
    private float _pickAnimationDuration = 0.5f;

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

        if (Physics.Raycast(ray, out RaycastHit hit, _pickUpDistance))
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
        _holdable.PickUp();

        if (_holdable is Item)
        {
            Item item = holdable as Item;

            Tweener movingIntoContainer = item.transform.DOMove(_container.position, _pickAnimationDuration).SetEase(Ease.OutCirc);
            movingIntoContainer
                .OnUpdate(() =>
                {
                    movingIntoContainer.ChangeEndValue(_container.position,
                        snapStartValue: true,
                        newDuration: movingIntoContainer.Duration() - movingIntoContainer.Elapsed());
                });

            Tweener rotateIntoContainer = item.transform.DORotate(_container.eulerAngles, _pickAnimationDuration);
            rotateIntoContainer
                .OnUpdate(() =>
                {
                    rotateIntoContainer.ChangeEndValue(_container.eulerAngles,
                        snapStartValue: true,
                        newDuration: movingIntoContainer.Duration() - movingIntoContainer.Elapsed());
                });

            movingIntoContainer.Play();
            rotateIntoContainer.Play();
            DOVirtual.DelayedCall(_pickAnimationDuration, () =>
            {
                item.transform.parent = _container;
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.Euler(Vector3.zero);
            });
        }
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

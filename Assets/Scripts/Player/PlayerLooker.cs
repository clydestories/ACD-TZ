using UnityEngine;

public class PlayerLooker : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _minRotationX;
    [SerializeField] private float _maxRotationX;
    [Header("Dependencies")]
    [SerializeField] private InputReader _input;
    [SerializeField] private Camera _camera;

    private float _rotationX;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _input.Looked += Look;
    }

    private void OnDisable()
    {
        _input.Looked -= Look;
    }

    private void Look(Vector2 direction)
    {
        _rotationX -= direction.y * _speed * Time.deltaTime;
        float rotationYDelta = direction.x * _speed * Time.deltaTime;

        _rotationX = Mathf.Clamp(_rotationX, _minRotationX, _maxRotationX);

        _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.Rotate(Vector3.up * rotationYDelta);
    }
}

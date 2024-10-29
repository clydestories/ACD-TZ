using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed;
    [Header("Dependencies")]
    [SerializeField] private InputReader _input;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _input.Moved += Move;
    }

    private void OnDisable()
    {
        _input.Moved -= Move;
    }

    private void Move(Vector2 direction)
    {
        Vector3 moveDirection = transform.forward * direction.y + transform.right * direction.x;
        _controller.Move(moveDirection * _speed * Time.deltaTime);
    }
}

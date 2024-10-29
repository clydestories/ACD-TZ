using System;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    [SerializeField] private List<KeyCode> _interactKeys;

    public event Action<Vector2> Moved;
    public event Action<Vector2> Looked;
    public event Action Interacted;

    private void Update()
    {
        float horizontalMovement = Input.GetAxisRaw(Horizontal);
        float verticalMovement = Input.GetAxisRaw(Vertical);
        Vector2 moveDirection = new Vector2(horizontalMovement, verticalMovement).normalized;
        Moved?.Invoke(moveDirection);

        float lookX = Input.GetAxisRaw(MouseX);
        float lookY = Input.GetAxisRaw(MouseY);
        Vector2 lookDirection = new Vector2(lookX, lookY);
        Looked?.Invoke(lookDirection);

        foreach (KeyCode key in _interactKeys)
        {
            if (Input.GetKeyDown(key))
            {
                Interacted?.Invoke();
            }
        }
    }
}

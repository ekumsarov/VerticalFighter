using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public delegate void VectorInputChangeDelegate(Vector2 Input);
    public VectorInputChangeDelegate VectorInputChange;

    private Vector2 _rawMoveInput;
    public void OnMove(InputAction.CallbackContext context)
    {
        _rawMoveInput = context.ReadValue<Vector2>();

        VectorInputChange?.Invoke(_rawMoveInput);
    }
}

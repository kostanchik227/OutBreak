using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public static GameInput Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public bool GetIsClickR()
    {
        return Keyboard.current.rKey.isPressed;
    }

    public bool GetIsClickLMB()
    {
        return Mouse.current.leftButton.wasPressedThisFrame;
    }

    public bool GetIsClickSpace()
    {
        return Keyboard.current.spaceKey.wasPressedThisFrame;
    }
    public bool GetIsClickEsc()
    {
        return Keyboard.current.escapeKey.wasPressedThisFrame;
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using InputKey = UnityEngine.InputSystem.Key;

namespace EnhancedSpectator.Features.Spectator;

/// <summary>
/// Reads local Unity Input System controls for spectator freecam input.
/// </summary>
public sealed class SpectatorInputService
{
    private const float MouseDeltaScale = 0.05f;

    private readonly SpectatorFreecamSettings _settings;

    /// <summary>
    /// Creates an input service for spectator freecam controls.
    /// </summary>
    public SpectatorInputService(SpectatorFreecamSettings settings)
    {
        _settings = settings;
    }

    /// <summary>
    /// Gets whether the freecam toggle key was pressed this frame.
    /// </summary>
    public bool ToggleFreecamPressed => GetKeyDown(_settings.ToggleFreecamKey);

    /// <summary>
    /// Gets whether the recenter key was pressed this frame.
    /// </summary>
    public bool RecenterPressed => GetKeyDown(_settings.RecenterKey);

    /// <summary>
    /// Gets whether the reset-to-vanilla key was pressed this frame.
    /// </summary>
    public bool ResetToVanillaPressed => GetKeyDown(_settings.ResetToVanillaViewKey);

    /// <summary>
    /// Gets whether the fast movement key is currently held.
    /// </summary>
    public bool FastMoveHeld => GetKey(_settings.FastMoveKey);

    /// <summary>
    /// Gets whether the slow movement key is currently held.
    /// </summary>
    public bool SlowMoveHeld => GetKey(_settings.SlowMoveKey);

    /// <summary>
    /// Gets whether the configured ascend key is currently held.
    /// </summary>
    public bool AscendHeld => GetKey(_settings.AscendKey);

    /// <summary>
    /// Gets whether the configured descend key is currently held.
    /// </summary>
    public bool DescendHeld => GetKey(_settings.DescendKey);

    /// <summary>
    /// Reads WASD plus configured vertical movement input.
    /// </summary>
    public Vector3 ReadMoveInput()
    {
        Keyboard? keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return Vector3.zero;
        }

        float x = 0f;
        float y = 0f;
        float z = 0f;

        if (IsPressed(keyboard, InputKey.A))
        {
            x -= 1f;
        }

        if (IsPressed(keyboard, InputKey.D))
        {
            x += 1f;
        }

        if (DescendHeld)
        {
            y -= 1f;
        }

        if (AscendHeld)
        {
            y += 1f;
        }

        if (IsPressed(keyboard, InputKey.S))
        {
            z -= 1f;
        }

        if (IsPressed(keyboard, InputKey.W))
        {
            z += 1f;
        }

        Vector3 move = new Vector3(x, y, z);
        return move.sqrMagnitude > 1f ? move.normalized : move;
    }

    /// <summary>
    /// Reads mouse look delta from Unity Input System.
    /// </summary>
    public Vector2 ReadLookDelta()
    {
        Mouse? mouse = Mouse.current;
        return mouse == null ? Vector2.zero : mouse.delta.ReadValue() * MouseDeltaScale;
    }

    /// <summary>
    /// Reads a configured key from Unity Input System.
    /// </summary>
    public static bool IsKeyHeld(KeyCode key)
    {
        return GetKey(key);
    }

    /// <summary>
    /// Reads whether a configured key was pressed during this frame.
    /// </summary>
    public static bool IsKeyPressedThisFrame(KeyCode key)
    {
        return GetKeyDown(key);
    }

    private static bool GetKey(KeyCode key)
    {
        if (!TryGetInputSystemKey(key, out InputKey inputKey))
        {
            return false;
        }

        Keyboard? keyboard = Keyboard.current;
        return keyboard != null && IsPressed(keyboard, inputKey);
    }

    private static bool GetKeyDown(KeyCode key)
    {
        if (!TryGetInputSystemKey(key, out InputKey inputKey))
        {
            return false;
        }

        Keyboard? keyboard = Keyboard.current;
        return keyboard != null && WasPressedThisFrame(keyboard, inputKey);
    }

    private static bool IsPressed(Keyboard keyboard, InputKey key)
    {
        KeyControl control = keyboard[key];
        return control != null && control.isPressed;
    }

    private static bool WasPressedThisFrame(Keyboard keyboard, InputKey key)
    {
        KeyControl control = keyboard[key];
        return control != null && control.wasPressedThisFrame;
    }

    private static bool TryGetInputSystemKey(KeyCode keyCode, out InputKey inputKey)
    {
        inputKey = keyCode switch
        {
            KeyCode.Backspace => InputKey.Backspace,
            KeyCode.Tab => InputKey.Tab,
            KeyCode.Return => InputKey.Enter,
            KeyCode.Escape => InputKey.Escape,
            KeyCode.Space => InputKey.Space,
            KeyCode.Quote => InputKey.Quote,
            KeyCode.Comma => InputKey.Comma,
            KeyCode.Minus => InputKey.Minus,
            KeyCode.Period => InputKey.Period,
            KeyCode.Slash => InputKey.Slash,
            KeyCode.Alpha0 => InputKey.Digit0,
            KeyCode.Alpha1 => InputKey.Digit1,
            KeyCode.Alpha2 => InputKey.Digit2,
            KeyCode.Alpha3 => InputKey.Digit3,
            KeyCode.Alpha4 => InputKey.Digit4,
            KeyCode.Alpha5 => InputKey.Digit5,
            KeyCode.Alpha6 => InputKey.Digit6,
            KeyCode.Alpha7 => InputKey.Digit7,
            KeyCode.Alpha8 => InputKey.Digit8,
            KeyCode.Alpha9 => InputKey.Digit9,
            KeyCode.Semicolon => InputKey.Semicolon,
            KeyCode.Equals => InputKey.Equals,
            KeyCode.LeftBracket => InputKey.LeftBracket,
            KeyCode.Backslash => InputKey.Backslash,
            KeyCode.RightBracket => InputKey.RightBracket,
            KeyCode.BackQuote => InputKey.Backquote,
            KeyCode.A => InputKey.A,
            KeyCode.B => InputKey.B,
            KeyCode.C => InputKey.C,
            KeyCode.D => InputKey.D,
            KeyCode.E => InputKey.E,
            KeyCode.F => InputKey.F,
            KeyCode.G => InputKey.G,
            KeyCode.H => InputKey.H,
            KeyCode.I => InputKey.I,
            KeyCode.J => InputKey.J,
            KeyCode.K => InputKey.K,
            KeyCode.L => InputKey.L,
            KeyCode.M => InputKey.M,
            KeyCode.N => InputKey.N,
            KeyCode.O => InputKey.O,
            KeyCode.P => InputKey.P,
            KeyCode.Q => InputKey.Q,
            KeyCode.R => InputKey.R,
            KeyCode.S => InputKey.S,
            KeyCode.T => InputKey.T,
            KeyCode.U => InputKey.U,
            KeyCode.V => InputKey.V,
            KeyCode.W => InputKey.W,
            KeyCode.X => InputKey.X,
            KeyCode.Y => InputKey.Y,
            KeyCode.Z => InputKey.Z,
            KeyCode.Delete => InputKey.Delete,
            KeyCode.Keypad0 => InputKey.Numpad0,
            KeyCode.Keypad1 => InputKey.Numpad1,
            KeyCode.Keypad2 => InputKey.Numpad2,
            KeyCode.Keypad3 => InputKey.Numpad3,
            KeyCode.Keypad4 => InputKey.Numpad4,
            KeyCode.Keypad5 => InputKey.Numpad5,
            KeyCode.Keypad6 => InputKey.Numpad6,
            KeyCode.Keypad7 => InputKey.Numpad7,
            KeyCode.Keypad8 => InputKey.Numpad8,
            KeyCode.Keypad9 => InputKey.Numpad9,
            KeyCode.KeypadPeriod => InputKey.NumpadPeriod,
            KeyCode.KeypadDivide => InputKey.NumpadDivide,
            KeyCode.KeypadMultiply => InputKey.NumpadMultiply,
            KeyCode.KeypadMinus => InputKey.NumpadMinus,
            KeyCode.KeypadPlus => InputKey.NumpadPlus,
            KeyCode.KeypadEnter => InputKey.NumpadEnter,
            KeyCode.KeypadEquals => InputKey.NumpadEquals,
            KeyCode.UpArrow => InputKey.UpArrow,
            KeyCode.DownArrow => InputKey.DownArrow,
            KeyCode.RightArrow => InputKey.RightArrow,
            KeyCode.LeftArrow => InputKey.LeftArrow,
            KeyCode.Insert => InputKey.Insert,
            KeyCode.Home => InputKey.Home,
            KeyCode.End => InputKey.End,
            KeyCode.PageUp => InputKey.PageUp,
            KeyCode.PageDown => InputKey.PageDown,
            KeyCode.F1 => InputKey.F1,
            KeyCode.F2 => InputKey.F2,
            KeyCode.F3 => InputKey.F3,
            KeyCode.F4 => InputKey.F4,
            KeyCode.F5 => InputKey.F5,
            KeyCode.F6 => InputKey.F6,
            KeyCode.F7 => InputKey.F7,
            KeyCode.F8 => InputKey.F8,
            KeyCode.F9 => InputKey.F9,
            KeyCode.F10 => InputKey.F10,
            KeyCode.F11 => InputKey.F11,
            KeyCode.F12 => InputKey.F12,
            KeyCode.Numlock => InputKey.NumLock,
            KeyCode.CapsLock => InputKey.CapsLock,
            KeyCode.ScrollLock => InputKey.ScrollLock,
            KeyCode.RightShift => InputKey.RightShift,
            KeyCode.LeftShift => InputKey.LeftShift,
            KeyCode.RightControl => InputKey.RightCtrl,
            KeyCode.LeftControl => InputKey.LeftCtrl,
            KeyCode.RightAlt => InputKey.RightAlt,
            KeyCode.LeftAlt => InputKey.LeftAlt,
            _ => InputKey.None,
        };

        return inputKey != InputKey.None;
    }
}

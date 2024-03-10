using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoForge.InputSystem;

public sealed class Gamepad : IGamepad
{
    private readonly PlayerIndex _playerIndex;

    private GamePadState _lastState;
    private GamePadState _currentState;

    internal Gamepad(PlayerIndex playerIndex)
    {
        _playerIndex = playerIndex;
    }

    public event Action? Connected;
    public event Action? Disconnected;
    public bool IsConnected { get; private set; }
    public Vector2 LeftStickValue { get; private set; }
    public Vector2 RightStickValue { get; private set; }
    public float LeftShoulderValue { get; private set; }
    public float RightShoulderValue { get; private set; }

    public void Update(IGame game, float deltaTime)
    {
        UpdateStates();
        HandleEvents();
        UpdateValues();
    }

    public void SetRumbleSpeed(float left, float right)
    {
        GamePad.SetVibration(_playerIndex, left, right);
    }

    public bool WasPressed(Buttons button)
    {
        return _lastState.IsButtonUp(button) && _currentState.IsButtonDown(button);
    }

    public bool IsPressed(Buttons button)
    {
        return _currentState.IsButtonDown(button);
    }

    public bool WasReleased(Buttons button)
    {
        return _lastState.IsButtonDown(button) && _currentState.IsButtonUp(button);
    }

    public void Dispose()
    {
    }

    private void UpdateStates()
    {
        _lastState = _currentState;
        _currentState = GamePad.GetState(_playerIndex);
    }

    private void UpdateValues()
    {
        IsConnected = _currentState.IsConnected;
        LeftStickValue = _currentState.ThumbSticks.Left;
        LeftShoulderValue = _currentState.Triggers.Left;
        RightStickValue = _currentState.ThumbSticks.Right;
        RightShoulderValue = _currentState.Triggers.Right;
    }

    private void HandleEvents()
    {
        if (!_lastState.IsConnected && _currentState.IsConnected)
        {
            Connected?.Invoke();
        }

        if (_lastState.IsConnected && !_currentState.IsConnected)
        {
            Disconnected?.Invoke();
        }
    }
}
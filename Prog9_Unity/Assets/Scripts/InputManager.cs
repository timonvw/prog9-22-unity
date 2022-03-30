using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class InputMoveEvent : UnityEvent<float, float> { }
[Serializable] public class InputJumpEvent : UnityEvent<bool> { }
[Serializable] public class InputSettingsEvent : UnityEvent { }

public class InputManager : MonoBehaviour {
    #region Singleton
    private static InputManager instance;
    public static InputManager Instance {
        get {
            if (instance == null) {
                throw new Exception("No InputManager");
            }
            return instance;
        }
    }
    #endregion
    
    // Our input action
    private MasterInput input;

    // Events
    [Header("Events")]
    public InputMoveEvent inputMoveEvent;
    public InputJumpEvent inputJumpEvent;
    public InputSettingsEvent inputMenuEvent;
    
    private void Awake() {
        instance = this;
        input = new MasterInput();

        // Gameplay
        input.Gameplay.Movement.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        input.Gameplay.Movement.canceled += ctx => OnMove(ctx.ReadValue<Vector2>());
        input.Gameplay.Jump.performed += _ => OnJump(true); 
        input.Gameplay.OpenMenu.performed += _ => OnSettings();

        // UI
        input.UI.CloseMenu.performed += _ => OnSettings();
    }
    
    private void OnEnable() {
        EnableGameplay(true);
    }

    private void OnDisable() {
        EnableGameplay(false);
    }

    public void EnableGameplay(bool enable) {
        if (enable) {
            input.Gameplay.Enable();
        } else {
            input.Gameplay.Disable();
        }
    }
    
    // Gameplay
    private void OnMove(Vector2 direction) {
        inputMoveEvent.Invoke(direction.x, direction.y);
    }

    private void OnJump(bool jump) {
        inputJumpEvent.Invoke(jump);
    }
    
    // UI
    private void OnSettings() {
        inputMenuEvent.Invoke();
        
        // TODO: Enable UI and Gameplay ETC
        if(input.Gameplay.enabled) {
            
        } else {
            
        }
    }

}

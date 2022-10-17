//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/InputAction/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Circle"",
            ""id"": ""41bcdfae-efaa-4de2-977b-0525346a846f"",
            ""actions"": [],
            ""bindings"": []
        },
        {
            ""name"": ""Player"",
            ""id"": ""3baab267-783c-4dd9-912b-b24f90ff0e90"",
            ""actions"": [
                {
                    ""name"": ""PlayerMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""673ed9a7-8f5a-4c3a-8f08-81e5f5c87fcf"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerAttack"",
                    ""type"": ""Value"",
                    ""id"": ""a8f9ebc7-32ae-4696-b1e7-ef24c43d80ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PlayerSpecial Attack"",
                    ""type"": ""Button"",
                    ""id"": ""b9a6496e-2367-4aa3-a1e5-0c73429c7f18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Button"",
                    ""id"": ""0485588a-b5ee-4fbc-91a5-7715d7e0a90c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Switch"",
                    ""type"": ""Button"",
                    ""id"": ""218dd42a-e25d-495a-800c-88d0f9fad91a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Meteorite"",
                    ""type"": ""Button"",
                    ""id"": ""9db291d6-ed9f-40d9-882a-9fe9aa028d1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChocWave"",
                    ""type"": ""Button"",
                    ""id"": ""679814ab-779a-4e63-a702-310c790b2e49"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""3D Vector"",
                    ""id"": ""93e5913b-68c4-4821-a249-190299a45bd6"",
                    ""path"": ""3DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a0b3967c-3a14-4549-9de4-e38f3df22620"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7ad3a6ee-c2e4-463f-aa0e-f7e233952ecc"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ca7d972b-3e5c-49a5-9df9-a31dd43dc4a4"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""forward"",
                    ""id"": ""7bcecbde-62db-4edc-97aa-f49b9a15ea64"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""814aa370-d041-4f4d-8b29-992d214baa61"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""backward"",
                    ""id"": ""2f5885e5-a5e7-4941-8046-c45da5b48452"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""30527c8f-92b6-4d79-ab63-16717136690c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2d2df7c-5d9d-4d69-b7be-77d5a52757c0"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerSpecial Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Rotation_Circle"",
                    ""id"": ""de36052b-3bc0-4919-8f70-a0cfb0583d65"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""5ece3899-b8a7-457a-9610-7c5bcc45b5fe"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CircularScheme"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""e8bfd145-f3b4-46e7-8ec6-a4cf6572143d"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CircularScheme"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Switch_Circle"",
                    ""id"": ""e2aa9cef-3a0e-4c6c-a80e-990bd31ce729"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e5c31516-0ca4-4687-bd51-8509964445e6"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CircularScheme"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d65bdaef-d64d-4d02-82e2-2541e6cd914c"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CircularScheme"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9194bcd4-9c4a-4a2c-b77c-97be63814d1c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Meteorite"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b32aa6c7-d599-450c-bee3-7a10daa37b6c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChocWave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""CircularScheme"",
            ""bindingGroup"": ""CircularScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Circle
        m_Circle = asset.FindActionMap("Circle", throwIfNotFound: true);
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_PlayerMove = m_Player.FindAction("PlayerMove", throwIfNotFound: true);
        m_Player_PlayerAttack = m_Player.FindAction("PlayerAttack", throwIfNotFound: true);
        m_Player_PlayerSpecialAttack = m_Player.FindAction("PlayerSpecial Attack", throwIfNotFound: true);
        m_Player_Rotation = m_Player.FindAction("Rotation", throwIfNotFound: true);
        m_Player_Switch = m_Player.FindAction("Switch", throwIfNotFound: true);
        m_Player_Meteorite = m_Player.FindAction("Meteorite", throwIfNotFound: true);
        m_Player_ChocWave = m_Player.FindAction("ChocWave", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Circle
    private readonly InputActionMap m_Circle;
    private ICircleActions m_CircleActionsCallbackInterface;
    public struct CircleActions
    {
        private @PlayerInput m_Wrapper;
        public CircleActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_Circle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CircleActions set) { return set.Get(); }
        public void SetCallbacks(ICircleActions instance)
        {
            if (m_Wrapper.m_CircleActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_CircleActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public CircleActions @Circle => new CircleActions(this);

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_PlayerMove;
    private readonly InputAction m_Player_PlayerAttack;
    private readonly InputAction m_Player_PlayerSpecialAttack;
    private readonly InputAction m_Player_Rotation;
    private readonly InputAction m_Player_Switch;
    private readonly InputAction m_Player_Meteorite;
    private readonly InputAction m_Player_ChocWave;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerMove => m_Wrapper.m_Player_PlayerMove;
        public InputAction @PlayerAttack => m_Wrapper.m_Player_PlayerAttack;
        public InputAction @PlayerSpecialAttack => m_Wrapper.m_Player_PlayerSpecialAttack;
        public InputAction @Rotation => m_Wrapper.m_Player_Rotation;
        public InputAction @Switch => m_Wrapper.m_Player_Switch;
        public InputAction @Meteorite => m_Wrapper.m_Player_Meteorite;
        public InputAction @ChocWave => m_Wrapper.m_Player_ChocWave;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @PlayerMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerMove;
                @PlayerMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerMove;
                @PlayerMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerMove;
                @PlayerAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerAttack;
                @PlayerAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerAttack;
                @PlayerAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerAttack;
                @PlayerSpecialAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerSpecialAttack;
                @PlayerSpecialAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerSpecialAttack;
                @PlayerSpecialAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerSpecialAttack;
                @Rotation.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Switch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                @Switch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                @Switch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                @Meteorite.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMeteorite;
                @Meteorite.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMeteorite;
                @Meteorite.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMeteorite;
                @ChocWave.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChocWave;
                @ChocWave.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChocWave;
                @ChocWave.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChocWave;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayerMove.started += instance.OnPlayerMove;
                @PlayerMove.performed += instance.OnPlayerMove;
                @PlayerMove.canceled += instance.OnPlayerMove;
                @PlayerAttack.started += instance.OnPlayerAttack;
                @PlayerAttack.performed += instance.OnPlayerAttack;
                @PlayerAttack.canceled += instance.OnPlayerAttack;
                @PlayerSpecialAttack.started += instance.OnPlayerSpecialAttack;
                @PlayerSpecialAttack.performed += instance.OnPlayerSpecialAttack;
                @PlayerSpecialAttack.canceled += instance.OnPlayerSpecialAttack;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @Switch.started += instance.OnSwitch;
                @Switch.performed += instance.OnSwitch;
                @Switch.canceled += instance.OnSwitch;
                @Meteorite.started += instance.OnMeteorite;
                @Meteorite.performed += instance.OnMeteorite;
                @Meteorite.canceled += instance.OnMeteorite;
                @ChocWave.started += instance.OnChocWave;
                @ChocWave.performed += instance.OnChocWave;
                @ChocWave.canceled += instance.OnChocWave;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_CircularSchemeSchemeIndex = -1;
    public InputControlScheme CircularSchemeScheme
    {
        get
        {
            if (m_CircularSchemeSchemeIndex == -1) m_CircularSchemeSchemeIndex = asset.FindControlSchemeIndex("CircularScheme");
            return asset.controlSchemes[m_CircularSchemeSchemeIndex];
        }
    }
    public interface ICircleActions
    {
    }
    public interface IPlayerActions
    {
        void OnPlayerMove(InputAction.CallbackContext context);
        void OnPlayerAttack(InputAction.CallbackContext context);
        void OnPlayerSpecialAttack(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnSwitch(InputAction.CallbackContext context);
        void OnMeteorite(InputAction.CallbackContext context);
        void OnChocWave(InputAction.CallbackContext context);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Player1.inputactions
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

namespace CarInput
{
    public partial class @Player1: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Player1()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player1"",
    ""maps"": [
        {
            ""name"": ""TinyCarInputSystem"",
            ""id"": ""a2c501c7-d45f-4ec4-8323-ae2f2f233db5"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a032fcc1-387f-4d40-97f8-7a9d8b49f7c7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Boost"",
                    ""type"": ""Button"",
                    ""id"": ""7757cfbd-8830-4375-b4c2-45e22e935b52"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Move2"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4c76121f-7cad-4d5c-86db-4d89a1ec92ce"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Button"",
                    ""id"": ""601d040c-20f7-43df-ba0d-2ca8c819a060"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""67f15379-4c39-493f-b7ac-22417d0b0926"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Button"",
                    ""id"": ""1a6ef773-2371-46fd-9f5a-6c60d40e4378"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8781165a-b1b2-4926-9497-2c9a9e3f3d76"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60790be1-7b8b-463d-845e-ee6f647e8dc9"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Move2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrow"",
                    ""id"": ""694a602a-29a6-49aa-bf80-9e720cef51cc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move2"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c95e1f61-5ff7-40c8-bc63-d3572a59e08e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard"",
                    ""action"": ""Move2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5de08a14-e9e0-46d7-ae83-d3dc09332653"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard"",
                    ""action"": ""Move2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""5d898047-230c-4563-b4b8-441e7c8a7992"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard"",
                    ""action"": ""Move2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""49f8d47f-6e66-4acf-840d-f0cb624f1e07"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard"",
                    ""action"": ""Move2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""22cbe470-2a22-4da3-a984-cc6e1f24aa5b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""4891ed14-e2ba-4bb6-9b88-fb35940a1bad"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""28624a7a-a5b9-4bb1-80c9-c3e833e2663e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4b73dd32-3c6b-4273-b739-92ead110c63b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""b3e9e075-083e-4c82-bb86-f83dff6b3e40"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""7d442f9e-d964-47c6-b183-170d4e02ab08"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2440812a-e2c1-4a61-8560-140405f4a601"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d77c925f-ce50-4c6b-a0d9-efaf39ad1d0d"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e91c0c28-d0f0-4406-8d87-34dd4a8184d1"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ca105b6-0376-4bc0-b7e9-7645e78c771e"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87b966e6-c08d-4155-a63a-9ace4aa13111"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
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
            // TinyCarInputSystem
            m_TinyCarInputSystem = asset.FindActionMap("TinyCarInputSystem", throwIfNotFound: true);
            m_TinyCarInputSystem_Move = m_TinyCarInputSystem.FindAction("Move", throwIfNotFound: true);
            m_TinyCarInputSystem_Boost = m_TinyCarInputSystem.FindAction("Boost", throwIfNotFound: true);
            m_TinyCarInputSystem_Move2 = m_TinyCarInputSystem.FindAction("Move2", throwIfNotFound: true);
            m_TinyCarInputSystem_Accelerate = m_TinyCarInputSystem.FindAction("Accelerate", throwIfNotFound: true);
            m_TinyCarInputSystem_Restart = m_TinyCarInputSystem.FindAction("Restart", throwIfNotFound: true);
            m_TinyCarInputSystem_Brake = m_TinyCarInputSystem.FindAction("Brake", throwIfNotFound: true);
        }

        ~@Player1()
        {
            UnityEngine.Debug.Assert(!m_TinyCarInputSystem.enabled, "This will cause a leak and performance issues, Player1.TinyCarInputSystem.Disable() has not been called.");
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

        // TinyCarInputSystem
        private readonly InputActionMap m_TinyCarInputSystem;
        private List<ITinyCarInputSystemActions> m_TinyCarInputSystemActionsCallbackInterfaces = new List<ITinyCarInputSystemActions>();
        private readonly InputAction m_TinyCarInputSystem_Move;
        private readonly InputAction m_TinyCarInputSystem_Boost;
        private readonly InputAction m_TinyCarInputSystem_Move2;
        private readonly InputAction m_TinyCarInputSystem_Accelerate;
        private readonly InputAction m_TinyCarInputSystem_Restart;
        private readonly InputAction m_TinyCarInputSystem_Brake;
        public struct TinyCarInputSystemActions
        {
            private @Player1 m_Wrapper;
            public TinyCarInputSystemActions(@Player1 wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_TinyCarInputSystem_Move;
            public InputAction @Boost => m_Wrapper.m_TinyCarInputSystem_Boost;
            public InputAction @Move2 => m_Wrapper.m_TinyCarInputSystem_Move2;
            public InputAction @Accelerate => m_Wrapper.m_TinyCarInputSystem_Accelerate;
            public InputAction @Restart => m_Wrapper.m_TinyCarInputSystem_Restart;
            public InputAction @Brake => m_Wrapper.m_TinyCarInputSystem_Brake;
            public InputActionMap Get() { return m_Wrapper.m_TinyCarInputSystem; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(TinyCarInputSystemActions set) { return set.Get(); }
            public void AddCallbacks(ITinyCarInputSystemActions instance)
            {
                if (instance == null || m_Wrapper.m_TinyCarInputSystemActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_TinyCarInputSystemActionsCallbackInterfaces.Add(instance);
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Boost.started += instance.OnBoost;
                @Boost.performed += instance.OnBoost;
                @Boost.canceled += instance.OnBoost;
                @Move2.started += instance.OnMove2;
                @Move2.performed += instance.OnMove2;
                @Move2.canceled += instance.OnMove2;
                @Accelerate.started += instance.OnAccelerate;
                @Accelerate.performed += instance.OnAccelerate;
                @Accelerate.canceled += instance.OnAccelerate;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
                @Brake.started += instance.OnBrake;
                @Brake.performed += instance.OnBrake;
                @Brake.canceled += instance.OnBrake;
            }

            private void UnregisterCallbacks(ITinyCarInputSystemActions instance)
            {
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
                @Boost.started -= instance.OnBoost;
                @Boost.performed -= instance.OnBoost;
                @Boost.canceled -= instance.OnBoost;
                @Move2.started -= instance.OnMove2;
                @Move2.performed -= instance.OnMove2;
                @Move2.canceled -= instance.OnMove2;
                @Accelerate.started -= instance.OnAccelerate;
                @Accelerate.performed -= instance.OnAccelerate;
                @Accelerate.canceled -= instance.OnAccelerate;
                @Restart.started -= instance.OnRestart;
                @Restart.performed -= instance.OnRestart;
                @Restart.canceled -= instance.OnRestart;
                @Brake.started -= instance.OnBrake;
                @Brake.performed -= instance.OnBrake;
                @Brake.canceled -= instance.OnBrake;
            }

            public void RemoveCallbacks(ITinyCarInputSystemActions instance)
            {
                if (m_Wrapper.m_TinyCarInputSystemActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(ITinyCarInputSystemActions instance)
            {
                foreach (var item in m_Wrapper.m_TinyCarInputSystemActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_TinyCarInputSystemActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public TinyCarInputSystemActions @TinyCarInputSystem => new TinyCarInputSystemActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        public interface ITinyCarInputSystemActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnBoost(InputAction.CallbackContext context);
            void OnMove2(InputAction.CallbackContext context);
            void OnAccelerate(InputAction.CallbackContext context);
            void OnRestart(InputAction.CallbackContext context);
            void OnBrake(InputAction.CallbackContext context);
        }
    }
}

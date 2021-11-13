// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Jump/New Controls 1.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @NewControls1 : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @NewControls1()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""New Controls 1"",
    ""maps"": [
        {
            ""name"": ""Sheep (player)"",
            ""id"": ""0ab60e1d-f056-424d-a138-07be6e7dd366"",
            ""actions"": [
                {
                    ""name"": ""right"",
                    ""type"": ""Button"",
                    ""id"": ""7cd1f13e-f76d-4cb4-ad4f-8d08d10d6a67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""left"",
                    ""type"": ""Button"",
                    ""id"": ""e47434a5-2a44-40b4-ad56-113faf334640"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""sheild"",
                    ""type"": ""Button"",
                    ""id"": ""75240a71-2e77-4a26-a1aa-a96af0a076d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a5d2270c-cc05-4690-a7ad-8672f88e92c8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""arrow keys and space bar"",
                    ""action"": ""right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f4e2202-35f5-42fa-872b-4e0c9de5b7e7"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""arrow keys and space bar"",
                    ""action"": ""left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a41f4e97-fe29-4538-aeb0-52d55c6dda6b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""arrow keys and space bar"",
                    ""action"": ""sheild"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""arrow keys and space bar"",
            ""bindingGroup"": ""arrow keys and space bar"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Sheep (player)
        m_Sheepplayer = asset.FindActionMap("Sheep (player)", throwIfNotFound: true);
        m_Sheepplayer_right = m_Sheepplayer.FindAction("right", throwIfNotFound: true);
        m_Sheepplayer_left = m_Sheepplayer.FindAction("left", throwIfNotFound: true);
        m_Sheepplayer_sheild = m_Sheepplayer.FindAction("sheild", throwIfNotFound: true);
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

    // Sheep (player)
    private readonly InputActionMap m_Sheepplayer;
    private ISheepplayerActions m_SheepplayerActionsCallbackInterface;
    private readonly InputAction m_Sheepplayer_right;
    private readonly InputAction m_Sheepplayer_left;
    private readonly InputAction m_Sheepplayer_sheild;
    public struct SheepplayerActions
    {
        private @NewControls1 m_Wrapper;
        public SheepplayerActions(@NewControls1 wrapper) { m_Wrapper = wrapper; }
        public InputAction @right => m_Wrapper.m_Sheepplayer_right;
        public InputAction @left => m_Wrapper.m_Sheepplayer_left;
        public InputAction @sheild => m_Wrapper.m_Sheepplayer_sheild;
        public InputActionMap Get() { return m_Wrapper.m_Sheepplayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SheepplayerActions set) { return set.Get(); }
        public void SetCallbacks(ISheepplayerActions instance)
        {
            if (m_Wrapper.m_SheepplayerActionsCallbackInterface != null)
            {
                @right.started -= m_Wrapper.m_SheepplayerActionsCallbackInterface.OnRight;
                @right.performed -= m_Wrapper.m_SheepplayerActionsCallbackInterface.OnRight;
                @right.canceled -= m_Wrapper.m_SheepplayerActionsCallbackInterface.OnRight;
                @left.started -= m_Wrapper.m_SheepplayerActionsCallbackInterface.OnLeft;
                @left.performed -= m_Wrapper.m_SheepplayerActionsCallbackInterface.OnLeft;
                @left.canceled -= m_Wrapper.m_SheepplayerActionsCallbackInterface.OnLeft;
                @sheild.started -= m_Wrapper.m_SheepplayerActionsCallbackInterface.OnSheild;
                @sheild.performed -= m_Wrapper.m_SheepplayerActionsCallbackInterface.OnSheild;
                @sheild.canceled -= m_Wrapper.m_SheepplayerActionsCallbackInterface.OnSheild;
            }
            m_Wrapper.m_SheepplayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @right.started += instance.OnRight;
                @right.performed += instance.OnRight;
                @right.canceled += instance.OnRight;
                @left.started += instance.OnLeft;
                @left.performed += instance.OnLeft;
                @left.canceled += instance.OnLeft;
                @sheild.started += instance.OnSheild;
                @sheild.performed += instance.OnSheild;
                @sheild.canceled += instance.OnSheild;
            }
        }
    }
    public SheepplayerActions @Sheepplayer => new SheepplayerActions(this);
    private int m_arrowkeysandspacebarSchemeIndex = -1;
    public InputControlScheme arrowkeysandspacebarScheme
    {
        get
        {
            if (m_arrowkeysandspacebarSchemeIndex == -1) m_arrowkeysandspacebarSchemeIndex = asset.FindControlSchemeIndex("arrow keys and space bar");
            return asset.controlSchemes[m_arrowkeysandspacebarSchemeIndex];
        }
    }
    public interface ISheepplayerActions
    {
        void OnRight(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnSheild(InputAction.CallbackContext context);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Events

        public delegate void StartDraw();
        public event StartDraw OnStartDraw;

        public delegate void EndDraw();
        public event EndDraw OnEndDraw;

        public delegate void StartErase();
        public event StartErase OnStartErase;

        public delegate void EndErase();
        public event EndErase OnEndErase;

    #endregion


    private MouseControls mousecontrols;

    private void Awake()
    {
        mousecontrols = new MouseControls();
    }

    private void OnEnable()
    {
        mousecontrols.Enable();
    }

    private void OnDisable()
    {
        mousecontrols.Disable();
    }

    void Start()
    {
        mousecontrols.Mouse.Click.started += _ => { if(OnStartDraw != null) OnStartDraw(); };
        mousecontrols.Mouse.Click.canceled += _ => { if(OnEndDraw != null ) OnEndDraw (); };
        mousecontrols.Mouse.Erase.started += _ => { if(OnStartErase != null) OnStartErase(); };
        mousecontrols.Mouse.Erase.canceled += _ => { if(OnEndErase != null) OnEndErase(); };

        Cursor.lockState = CursorLockMode.Confined;
    }

    public float GetZoom()
    {
        return mousecontrols.Mouse.Zoom.ReadValue<float>();
    }
    public Vector2 GetmousePosition()
    {
        return mousecontrols.Mouse.Positions.ReadValue<Vector2>();
    }
}


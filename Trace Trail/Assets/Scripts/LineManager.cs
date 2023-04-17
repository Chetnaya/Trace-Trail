using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class LineManager : MonoBehaviour
{
    private InputManager inputmanager;

    [SerializeField]
    private float lineSeperationDistance = .2f;

    [SerializeField]
    private float lineWidth = .1f;

    [SerializeField]
    private Color lineColor = Color.black;

    [SerializeField]
    private int lineCapVertices = 5;

    #region  Private

    // Keep track of lines 
    private List<GameObject> lines;
    private List<Vector2> currentLine;
    private LineRenderer currentLineRenderer;
    private EdgeCollider2D currentLineEdgeCollider;

    private bool drawing = false;
    private bool erasing = false;

    private Camera MainCamera;

    // private InputManager inputmanager;

    #endregion

    private void Awake()
    {
        inputmanager = GetComponent<InputManager>();
        MainCamera = Camera.main;
    }

    private void OnEnable()
    {
        // Subscribing to the event
        inputmanager.OnStartDraw += OnStartDraw ;
        inputmanager.OnEndDraw += OnEndDraw ;
        inputmanager.OnStartErase += OnStartErase ;
        inputmanager.OnEndErase += OnEndErase ;
    }

    private void OnDisable()
    {
        // Unsubscribing to the event
        inputmanager.OnStartDraw -= OnStartDraw ;
        inputmanager.OnEndDraw -= OnEndDraw ;
        inputmanager.OnStartErase -= OnStartErase ;
        inputmanager.OnEndErase -= OnEndErase ;
    }

    private void OnStartDraw()
    {
        StartCoroutine("Drawing");
    }

    private void OnEndDraw()
    {

    }

    private void OnStartErase()
    {

    }

    private void OnEndErase()
    {

    }

    IEnumerator Drawing()
    {
        drawing = true;

        StartLine();
        
        while(drawing)
        {
            // AddPoint(GetCurrentWorldPoint());
            yield return null;
        }

        // Endline();
    }
    private void StartLine()
    {
        currentLine = new List<Vector2>();
        GameObject currentLineObject = new GameObject();
        currentLineObject.name = "Line";
        currentLineObject.transform.parent = transform;
        currentLineRenderer = currentLineObject.AddComponent<LineRenderer>();
        currentLineEdgeCollider = currentLineObject.AddComponent<EdgeCollider2D>();
    }

    private Vector2 GetCurrentWorldPoint()
    {
        return MainCamera.ScreenToWorldPoint(inputmanager.GetmousePosition());
    }


}

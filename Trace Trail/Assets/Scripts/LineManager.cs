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
    //The more vertices at the end of the line, the smoother the curve will be at the end
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
            AddPoint(GetCurrentWorldPoint());
            yield return null;
        }
        // Endline();
    }

    private void StartLine()
    {
        //Initiating the line
        currentLine = new List<Vector2>();
        GameObject currentLineObject = new GameObject();
        currentLineObject.name = "Line";
        currentLineObject.transform.parent = transform;
        currentLineRenderer = currentLineObject.AddComponent<LineRenderer>();
        currentLineEdgeCollider = currentLineObject.AddComponent<EdgeCollider2D>();

        //Set settings
        currentLineRenderer.positionCount = 0;
        currentLineRenderer.startWidth = lineWidth;
        currentLineRenderer.endWidth = lineWidth;
        currentLineRenderer.numcapvertices = lineCapVertices;
        //We need material for color change to show
        currentLineRenderer.material = new Material (Shader.Find("Particles/Standard Unlit")); 
        currentLineRenderer.startColor = lineColor;
        currentLineRenderer.endColor = lineColor;
        //Setting Edge Radius of the Edge collider
        currentLineEdgeCollider.edgeRadius = .1f;
    }

    private Vector2 GetCurrentWorldPoint()
    {
        return MainCamera.ScreenToWorldPoint(inputmanager.GetmousePosition());
    }

    private void AddPoint(Vector2 point )
    {
        if(PlacePoint(point))
        {
            currentLine.Add(point);
            currentLineRenderer.positionCount++;
            currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, point);
        }
    }

    private bool PlacePoint(Vector2 point)
    {
        if(currentLine.Count == 0)
        {
            return true;
        }
        if(Vector2.Distance(point, currentLine[currentLine.Count - 1]) < lineSeperationDistance)
        {
            return false;
        }
        else 
        {
            return true;
        }
    }
}

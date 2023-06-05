using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class LineManager : MonoBehaviour
{
    private InputManager inputmanager;

    [SerializeField]
    private Player player;

    [SerializeField]
    private float lineSeperationDistance = .2f;

    [SerializeField]
    private float effectorSpeed = 2f;

    [SerializeField]
    private float lineWidth = .1f;

    [SerializeField]
    private Color lineColor = Color.black;

    [SerializeField]
    private PhysicsMaterial2D physicsmaterial2d;

    [SerializeField]
    //The more vertices at the end of the line, the smoother the curve will be at the end
    private int lineCapVertices = 5;


    #region  Private

    // Keep track of lines 
    private List<GameObject> lines;
    private List<Vector2> currentLine;
    private LineRenderer currentLineRenderer;
    private EdgeCollider2D currentLineEdgeCollider;
    //

    private GameObject currentLineObject;

    private bool drawing = false;
    private bool erasing = false;

    private Camera MainCamera;

    private Panning panning;

    #endregion
    private void Awake()
    {
        inputmanager = GetComponent<InputManager>();
        MainCamera = Camera.main;
        panning = GetComponent<Panning>();
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
    /*------------------------------------------------------
    ---------------------DRAWING----------------------------
    ------------------------------------------------------- */
    private void OnStartDraw()
    {
        if(!erasing)
        {
            StartCoroutine("Drawing");
        }
    }

    private void OnEndDraw()
    {
        drawing = false;
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
        Endline();
    }

    private void StartLine()
    {
        //---------------------Initiating the line----------------------------------
        currentLine = new List<Vector2>();
        currentLineObject = new GameObject();
        currentLineObject.name = "Line";
        currentLineObject.transform.parent = transform;
        currentLineRenderer = currentLineObject.AddComponent<LineRenderer>();
        currentLineEdgeCollider = currentLineObject.AddComponent<EdgeCollider2D>();
        //Adding surface effector component for the line
        SurfaceEffector2D curreentEffector = currentLineObject.AddComponent<SurfaceEffector2D>();


        //----------------Set settings--------------------------------------------------
        currentLineRenderer.positionCount = 0;
        currentLineRenderer.startWidth = lineWidth;
        currentLineRenderer.endWidth = lineWidth;
        currentLineRenderer.numCapVertices = lineCapVertices;
        //We need material for color change to show
        currentLineRenderer.material = new Material (Shader.Find("Particles/Standard Unlit")); 
        currentLineRenderer.startColor = lineColor;
        currentLineRenderer.endColor = lineColor;
        //Setting Edge Radius of the Edge collider
        currentLineEdgeCollider.edgeRadius = .1f;

        currentLineEdgeCollider.sharedMaterial = physicsmaterial2d; 
 
        currentLineObject.layer = 1<<3;//100

        curreentEffector.speed =  effectorSpeed;
        currentLineEdgeCollider.usedByEffector = true;
    }

    private void Endline()
    //When line ends, set those points in the edge collider
    {
        if(currentLine.Count == 1 )
        {
            DestroyLine(currentLineObject);
        }
        else
        {
            currentLineEdgeCollider.SetPoints(currentLine);
        }
        
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
    /*------------------------------------------------------
    ---------------------ERASING----------------------------
    ------------------------------------------------------- */
    private void OnStartErase()
    {
        if(!drawing)
        {
            StartCoroutine("Erasing");
        }
    }

    private void OnEndErase()
    {
        erasing =  false;
    }

    IEnumerator Erasing()
    {
        erasing = true;
        while(erasing)
        {
            Vector2 screenMousePosition = GetCurrentScreenPoint();
            GameObject g = Utils.Raycast(MainCamera, screenMousePosition, 1<<8);//Passing in the layerMask, Left shift 8 times as out layer is set to 8 in unity.
            if( g != null)
            {
                DestroyLine(g);
            }
            yield return null;
        }

    }

    private void DestroyLine(GameObject g)
    {
        //Destroy line code
        Destroy(g);
    }

    private Vector2 GetCurrentScreenPoint()
    {
        return inputmanager.GetmousePosition();
    }

    /*------------------------------------------------------
    --------------------------------------------------------
    ------------------------------------------------------- */
    
    private Vector2 GetCurrentWorldPoint()
    {
        return MainCamera.ScreenToWorldPoint(inputmanager.GetmousePosition());
    }

    /*------------------------------------------------------
    --------------------------------------------------------
    ------------------------------------------------------- */
    private void Update()
    {
        if(!player.playing){
            panning.PanScreen(GetCurrentScreenPoint());
        }
    }
}


using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerControls playercontrols;
    private Rigidbody2D rb;
    private Vector3 startingPosition;
    private Quaternion startingRotation;

    private CameraFollow camerafollow;

    private bool playing = false;

    private void Awake()
    {
        camerafollow = Camera.main.GetComponent<CameraFollow>();
        playercontrols = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void OnEnable()
    {
        playercontrols.Enable();
    }

    private void OnDisable()
    {
        playercontrols.Disable();
    }

    void Start()
    {
        playercontrols.Player.Space.performed +=_=> StartGame();   
    }

    private void StartGame()
    {
        playing = !playing;

        if(playing)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;    
            camerafollow.enabled = true;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
            transform.position = startingPosition;
            transform.rotation = startingRotation;
            camerafollow.enabled = false;
        }
    }
}

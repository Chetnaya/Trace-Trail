using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerControls playercontrols;
    private Rigidbody2D rb;
    private Vector3 startingPosition;

    private bool playing = false;


    private void Awake()
    {
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
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
            transform.position = startingPosition;
        }
    }
}

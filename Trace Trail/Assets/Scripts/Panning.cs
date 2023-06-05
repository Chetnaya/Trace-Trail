using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panning : MonoBehaviour
{
   [SerializeField]
   private float panSpeed = 2f;
   private Transform mainCamera;

   private void Awake()
   {
    mainCamera = Camera.main.transform;
   }

   private Vector2 PanDirection(Vector2 mouseScreenPosition)
   {
    Vector2 direction = Vector2.zero;
    if(mouseScreenPosition.y >= Screen.height * .95f)
    {
        direction.y += 1;
    }
    else if(mouseScreenPosition.y <= Screen.height * .05f)
    {
        direction.y -= 1;
    }

    if(mouseScreenPosition.x >= Screen.width * .95f)
    {
        direction.x += 1;
    }
    else if(mouseScreenPosition.x <= Screen.width * .05f)
    {
        direction.x -= 1;
    }
    return direction;
   }

   public void PanScreen(Vector2 mouseScreenPosition)
   {
    Vector2 direction = PanDirection(mouseScreenPosition);
    mainCamera.position = Vector3.Lerp(mainCamera.position, (Vector3)direction + mainCamera.position, Time.deltaTime * panSpeed);
   }
}

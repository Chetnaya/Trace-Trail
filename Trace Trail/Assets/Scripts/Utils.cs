using UnityEngine;
// A script for all the utility functions
public static class Utils
{
    //Utility function for RAYCASTING
    public static GameObject Raycast(Camera mainCamera, Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

        if(hit2D.collider != null) 
        {
            return hit2D.collider.gameObject;

        }
        else
        {
            return null;
        }

    }
}

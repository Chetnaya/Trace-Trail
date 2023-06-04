using UnityEngine;
// A script for all the utility functions
public static class Utils
{
    //Utility function for RAYCASTING
    public static GameObject Raycast(Camera mainCamera, Vector2 screenPosition, int layer)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layer);

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

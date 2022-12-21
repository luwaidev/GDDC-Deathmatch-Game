using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLog : MonoBehaviour
{
    public List<GameObject> visibleGameObjects = new List<GameObject>();

    void Update()
    {
        FindObjects();
    }

    void FindObjects()
    {
        GameObject[] detectables = GameObject.FindGameObjectsWithTag("Detectable");

        visibleGameObjects.Clear();
        foreach (GameObject detetectable in detectables)
        {
            if (IsVisible(detetectable.GetComponent<Renderer>()))
            {
                visibleGameObjects.Add(detetectable);
            }
        }
    }

    bool IsVisible(Renderer renderer)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TapScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello there");
    }

    [SerializeField]
    ARRaycastManager m_RaycastManager;
    
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    
    void Update()
    {
        if (Input.touchCount == 0)
            return;
    
        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            // Only returns true if there is at least one hit
            Debug.Log("hit something");
        }
    }
}

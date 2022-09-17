using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceBlockScript : MonoBehaviour
{

    public bool placingBlock = false;

    
    //Remove all reference points created
    public void RemoveAllReferencePoints()
    {
        foreach (var referencePoint in m_ARAnchor)
        {
            m_AnchorManager.RemoveAnchor(referencePoint);
        }
        m_ARAnchor.Clear();
    }
    
    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_AnchorManager = GetComponent<ARAnchorManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        m_ARAnchor = new List<ARAnchor>();
        placingBlock = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (placingBlock)
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;
                TrackableId planeId = s_Hits[0].trackableId; //get the ID of the plane hit by the raycast
                var arAnchor = m_AnchorManager.AttachAnchor(m_PlaneManager.GetPlane(planeId), hitPose);
                if (arAnchor != null)
                {
                    RemoveAllReferencePoints();
                    m_ARAnchor.Add(arAnchor);
                }
            }

            placingBlock = false;
        }
    }
    
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    public void startPlaceBlock()
    {
        // placingBlock = true;


    }
    
    
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
    ARAnchorManager m_AnchorManager;
    List<ARAnchor> m_ARAnchor; 
    ARPlaneManager m_PlaneManager;

}

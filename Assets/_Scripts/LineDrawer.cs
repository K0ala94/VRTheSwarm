using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineDrawer : MonoBehaviour {

    private List<Vector3> points = new List<Vector3>();
    private int pointCount = 0;
    private LineRenderer lineRenderer;
    private bool shouldDraw = false;

    public void addPointOnDrag(BaseEventData eventData){
        PointerEventData pointerData = (PointerEventData)eventData;

        if (shouldDraw)
        {
            points.Add(pointerData.pointerCurrentRaycast.worldPosition);
            lineRenderer.positionCount = pointCount++;
            lineRenderer.SetPositions(points.ToArray());
        }
       
    }

    public void toggleShouldDraw(BaseEventData bed)
    {
        if (shouldDraw)
            shouldDraw = false;
        else
            shouldDraw = true;

        
    }

	void Start () {
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth=0.02f;
        lineRenderer.endWidth = 0.02f;
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = Color.black;
        lineRenderer.material = mat;
	}

	void Update () {
		
	}
}

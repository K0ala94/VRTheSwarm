using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineDrawer : MonoBehaviour {

    private List<Vector3> points = new List<Vector3>();
    private int pointCount = 0;
    private LineRenderer lineRenderer;
    private bool shouldDraw = false;
    private GameManager gameManager;

    public void addPointOnDrag(BaseEventData eventData){
        PointerEventData pointerData = (PointerEventData)eventData;

        if (shouldDraw && !gameManager.Phase2)
        {
            points.Add(pointerData.pointerCurrentRaycast.worldPosition);
            lineRenderer.positionCount = pointCount++;
            lineRenderer.SetPositions(points.ToArray());
        }
       
    }

    public void toggleShouldDraw()
    {
        if (shouldDraw)
            shouldDraw = false;
        else
            shouldDraw = true;
 
    }

    public void onExitTest()
    {
        GameObject.Find("Player").GetComponent<Rigidbody>().AddForce(Vector3.up * 200);
    }

	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth=0.01f;
        lineRenderer.endWidth = 0.01f;
        //lineRenderer.alignment = LineAlignment.Local;
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = Color.black;
        lineRenderer.material = mat;
	}

    public void resetLineDrawer()
    {
        points = new List<Vector3>();
        pointCount = 0;
        lineRenderer.positionCount = pointCount++;
        lineRenderer.SetPositions(points.ToArray());
    }

	void Update () {
		
	}
}

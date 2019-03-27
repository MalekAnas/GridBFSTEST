using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridSize = 10;
    Vector2Int gridPos;
    public bool isExplored = false;
    public bool isReserved = false;
    public bool pathFound = false;
    public Waypoint exploredFrom;

    [HideInInspector]
    public Color currentColor;



    bool mouseClicked = false;
    bool mouseReleased = false;
    bool mouseOver = false;

    Color startColor = Color.red;
    Color selectedColor = Color.blue;



















    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClicked = true;
            mouseOver = false;

        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseReleased = true;
            mouseOver = false;
        }


    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x / gridSize), Mathf.RoundToInt(transform.position.z / gridSize));
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    void SetColorOFWayPoint(Color color, Waypoint wayPoint)
    {
        wayPoint.SetTopColor(color);
    }



    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
        currentColor = color;
    }

    void OnMouseOver()
    {


        mouseOver = true;
        if (mouseOver && !mouseClicked && !mouseReleased)
        {
            MeshRenderer topMeshRenderer = gameObject.transform.Find("top").GetComponent<MeshRenderer>();
            topMeshRenderer.material.color = Color.blue;

        }




    }



    private void OnMouseExit()
    {
        mouseOver = false;
        if (!mouseReleased && !mouseClicked && !mouseOver)
        {
            MeshRenderer topMeshRenderer = gameObject.transform.Find("top").GetComponent<MeshRenderer>();
            topMeshRenderer.material.color = startColor;
        }






    }




}

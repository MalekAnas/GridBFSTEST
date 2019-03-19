using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]


public class GridMaker : MonoBehaviour
{







    Waypoint block;

    private void Awake()
    {
        block = GetComponent<Waypoint>();
    }


    // Start is called before the first frame update
    void Start()
    {


    }


    


    // Update is called once per frame
    void Update()
    {
        snapToGrid();
        updateLabel();
    }

    private void snapToGrid()
    {
        int gridSize = block.GetGridSize();
        transform.position = new Vector3(block.GetGridPos().x * gridSize, 0f, block.GetGridPos().y * gridSize);
    }

    private void updateLabel()
    {
        int gridSize = block.GetGridSize();
        TextMesh cubeTextMesh = GetComponentInChildren<TextMesh>();
        string labelText = block.GetGridPos().x + "," + block.GetGridPos().y;
        cubeTextMesh.text = labelText;
        gameObject.name = "Cube : " + labelText;
    }



}

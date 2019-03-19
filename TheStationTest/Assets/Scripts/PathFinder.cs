using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    public Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    public Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    public Waypoint searchCenter;
    
    public List<List<Waypoint>> paths = new List<List<Waypoint>>();

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };


    private void Start()
    {
        LoadBlocks();
        //CalculatePath();
        //ColorThePath(path);
    }

    private void Update()
    {
        ClickToSelectStartPoint();
        ClickToSelectEndPoint();
        
       // CalculatePath();
        // ColorThePath(path);
        
    }

    void ColorThePath(List<Waypoint> path)
    {
        foreach (Waypoint wp in path)
        {
            wp.SetTopColor(Color.cyan);
        }
    }

    // Start is called before the first frame update

    

    public void CalculatePath()
    {
        
        BreadthFirstSearch();
        paths.Add(CreatePath());
        
    }

    private List<Waypoint> CreatePath()
    {
        
        List<Waypoint> path = new List<Waypoint>();
        
        path.Add(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }
        path.Add(startWaypoint);
        
        path.Reverse();
        ColorThePath(path);

        return path;
    }

    

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            HaltIfEndFound();
            ExploreNeighbours();
            searchCenter.isExplored = true;
        }
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            // do nothing
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    public void ClickToSelectStartPoint()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            
            startWaypoint = null;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.tag == "WayPoint")
                {
                    GameObject block = hit.transform.gameObject;
                    print(block.name + "  Is Clicked");
                    Waypoint startPoint = block.GetComponentInParent<Waypoint>();
                    print(startPoint.name + "This is startpoint");
                    startWaypoint = startPoint;
                    startWaypoint.SetTopColor(Color.green);
                    searchCenter = startWaypoint;
                    searchCenter.exploredFrom = null;


                }

                else
                {
                    //DoNothing
                }





            }

            
        }
        
    }

    public void ClickToSelectEndPoint()
    {
        

        if (Input.GetMouseButtonUp(0))
        {
            endWaypoint = null;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.tag == "WayPoint")
                {
                    GameObject block = hit.transform.gameObject;
                    print(block.name + "  Is Clicked");
                    Waypoint endPoint = block.GetComponentInParent<Waypoint>();
                    print(endPoint.name + "This is End point");
                    endWaypoint = endPoint;
                    endWaypoint.SetTopColor(Color.green);


                }

                else
                {
                    //DoNothing
                }


            }
            CalculatePath();
            
            
        }


       

    }


    public void LoadBlocks()
    {

        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }

}

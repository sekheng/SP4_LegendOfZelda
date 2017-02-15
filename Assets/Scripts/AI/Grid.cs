using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour{

    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void Update()
    {
       //// grid = new Node[gridSizeX, gridSizeY];
       // Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

       // for (int x = 0; x < gridSizeX; ++x)
       // {
       //     for (int y = 0; y < gridSizeY; ++y)
       //     {
       //         Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
       //         //collision check
       //         bool walkable = (Physics2D.OverlapCircle(worldPoint, nodeRadius - 0.2f, unwalkableMask) == null);//check with the layer mask to see which is collidable.radius - offset to make narrow walkway walkable

       //         grid[x, y].m_bWalkable = walkable;
       //     }
       // }
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for(int x = 0; x < gridSizeX; ++x)
        {
            for(int y = 0; y < gridSizeY; ++y)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                //collision check
                bool walkable = (Physics2D.OverlapCircle(worldPoint, nodeRadius-0.2f, unwalkableMask) == null);//check with the layer mask to see which is collidable.radius - offset to make narrow walkway walkable

                grid[x, y] = new Node(walkable, worldPoint,x,y);
            }
        }

    }

    //convert world position to a node
    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        //first convert the position in world point to percentage based on the grid
        float percentX = ((worldPosition.x) + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = ((worldPosition.y) + gridWorldSize.y / 2) / gridWorldSize.y;

        //make sure the percent is in the grid
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        //convert the percentage to a position in the grid
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for(int x = -1; x <= 1; ++x)
        {
            for (int y = -1; y <= 1; ++y)
            {
                if(x == 0 && y == 0)//make sure the if not checking the it self.
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    public List<Node> path;
    void OnDrawGizmos()
    {
        //draw out the grid to debug and see where can be walk to
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));
        if(grid != null)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = Color.red;
      
                if (node.m_bWalkable)//change color if walkable
                {
                    Gizmos.color = Color.white;
                }
                if(path != null)
                {
                    if(path.Contains(node))
                    {
                        Gizmos.color = Color.cyan;
                       // Debug.Log("weeeeeee");
                    }
                }
                Gizmos.DrawCube(node.m_v2_worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class Node {
    public bool m_bWalkable;
    public Vector2 m_v2_worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool zeWalkable, Vector2 zeWoldPos, int x, int y)
    {
        m_bWalkable = zeWalkable;
        m_v2_worldPosition = zeWoldPos;
        gridX = x;
        gridY = y;
    }
    
    public int fCost //A*
    {
        get { return gCost + hCost; }
    }	
}

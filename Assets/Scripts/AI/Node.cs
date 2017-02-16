using UnityEngine;
using System.Collections;
using System;

public class Node: IHeapItem<Node>{
    public bool m_bWalkable;
    public Vector2 m_v2_worldPosition;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public Node parent;

    int heapIndex;


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

    public int HeapIndex
    {
        get { return heapIndex; }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node other)
    {
        int compare = fCost.CompareTo(other.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(other.hCost);
        }
        return -compare;
    }
}

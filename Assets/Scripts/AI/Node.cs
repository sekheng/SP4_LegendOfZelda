using UnityEngine;
using System.Collections;

public class Node {
    public bool m_bWalkable;
    public Vector2 m_v2_worldPosition;


    public Node(bool zeWalkable, Vector2 zeWoldPos)
    {
        m_bWalkable = zeWalkable;
        m_v2_worldPosition = zeWoldPos;
    }
	
}

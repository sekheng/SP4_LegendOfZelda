using UnityEngine;
using System.Collections;

public class Node {
    public bool m_bWalkable;
    public Vector3 m_v3_worldPosition;


    public Node(bool zeWalkable, Vector3 zeWoldPos)
    {
        m_bWalkable = zeWalkable;
        m_v3_worldPosition = zeWoldPos;
    }
	
}

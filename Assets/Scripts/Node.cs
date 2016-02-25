using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

    public Node[] connections;
    public bool showConnections;

    void OnDrawGizmos()
    {
        if (!showConnections)
            return;
        foreach (Node n in connections)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, n.transform.position);
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    List<Node> path;

    void Start()
    {
        path = new List<Node>();
    }

	public void FollowPath(List<Node> _path)
    {
        path = _path;
    }

    void OnDrawGizmos()
    {
        if(path.Count > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, path[0].transform.position);
            for(int i = 1; i < path.Count; i++)
            {
                Gizmos.DrawLine(path[i - 1].transform.position, path[i].transform.position);
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathFinding : MonoBehaviour {

    public Node[] nodes;
    public Node[] path;

    List<float> distToPlayer;
    float minDist;
    int minDistIndex;
    int lastMinDistIndex = -1;

    int currentNodeIndex;

    Transform player;
    Enemy enemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        distToPlayer = new List<float>();
        distToPlayer.Capacity = nodes.Length;
    }

    void Update()
    {
        FindQuickestRoute();
    }

    void FindQuickestRoute()
    {
        minDistIndex = ClosestNode();
        if (minDistIndex == lastMinDistIndex)
        {
            enemy.FollowPath(path);
            return;
        }

        
    }

    int ClosestNode()
    {
        for(int i = 0; i < nodes.Length; i++)
        {
            distToPlayer[i] = Vector2.Distance(nodes[i].transform.position, player.position);
        }
        minDist = distToPlayer.Min();
        return distToPlayer.IndexOf(minDist);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathFinding : MonoBehaviour {

    public List<Node> nodes;
    public List<Node> path;
    public int maxPathLength = 13;

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
        distToPlayer.Capacity = nodes.Count;
        path = new List<Node>();
    }

    void Update()
    {
        FindQuickestRoute();
    }

    void FindQuickestRoute()
    {
        minDistIndex = ClosestNode(player.position);
        if (minDistIndex == lastMinDistIndex)
        {
            enemy.SetPath(path);
            return;
        }
        lastMinDistIndex = minDistIndex;
        currentNodeIndex = ClosestNode(enemy.transform.position);
        path.Clear();

        if(currentNodeIndex == minDistIndex)
        {
            path.Add(nodes[currentNodeIndex]);
            enemy.SetPath(path);
            return;
        }

        path.Add(nodes[currentNodeIndex]);

        for(int i = 0; i < maxPathLength; i++)
        {
            path.Add(NextClosestNode(nodes.IndexOf(path.Last())));
            if(path.Last() == nodes[minDistIndex])
            {
                enemy.SetPath(path);
                return;
            }
        }
    }

    Node NextClosestNode(int index)
    {
        for (int a = 0; a < nodes[index].connections.Length; a++)
        {
            if (Vector2.Distance(nodes[index].transform.position, player.position) >
                Vector2.Distance(nodes[index].connections[a].transform.position, player.position))
            {
                return nodes[index].connections[a];
            }
        }
        return nodes.Last();
    }

    int ClosestNode(Vector2 target)
    {
        distToPlayer.Clear();
        for(int i = 0; i < nodes.Count; i++)
        {
            distToPlayer.Add(Vector2.Distance(nodes[i].transform.position, target));
        }
        minDist = distToPlayer.Min();
        return distToPlayer.IndexOf(minDist);
    }
}

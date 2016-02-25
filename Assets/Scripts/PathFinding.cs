using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathFinding : MonoBehaviour {

    public List<Node> nodes;
    public List<Node> path;
    public int maxPathLength
    {
        get { return nodes.Count; }
    }

    List<float> distToPlayer;
    float playerNode;
    int playerNodeIndex;
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
        playerNodeIndex = ClosestNode(player);
        if (playerNodeIndex == lastMinDistIndex)
        {
            //enemy.SetPath(path);
            return;
        }
        lastMinDistIndex = playerNodeIndex;
        currentNodeIndex = ClosestNode(enemy.transform);
        path.Clear();

        if (currentNodeIndex == playerNodeIndex)
        {
            path.Add(nodes[currentNodeIndex]);
            enemy.SetPath(path);
            return;
        }

        path.Add(nodes[currentNodeIndex]);

        for(int i = 0; i < maxPathLength; i++)
        {
            path.Add(NextClosestNode(nodes.IndexOf(path.Last())));
            if(path.Last() == nodes[playerNodeIndex])
            {
                enemy.SetPath(path);
                return;
            }
        }
        enemy.SetPath(path);
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
        return path.Last();
    }

    int ClosestNode(Transform target)
    {
        distToPlayer.Clear();
        for(int i = 0; i < nodes.Count; i++)
        {
            if(nodes[i].level == target.GetComponent<PlatformLevel>().level)
                distToPlayer.Add(Vector2.Distance(nodes[i].transform.position, target.position));
        }
        playerNode = distToPlayer.Min();
        
        return distToPlayer.IndexOf(playerNode);
    }
}

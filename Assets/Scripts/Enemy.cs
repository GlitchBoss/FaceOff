using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public List<Node> path;
    public bool showPath;
    public float jumpForce;
    public float speed;
    public LayerMask ground;
    public PlatformLevel level;
    public float stoppingDist;

    Rigidbody2D _rigidbody;
    CircleCollider2D _collider;
    float distToGround;
    Player player;
    public int currentNodeIndex;
    Vector2 side, velocity;
    public bool atPlayer;

    void Start()
    {
        path = new List<Node>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        level = GetComponent<PlatformLevel>();
        distToGround = _collider.bounds.extents.y;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player.level.level == level.level)
        {
            side = transform.InverseTransformPoint(player.transform.position);
            side.Normalize();
            velocity = new Vector2(side.x * speed, _rigidbody.velocity.y);
            _rigidbody.velocity = velocity;
            return;
        }
        if (path.Count == 0)
            return;


        float distToNode;

        if(path[currentNodeIndex].level < level.level || 
            level.level == PlatformLevel.Level.GroundLevel)
            distToNode = Mathf.Abs(transform.position.x - 
                path[currentNodeIndex].transform.position.x);
        else if(path[currentNodeIndex].level == level.level)
            distToNode = Vector2.Distance(transform.position, 
                path[currentNodeIndex].transform.position);
        else
            distToNode = Vector2.Distance(transform.position,
                    path[currentNodeIndex].transform.position);

        if (distToNode <= stoppingDist)
        {
            currentNodeIndex = NextNode();
            return;
        }

        if (path[currentNodeIndex].level > level.level)
            Jump();

        side = transform.InverseTransformPoint(
            path[currentNodeIndex].transform.position);
        side.Normalize();
        velocity = new Vector2(side.x * speed, _rigidbody.velocity.y);
        _rigidbody.velocity = velocity;
    }

    int NextNode()
    {
        if (currentNodeIndex == path.Count - 1)
        {
            atPlayer = true;
            return currentNodeIndex;
        }
        return currentNodeIndex + 1;
    }

	public void SetPath(List<Node> _path)
    {
        path = _path;
        atPlayer = false;
        currentNodeIndex = 0;
        //StartCoroutine("FollowPath");
    }

    IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count; i++)
        { 
            yield return StartCoroutine("MoveToNode", path[i]);
        }
        if (player.level.level == level.level)
        {
            Vector2 side = transform.InverseTransformPoint(player.transform.position);
            side.Normalize();
            Vector2 velocity = new Vector2(side.x * speed, _rigidbody.velocity.y);
            _rigidbody.velocity = velocity;
        }
    }

    IEnumerator MoveToNode(Node target)
    {
        if(target.level <= level.level)
        {
            Vector2 side = transform.InverseTransformPoint(target.transform.position);
            side.Normalize();
            Vector2 velocity = new Vector2(side.x * speed, _rigidbody.velocity.y);
            _rigidbody.velocity = velocity;
        }
        if(target.level > level.level)
        {
            Jump();
            Vector2 side = transform.InverseTransformPoint(target.transform.position);
            side.Normalize();
            Vector2 velocity = new Vector2(side.x * speed, _rigidbody.velocity.y);
            _rigidbody.velocity = velocity;
        }
        if (transform.position.x >= target.transform.position.x - 0.1f &&
            transform.position.x <= target.transform.position.x)
            yield return null;
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector3.up,
            distToGround + 0.1f, ground);
    }

    void Jump()
    {
        if(IsGrounded() && _rigidbody.velocity == new Vector2(_rigidbody.velocity.x, 0))
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnDrawGizmos()
    {
        if (!showPath)
            return;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "PlatformLevel")
        {
            level.level = col.GetComponent<PlatformLevel>().level;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "PlatformLevel")
        {
            if (level.level == col.GetComponent<PlatformLevel>().level)
                return;
            level.level = col.GetComponent<PlatformLevel>().level;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public List<Node> path;
    public bool showPath;
    public float jumpForce;
    public float speed;
    public LayerMask ground;
    public PlatformLevel.Level level;

    Rigidbody2D _rigidbody;
    CircleCollider2D _collider;
    float distToGround;
    Player player;

    void Start()
    {
        path = new List<Node>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        distToGround = _collider.bounds.extents.y;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

	public void SetPath(List<Node> _path)
    {
        path = _path;
        StartCoroutine("FollowPath");
        if(player.level == level)
        {
            Vector2 side = transform.InverseTransformPoint(player.transform.position);
            side.Normalize();
            Vector2 velocity = new Vector2(side.x * speed, _rigidbody.velocity.y);
            _rigidbody.velocity = velocity;
        }
    }

    IEnumerator FollowPath()
    {
        for(int i = 0; i < path.Count; i++)
        {
            yield return StartCoroutine("MoveToNode", path[i]);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector3.up,
            distToGround + 0.1f, ground);
    }

    void Jump()
    {
        if(IsGrounded())
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    IEnumerator MoveToNode(Node target)
    {
        if(target.level < level)
        {
            Vector2 side = transform.InverseTransformPoint(target.transform.position);
            side.Normalize();
            Vector2 velocity = new Vector2(side.x * speed, _rigidbody.velocity.y);
            _rigidbody.velocity = velocity;
            yield return null;
        }
        if(target.level > level)
        {
            Jump();
            Vector2 side = transform.InverseTransformPoint(target.transform.position);
            side.Normalize();
            Vector2 velocity = new Vector2(side.x * speed, _rigidbody.velocity.y);
            _rigidbody.velocity = velocity;
        }
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
            level = col.GetComponent<PlatformLevel>().level;
        }
    }
}

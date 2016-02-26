using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character {

    public Transform[] path;
    public bool showPath;

    bool toHigherLevel;
    Player player;
    Vector2 side, velocity;
    PathFinding pathFinding;

	protected override void StartUp()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        level = GetComponent<PlatformLevel>();
        pathFinding = GameObject.Find("Path").GetComponent<PathFinding>();
        distToGround = _collider.bounds.extents.y;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

	protected override void OnUpdate()
    {
        if (player.level.level == level.level)
        {
            MoveToPlayer();
            toHigherLevel = false;
            return;
        }

        if(level.level <= player.level.level)
        {
            Vector3 playerSide = player.transform.InverseTransformPoint(0,0,0);
            playerSide.Normalize();
            if(playerSide.x > 0)
            {
                path = pathFinding.rightPath;
            }
            else
            {
                path = pathFinding.leftPath;
            }
            toHigherLevel = true;
            MoveToNode(path[(int)level.level].position);
        } 
    }

    void MoveToPlayer()
    {
        side = transform.InverseTransformPoint(player.transform.position);
        side.Normalize();
        velocity = new Vector2(side.x * speed, _rigidbody.velocity.y);
        _rigidbody.velocity = velocity;
    }

    void MoveToNode(Vector2 target)
    {
        Vector2 side = transform.InverseTransformPoint(target);
        side.Normalize();
        Vector2 velocity = new Vector2(side.x * speed, _rigidbody.velocity.y);
        _rigidbody.velocity = velocity;
    }

    void Jump()
    {
        if(IsGrounded() && _rigidbody.velocity == new Vector2(_rigidbody.velocity.x, 0) && toHigherLevel)
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnDrawGizmos()
    {
        if (!showPath)
            return;
        if(path.Length > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, path[0].transform.position);
            for(int i = 1; i < path.Length; i++)
            {
                Gizmos.DrawLine(path[i - 1].transform.position, path[i].transform.position);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlatformLevel")
        {
            level.level = col.GetComponent<PlatformLevel>().level;
        }
    }

	protected override void OnOnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "JumpBox")
        {
            Jump();
        }
        else if (col.tag == "PlatformLevel" && level.level != col.GetComponent<PlatformLevel>().level)
        {
            level.level = col.GetComponent<PlatformLevel>().level;
        }
    }
}

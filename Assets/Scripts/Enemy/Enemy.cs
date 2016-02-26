using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character {

    public Transform[] path;
    public bool showPath;
	public bool shouldAttack;
	public float fireRate;

    bool toHigherLevel;
    Player player;
    Vector2 side, velocity;
    PathFinding pathFinding;
	int horizontal = 1;
	public bool canAttack = true;

	protected override void StartUp()
    {
        pathFinding = GameObject.Find("Path").GetComponent<PathFinding>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

	protected override void OnUpdate()
    {
		if (horizontal > 0)
			image.localScale = new Vector3(1, 1, 1);
		else if (horizontal < 0)
			image.localScale = new Vector3(-1, 1, 1);

		if (shouldAttack && canAttack)
		{
			weapon.Attack(IsGrounded());
			canAttack = false;
			StartCoroutine(DelayAttack(fireRate));
		}
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
			weapon.isAttacking = false;
		else
			weapon.isAttacking = true;

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
				horizontal = 1;
            }
            else
            {
                path = pathFinding.leftPath;
				horizontal = -1;
            }
            toHigherLevel = true;
            MoveToNode(path[(int)level.level].position);
		}
    }

	IEnumerator DelayAttack(float delay)
	{
		yield return new WaitForSeconds(delay);
		canAttack = true;
	}

    void MoveToPlayer()
    {
        side = transform.InverseTransformPoint(player.transform.position);
        side.Normalize();
		if (side.x > 0)
		{
			horizontal = 1;
		}
		else
		{
			horizontal = -1;
		}
		velocity = new Vector2(side.x * speed, _rigidbody.velocity.y);
        _rigidbody.velocity = velocity;
    }

    void MoveToNode(Vector2 target)
    {
        Vector2 side = transform.InverseTransformPoint(target);
        side.Normalize();
		if (side.x > 0)
		{
			horizontal = 1;
		}
		else
		{
			horizontal = -1;
		}
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

	protected override void OnOnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "JumpBox")
        {
            Jump();
        }
    }
}

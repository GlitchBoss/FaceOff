using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Character {

    public KeyCode[] keys;

    protected override void StartUp()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        anim = GetComponent<Animator>();
        SP = GetComponent<SpecialPower>();
        level = GetComponent<PlatformLevel>();
        distToGround = _collider.bounds.extents.y;
        UpdateHealthSlider();
    }

    protected override void OnSetControls()
    {
        if (useSecondaryControls)
        {
            keys[0] = KeyCode.W;
            keys[1] = KeyCode.Space;
            keys[2] = KeyCode.D;
            keys[3] = KeyCode.A;
            keys[4] = KeyCode.LeftShift;
            facingRight = true;
        }
        else
        {
            keys[0] = KeyCode.UpArrow;
            keys[1] = KeyCode.Return;
            keys[2] = KeyCode.RightArrow;
            keys[3] = KeyCode.LeftArrow;
            keys[4] = KeyCode.RightShift;
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
    }

	protected override void OnUpdate()
    {
        if (lost)
        {
            weapon.isAttacking = false;
            weapon.col.hasAttacked = true;
            return;
        }
        if (Input.GetKeyDown(keys[0]) && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(keys[1]))
        {
            weapon.Attack(IsGrounded());
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
            weapon.isAttacking = false;
        else
            weapon.isAttacking = true;

        int horizontal = 0;
        if (Input.GetKey(keys[2]))
        {
            horizontal = 1;
            facingRight = true;
        }
        else if (Input.GetKey(keys[3]))
        {
            horizontal = -1;
            facingRight = false;
        }
        else
            horizontal = 0;

        if (Input.GetKeyDown(keys[4]) && power.num >= power.max)
        {
            SP.Engage();
        }

        if(horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if(horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        _rigidbody.velocity = new Vector2(horizontal * speed, _rigidbody.velocity.y);
        UpdatePowerSlider(engaged);
    }

	protected override void OnOnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "PlatformLevel" && level.level != col.GetComponent<PlatformLevel>().level)
        {
            level.level = col.GetComponent<PlatformLevel>().level;
        }
    }
}

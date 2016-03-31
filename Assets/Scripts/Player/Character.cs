using UnityEngine;
using UnityEngine.UI;
using GLITCH.Helpers;
using System;

public class Character : MonoBehaviour {

	public float speed, jumpForce;
	public LayerMask ground;

	[HideInInspector]
	public bool useSecondaryControls, facingRight, lost, engaged;
	[HideInInspector]
	public GameObject winGO;
	[HideInInspector]
	public Transform image;
	[HideInInspector]
	public PlatformLevel level;
	[HideInInspector]
	public Weapon weapon;
	[HideInInspector]
	public Animator anim;
	[HideInInspector]
	public Rigidbody2D _rigidbody;
	[HideInInspector]
	public float distToGround;
	[HideInInspector]
	public Collider2D _collider;
	[HideInInspector]
	public SpecialPower SP;
	[HideInInspector]
	public int horizontal;
	[HideInInspector]
	public bool paused;
	[HideInInspector]
	public CharacterHealth health;
	[HideInInspector]
	public CharacterPower power;

	public enum Movement
	{
		Left = 1,
		Right = 2,
		Jump = 3,
		Attack = 4,
		SpecialPower = 5,
		Stop = 6
	}

	void Awake()
	{
		health = GetComponent<CharacterHealth>();
		power = GetComponent<CharacterPower>();
		SP = GetComponent<SpecialPower>();
	}

	void Start()
	{
		image = transform.FindChild("Image");
		winGO = transform.FindChild("Winner").gameObject;
		level = GetComponent<PlatformLevel>();
		_rigidbody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<CircleCollider2D>();
		weapon = GetComponentInChildren<Weapon>();
		anim = GetComponent<Animator>();
		distToGround = _collider.bounds.extents.y;
		
		StartUp();
	}

	public void SetControls() { OnSetControls(); }

	void Update()
	{
		if (lost || paused)
			return;
		if (horizontal > 0)
			image.localScale = new Vector3(1, 1, 1);
		else if (horizontal < 0)
			image.localScale = new Vector3(-1, 1, 1);
		OnUpdate();
		power.UpdatePowerSlider(engaged);
	}

	void OnTriggerStay2D(Collider2D col) {	if (lost || paused)	return; OnOnTriggerStay2D(col);	}

	public void Move(int movement) { if (lost || paused) return; OnMove((Movement)movement); }

	public void Attack()
	{
		if (lost || paused)
			return;
		OnAttack();
	}

	protected virtual void OnAttack() { }

	protected virtual void StartUp() { }

	protected virtual void OnSetControls() { }

	protected virtual void OnUpdate() { }

	protected virtual void OnMove(Movement movement) { }

	protected virtual void OnOnTriggerStay2D(Collider2D col) { }

	public bool IsGrounded()
	{
		return Physics2D.Raycast(transform.position, -Vector3.up,
			distToGround + 0.1f, ground);
	}

	public void Win()
	{
		winGO.SetActive(true);
	}

	public void Lose()
	{
		if (lost || paused)
			return;
		GameManager.instance.AddScore(this);
		lost = true;
	}

	public void OnPauseGame()
	{
		paused = true;
	}

	public void OnResumeGame()
	{
		paused = false;
	}
}

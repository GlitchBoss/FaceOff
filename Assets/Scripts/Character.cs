using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character : MonoBehaviour {

	public float speed, jumpForce;
	public RangeFloat health, power;
	public Slider healthSlider, powerSlider;
	public int powerDecrease, powerIncrease;
	public bool useSecondaryControls, facingRight, lost, engaged;
	public LayerMask ground;
	public GameObject winGO;
	public Animator anim;
	public Weapon weapon;
	public PlatformLevel level;
	public Transform image;

	[HideInInspector]
	public Rigidbody2D _rigidbody;
	[HideInInspector]
	public float distToGround;
	[HideInInspector]
	public Collider2D _collider;
	[HideInInspector]
	public SpecialPower SP;

	public enum Movement
	{
		Left = 1,
		Right = 2,
		Jump = 3,
		Attack = 4,
		SpecialPower = 5,
		Stop = 6
	}

	void Start()
	{
		image = transform.FindChild("Image").transform;
		level = GetComponent<PlatformLevel>();
		_rigidbody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<CircleCollider2D>();
		weapon = GetComponentInChildren<Weapon>();
		anim = GetComponent<Animator>();
		SP = GetComponent<SpecialPower>();
		distToGround = _collider.bounds.extents.y;
		if (SP.duration >= 1)
		{
			powerDecrease = (int)(power.max / SP.duration);
		}
		else
		{
			powerDecrease = (int)power.max;
		}
		StartUp();
	}

	public void SetControls() { OnSetControls(); }

	void Update()
	{
		if (lost)
			return;
		OnUpdate();
		UpdatePowerSlider(engaged);
	}

	void OnTriggerStay2D(Collider2D col) { OnOnTriggerStay2D(col);	}

	public void Move(int movement) { OnMove((Movement)movement); }

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

	public void UpdateHealthSlider()
	{
		healthSlider.maxValue = health.max;
		healthSlider.minValue = health.min;
		healthSlider.value = health.num;
	}

	public void UpdatePowerSlider(bool inUse)
	{
		if (power.num > power.max)
		{
			power.num = power.max;
			return;
		}
		else if (power.num < power.min)
		{
			power.num = power.min;
			return;
		}

		if (inUse)
		{
			power.num -= powerDecrease * Time.deltaTime;
		}
		else
		{
			power.num += powerIncrease * Time.deltaTime;
		}
		powerSlider.value = power.num;
	}

	public void Win()
	{
		winGO.SetActive(true);
	}

	public void Lose()
	{
		GameManager.instance.AddScore(this);
		lost = true;
	}

	public void TakeDamage(float amount)
	{
		if (lost)
			return;
		health.num -= amount;
		if (health.num <= 0)
		{
			Lose();
		}
		UpdateHealthSlider();
	}
}

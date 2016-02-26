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

	[HideInInspector]
	public Rigidbody2D _rigidbody;
	[HideInInspector]
	public float distToGround;
	[HideInInspector]
	public Collider2D _collider;
	[HideInInspector]
	public SpecialPower SP;

	void Start() { StartUp(); }

	public void SetControls() { OnSetControls(); }

	void Update() { OnUpdate();	}

	void OnTriggerStay2D(Collider2D col) { OnOnTriggerStay2D(col);	}

	protected virtual void StartUp()
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

	protected virtual void OnSetControls() { }

	protected virtual void OnUpdate() { }

	protected virtual void OnOnTriggerStay2D(Collider2D col) { }

	public bool IsGrounded()
	{
		return Physics2D.Raycast(transform.position, -Vector3.up,
			distToGround + 0.1f, ground);
	}

	public void UpdateHealthSlider()
	{
		//healthSlider.maxValue = health.max;
		//healthSlider.minValue = health.min;
		//healthSlider.value = health.num;
	}

	public void UpdatePowerSlider(bool inUse)
	{
		//if (power.num > power.max)
		//{
		//    power.num = power.max;
		//    return;
		//}
		//else if(power.num < power.min)
		//{
		//    power.num = power.min;
		//    return;
		//}

		//if (inUse)
		//{
		//    power.num -= powerDecrease * Time.deltaTime;
		//}
		//else
		//{
		//    power.num += powerIncrease * Time.deltaTime;
		//}
		//powerSlider.value = power.num;
	}

	public void Win()
	{
		winGO.SetActive(true);
	}

	public void Lose()
	{
		GameManager.instance.GameOver(this);
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

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "PlatformLevel")
		{
			level.level = col.GetComponent<PlatformLevel>().level;
		}
	}
}

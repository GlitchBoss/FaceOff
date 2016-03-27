using UnityEngine;
using System.Collections;

public class SpecialPower : MonoBehaviour {

    public float duration;
    public string animBool;

    public int extraHealth, extraDamage;
    public float extraSpeed;
	public bool fillHealth;

    public int maxDamage;
    public float radius;
    public float delay;
    public GameObject flash;

	public bool background;
    public Type type;

    public enum Type
    {
        Enhance,
        Bomb
    }

	Character character;
	BackgroundFace bkgFace;
	CharacterHealth playerHealth;
    int originalHealth, originalDamage;
    float originalSpeed;

    void Start()
    {
		if (background)
		{
			bkgFace = GetComponent<BackgroundFace>();
		}
		else
		{
			character = GetComponent<Character>();
		}
		playerHealth = GetComponent<CharacterHealth>();
    }

    public void Engage()
    {
		if (background)
		{
			bkgFace.anim.SetBool(animBool, true);
			bkgFace.engaged = true;
		}
		else
		{
			character.anim.SetBool(animBool, true);
			character.engaged = true;
		}
        switch (type)
        {
            case Type.Enhance:
                EngageEnhance();
                break;
            case Type.Bomb:
                EngageBomb();
                break;
        }
    }

    void EngageEnhance()
    {
        StartCoroutine(Timer());
        originalHealth = (int)playerHealth.health.num;
		playerHealth.health.max += extraHealth;
		if (background)
		{
			if (fillHealth)
				bkgFace.health.health.num = bkgFace.health.health.max;
		}
		else
		{
			if(fillHealth)
				character.health.health.num = character.health.health.max;
		}
		playerHealth.UpdateHealthSlider();
		if (background)
		{
			originalDamage = bkgFace.weapon.damage;
			bkgFace.weapon.damage += extraDamage;
			originalSpeed = bkgFace.speed;
			bkgFace.speed += extraSpeed;
		}
		else
		{
			originalDamage = character.weapon.damage;
			character.weapon.damage += extraDamage;
			originalSpeed = character.speed;
			character.speed += extraSpeed;
		}
    }

    void EngageBomb()
    {
        StartCoroutine(Timer());
		if (background)
		{
			return;
		}
		Character otherPlayer;
		if (GameManager.instance.players.IndexOf(character) == 0)
        {
            otherPlayer = GameManager.instance.players[1];
        }
        else
        {
            otherPlayer = GameManager.instance.players[0];
        }

        float dist = Vector3.Distance(transform.position, otherPlayer.transform.position);
        float relativeDistance = (radius - dist) / radius;
        float damage = relativeDistance * maxDamage;
        damage = Mathf.Max(0f, damage);
        otherPlayer.health.LoseHealth(damage);
        flash.SetActive(true);
    }

    void OnDrawGizmos()
    {
        if(type == Type.Bomb)
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(duration);
        Disengage();
    }

    void Disengage()
    {
		if (background)
		{
			bkgFace.anim.SetBool(animBool, false);
			bkgFace.engaged = false;
		}
		else
		{
			character.anim.SetBool(animBool, false);
			character.engaged = false;
		}
        switch (type)
        {
            case Type.Enhance:
                DisengageEnhance();
                break;
            case Type.Bomb:
                DisengageBomb();
                break;
        }
    }

    void DisengageEnhance()
    {
		if(fillHealth)
			playerHealth.health.num = originalHealth;
		playerHealth.health.max -= extraHealth;
		playerHealth.UpdateHealthSlider();
		if (background)
		{
			bkgFace.weapon.damage = originalDamage;
			bkgFace.speed = originalSpeed;
		}
		else
		{
			character.weapon.damage = originalDamage;
			character.speed = originalSpeed;
		}
    }

    void DisengageBomb()
    {
		if (background)
		{
			bkgFace.power.power.num = 0;
		}
		else
		{
			character.power.power.num = 0;
		}
        flash.SetActive(false);
    }
}

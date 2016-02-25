using UnityEngine;
using System.Collections;

public class SpecialPower : MonoBehaviour {

    public float duration;
    public string animBool;

    public int extraHealth, extraDamage;
    public float extraSpeed;

    public int maxDamage;
    public float radius;
    public float delay;
    public GameObject flash;

    public Type type;

    public enum Type
    {
        Enhance,
        Bomb
    }

    Player player;
    int originalHealth, originalDamage;
    float originalSpeed;
    float minDist;

    void Start()
    {
        player = GetComponent<Player>();
        minDist = GetComponent<CircleCollider2D>().radius * 2;
    }

    public void Engage()
    {
        player.anim.SetBool(animBool, true);
        player.engaged = true;
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
        originalHealth = (int)player.health.num;
        player.health.max += extraHealth;
        player.health.num = player.health.max;
        player.UpdateHealthSlider();
        originalDamage = player.weapon.damage;
        player.weapon.damage += extraDamage;
        originalSpeed = player.speed;
        player.speed += extraSpeed;
    }

    void EngageBomb()
    {
        StartCoroutine(Timer());
        Player otherPlayer;
        if(GameManager.instance.players.IndexOf(player) == 0)
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
        otherPlayer.TakeDamage(damage);
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
        player.anim.SetBool(animBool, false);
        player.engaged = false;
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
        player.health.num = originalHealth;
        player.health.max -= extraHealth;
        player.UpdateHealthSlider();
        player.weapon.damage = originalDamage;
        player.speed = originalSpeed;
    }

    void DisengageBomb()
    {
        player.power.num = 0;
        flash.SetActive(false);
    }
}

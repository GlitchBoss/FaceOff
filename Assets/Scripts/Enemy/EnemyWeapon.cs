using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {

    public int damage;
    public LayerMask playerLayer;
    public Enemy enemy;
    public Animator anim;
    public bool isAttacking;
    public WeaponCol col;
    public bool singlePlayer;

    void Start ()
    {
        enemy = transform.parent.GetComponentInParent<Enemy>();
        anim = enemy.GetComponent<Animator>();
        StartUp();
    }

    protected virtual void StartUp() { }

    void Update()
    {
        if (enemy.lost)
            return;
    }

    public void Attack(bool onGround)
    {
        OnAttack(onGround);
    }

    protected virtual void OnAttack(bool onGround) { }
}

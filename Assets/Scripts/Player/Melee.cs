using UnityEngine;
using System.Collections;

public class Melee : Weapon {

    protected override void StartUp()
    {
        col = GetComponentInChildren<WeaponCol>();
    }

    protected override void OnAttack(bool onGround)
    {
        if (onGround)
            anim.SetTrigger("Attack");
        else
            anim.SetTrigger("SpinAttack");
        isAttacking = true;
        col.hasAttacked = false;
    }

    public void Hit(Transform target, WeaponCol _col)
    {
        if(isAttacking)
        {
            if(singlePlayer)
                target.GetComponent<Enemy>().TakeDamage(damage);
            else
                target.GetComponent<Player>().TakeDamage(damage);
            _col.hasAttacked = true;
        }
    }
}

using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public int damage;
    public LayerMask playerLayer;
    public Character character;
    public Animator anim;
    public bool isAttacking;
    public WeaponCol col;

    void Start ()
    {
        character = transform.parent.GetComponentInParent<Character>();
        anim = character.GetComponent<Animator>();
        StartUp();
    }

    protected virtual void StartUp() { }

    void Update()
    {
        if (character.lost)
            return;
    }

    public void Attack(bool onGround)
    {
        OnAttack(onGround);
    }

    protected virtual void OnAttack(bool onGround) { }
}

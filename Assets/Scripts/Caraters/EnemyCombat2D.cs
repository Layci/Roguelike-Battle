/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat2D : BaseUnit
{
    private EnemyChase2D chase;
    public Transform target;
    private float attackTimer = 0f;

    public override void Awake()
    {
        base.Awake();
        chase = GetComponent<EnemyChase2D>();
    }

    public override void Start()
    {
        base.Start();
        attackTimer = AttackCooldown;
    }

    public override void Update()
    {
        base.Update();

        if (isDead) return;

        attackTimer += Time.deltaTime;

        if (chase != null && chase.target != null)
        {
            float distance = Vector2.Distance(transform.position, chase.target.position);
            if (distance <= AttackRange && attackTimer >= AttackCooldown)
            {
                animator.SetTrigger("Attack");
                attackTimer = AttackCooldown;
            }
        }
    }
}
*/
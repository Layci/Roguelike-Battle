using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat2D : BaseUnit
{
    private EnemyChase2D chase;
    private float attackTimer = 0f;
    //private Transform target;

    public override void Awake()
    {
        base.Awake();
        chase = GetComponent<EnemyChase2D>();
    }

    public override void Start()
    {
        base.Start();
        //target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public override void Update()
    {
        if (isDead) return;

        attackTimer -= Time.deltaTime;

        if (chase != null && chase.target != null)
        {
            float distance = Vector2.Distance(transform.position, chase.target.position);
            if (distance <= AttackRange && attackTimer <= 0f)
            {
                StartCoroutine(PerformAttack());
                attackTimer = AttackCooldown;
            }
        }
    }

    private IEnumerator PerformAttack()
    {
        if (animator)
        {
            animator.SetTrigger("Attack");
            animator.speed = AttackSpeed;
        }

        if (chase != null) chase.SetCanMove(false);

        yield return new WaitForSeconds(0.5f / AttackSpeed); // 공격 모션 중간 시점에 판정 발생

        // 실제 공격 판정 (예시)
        if (chase.target != null)
        {
            BaseUnit playerUnit = chase.target.GetComponent<BaseUnit>();
            if (playerUnit != null)
                playerUnit.TakeDamage(AttackPower);
        }

        if (chase != null) chase.SetCanMove(true);
    }
}

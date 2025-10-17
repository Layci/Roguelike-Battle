using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseUnit))]
public class UnitController2D : MonoBehaviour
{
    public string targetTag = "Enemy";
    public float knockbackDuration = 0.3f;
    public float attackRangeBuffer = 0.3f;

    private BaseUnit unit;
    public Transform target;
    private Rigidbody2D rb;
    private float nextAttackTime;
    private Vector2 knockbackForce;
    private float knockbackTimer;

    void Awake()
    {
        unit = GetComponent<BaseUnit>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        FindTarget();
        unit.ChangeState(UnitState.Idle);
    }

    private void FixedUpdate()
    {
        switch (unit.CurrentState)
        {
            case UnitState.Idle:
                HandleIdle();
                break;
            case UnitState.Chase:
                HandleChase();
                break;
            case UnitState.Attack:
                HandleAttack();
                break;
            case UnitState.Stun:
                // 스턴 상태
                break;
            case UnitState.Knockback:
                HandleKnockback();
                break;
            case UnitState.Enraged:
                HandleEnraged();
                break;
        }
    }
    // 분노 상태 처리
    void HandleEnraged()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance > unit.characterData.attackRange)
        {
            unit.ChangeState(UnitState.Chase);
            Vector2 dir = (target.position - transform.position).normalized;
            rb.MovePosition(rb.position + dir * unit.characterData.moveSpeed * Time.deltaTime);
        }
        else
        {
            unit.ChangeState(UnitState.Attack);
        }
    }
    // 대기 상태 처리
    void HandleIdle()
    {
        if (target == null)
        {
            FindTarget();
        }

        if (target != null)
        {
            unit.ChangeState(UnitState.Chase);
        }
    }
    // 추적 상태 처리
    void HandleChase()
    {
        if (target == null)
        {
            unit.ChangeState(UnitState.Idle);
            return;
        }

        Vector2 dir = (target.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > unit.characterData.attackRange)
        {
            rb.MovePosition(rb.position + dir * unit.characterData.moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
            unit.ChangeState(UnitState.Attack);
        }
    }
    // 공격 상태 처리
    void HandleAttack()
    {
        if (target == null) { unit.ChangeState(UnitState.Idle); return; }

        float distance = Vector2.Distance(transform.position, target.position);

        // 사거리 밖이면 추적 상태로 전환
        if (distance > unit.characterData.attackRange)
        {
            unit.ChangeState(UnitState.Chase);
            return;
        }
        // 사거리 이내이고 마나가 전부 회복되지 않은 생태에서 쿨다운이 끝났으면 공격
        if (Time.time >= nextAttackTime && unit.CurrentMana < unit.MaxMana)
        {
            nextAttackTime = Time.time + unit.characterData.attackCooldown;
            unit.animator.SetTrigger("Attack");
        } // 사거리 이내이고 마나가 최대치이면 스킬 사용
        if (unit.CurrentMana >= unit.MaxMana)
        {
            unit.animator.SetTrigger("Skill");
            unit.ResetMana(); // 스킬 사용 후 마나 초기화
        }
    }
    // 넉백 상태 처리
    void HandleKnockback()
    {
        if (knockbackTimer > 0)
        {
            rb.MovePosition(rb.position + knockbackForce * Time.fixedDeltaTime);
            knockbackTimer -= Time.fixedDeltaTime;
        }
        else
        {
            knockbackForce = Vector2.zero;
            unit.ChangeState(UnitState.Chase);
        }
    }
    // 넉백 적용
    void ApplyKnockback(Vector2 force)
    {
        knockbackForce = force;
        knockbackTimer = knockbackDuration;
        unit.ChangeState(UnitState.Knockback);
    }

    // 타겟 태그로 대상 찾기
    void FindTarget()
    {
        GameObject found = GameObject.FindWithTag(targetTag);
        if (found != null)
        {
            target = found.transform;
        }
    }
}

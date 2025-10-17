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
                // ���� ����
                break;
            case UnitState.Knockback:
                HandleKnockback();
                break;
            case UnitState.Enraged:
                HandleEnraged();
                break;
        }
    }
    // �г� ���� ó��
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
    // ��� ���� ó��
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
    // ���� ���� ó��
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
    // ���� ���� ó��
    void HandleAttack()
    {
        if (target == null) { unit.ChangeState(UnitState.Idle); return; }

        float distance = Vector2.Distance(transform.position, target.position);

        // ��Ÿ� ���̸� ���� ���·� ��ȯ
        if (distance > unit.characterData.attackRange)
        {
            unit.ChangeState(UnitState.Chase);
            return;
        }
        // ��Ÿ� �̳��̰� ������ ���� ȸ������ ���� ���¿��� ��ٿ��� �������� ����
        if (Time.time >= nextAttackTime && unit.CurrentMana < unit.MaxMana)
        {
            nextAttackTime = Time.time + unit.characterData.attackCooldown;
            unit.animator.SetTrigger("Attack");
        } // ��Ÿ� �̳��̰� ������ �ִ�ġ�̸� ��ų ���
        if (unit.CurrentMana >= unit.MaxMana)
        {
            unit.animator.SetTrigger("Skill");
            unit.ResetMana(); // ��ų ��� �� ���� �ʱ�ȭ
        }
    }
    // �˹� ���� ó��
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
    // �˹� ����
    void ApplyKnockback(Vector2 force)
    {
        knockbackForce = force;
        knockbackTimer = knockbackDuration;
        unit.ChangeState(UnitState.Knockback);
    }

    // Ÿ�� �±׷� ��� ã��
    void FindTarget()
    {
        GameObject found = GameObject.FindWithTag(targetTag);
        if (found != null)
        {
            target = found.transform;
        }
    }
}

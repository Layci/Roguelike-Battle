using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseUnit))]
public class UnitCombat2D : MonoBehaviour
{
    [Tooltip("�� ������ ������ ��� �±�")]
    public string targetTag = "Enemy";

    private BaseUnit unit;
    private BaseUnit target;

    private float nextAttackTime;

    private void Awake()
    {
        unit = GetComponent<BaseUnit>();
    }

    private void Start()
    {
        // ���� �� ���� ��� Ž��
        FindTarget();
    }

    private void Update()
    {
        if (target == null)
        {
            // ����� ��������� �ٽ� Ž��
            FindTarget();
            return;
        }

        float distance = Vector2.Distance(transform.position, target.transform.position);

        // ��Ÿ� �̳��̰� ��ٿ��� �������� ����
        if (distance <= unit.characterData.attackRange && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + unit.characterData.attackCooldown;

            unit.animator.SetTrigger("Attack");
        }
        // ��Ÿ� �̳��̰� ������ �ִ�ġ�̸� ��ų ���
        if (distance <= unit.characterData.attackRange && unit.CurrentMana >= unit.MaxMana)
        {
            unit.animator.SetTrigger("Skill");
            unit.ResetMana(); // ��ų ��� �� ���� �ʱ�ȭ
        }

    }

    private void FindTarget()
    {
        GameObject found = GameObject.FindGameObjectWithTag(targetTag);
        if (found != null)
            target = found.GetComponent<BaseUnit>();
    }

    public BaseUnit GetTarget() => target;
}

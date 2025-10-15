using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseUnit))]
public class UnitCombat2D : MonoBehaviour
{
    [Tooltip("이 유닛이 공격할 대상 태그")]
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
        // 최초 한 번만 대상 탐색
        FindTarget();
    }

    private void Update()
    {
        if (target == null)
        {
            // 대상이 사라졌으면 다시 탐색
            FindTarget();
            return;
        }

        float distance = Vector2.Distance(transform.position, target.transform.position);

        // 사거리 이내이고 쿨다운이 끝났으면 공격
        if (distance <= unit.characterData.attackRange && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + unit.characterData.attackCooldown;

            unit.animator.SetTrigger("Attack");
        }
        // 사거리 이내이고 마나가 최대치이면 스킬 사용
        if (distance <= unit.characterData.attackRange && unit.CurrentMana >= unit.MaxMana)
        {
            unit.animator.SetTrigger("Skill");
            unit.ResetMana(); // 스킬 사용 후 마나 초기화
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

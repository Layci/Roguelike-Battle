using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private BaseUnit unit;

    void Awake()
    {
        unit = GetComponent<BaseUnit>();
    }

    // 기본공격 이벤트
    public void OnBasicAttack()
    {
        unit.PerformBasicAttack();
    }

    // 스킬 이벤트
    public void OnSkillCast()
    {
        unit.PerformSkill();
    }
}

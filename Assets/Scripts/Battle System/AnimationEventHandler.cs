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

    // �⺻���� �̺�Ʈ
    public void OnBasicAttack()
    {
        unit.PerformBasicAttack();
    }

    // ��ų �̺�Ʈ
    public void OnSkillCast()
    {
        unit.PerformSkill();
    }
}

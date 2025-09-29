using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageCalculator
{
    public static int CalculateDamage(
        int baseDamage,
        float attackMultiplier,
        float defenseMultiplier,
        float damageTakenMultiplier,
        bool isCritical,
        float criticalMultiplier)
    {
        float damage = baseDamage;

        damage *= attackMultiplier;       // ���ݷ� ����
        damage *= defenseMultiplier;      // ���� ����
        damage *= damageTakenMultiplier;  // �޴� ���� ����

        if (isCritical)
            damage *= criticalMultiplier; // ũ��Ƽ�� ���

        return Mathf.Max(1, Mathf.RoundToInt(damage));
    }
}


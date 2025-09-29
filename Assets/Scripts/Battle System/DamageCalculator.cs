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

        damage *= attackMultiplier;       // 공격력 보정
        damage *= defenseMultiplier;      // 방어력 보정
        damage *= damageTakenMultiplier;  // 받는 피해 보정

        if (isCritical)
            damage *= criticalMultiplier; // 크리티컬 배수

        return Mathf.Max(1, Mathf.RoundToInt(damage));
    }
}


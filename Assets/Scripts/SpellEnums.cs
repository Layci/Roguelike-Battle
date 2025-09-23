using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEnums : MonoBehaviour
{
    public enum CardRarity
    {
        Common, // 일반
        Rare,   // 희귀
        Epic    // 에픽 (중복 불가)
    }
        
    public enum SpellEffectType
    {
        HealInstant,        // 즉시 체력 회복
        ManaInstant,        // 즉시 마나 충전
        HealthBuff,         // 최대 체력 증가 (웨이브 지속/영구)
        ManaRegenBuff,      // 공격 시 마나 회복 증가 (웨이브 지속/영구)
        DamageReduction,    // 받는 피해 감소 (곱연산)
        DamageIncrease,     // 주는 피해 증가 (곱연산)
        CritChanceBuff,     // 치명타 확률 증가
        CritDamageBuff      // 치명타 피해량 증가
    }
}

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
        HealthBuff,
        ManaRegenBuff,
        DamageReduction, // 받는 피해 감소
        DamageIncrease, // 주는 피해 증가
        CritChanceBuff,
        CritDamageBuff,
        // 필요하면 계속 추가
    }
}

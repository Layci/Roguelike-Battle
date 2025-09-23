using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpellEnums;

public class SpellManager : MonoBehaviour
{
    public List<SpellCard> allCards;        // 전체 카드 풀
    public List<SpellCard> acquiredCards;   // 플레이어가 얻은 카드
    public List<SpellCard> activeBuffs = new List<SpellCard>();

    // 웨이브 시작 시 카드 선택 (예: 5장 뽑기)
    public List<SpellCard> GetRandomCards(int count)
    {
        List<SpellCard> available = new List<SpellCard>(allCards);

        // 에픽 중복 방지 → 이미 얻은 건 제거
        foreach (var card in acquiredCards)
        {
            if (card.rarity == CardRarity.Epic)
                available.RemoveAll(c => c.name == card.name);
        }

        List<SpellCard> result = new List<SpellCard>();
        for (int i = 0; i < count && available.Count > 0; i++)
        {
            int index = Random.Range(0, available.Count);
            result.Add(available[index]);
            available.RemoveAt(index);
        }

        return result;
    }

    // 카드 획득 시 처리
    public void AcquireCard(BaseUnit unit, SpellCard card)
    {
        acquiredCards.Add(card);
        ApplyCard(unit, card);
    }

    // 카드 적용
    public void ApplyCard(BaseUnit unit, SpellCard card)
    {
        if (card.duration == 0) // 즉시형
        {
            switch (card.effectType)
            {
                case SpellEffectType.HealInstant:
                    unit.Heal(card.value);
                    break;
                case SpellEffectType.ManaInstant:
                    unit.GainMana(card.value);
                    break;
            }
        }
        else // 지속형 (영구 포함)
        {
            activeBuffs.Add(card);
            ApplyBuff(unit, card);
        }
    }

    private void ApplyBuff(BaseUnit unit, SpellCard card)
    {
        switch (card.effectType)
        {
            case SpellEffectType.HealthBuff:
                unit.healthMultiplier.AddMultiplier(card.value);
                break;
            case SpellEffectType.ManaRegenBuff:
                unit.manaRegenMultiplier.AddMultiplier(card.value);
                break;
            case SpellEffectType.DamageReduction:
                unit.damageTakenMultiplier.AddMultiplier(-card.value);
                break;
            case SpellEffectType.DamageIncrease:
                unit.attackPowerMultiplier.AddMultiplier(card.value);
                break;
            case SpellEffectType.CritChanceBuff:
                unit.critChanceMultiplier.AddMultiplier(card.value);
                break;
            case SpellEffectType.CritDamageBuff:
                unit.critDamageMultiplier.AddMultiplier(card.value);
                break;
        }
    }

    // 웨이브 종료 시 호출
    public void OnWaveEnd(BaseUnit unit)
    {
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            if (activeBuffs[i].duration > 0) // 영구(-1) 제외
            {
                activeBuffs[i].duration--;
                if (activeBuffs[i].duration <= 0)
                {
                    RemoveBuff(unit, activeBuffs[i]);
                    activeBuffs.RemoveAt(i);
                }
            }
        }
    }

    private void RemoveBuff(BaseUnit unit, SpellCard card)
    {
        switch (card.effectType)
        {
            case SpellEffectType.HealthBuff:
                unit.healthMultiplier.RemoveMultiplier(card.value);
                break;
            case SpellEffectType.ManaRegenBuff:
                unit.manaRegenMultiplier.RemoveMultiplier(card.value);
                break;
            case SpellEffectType.DamageReduction:
                unit.damageTakenMultiplier.RemoveMultiplier(-card.value);
                break;
            case SpellEffectType.DamageIncrease:
                unit.attackPowerMultiplier.RemoveMultiplier(card.value);
                break;
            case SpellEffectType.CritChanceBuff:
                unit.critChanceMultiplier.RemoveMultiplier(card.value);
                break;
            case SpellEffectType.CritDamageBuff:
                unit.critDamageMultiplier.RemoveMultiplier(card.value);
                break;
        }
    }
}

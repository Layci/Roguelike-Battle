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
                available.RemoveAll(c => c.spellName == card.spellName);
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
                case SpellEffectType.HealthBuff:
                    unit.Heal(card.value);
                    break;
                case SpellEffectType.ManaRegenBuff:
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
        float multiplier = 1f; // 기본 배율 1배

        switch (card.effectType)
        {
            case SpellEffectType.HealthBuff:
                multiplier = 1f + card.value; // ex) +0.2f → 1.2배
                unit.healthMultiplier.Add(multiplier);
                break;

            case SpellEffectType.ManaRegenBuff:
                multiplier = 1f + card.value;
                unit.manaRegenMultiplier.Add(multiplier);
                break;

            case SpellEffectType.DamageReduction:
                multiplier = Mathf.Max(0f, 1f - card.value); // ex) 0.2f → 받는 피해 0.8배
                unit.damageTakenMultiplier.Add(multiplier);
                break;

            case SpellEffectType.DamageIncrease:
                multiplier = 1f + card.value;
                unit.attackPowerMultiplier.Add(multiplier);
                break;

            case SpellEffectType.CritChanceBuff:
                multiplier = 1f + card.value;
                unit.critChanceMultiplier.Add(multiplier);
                break;

            case SpellEffectType.CritDamageBuff:
                multiplier = 1f + card.value;
                unit.critDamageMultiplier.Add(multiplier);
                break;
                /*case SpellEffectType.HealthBuff:
                    unit.healthMultiplier.Add(card.value);
                    break;
                case SpellEffectType.ManaRegenBuff:
                    unit.manaRegenMultiplier.Add(card.value);
                    break;
                case SpellEffectType.DamageReduction:
                    unit.damageTakenMultiplier.Add(-card.value);
                    break;
                case SpellEffectType.DamageIncrease:
                    unit.attackPowerMultiplier.Add(card.value);
                    break;
                case SpellEffectType.CritChanceBuff:
                    unit.critChanceMultiplier.Add(card.value);
                    break;
                case SpellEffectType.CritDamageBuff:
                    unit.critDamageMultiplier.Add(card.value);
                    break;*/
        }

        // 초 단위 버프일 경우 자동 해제 코루틴 실행
        if (card.durationType == SpellDurationType.Time && card.duration > 0f)
        {
            StartCoroutine(RemoveBuffAfterDuration(unit, card, card.duration));
        }

        // 웨이브 단위 / 영구 효과는 기존처럼 activeBuffs에 등록
        else if (card.durationType != SpellDurationType.Time)
        {
            activeBuffs.Add(card);
        }

        card.appliedMultiplier = multiplier;
    }

    private IEnumerator RemoveBuffAfterDuration(BaseUnit unit, SpellCard card, float duration)
    {
        yield return new WaitForSeconds(duration);
        RemoveBuff(unit, card); // 실제 해제는 여기서 호출
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
        float multiplier = 1f + card.value;

        switch (card.effectType)
        {
            case SpellEffectType.HealthBuff:
                unit.healthMultiplier.Remove(multiplier);
                break;
            case SpellEffectType.ManaRegenBuff:
                unit.manaRegenMultiplier.Remove(multiplier);
                break;
            case SpellEffectType.DamageReduction:
                multiplier = Mathf.Max(0f, 1f - card.value);
                unit.damageTakenMultiplier.Remove(multiplier);
                break;
            case SpellEffectType.DamageIncrease:
                unit.attackPowerMultiplier.Remove(multiplier);
                break;
            case SpellEffectType.CritChanceBuff:
                unit.critChanceMultiplier.Remove(multiplier);
                break;
            case SpellEffectType.CritDamageBuff:
                //unit.critDamageMultiplier.RemoveMultiplier(card.value);
                break;
        }
    }
}

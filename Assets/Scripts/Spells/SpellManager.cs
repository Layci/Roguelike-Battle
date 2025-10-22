using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpellEnums;

public class SpellManager : MonoBehaviour
{
    public List<SpellCard> allCards;        // ��ü ī�� Ǯ
    public List<SpellCard> acquiredCards;   // �÷��̾ ���� ī��
    public List<SpellCard> activeBuffs = new List<SpellCard>();

    // ���̺� ���� �� ī�� ���� (��: 5�� �̱�)
    public List<SpellCard> GetRandomCards(int count)
    {
        List<SpellCard> available = new List<SpellCard>(allCards);

        // ���� �ߺ� ���� �� �̹� ���� �� ����
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

    // ī�� ȹ�� �� ó��
    public void AcquireCard(BaseUnit unit, SpellCard card)
    {
        acquiredCards.Add(card);
        ApplyCard(unit, card);
    }

    // ī�� ����
    public void ApplyCard(BaseUnit unit, SpellCard card)
    {
        if (card.duration == 0) // �����
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
        else // ������ (���� ����)
        {
            activeBuffs.Add(card);
            ApplyBuff(unit, card);
        }
    }

    private void ApplyBuff(BaseUnit unit, SpellCard card)
    {
        float multiplier = 1f; // �⺻ ���� 1��

        switch (card.effectType)
        {
            case SpellEffectType.HealthBuff:
                multiplier = 1f + card.value; // ex) +0.2f �� 1.2��
                unit.healthMultiplier.Add(multiplier);
                break;

            case SpellEffectType.ManaRegenBuff:
                multiplier = 1f + card.value;
                unit.manaRegenMultiplier.Add(multiplier);
                break;

            case SpellEffectType.DamageReduction:
                multiplier = Mathf.Max(0f, 1f - card.value); // ex) 0.2f �� �޴� ���� 0.8��
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

        // �� ���� ������ ��� �ڵ� ���� �ڷ�ƾ ����
        if (card.durationType == SpellDurationType.Time && card.duration > 0f)
        {
            StartCoroutine(RemoveBuffAfterDuration(unit, card, card.duration));
        }

        // ���̺� ���� / ���� ȿ���� ����ó�� activeBuffs�� ���
        else if (card.durationType != SpellDurationType.Time)
        {
            activeBuffs.Add(card);
        }

        card.appliedMultiplier = multiplier;
    }

    private IEnumerator RemoveBuffAfterDuration(BaseUnit unit, SpellCard card, float duration)
    {
        yield return new WaitForSeconds(duration);
        RemoveBuff(unit, card); // ���� ������ ���⼭ ȣ��
    }

    // ���̺� ���� �� ȣ��
    public void OnWaveEnd(BaseUnit unit)
    {
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            if (activeBuffs[i].duration > 0) // ����(-1) ����
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

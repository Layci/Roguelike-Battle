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
                case SpellEffectType.HealInstant:
                    unit.Heal(card.value);
                    break;
                case SpellEffectType.ManaInstant:
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

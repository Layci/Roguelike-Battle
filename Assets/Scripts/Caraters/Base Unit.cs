using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [Header("캐릭터 데이터 (원본 SO)")]
    public CharacterData characterData;

    [Header("멀티플라이어 시스템")]
    public StatMultiplier healthMultiplier = new StatMultiplier();
    public StatMultiplier manaRegenMultiplier = new StatMultiplier();
    public StatMultiplier damageTakenMultiplier = new StatMultiplier();
    public StatMultiplier attackPowerMultiplier = new StatMultiplier();
    public StatMultiplier critChanceMultiplier = new StatMultiplier();
    public StatMultiplier critDamageMultiplier = new StatMultiplier();

    // 현재 값
    public int CurrentHealth { get; private set; }
    public int CurrentMana { get; private set; }

    // 최종 계산된 값
    public int MaxHealth => Mathf.RoundToInt(characterData.maxHealth * healthMultiplier.GetMultiplier());
    public int AttackPower => Mathf.RoundToInt(characterData.attackPower * attackPowerMultiplier.GetMultiplier());
    public int SkillPower => Mathf.RoundToInt(characterData.skillPower * attackPowerMultiplier.GetMultiplier());
    public int CritChance => Mathf.RoundToInt(characterData.critChance * critChanceMultiplier.GetMultiplier());
    public int CritDamage => Mathf.RoundToInt(characterData.critDamage * critDamageMultiplier.GetMultiplier());

    void Awake()
    {
        if (characterData != null)
        {
            CurrentHealth = characterData.maxHealth;
            CurrentMana = 0;
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + Mathf.RoundToInt(amount), MaxHealth);
    }

    public void GainMana(float amount)
    {
        CurrentMana = Mathf.Min(CurrentMana + Mathf.RoundToInt(amount * manaRegenMultiplier.GetMultiplier()), characterData.maxMana);
    }

    public void TakeDamage(int rawDamage)
    {
        float multiplier = damageTakenMultiplier.GetMultiplier();
        int finalDamage = Mathf.RoundToInt(rawDamage * multiplier);

        CurrentHealth -= finalDamage;
        if (CurrentHealth <= 0) Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}

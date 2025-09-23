using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [Header("�⺻ ����")]
    public int baseMaxHealth = 2000;
    public int baseMana = 50;
    public int baseAttackPower = 100;
    public int baseSkillPower = 400;

    [Header("��Ƽ�ö��̾� �ý���")]
    public StatMultiplier healthMultiplier = new StatMultiplier();
    public StatMultiplier manaRegenMultiplier = new StatMultiplier();
    public StatMultiplier damageTakenMultiplier = new StatMultiplier();
    public StatMultiplier attackPowerMultiplier = new StatMultiplier();
    public StatMultiplier critChanceMultiplier = new StatMultiplier();
    public StatMultiplier critDamageMultiplier = new StatMultiplier();

    // ���� ��
    public int CurrentHealth { get; private set; }
    public int CurrentMana { get; private set; }

    // ���� ���� ��
    public int MaxHealth => Mathf.RoundToInt(baseMaxHealth * healthMultiplier.GetMultiplier());
    public int AttackPower => Mathf.RoundToInt(baseAttackPower * attackPowerMultiplier.GetMultiplier());
    public int SkillPower => Mathf.RoundToInt(baseSkillPower * attackPowerMultiplier.GetMultiplier());

    void Awake()
    {
        CurrentHealth = baseMaxHealth;
        CurrentMana = 0;
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + Mathf.RoundToInt(amount), MaxHealth);
    }

    public void GainMana(float amount)
    {
        CurrentMana = Mathf.Min(CurrentMana + Mathf.RoundToInt(amount * manaRegenMultiplier.GetMultiplier()), baseMana);
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

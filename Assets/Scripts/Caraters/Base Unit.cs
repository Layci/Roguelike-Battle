using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class BaseUnit : MonoBehaviour
{
    [Header("ĳ���� ������ (���� SO)")]
    public CharacterData characterData;

    [Header("���� �÷���")]
    public bool isDead = false;

    [Header("��Ƽ�ö��̾� �ý���")]
    public StatMultiplier healthMultiplier = new StatMultiplier(); // ü��
    public StatMultiplier manaRegenMultiplier = new StatMultiplier(); // ���� ȸ����
    public StatMultiplier damageTakenMultiplier = new StatMultiplier(); // �޴� ���� ���ҷ�
    public StatMultiplier attackPowerMultiplier = new StatMultiplier(); // �Ϲݰ��� ������
    public StatMultiplier attackSkillPowerMultiplier = new StatMultiplier(); // ��ų���� ������
    public StatMultiplier critChanceMultiplier = new StatMultiplier();  // ũ��Ƽ�� Ȯ��
    public StatMultiplier critDamageMultiplier = new StatMultiplier();  // ũ��Ƽ�� ������
    public StatMultiplier attackSpeedMultiplier = new StatMultiplier(); // ���� �ִϸ��̼� �ӵ�
    public StatMultiplier attackRangeMultiplier = new StatMultiplier(); // ���� ��Ÿ�
    public StatMultiplier attackCooldownMultiplier = new StatMultiplier(); // ���� �ӵ�

    public Animator animator;

    // ���� ��
    public int CurrentHealth { get; private set; }
    public int CurrentMana { get; private set; }
    public float CurrentAttackRange { get; private set; }
    public float CurrentAttackCooldown { get; private set; }

    // ���� ���� ��
    public int MaxHealth => Mathf.RoundToInt(characterData.maxHealth * healthMultiplier.GetMultiplier());
    public int MaxMana => characterData.maxMana;
    public int ManaRegen => Mathf.RoundToInt(characterData.manaPerAttack * healthMultiplier.GetMultiplier());
    public int AttackPower => Mathf.RoundToInt(characterData.attackPower * attackPowerMultiplier.GetMultiplier());
    public int SkillPower => Mathf.RoundToInt(characterData.skillPower * attackPowerMultiplier.GetMultiplier());
    public int CritChance => Mathf.RoundToInt(characterData.critChance * critChanceMultiplier.GetMultiplier());
    public int CritDamage => Mathf.RoundToInt(characterData.critDamage * critDamageMultiplier.GetMultiplier());
    public float AttackSpeed => characterData.attackSpeed * attackSpeedMultiplier.GetMultiplier();
    public float AttackRange => characterData.attackRange * attackRangeMultiplier.GetMultiplier();
    public float AttackCooldown => 1f / AttackSpeed; // ���ݼӵ� ��� ����� ��Ÿ��
    public float MoveSpeed => characterData.moveSpeed;

    public virtual void Awake()
    {
        if (characterData != null)
        {
            CurrentHealth = characterData.maxHealth;
            CurrentMana = 0;
            CurrentAttackCooldown = 1f / characterData.attackSpeed;
            CurrentAttackRange = characterData.attackRange;
            animator = GetComponent<Animator>();
        }
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + Mathf.RoundToInt(amount), MaxHealth);
    }

    public void GainMana(float amount)
    {
        CurrentMana = Mathf.Min(CurrentMana + Mathf.RoundToInt(amount * manaRegenMultiplier.GetMultiplier()), characterData.maxMana);
    }

    // �ִϸ��̼� �̺�Ʈ���� ����
    public virtual void PerformBasicAttack()
    {
        Debug.Log($"{gameObject.name} �⺻ ����!");
        // Ÿ�� ��� Ž�� (��Ÿ� ���� �÷��̾ �� ��)
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, AttackRange);
        foreach (var hit in hits)
        {
            BaseUnit target = hit.GetComponent<BaseUnit>();
            if (target != null && target != this)
            {
                target.TakeDamage(AttackPower);
                break; // ù ��° Ÿ�ݸ� ó��
            }
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ����
    public virtual void PerformSkill()
    {
        Debug.Log($"{gameObject.name} ��ų �ߵ�!");
        // ��ų ���� ȿ�� �� �ʿ� �� �ڽ� Ŭ�������� �������̵�
    }

    public void TakeDamage(int rawDamage)
    {
        float multiplier = damageTakenMultiplier.GetMultiplier();
        int finalDamage = Mathf.RoundToInt(rawDamage * multiplier);

        CurrentHealth -= finalDamage;
        if (CurrentHealth <= 0) Die();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, characterData != null ? characterData.attackRange : 1.5f);
    }

    public virtual void UseSkill()
    {
        if (CurrentMana >= MaxMana)
        {
            animator.SetTrigger("Skill");
            CurrentMana = 0;
        }
    }

    public virtual void Die()
    {
        isDead = true;
        if (animator) animator.SetTrigger("Die");
        Destroy(gameObject, 1.5f);
    }
}

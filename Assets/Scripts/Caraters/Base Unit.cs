using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class BaseUnit : MonoBehaviour
{
    [Header("캐릭터 데이터 (원본 SO)")]
    public CharacterData characterData;

    [Header("상태 플래그")]
    public bool isDead = false;

    [Header("멀티플라이어 시스템")]
    public StatMultiplier healthMultiplier = new StatMultiplier(); // 체력
    public StatMultiplier manaRegenMultiplier = new StatMultiplier(); // 마나 회복량
    public StatMultiplier damageTakenMultiplier = new StatMultiplier(); // 받는 비해 감소량
    public StatMultiplier attackPowerMultiplier = new StatMultiplier(); // 일반공격 데미지
    public StatMultiplier attackSkillPowerMultiplier = new StatMultiplier(); // 스킬공격 데미지
    public StatMultiplier critChanceMultiplier = new StatMultiplier();  // 크리티컬 확률
    public StatMultiplier critDamageMultiplier = new StatMultiplier();  // 크리티컬 데미지
    public StatMultiplier attackSpeedMultiplier = new StatMultiplier(); // 공격 애니메이션 속도
    public StatMultiplier attackRangeMultiplier = new StatMultiplier(); // 공격 사거리
    public StatMultiplier attackCooldownMultiplier = new StatMultiplier(); // 공격 속도

    public Animator animator;

    // 현재 값
    public int CurrentHealth { get; private set; }
    public int CurrentMana { get; private set; }
    public float CurrentAttackRange { get; private set; }
    public float CurrentAttackCooldown { get; private set; }

    // 최종 계산된 값
    public int MaxHealth => Mathf.RoundToInt(characterData.maxHealth * healthMultiplier.GetMultiplier());
    public int MaxMana => characterData.maxMana;
    public int ManaRegen => Mathf.RoundToInt(characterData.manaPerAttack * healthMultiplier.GetMultiplier());
    public int AttackPower => Mathf.RoundToInt(characterData.attackPower * attackPowerMultiplier.GetMultiplier());
    public int SkillPower => Mathf.RoundToInt(characterData.skillPower * attackPowerMultiplier.GetMultiplier());
    public int CritChance => Mathf.RoundToInt(characterData.critChance * critChanceMultiplier.GetMultiplier());
    public int CritDamage => Mathf.RoundToInt(characterData.critDamage * critDamageMultiplier.GetMultiplier());
    public float AttackSpeed => characterData.attackSpeed * attackSpeedMultiplier.GetMultiplier();
    public float AttackRange => characterData.attackRange * attackRangeMultiplier.GetMultiplier();
    public float AttackCooldown => 1f / AttackSpeed; // 공격속도 기반 계산형 쿨타임
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

    // 애니메이션 이벤트에서 참조
    public virtual void PerformBasicAttack()
    {
        Debug.Log($"{gameObject.name} 기본 공격!");
        // 타격 대상 탐색 (사거리 내의 플레이어나 적 등)
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, AttackRange);
        foreach (var hit in hits)
        {
            BaseUnit target = hit.GetComponent<BaseUnit>();
            if (target != null && target != this)
            {
                target.TakeDamage(AttackPower);
                break; // 첫 번째 타격만 처리
            }
        }
    }

    // 애니메이션 이벤트에서 참조
    public virtual void PerformSkill()
    {
        Debug.Log($"{gameObject.name} 스킬 발동!");
        // 스킬 고유 효과 → 필요 시 자식 클래스에서 오버라이드
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("강화(Enrage) 설정")]
    public bool canEnrage = false;           // 강화 가능 여부
    [Header("강화 상태에 진입할 체력 비율")]
    [Range(0f, 1f)] public float enrageThreshold = 0.5f; // 체력 비율 (기본 50%)
    [Header("강화 상태시 공격력 증가율")]
    public float enrageAttackMultiplier = 1.2f;
    [Header("강화 상태시 스킬공격력 증가율")]
    public float enrageSkillMultiplier = 1.2f;
    [Header("강화 상태시 공격력속도 증가율")]
    public float enrageAttackSpeedMultiplier = 1.2f;
    [Header("강화 상태시 이동속도 증가율")]
    public float enrageMoveSpeedMultiplier = 1.3f;

    [Header("기본 스탯")]
    public string characterName;
    public int maxHealth = 2000;
    public int maxMana = 50;
    public float moveSpeed = 5f;         // 이동 속도

    [Header("전투 스탯")]
    public int attackPower = 100;
    public int skillPower = 400;
    public int armor = 5;             // 데미지 감소율 %
    public float attackRange = 1.5f;  // 공격 사거리
    public float attackSpeed = 1.0f;  // 공격애니메이션 speed값
    public float attackCooldown = 1f; // 공격 속도
    public int manaPerAttack = 10;    // 공격당 회복되는 마나량
    public int critChance = 5;        // 크리티컬 확률 %
    public int critDamage = 100;      // 크리티컬 데미지 %
}

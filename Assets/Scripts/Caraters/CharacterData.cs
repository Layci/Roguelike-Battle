using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("기본 스탯")]
    public string characterName;
    public int maxHealth = 2000;
    public int maxMana = 50;
    public int moveSpeed = 5;         // 이동 속도

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

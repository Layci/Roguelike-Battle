using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    int maxHealth = 100; // 최대 체력
    int health = 100;    // 현재 체력
    int maxMana = 50;    // 스킬에 필요한 마나값
    int mana = 50;       // 마나
    int minMana = 0;     // 스킬 사용한 뒤 마나 초기값
    int armor = 5;       // 방어력
    int criticalarmor = 5; // %계산 ex) 5일시 크리티컬 데미지 5% 감소
    int attackSpeed = 1; // 공격 속도
    int range = 10;      // 공격 사거리
    int speed = 5;       // 이동속도
    int attackPower = 10; // 공격력
    int skillPower = 20; // 스킬 공격력
    int level = 1;      // 레벨
    int criticalChance = 5; // %계산 ex) 5일시 5% 확률로 크리티컬 발생 (기본공격, 스킬공격 포함)
    int criticalDamage = 100; // %계산 ex) 100일시 크리티컬시 공격력 2배, 스킬도 크리티컬시 스킬 공격력 2배
    int exp = 0;        // 경험치

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}

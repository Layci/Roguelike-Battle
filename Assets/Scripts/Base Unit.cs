using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    int maxHealth = 100; // �ִ� ü��
    int health = 100;    // ���� ü��
    int maxMana = 50;    // ��ų�� �ʿ��� ������
    int mana = 50;       // ����
    int minMana = 0;     // ��ų ����� �� ���� �ʱⰪ
    int armor = 5;       // ����
    int criticalarmor = 5; // %��� ex) 5�Ͻ� ũ��Ƽ�� ������ 5% ����
    int attackSpeed = 1; // ���� �ӵ�
    int range = 10;      // ���� ��Ÿ�
    int speed = 5;       // �̵��ӵ�
    int attackPower = 10; // ���ݷ�
    int skillPower = 20; // ��ų ���ݷ�
    int level = 1;      // ����
    int criticalChance = 5; // %��� ex) 5�Ͻ� 5% Ȯ���� ũ��Ƽ�� �߻� (�⺻����, ��ų���� ����)
    int criticalDamage = 100; // %��� ex) 100�Ͻ� ũ��Ƽ�ý� ���ݷ� 2��, ��ų�� ũ��Ƽ�ý� ��ų ���ݷ� 2��
    int exp = 0;        // ����ġ

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

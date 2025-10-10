using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("�⺻ ����")]
    public string characterName;
    public int maxHealth = 2000;
    public int maxMana = 50;
    public int moveSpeed = 5;         // �̵� �ӵ�

    [Header("���� ����")]
    public int attackPower = 100;
    public int skillPower = 400;
    public int armor = 5;             // ������ ������ %
    public float attackRange = 1.5f;  // ���� ��Ÿ�
    public float attackSpeed = 1.0f;  // ���ݾִϸ��̼� speed��
    public float attackCooldown = 1f; // ���� �ӵ�
    public int manaPerAttack = 10;    // ���ݴ� ȸ���Ǵ� ������
    public int critChance = 5;        // ũ��Ƽ�� Ȯ�� %
    public int critDamage = 100;      // ũ��Ƽ�� ������ %
}

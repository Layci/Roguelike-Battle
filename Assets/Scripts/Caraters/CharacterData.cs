using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("��ȭ(Enrage) ����")]
    public bool canEnrage = false;           // ��ȭ ���� ����
    [Header("��ȭ ���¿� ������ ü�� ����")]
    [Range(0f, 1f)] public float enrageThreshold = 0.5f; // ü�� ���� (�⺻ 50%)
    [Header("��ȭ ���½� ���ݷ� ������")]
    public float enrageAttackMultiplier = 1.2f;
    [Header("��ȭ ���½� ��ų���ݷ� ������")]
    public float enrageSkillMultiplier = 1.2f;
    [Header("��ȭ ���½� ���ݷ¼ӵ� ������")]
    public float enrageAttackSpeedMultiplier = 1.2f;
    [Header("��ȭ ���½� �̵��ӵ� ������")]
    public float enrageMoveSpeedMultiplier = 1.3f;

    [Header("�⺻ ����")]
    public string characterName;
    public int maxHealth = 2000;
    public int maxMana = 50;
    public float moveSpeed = 5f;         // �̵� �ӵ�

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

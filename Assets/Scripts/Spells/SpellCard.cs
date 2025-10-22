using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpellEnums;

// ���� ���ӽð� Ÿ��
public enum SpellDurationType
{
    Permanent,    // ����
    Wave,         // ���̺� ���� (���̺� ���� �� ����)
    Time          // �ð� ���� (�� ������ ����)
}

[CreateAssetMenu(fileName = "New Spell Card", menuName = "Game/Spell Card")]
public class SpellCard : ScriptableObject
{
    [Header("�⺻ ����")]
    public string spellName;      // �����ͳ� UI�� ǥ�õǴ� �̸�
    public CardRarity rarity;      // SpellEnums�� ���ǵ� enum ���
    [TextArea]
    public string description;    // ����/�����

    [Header("ȿ�� ����")]
    public SpellEffectType effectType;  // � ȿ������
    [Tooltip("0.2 = +20%, -0.3 = -30%")]
    public float value;           // ���� (1.2�� �� 0.2 �Է�)

    [Header("���� ���ӽð� ����")]
    public SpellDurationType durationType = SpellDurationType.Wave;
    public float duration = 0f;   // ���ӽð� (0�̸� ����)

    [HideInInspector] public float appliedMultiplier;

    [Header("���־� / ����")]
    public Sprite icon;           // UI�� ������
    public AudioClip castSound;   // ��� �� ����� ����
}

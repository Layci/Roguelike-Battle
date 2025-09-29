using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpellEnums;

[System.Serializable]
public class SpellCard
{
    public string name;
    public SpellEffectType effectType;
    public float value;      // ȿ�� ��ġ
    public int duration;     // 0=���, -1=����, >0=N���̺� ����
    public CardRarity rarity;
}

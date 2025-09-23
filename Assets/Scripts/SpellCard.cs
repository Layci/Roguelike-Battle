using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpellEnums;

[System.Serializable]
public class SpellCard
{
    public string name;
    public SpellEffectType effectType;
    public float value;      // 효과 수치
    public int duration;     // 0=즉시, -1=영구, >0=N웨이브 지속
    public CardRarity rarity;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("±‚∫ª Ω∫≈»")]
    public int maxHealth = 2000;
    public int maxMana = 50;
    public int attackPower = 100;
    public int skillPower = 400;

    [Header("±‚≈∏ Ω∫≈»")]
    public int armor = 5;
    public int critChance = 5;
    public int critDamage = 100;
    public int manaPerAttack = 10;
}

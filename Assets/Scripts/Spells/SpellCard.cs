using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpellEnums;

// 스펠 지속시간 타입
public enum SpellDurationType
{
    Permanent,    // 영구
    Wave,         // 웨이브 단위 (웨이브 종료 시 해제)
    Time          // 시간 단위 (초 단위로 해제)
}

[CreateAssetMenu(fileName = "New Spell Card", menuName = "Game/Spell Card")]
public class SpellCard : ScriptableObject
{
    [Header("기본 정보")]
    public string spellName;      // 에디터나 UI에 표시되는 이름
    public CardRarity rarity;      // SpellEnums에 정의된 enum 사용
    [TextArea]
    public string description;    // 툴팁/설명용

    [Header("효과 설정")]
    public SpellEffectType effectType;  // 어떤 효과인지
    [Tooltip("0.2 = +20%, -0.3 = -30%")]
    public float value;           // 비율 (1.2배 → 0.2 입력)

    [Header("스펠 지속시간 설정")]
    public SpellDurationType durationType = SpellDurationType.Wave;
    public float duration = 0f;   // 지속시간 (0이면 영구)

    [HideInInspector] public float appliedMultiplier;

    [Header("비주얼 / 사운드")]
    public Sprite icon;           // UI용 아이콘
    public AudioClip castSound;   // 사용 시 재생될 사운드
}

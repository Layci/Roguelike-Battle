using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEnums : MonoBehaviour
{
    public enum CardRarity
    {
        Common, // �Ϲ�
        Rare,   // ���
        Epic    // ���� (�ߺ� �Ұ�)
    }

    public enum SpellEffectType
    {
        HealthBuff,
        ManaRegenBuff,
        DamageReduction, // �޴� ���� ����
        DamageIncrease, // �ִ� ���� ����
        CritChanceBuff,
        CritDamageBuff,
        // �ʿ��ϸ� ��� �߰�
    }
}

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
        HealInstant,        // ��� ü�� ȸ��
        ManaInstant,        // ��� ���� ����
        HealthBuff,         // �ִ� ü�� ���� (���̺� ����/����)
        ManaRegenBuff,      // ���� �� ���� ȸ�� ���� (���̺� ����/����)
        DamageReduction,    // �޴� ���� ���� (������)
        DamageIncrease,     // �ִ� ���� ���� (������)
        CritChanceBuff,     // ġ��Ÿ Ȯ�� ����
        CritDamageBuff      // ġ��Ÿ ���ط� ����
    }
}

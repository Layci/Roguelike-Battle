using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatMultiplier
{
    private List<float> multipliers = new List<float>();

    public void Add(float value)
    {
        multipliers.Add(value);
    }

    public void Remove(float value)
    {
        multipliers.Remove(value);
    }

    public float GetMultiplier()
    {
        float total = 1f;
        foreach (float m in multipliers)
        {
            total *= m;
        }
        return total;
    }

    public void Reset()
    {
        multipliers.Clear();
    }

    /*// �ܼ� float ��� key-value ���� (���� ���� ����)
    private Dictionary<string, float> multipliers = new Dictionary<string, float>();

    /// <summary>
    /// ���� �̸�(key)���� �߰�. ���� �̸��� ���ŵ�.
    /// </summary>
    public void Add(string key, float value)
    {
        multipliers[key] = value; // �ߺ� ��� ����
    }

    /// <summary>
    /// ���� ���� (key ���)
    /// </summary>
    public void Remove(string key)
    {
        if (multipliers.ContainsKey(key))
            multipliers.Remove(key);
    }

    /// <summary>
    /// ���� ���� �� ���
    /// </summary>
    public float GetMultiplier()
    {
        float total = 1f;
        foreach (var m in multipliers.Values)
        {
            total *= m;
        }
        return total;
    }

    /// <summary>
    /// ��� ���� �ʱ�ȭ
    /// </summary>
    public void Reset()
    {
        multipliers.Clear();
    }

    /// <summary>
    /// �Ͻ��� ������ �ڷ�ƾ���� �����ϴ� ��ƿ (MonoBehaviour �ʿ��� ���)
    /// </summary>
    public IEnumerator TemporaryBuff(string key, float value, float duration)
    {
        Add(key, value);
        yield return new WaitForSeconds(duration);
        Remove(key);
    }*/

}

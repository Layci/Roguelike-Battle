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

    /*// 단순 float 대신 key-value 관리 (버프 구분 가능)
    private Dictionary<string, float> multipliers = new Dictionary<string, float>();

    /// <summary>
    /// 버프 이름(key)으로 추가. 같은 이름은 갱신됨.
    /// </summary>
    public void Add(string key, float value)
    {
        multipliers[key] = value; // 중복 등록 방지
    }

    /// <summary>
    /// 버프 제거 (key 기반)
    /// </summary>
    public void Remove(string key)
    {
        if (multipliers.ContainsKey(key))
            multipliers.Remove(key);
    }

    /// <summary>
    /// 현재 누적 곱 계산
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
    /// 모든 버프 초기화
    /// </summary>
    public void Reset()
    {
        multipliers.Clear();
    }

    /// <summary>
    /// 일시적 버프를 코루틴으로 적용하는 유틸 (MonoBehaviour 쪽에서 사용)
    /// </summary>
    public IEnumerator TemporaryBuff(string key, float value, float duration)
    {
        Add(key, value);
        yield return new WaitForSeconds(duration);
        Remove(key);
    }*/

}

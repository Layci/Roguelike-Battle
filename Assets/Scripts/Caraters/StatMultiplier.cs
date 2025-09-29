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
}

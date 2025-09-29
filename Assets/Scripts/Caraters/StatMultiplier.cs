using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatMultiplier
{
    private List<float> multipliers = new List<float>();

    public void AddMultiplier(float value)
    {
        multipliers.Add(1f + value); // ex) 0.2f = +20%, -0.3f = -30%
    }

    public void RemoveMultiplier(float value)
    {
        multipliers.Remove(1f + value);
    }

    public float GetMultiplier()
    {
        float total = 1f;
        foreach (float m in multipliers)
            total *= m;
        return total;
    }
}

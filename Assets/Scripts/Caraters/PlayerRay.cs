using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : BaseUnit
{
    public int mana = 0;
    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        mana = CurrentMana;
    }
}

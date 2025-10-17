using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(UnitController2D))]
public class EnemyDeath : BaseUnit
{
    public GameObject skillEffect;
    private UnitController2D unitController2D;
    public int mana = 0;
    public override void Awake()
    {
        base.Awake();
        unitController2D = GetComponent<UnitController2D>();
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

    public override void PerformSkill()
    {
        base.PerformSkill();
        GameObject skillPrefab = Instantiate(skillEffect, unitController2D.target.position + new Vector3(0, 0, 5), Quaternion.identity);
    }

    protected override void OnEnrage()
    {
        base.OnEnrage();
        characterData.moveSpeed *= 1.3f;
        Debug.Log($"{gameObject.name} 이 분노하여 이동속도가 증가했습니다!");
    }
}

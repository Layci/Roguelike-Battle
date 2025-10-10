using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : BaseUnit
{
    public Transform target;
    public Rigidbody2D rb;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Start()
    {
        base.Start();
        // 플레이어 타겟
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            target = player.transform;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (target == null)
        {
            return;
        }
        Vector2 dir = (target.position - transform.position).normalized;
        Vector2 newpos = rb.position + dir * characterData.moveSpeed * Time.deltaTime;

        rb.MovePosition(newpos);
    }
}

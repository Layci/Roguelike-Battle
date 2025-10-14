using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*[RequireComponent(typeof(EnemyCombat2D))]
[RequireComponent(typeof(EnemyChase2D))]*/
public class EnemyDeath : BaseUnit
{
    /*private EnemyCombat2D combat;
    private EnemyChase2D chase;*/
    //public Transform target;
    public Rigidbody2D rb;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        //combat = GetComponent<EnemyCombat2D>();
        //chase = GetComponent<EnemyChase2D>();
    }

    public override void Start()
    {
        base.Start();

        // Å¸°Ù ¼³Á¤
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //chase.target = player.transform;
            //combat.target = player.GetComponent<BaseUnit>();
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        /*if (target == null)
        {
            return;
        }
        Vector2 dir = (target.position - transform.position).normalized;
        Vector2 newpos = rb.position + dir * characterData.moveSpeed * Time.deltaTime;*/

        //rb.MovePosition(newpos);
    }
}

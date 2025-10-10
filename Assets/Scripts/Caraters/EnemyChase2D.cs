using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase2D : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    private BaseUnit unit;
    private bool canMove = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        unit = GetComponent<BaseUnit>();
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            target = player.transform;
    }

    void FixedUpdate()
    {
        if (!canMove || target == null || unit == null || unit.isDead) return;

        float distance = Vector2.Distance(transform.position, target.position);

        // ���� ��Ÿ� �ȿ� ���� �̵� ����
        if (distance <= unit.AttackRange)
            return;

        Vector2 dir = (target.position - transform.position).normalized;
        rb.MovePosition(rb.position + dir * unit.MoveSpeed * Time.fixedDeltaTime);
    }

    public void SetCanMove(bool value) => canMove = value;
}

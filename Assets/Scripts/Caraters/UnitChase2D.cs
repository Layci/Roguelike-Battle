using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseUnit), typeof(Rigidbody2D))]
public class UnitChase2D : MonoBehaviour
{
    public string targetTag = "Enemy"; // 이 유닛이 추적할 대상 태그
    public float stoppingDistance = 0.5f; // 사거리보다 살짝 작은 멈춤 거리

    private BaseUnit unit;
    private Transform target;
    private Rigidbody2D rb;

    private void Awake()
    {
        unit = GetComponent<BaseUnit>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        FindTarget();
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, target.position);

        // 사거리보다 멀면 추적
        if (distance > unit.characterData.attackRange)
        {
            rb.MovePosition(rb.position + direction * unit.characterData.moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void FindTarget()
    {
        GameObject found = GameObject.FindGameObjectWithTag(targetTag);
        if (found != null)
            target = found.transform;
    }

    public Transform GetTarget() => target;
}

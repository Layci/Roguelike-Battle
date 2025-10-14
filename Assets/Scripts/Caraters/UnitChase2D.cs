using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseUnit), typeof(Rigidbody2D))]
public class UnitChase2D : MonoBehaviour
{
    public string targetTag = "Enemy"; // �� ������ ������ ��� �±�
    public float stoppingDistance = 0.5f; // ��Ÿ����� ��¦ ���� ���� �Ÿ�

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

        // ��Ÿ����� �ָ� ����
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

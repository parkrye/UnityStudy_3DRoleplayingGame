using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    Animator animator;
    [SerializeField] float range;
    [SerializeField] int damage;
    [SerializeField] [Range(0f, 360f)] float angle;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    void OnAttack(InputValue inputValue)
    {
        Attack();
    }

    public void AttackTiming()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, range);
        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                continue;

            IHitable hitable = collider.GetComponent<IHitable>();
            hitable?.TakeHit(damage);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward + transform.up, range);
        
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position + transform.up, rightDir * range, Color.blue);
        Debug.DrawRay(transform.position + transform.up, leftDir * range, Color.blue);
    }

    Vector3 AngleToDir(float _angle)
    {
        float radian = _angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}

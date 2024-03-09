using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Range(0, 50)][SerializeField] float attackRange = 1, sighRange = 50, timeBetweenAttacks = 1;

    [Range(0, 20)][SerializeField] int power = 10;
    public Transform player;
    private NavMeshAgent thisEnemy;

    private bool isAttacking;

    private Animator animator;
    private void Start()
    {
        thisEnemy = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerHealth>().transform;
        animator = transform.GetComponent<Animator>();
    }

    private void Update()
    {

        float distanceFromPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceFromPlayer > sighRange)
        {
            animator.SetBool("isMoving", false);
            thisEnemy.isStopped = true;
        }
        if (distanceFromPlayer <= sighRange && distanceFromPlayer > attackRange && !isAttacking && !PlayerHealth.isDead)
        {
            animator.SetBool("isMoving", true);

            facePlayer();
            isAttacking = false;
            thisEnemy.isStopped = false;
            StopAllCoroutines();
            ChasePlayer();

        }
        if (distanceFromPlayer <= attackRange && !isAttacking && !PlayerHealth.isDead)
        {
            StartCoroutine(AttackPlayer());
        }

        if (PlayerHealth.isDead)
        {
            thisEnemy.isStopped = true;
            animator.SetBool("isMoving", false);
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        thisEnemy.isStopped = true;
        facePlayer();
        animator.SetTrigger("TriggerAttack");
        FindObjectOfType<PlayerHealth>().TakeDamage(power);
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
        thisEnemy.isStopped = false;
        animator.SetBool("isMoving", true);
    }

    private void ChasePlayer()
    {
        animator.SetBool("isMoving", true);
        thisEnemy.SetDestination(player.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sighRange);
    }


    private void facePlayer()
    {
        transform.LookAt(player);
        // rotate a bit left
        transform.Rotate(new Vector3(0, -18, 0));
    }
}
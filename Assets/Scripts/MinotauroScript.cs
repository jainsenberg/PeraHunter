using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotauroScript : MonoBehaviour
{
    public GameObject player;

    public int life = 35;

    public float speed;

    private float distance;

    public bool inmune = false;

    [SerializeField]
    Transform[] waypoint;

    int waypointIndex = 0;
    //amimator.SetBool("isAttack", value)
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log("" + distance);
        if (distance >= 0.5 && distance <= 0.8)
        {
            Invoke(nameof(Attack1), 2.0f);
        }
        else
        {
            animator.SetBool("isAttack1", false);
        }
        if (life <= 2)
        {
            inmune = true;
            transform.position = Vector2.MoveTowards(transform.position, waypoint[waypointIndex].transform.position, speed * Time.deltaTime);
            animator.SetBool("isWait", true);

            if (transform.position == waypoint[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }

        }
        else
        {
            Vector2 direccion = player.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (life <= 0)
        {
            animator.SetBool("isDead", true);
            Invoke(nameof(Delete), 1.19f);

        }

    }

    private void Delete()
    {
        Destroy(this.gameObject);
    }
    private void Attack1()
    {
        animator.SetBool("isAttack1", true);
    }
    // void Move(){
    //     transform.position = Vector2.MoveTowards(transform.position, waypoint[waypointIndex].transform.position, speed * Time.deltaTime);
    // }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shoot") && inmune == false)
        {
            life--;
        }
    }
}

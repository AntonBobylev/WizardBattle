using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFieldOfView : MonoBehaviour
{
    public GameObject Enemy;
    private bool timer_go = false;
    public float timer;//для потери перса из виду
    private float new_timer;
    EnemyPatrol EnemyPatrolScript;
    ToPlayer ToPlayerScript;
    private bool disable = false;
    RaycastHit hit;

    void Start()
    {
        ToPlayerScript = GameObject.FindWithTag("Enemy").GetComponent<ToPlayer>();
        EnemyPatrolScript = GameObject.FindWithTag("Enemy").GetComponent<EnemyPatrol>();
        new_timer = timer;
    }

   

    void OnTriggerEnter(Collider other)
    {
        disable = true;

        Vector3 dir = GetComponent<Transform>().transform.localPosition - transform.localPosition;
        
        if (Physics.Raycast(transform.position,dir, 10)
            && hit.collider.gameObject == GameObject.FindWithTag("Player")) //player object here will be your Player GameObject
        {
            if (other.tag == "Player")
            {
                timer_go = false;
                timer = new_timer;
                Enemy.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                //сюда анимацию ходьбы
                //Enemy.GameObject.GetComponent<Animator>().SetTrigger("Walk");
            }
        }

        
        
    }
    
    void OnTriggerExit(Collider other)
    {
        timer_go = true;
    }
    
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,0);
        if (disable == true)
        {
            ToPlayerScript.enabled = true;
            EnemyPatrolScript.enabled = false;
        }
        if (timer_go == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            disable = false;
            timer_go = false;
           
            ToPlayerScript.enabled = false;
            EnemyPatrolScript.enabled = true;
            Enemy.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();

            
            timer = new_timer;
            //сюда анимацию idle
            //Enemy.GameObject.GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}

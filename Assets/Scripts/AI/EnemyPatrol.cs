using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;//Движения бота
    public Transform[] patrolPoints;//Точки патруля
    public int current_point = 0;//Указывает на текущую точку патруля, куда идет бот
    public GameObject Player;//Объект игрок
    public bool to_next_point = false;//Надо ли идти на следующую точку сейчас?
    ToPlayer ToPlayerScript;//Для выключения/включения скрипта ToPlayer
    public bool disable = false;//Включить/отключить (для скриптов)


    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //Инициализирую движок бота
        if(agent.enabled == false)//Если он не вкнлючен
            agent.enabled = true;//Включаю
        ToPlayerScript = GetComponent<ToPlayer>();//Инициализирую переменную для включения/выключения скрипта ToPlayer
        to_next_point = true;//Боту надо идти до следующей точки патруля
    }

    void MoveToNextPatrolPoint()//Движение бота к следующей точке
    {
        if(patrolPoints.Length > 0)//Если точки вообще есть
            if(to_next_point == true)//И надо идти
            {
                agent.destination = patrolPoints[current_point].position;//Направляю бота на следующую точку
                current_point++;//В текущуюю точку записываю следующую
                current_point %= patrolPoints.Length;
            }
    }

    void Update()
    {
        if(disable == false)//Если выключаем скрипт EnemyPatrol
        {
            ToPlayerScript.enabled = false;//Включаем скрипт ToPlayer
        }

        if(agent.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled == true)//Если включен движок бота
        {
            float dist = Vector3.Distance(Player.transform.position, this.transform.position);//Считаем дистанцию от бота до точки патруля

            bool patrol = false;//Для проверки точек патруля

            patrol = patrolPoints.Length > 0;//Если точки есть, то true, иначе false
            if (!patrol)//Если точек нет
                agent.SetDestination(transform.position);//Отправяю бота к себе
            if(patrol)//Если точки есть
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    MoveToNextPatrolPoint();//Отправляю бота до следующей точки
            }
        }
    }
}

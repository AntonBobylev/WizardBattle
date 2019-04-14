using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDA_Player : MonoBehaviour
{
    public float y;
    public float x;

    public static float rotate_x = 0.0f; //угол поворота персонажа по оси x
    public int rotation = 250; //Скорость пповорота персонажа 

    //!!!!!!!!!!!!!
    public GameObject player;
    public float speed = 0.1f;

    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    public GameObject skill4;

    public AnimationClip anim;


    void Start()
    {
        player = (GameObject)this.gameObject;
    }
    void Update()
    {
        y = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Instantiate(skill1, transform.position, transform.rotation);
        //}

        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y, 0), 0.02f);
        //    player.transform.position += player.transform.forward * speed * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y + 180, 0), 0.02f);
        //    player.transform.position += player.transform.forward * speed * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y - 90, 0), 0.02f);
        //    player.transform.position += player.transform.forward * speed * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y + 90, 0), 0.02f);
        //    player.transform.position += player.transform.forward * speed * Time.deltaTime;
        //}

        if (Input.GetKey(KeyCode.W)) //Если нажать W 
        {
            player.transform.position += player.transform.forward * speed * Time.deltaTime; //Перемещаем персонажа в перед, с заданой скорость. Time.deltaTime ставится для плавного перемещения персонажа, если этого не будет он будет двигаться рывками 
            
        }

        if (Input.GetKey(KeyCode.S))
        {
            player.transform.position -= player.transform.forward * speed * Time.deltaTime; //Перемещаем назад 
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.position -= player.transform.right * speed * Time.deltaTime; //перемещаем в лево 
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.position += player.transform.right * speed * Time.deltaTime; //перемещаем в право 
        }
       

        //FixedUpdate();

        //Поворачиваем персонажа. Так как наша переменная x глобальна, из скрипта камеры в неё будем записывать длину на сколько сместился указатель мыши и по оси X и относительно этого будет повернут наш персонаж 
        Quaternion rotate = Quaternion.Euler(0, rotate_x, 0); //Создаем новую переменную типа Quaternion для задавания угла поворота 
        player.transform.rotation = rotate; //Поворачиваем персонаж 
    }

    void FixedUpdate()
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", x, 0.1f, Time.deltaTime);
        gameObject.GetComponent<Animator>().SetFloat("Direction", y, 0.1f, Time.deltaTime);
        //transform.position = transform.forward * 10.0f * Time.deltaTime;
    }
}




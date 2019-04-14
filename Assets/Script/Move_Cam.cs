using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Cam : MonoBehaviour
{
    
    public float distance = 3.0f; //На каком ратоянии от него 
    public float xSpeed = 125.0f; //Чуствительность по Х 
    public float ySpeed = 50.0f; //Y Чуствительность 
    public float targetHeight = 2.0f; //Высота относительно объекта 
                                      //Минимальный и максимальный угол поворота Y инче камеру разверет, Дальше у нас будет простая функция для инвертации их в обратные числа 
    public float yMinLimit = -40;
    public float yMaxLimit = 80;
    //Максимальное удаление и приближение камеры к персонажу, искорость. 
    public float maxDistance = 10.0f;
    public float minDistance = 0.5f;
    public float zoomRote = 90.0f;

    private float x = 0.0f; //Угол поворота по Y? 
    private float y = 0.0f; //Уго поворота по X? 

    [AddComponentMenu("Scripts/Mouse Orbit")] //Добавляем в меню 

    public void Start()
    {
        //переворачивам углы 
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        //if (rigidbody)
        //    rigidbody.freezeRotation = true; //Если камера столкнется с физ.объектомона остановиться 
    }

    public void LateUpdate()
    {
        
         //Если цель установлена(Персонаж) 
         //Меняем углы согласно положению мыши 
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            //Меняем дистанция до персонажа. 
            distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRote * Mathf.Abs(distance);
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            y = ClampAngle(y, yMinLimit, yMaxLimit); //Вызыв самописной функции для ограничения углов поврот 
        if(Input.GetKey(KeyCode.W | KeyCode.A | KeyCode.S | KeyCode.D))
        {
            Move_Player.x = x;
            TDA_Player.rotate_x = x;
        }
           
            //Повернуть камеру согласно поченым данным 
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;

            //Двигаем камеру и следим за персонажем 
            Vector3 position = rotation * new Vector3(0.0f, targetHeight + 0.5f, -distance);
            transform.position = position;

           

    }
    //Меняем значения углов 
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicMagic : MonoBehaviour
{

    public KeyCode KeyBeginMagic = KeyCode.G;
    public KeyCode KeyFire = KeyCode.Alpha1;
    public GameObject FireProjecile;
    public Transform Spawn;
    public float FireRate = 0.5f;
    public float NextFire = 0.0f;
    int phase = 0;
    int Element = 0;
    int Form = 0;
    int Range = 0;
    GameObject capsule;

    // Start is called before the first frame update
    void Start()
    {
        capsule = GameObject.Find("Capsule");
    }
    IEnumerator StartCastMagicElement()
    {
        while(true)
        {
            switch(phase)
            {
                case 1:
                    {
                        
                        if (Input.GetKeyDown(KeyCode.Alpha1))
                        {
                            yield return new WaitForFixedUpdate();
                            Element = 1;
                            phase = 2;
                            Debug.Log("Fire Magic (1-Projectile 2-Stream)");
                        }
                        if (Input.GetKeyDown(KeyCode.Alpha2))
                        {
                            yield return new WaitForFixedUpdate();
                            Element = 2;
                            phase = 2;
                            Debug.Log("Ice Magic (1-Projectile 2-Stream)");
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            Element = Form = Range = phase = 0;
                            yield break;
                        }
                        
                        break;
                    }

                case 2:
                    {
                        if(Input.GetKeyDown(KeyCode.Alpha1))//Форма снаряда
                        {
                            yield return new WaitForFixedUpdate();
                            Form = 1;
                            phase=3;
                            Debug.Log("Projectile (1-Vector 2-Me 3-Region)");
                            
                        }
                        if (Input.GetKeyDown(KeyCode.Alpha2))//Форма потока
                        {
                            yield return new WaitForFixedUpdate();
                            Form = 2;
                            phase=3;
                            Debug.Log("Stream (1-Vector 2-Me 3-Region)");
                            
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            Element = Form = Range = phase = 0;
                            yield break;
                        }

                        break;
                    }

                case 3:
                    {
                        
                        if (Input.GetKeyDown(KeyCode.Alpha1))//Вперед
                        {
                            yield return new WaitForFixedUpdate();
                            Range = 1;
                            phase=4;
                            Debug.Log("Vector");
                        }
                        if (Input.GetKeyDown(KeyCode.Alpha2))//На себя
                        {
                            yield return new WaitForFixedUpdate();
                            Range = 2;
                            phase=4;
                            Debug.Log("Me");
                           
                        }
                        if (Input.GetKeyDown(KeyCode.Alpha3))//Вокруг себя
                        {
                            yield return new WaitForFixedUpdate();
                            Range = 3;
                            phase=4;
                            Debug.Log("Region");
                            
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            Element = Form = Range = phase = 0;
                            yield break;
                            
                        }
                        break;
                    }
                case 4:
                    {
                        if(Element == 1 && Form == 1 && Range == 1 && Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            yield return null;
                            Element = Form = Range = phase = 0;
                            CreateProjectile();
                            
                        }
                        if (Element == 1 && Form == 2 && Range == 1 && Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            yield return null;
                            Element = Form = Range = phase = 0;
                            StartCoroutine(CreateStream());
                        }
                        if (Element == 1 && Form == 1 && Range == 3 && Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            yield return null;
                            Element = Form = Range = phase = 0;
                            CreateAuraProjectile();
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            Element = Form = Range = phase = 0;
                            yield break;
                            
                        }

                        break;
                    }
                default:
                    {
                        Element = Form = Range = phase = 0;
                        yield break;
                    }
            }
            yield return null;
        }
    }
    void CreateProjectile()
    {
        // Object ball =  Resources.Load(GameObject.FindWithTag("FireBall").name);
        Object ball = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/FireProjectile.prefab", typeof(GameObject));
        if(!ball)
        {
            Debug.Log("Not loaded");
        }
        
        GameObject.Instantiate(ball, Spawn.position, Spawn.rotation);
    }//Создание снаряда
    void CreateAuraProjectile()//Создание взрыва вокруг себя
    {
        RaycastHit hit;
        Vector3 pos = transform.position;
        if (Physics.SphereCast(pos, 6.0f, transform.forward, out hit, 6)&&Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("Found an object " + hit.transform.name + "distance: " + hit.distance);
        }
    }
    IEnumerator CreateStream()//Создание потока
    {
        
        RaycastHit hit;
        while (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(transform.position, capsule.transform.forward, out hit, 6.0f))
            {
                print("Found an object " + hit.transform.name + "distance: " + hit.distance);
            }
            yield return null;
        }
        yield break;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyBeginMagic)&&phase==0)
        {
                phase = 1;
                Debug.Log("Begin Magic");
                StartCoroutine(StartCastMagicElement());
        }
    }

}

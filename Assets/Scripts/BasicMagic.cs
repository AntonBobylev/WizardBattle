using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicMagic : MonoBehaviour
{

    public KeyCode KeyBeginMagic = KeyCode.G;
    private int phase = 0;
    private int Element = 0;
    private int Form = 0;
    private int Range = 0;
    private float RangeRegionDamage = 10.0f;//Размер взрыва
    private float RangeStreamDamage = 6.0f;//Длина потока
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
    //Создание снаряда
    void CreateProjectile()
    {
        // Object ball =  Resources.Load(GameObject.FindWithTag("FireBall").name);
        Object ball = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/FireProjectile.prefab", typeof(GameObject));
        if(!ball)
        {
            Debug.Log("Not loaded");
        }
        
        GameObject.Instantiate(ball, capsule.transform.position + capsule.transform.forward * 0.8f,capsule.transform.rotation);
    }
    //Создание взрыва вокруг себя
    void CreateAuraProjectile()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, RangeRegionDamage);
        int i = 0;
        while (i < hitColliders.Length)
        {
            print(hitColliders[i].transform.name);
            if(hitColliders[i].transform.name == "Enemy")
            {
                GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>().Health -= 20f;
            }
            i++;
        }
        print("BA-DA-BUM");
    }
    //Создание потока
    IEnumerator CreateStream()
    {
        
        RaycastHit hit;
        while (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(transform.position, capsule.transform.forward, out hit, RangeStreamDamage))
            {
                print("Found an object " + hit.transform.name + "distance: " + hit.distance);
                if(hit.transform.name == "Enemy")
                {
                    GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>().Health -= 0.2f;
                }
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

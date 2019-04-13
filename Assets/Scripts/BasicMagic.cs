using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
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
                            Debug.Log("Fire Magic");
                        }
                        if (Input.GetKeyDown(KeyCode.Alpha2))
                        {
                            yield return new WaitForFixedUpdate();
                            Element = 2;
                            phase = 2;
                            Debug.Log("Ice Magic");
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
                            Debug.Log("Range");
                            
                        }
                        if (Input.GetKeyDown(KeyCode.Alpha2))//Форма потока
                        {
                            yield return new WaitForFixedUpdate();
                            Form = 2;
                            phase=3;
                            Debug.Log("Range");
                            
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
        Instantiate(FireProjecile, Spawn.position, Spawn.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f))
            print("Found an object - distance: " + hit.distance);

        if (Input.GetKeyDown(KeyBeginMagic)&&phase==0)
        {
                phase = 1;
                Debug.Log("Begin Magic");
                StartCoroutine(StartCastMagicElement());
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicProjectile : MonoBehaviour
{
    Vector3 lastPos = new Vector3(0.0f,0.0f,0.0f);
    float Speed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        RaycastHit hit;
        Debug.DrawLine(lastPos, transform.position);
        if(Physics.Linecast(lastPos,transform.position,out hit))
        {
            
            if(gameObject && hit.transform.name!="FireProjectile(Clone)")
            {
                print(hit.transform.name);
                Destroy(gameObject);
            }
            if (hit.transform.name == "Enemy")
            {
                GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>().Health -= 60f;
            }
        }

        lastPos = transform.position;
    }
}

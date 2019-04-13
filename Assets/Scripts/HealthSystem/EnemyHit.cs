using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>().Health -= 100;         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

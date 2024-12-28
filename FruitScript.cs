using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{

    ParticleSystem curPartycle;
    public bool isPlay = false;

    private void Awake()
    {
        curPartycle = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        
    }

    public void PlayParticle()
    {
        if (isPlay == false)
        {
            curPartycle.Play();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }


    public float timer = 0;
    void Update()
    {
        if (isPlay)
        {
            timer += Time.deltaTime;
        }

        if (timer > 0.4f)
        {
            Die();
        }
    }
}

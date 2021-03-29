using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{
    protected float startSpawnTime;

    public float destroyedTime, speed;
    public bool haveDestroyedTime;
    public ParticleSystem startEffect;

    //public GameObject effectSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        if (startEffect != null)
        {
            Instantiate(startEffect);
            startEffect.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

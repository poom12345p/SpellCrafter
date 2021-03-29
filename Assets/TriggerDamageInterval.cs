using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamageInterval : MonoBehaviour
{
    // Start is called before the first frame update
    public float interval;

    float startTime;
    DamageObject dmo;
    void OnEnable()
    {
        dmo = GetComponent<DamageObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= startTime + interval) dmo.isActiveHit = true;
    }

    public void TriggerTime()
    {
        dmo.isActiveHit = false;
        startTime = Time.time;
    }
}

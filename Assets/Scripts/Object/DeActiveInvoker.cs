using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeActiveInvoker : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    ParticleSystem particle;
    [SerializeField]
    float delayTime;

    public UnityEvent deactiveEvent;
    // Start is called before the first frame update
    public void DeActiveWaitForParticle()
    {
        Invoke("DeActive", particle.duration);
    }

    public void DeActiveWaitForAudio()
    {
        Invoke("DeActive", audioSource.clip.length);
    }

    public void DeActiveWaitForDelayTime()
    {
        Invoke("DeActive", delayTime);
    }

    public void DeActiveEventWaitForParticle()
    {
        Invoke("DeActiveOnlyEvent", particle.duration);
    }

    public void DeActiveEventWaitForAudio()
    {
        Invoke("DeActiveOnlyEvent", audioSource.clip.length);
    }

    public void DeActiveEventWaitForDelayTime()
    {
        Invoke("DeActiveOnlyEvent", delayTime);
    }


    public void DeActive()
    {
        deactiveEvent.Invoke();
        gameObject.SetActive(false);
    }

    public void DeActiveOnlyEvent()
    {
        deactiveEvent.Invoke();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class AnimationEvent : MonoBehaviour
{
    [System.Serializable]
    public struct AnimAction
    {
        public int id;
        public UnityEvent unityEvent;
    }
    [SerializeField]
    protected AudioSource bodyAudioSource;
    [SerializeField]
    protected AudioSource feetAudio;

    public List<UnityEvent> animActionList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootSteep()
    {
        feetAudio.Play();
    }
    public void PlayAudio(AudioClip ac)
    {
        bodyAudioSource.clip = ac;
        bodyAudioSource.Play();
    }

    public void ActiveAction(int i)
    {
        if (animActionList!=null && i < animActionList.Count && i >= 0)
        {
            animActionList[i].Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorsShake : Interactable
{
    Animator[] floorAnimators;
    public Transform[] floorTranfrom;
    public AudioSource floorAudio;
    public AudioClip preBreakSoundClip;
    public AudioClip breakSoundClip;
    // Start is called before the first frame update
    void Start()
    {
        floorAnimators = new Animator[floorTranfrom.Length];
        for (int i = 0; i < floorTranfrom.Length; i++)
        {
            if(floorTranfrom[i].GetComponent<Animator>())
             floorAnimators[i] = floorTranfrom[i].GetComponent<Animator>();
        }

        foreach (var anim in floorAnimators)
        {
            if (anim!=null)
                 anim.SetFloat("spd", Random.Range(0.7f, 1.5f));
            
        }
    }

    // Update is called once per frame
    public void ActiveShake()
    {
        foreach (var anim in floorAnimators)
        {
            anim.SetBool("shake",true);
        }
    }

    public void DeActiveShake()
    {
        foreach (var anim in floorAnimators)
        {
            anim.SetBool("shake", false);

        }
    }

    public void BreakShake()
    {
        foreach (var anim in floorAnimators)
        {
            anim.SetBool("shake", true);
        }
    }
    public void ActiveCrack()
    {
        foreach (var floor in floorTranfrom)
        {
            floor.Rotate(new Vector3(0.0f, 0.0f, Random.Range(-15.0f,15.0f)));

        }
        floorAudio.clip = preBreakSoundClip;
        floorAudio.Play();
        Invoke("BreakFloor",  preBreakSoundClip.length);
    }
    public void BreakFloor()
    {
        floorAudio.clip = breakSoundClip;
        foreach (var floor in floorTranfrom)
        {
            floor.transform.SetParent(transform.parent);

            if(floor.GetComponent<Collider2D>())
                floor.GetComponent<Collider2D>().enabled = true;

            var rid = floor.GetComponent<Rigidbody2D>();
            rid.gravityScale = 1;
           // rid.AddForce(new Vector2.r)



        }
        floorAudio.Play();
        SetActiveFalse();
        DisableInteract();
    }
}

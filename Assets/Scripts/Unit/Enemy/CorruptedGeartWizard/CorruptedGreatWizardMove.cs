using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedGreatWizardMove : EnemyMove
{
    [SerializeField]
    SpinAroundObject woodenSheild;
    [SerializeField]
    SpinAroundObject fireSheild;

    [SerializeField]
    ObjectPooler skillPooler;

    // private Vector3 stayPos;

    // [HideInInspector]
    // public bool isCast;
    [SerializeField]
    GameObject[] smokes;

    float countperSec;
    public AudioSource laughAudioSource;
    public AudioSource SmokeAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        countperSec = (1.0f / Time.deltaTime);
        isImmuneDaze = true;
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();




    }
    public void FireMagicBall()
    {
        animator.SetTrigger("FireMagic");
        var obj = skillPooler.SpawnFromPool("MagicBall", transform.position, faceDirection == 1 ? Quaternion.identity : Quaternion.Euler(0.0f, 180.0f, 0.0f));
        var dmgobj = obj.GetComponent<DamageObject>();
        if (dmgobj)
            dmgobj.SetOwner(gameObject.GetComponent<Unit>());
    }

    public void CreateWoodenSheild()
    {
        // isCast = true;
        StartCast();
        fireSheild.DisableObjects();
        woodenSheild.CreateObjects();
    }

    public void CreateFireBalls()
    {
        //isCast = true;
       
        StartCast();
        woodenSheild.DisableObjects();
        fireSheild.CreateObjects();
    }

    public void StartCast()
    {
        animator.SetBool("Cast", true);
        if(!laughAudioSource.isPlaying)laughAudioSource.Play();
    }

    public void EndCast()
    {
        animator.SetBool("Cast", false);
    }
    public void moveTo(Vector3 pos)
    {
        var des= Vector3.MoveTowards(transform.position, pos, speedX * Time.deltaTime); ;
       // Debug.LogFormat("speed : {0}",transform.position,des)* countperSec);
        animator.SetFloat("Speed",Mathf.Abs(transform.position.x- des.x) * countperSec);
        transform.position = des;
    
    }




    public override void FaceTo(bool isLeft)
    {
        base.FaceTo(isLeft);
    }

    public void StartCastMagic()
    {

    }

    public void StartCastSmoke()
    {
        StartCast();
        Invoke("EndCastSmoke", 3.0f);
        
    }

    public void EndCastSmoke()
    {
        EndCast();
        if (!GetComponent<CorruptedGreatWizardUnit>().isDaed)
        {
            SmokeAudioSource.Play();
            foreach (var smk in smokes)
            {
                smk.SetActive(true);
                smk.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void ClearSmoke()
    {

        foreach (var smk in smokes)
        {
            smk.GetComponent<Interactable>().Interacted();
            smk.GetComponent<Collider2D>().enabled = false;
        }
    }
    public void StartCastSpike(Vector2 pos)
    {

        var obj = skillPooler.SpawnFromPool("Spike", pos, Quaternion.identity);
    }

  
}

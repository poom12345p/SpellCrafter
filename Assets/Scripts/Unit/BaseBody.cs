using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBody : MonoBehaviour
{

    public UnityEvent afterBeHitEvent;

    [SerializeField]
    protected bool isReciveDamage = true;


    [SerializeField]
    protected float disableDamagedTime = 0.5f;
    protected float disableDamagedCounter = 0;

    protected Collider2D myCol;

    [SerializeField]
    protected ParticleSystem beingHitEffect;
    [SerializeField]
    protected ObjectPooler ParticlePool;
    [SerializeField]
    protected AudioSource bodyAudioSource;
    [SerializeField]
    protected AudioClip beingHitAudioClip;

    [HideInInspector]
    protected List<ReciveHit> reciveHits;

    public bool unBreakRock;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {

        //else
        //{
        //    isWet = false;
        //}

    


        if (disableDamagedCounter > 0)
        {
            disableDamagedCounter -= Time.deltaTime;
        }

        if (!isReciveDamage && disableDamagedCounter <= 0 )
        {
            isReciveDamage = true;
        }


    }
    public virtual void ReciveHitAction(GameObject hitObj)
    {
        // if (!isDetectHit) return;
        if (!isReciveDamage) return;


        if (reciveHits != null)
        {
            foreach (var rh in reciveHits)
            {
                rh.PrefromReciveHit(hitObj.GetComponent<HitArea>());
            }
        }

        afterBeHitEvent.Invoke();

    }




   

   

    
    protected virtual void OnStart()
    {


        if (beingHitEffect != null)
        {
            beingHitEffect = Instantiate(beingHitEffect);
        }
        myCol = GetComponent<Collider2D>();
        if (!myCol)
        {
            Debug.LogError("can't find colider2D of " + name);
        }

    }


    //public int GetDamage()
    //{
    //    return baseDmage;
    //}

  

    public void AddReciveHitObserver(ReciveHit rh)
    {
        if(reciveHits==null)
        {
            reciveHits = new List<ReciveHit>();
           
        }
        reciveHits.Add(rh);
        Debug.Log(rh);
    }
}

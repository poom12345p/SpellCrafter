using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedGreatWizardAI :EnemyAI,ReciveSignal
{
    public bool activeAI;
    [SerializeField]
    DetectUnitArea upperArea, lowerArea;
    CorruptedGreatWizardMove cgwMove;
    WaitForFixedUpdate fixdupdate = new WaitForFixedUpdate();
    bool isFollowY;

    private Vector3 stayPos;
    bool onStayPos;

    bool isAction;

    int state = 0;

    bool isFireBarier;

    [SerializeField]
    Transform spikeSpawnPoint;
    [SerializeField]
    ParticleSystem spikeWarningParticle;

    [SerializeField]
    bool castSmoke1, castSmoke2,ReadyCastSmoke;
    [SerializeField]
    bool ReadyCastSpike;

    int loopCount=0;
    [Header("skill modify")]
    [SerializeField]
    float spawnTime;
    [SerializeField]
    float spawnCounterTime;
    [SerializeField]
    float actionLoopTime;
    [SerializeField]
    float actionCounterTime;

    [Header("static stay porion")]
    [SerializeField]
    Transform[] stayPositions;

  
    public void RecivceSignal(string tag)
    {
        
    }

    public void RecivceSignal(float num) 
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        cgwMove = (CorruptedGreatWizardMove)baseMove;
        ChangePosition(stayPositions[0].position);
      
    }
    IEnumerator followPlayerYAndFireMagic(float t,int loop)
    {
        var ft = 0.0f;
        while (loop > 0)
        {
            loop--;
            ft = t;
            while (ft > 0)
            {
                var tpos = new Vector2(transform.position.x, visiblePlayer.position.y<2.75f?2.75f : visiblePlayer.position.y);
                cgwMove.moveTo(tpos);
                ft -= Time.fixedDeltaTime;
                yield return fixdupdate;
                Debug.Log("FollowPlayerY");
            }
           
            isFollowY = false;
            yield return new WaitForSeconds(1.0f);
            cgwMove.FireMagicBall();
            yield return new WaitForSeconds(1.0f);

        }
        EndAction();
    }

    IEnumerator CastingSpike(int amount)
    {
        cgwMove.StartCast();
        while (amount > 0)
        {
            amount--;
            var spos = new Vector2(visiblePlayer.transform.position.x, spikeSpawnPoint.position.y);
            spikeSpawnPoint.position = spos;
            spikeWarningParticle.Play();
            yield return new WaitForSeconds(0.75f);

            spikeWarningParticle.Stop();
            cgwMove.StartCastSpike(spikeSpawnPoint.position);

        }
        cgwMove.EndCast();
        EndAction();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeAI) return;
        onPlayOffset.Set(offset.x * baseMove.faceDirection, offset.y, offset.z);
        onGCPlayOffset.Set(GCoffset.x * baseMove.faceDirection, GCoffset.y, GCoffset.z);
        nextIsGround = Physics2D.Linecast(transform.position + onGCPlayOffset, transform.position + onGCPlayOffset + Vector3.down, groundMask);
        FindPlayer();

    

        //move

        if (!onStayPos)
        {
            if (Vector3.Distance(stayPos, transform.position) > 0.1)
            {
               
                cgwMove.moveTo(stayPos);
                if(stayPos.x < transform.position.x)
                {
                    baseMove.FaceTo(true);
                }
                else
                {
                    baseMove.FaceTo(false);
                }
                Debug.Log("Move");
            }
            else
            {
                onStayPos = true;
                cgwMove.moveTo(transform.position);
                EndAction();
            }
        }
        else
        {
            if (visiblePlayer)
            {
                if (visiblePlayer.transform.position.x < transform.position.x)
                {
                    baseMove.FaceTo(true);
                }
                else
                {
                    baseMove.FaceTo(false);
                }
            }
        }

        //Counter
        if (spawnCounterTime < spawnTime)
        {
            spawnCounterTime += Time.deltaTime;
        }
        //else
        //{
        //    var s = Random.Range(0, 2);
        //    switch (s)
        //    {
        //        case 0:
        //            cgwMove.CreateWoodenSheild();
        //            break;
        //        case 1:
        //            cgwMove.CreateFireBalls();
        //            break;
        //    }
        //    spawnCounterTime = 0;
        //}
        //
        //if (actionCounterTime < actionLoopTime)
        //{
        //    actionCounterTime += Time.deltaTime;
        //}
        //else
        //{
        //    if (!isFollowY && onStayPos)
        //    {
        //        var s = Random.Range(0, 2);
        //        switch (s)
        //        {
        //            case 0:

        //                break;
        //            case 1:

        //                break;
        //        }
        //        spawnCounterTime = 0;
        //    }
        //}
    }


    public void ActiveBoss()
    {
        activeAI = true;
    }

    public void ChangePosition(Vector3 pos)
    {
       stayPos = pos;
        StartAction();
       onStayPos = false;
    }

    public void StarFollowPlayerYAndFireMagic()
    {
        StartAction();
        //isFollowY = true;
        StartCoroutine(followPlayerYAndFireMagic(1.5f, 3));
    }

    public void StartAction()
    {

        isAction = true;
    }


    public void EndAction()
    {
        isAction = false;
        cgwMove.EndCast();
        if (ReadyCastSmoke)
        {
            if (stayPos != stayPositions[4].position)
            {
                ChangePosition(stayPositions[4].position);
            }
            else
            {
                cgwMove.StartCastSmoke();
                ReadyCastSmoke = false;
                Invoke("EndAction", 3.0f);
            }
        }
       else if (ReadyCastSpike)
        {
            if (stayPos != stayPositions[4].position)
            {
                ChangePosition(stayPositions[4].position);
            }
            else
            {
              
                ReadyCastSpike = false;
                StartCoroutine("CastingSpike", 4);
            }
        }
        else if (spawnCounterTime >= spawnTime)
        {

            var s = Random.Range(0, 2);
            switch (s)
            {
                case 0:
                    cgwMove.CreateWoodenSheild();
                    break;
                case 1:
                    cgwMove.CreateFireBalls();
                    break;

            }
            spawnCounterTime = 0;
            Invoke("EndAction", 5.0f);
        }
        else
        {
           // var s = Random.Range(0, 2);

            switch (state)
            {
                case 0:
                    ChangePosition(stayPositions[Random.Range(0, 4)].position);
                    break;
                case 1:
                    loopCount++;
                    if (loopCount % 3 == 0)
                    {
                        ReadyCastSpike = true;
                        ChangePosition(stayPositions[4].position);
                    }
                    else
                    {
                        StarFollowPlayerYAndFireMagic();
                    }
                    break;
            
            }
            state=(state+1)%2;
        }

    }

    public void GetHp(int maxHp,int Hp)
    {
        if(!castSmoke1)
        {
            if(((float)Hp/(float)maxHp)<0.4f)
            {
                castSmoke1 = true;
                ReadyCastSmoke = true;
            }
        }

        if (!castSmoke2)
        {
            if (((float)Hp / (float)maxHp) < 0.15f)
            {
                castSmoke2 = true;
                ReadyCastSmoke = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour,ReciveHit
{
    [Header("Viewpoint")]
    [SerializeField]
    protected float width;
    [SerializeField]
    protected float hight;
    [SerializeField]
    protected Vector3 offset;
    protected Vector3 onPlayOffset;
    [Header("behavior")]
    [SerializeField]
    protected float stopRange;
    [Header("GroundCheck")]
    [SerializeField]
    protected Vector3 GCoffset;
    [SerializeField]
    protected LayerMask groundMask;
    public bool nextIsGround;
    protected Vector3 onGCPlayOffset;


    public EnemyMove enemyMove;
    [SerializeField]
    protected Transform visiblePlayer;

    protected Vector2 viewBoxSize;

    protected BaseMove baseMove;
    protected EnemyUnit unit;

    
    [SerializeField]
    float leftPathOffset;
    [SerializeField]
    float rightPathOffset;
    bool loop;


    Vector3 leftEndPoint=Vector3.zero;
    Vector3 rightEndPoint=Vector3.zero;

    protected enum BaseState
    {
        ROAMING,TRACKING,ATTACKING
    }

    protected BaseState baseState; 
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        if (baseMove)
        {
            onPlayOffset.Set(offset.x * baseMove.faceDirection, offset.y, offset.z);
            onGCPlayOffset.Set(GCoffset.x * baseMove.faceDirection, GCoffset.y, GCoffset.z);
            Gizmos.DrawWireCube(transform.position + onPlayOffset, new Vector2(width, hight));
            Gizmos.DrawLine(transform.position + onGCPlayOffset, transform.position + onGCPlayOffset + Vector3.down);
        }
        else
        {

            Gizmos.DrawWireCube(transform.position + offset, new Vector2(width, hight));
            Gizmos.DrawLine(transform.position + GCoffset, transform.position + GCoffset + Vector3.down);
        }
        Gizmos.color = Color.green;

        if (leftEndPoint == Vector3.zero && rightEndPoint == Vector3.zero)
        {
            Gizmos.DrawLine(new Vector3(transform.position.x - leftPathOffset, transform.position.y, 0), new Vector3(transform.position.x + rightPathOffset, transform.position.y, 0));
        }
        else
        {
            Gizmos.DrawLine(leftEndPoint, rightEndPoint);
        }
    }
    // Start is called before the first frame update


    void Start()
    {
        OnStart();

    }

    // Update is called once per frame
    void Update()
    {

        OnUpdate();
        //enemyMove.moveHorizontal(1);
        // enemyMove.updateAnimatorValue(1);
    }


    public virtual void OnStart()
    {
        viewBoxSize.Set(width, hight);
        baseMove = gameObject.GetComponent<BaseMove>();
        unit = gameObject.GetComponent<EnemyUnit>();
        var hitdetect = gameObject.GetComponent<BaseBody>();
        hitdetect.AddReciveHitObserver(this);
        if (leftPathOffset != 0)
        {
            leftEndPoint.Set(transform.position.x - leftPathOffset, transform.position.y, 0);
        }
        if (rightPathOffset != 0)
        {
            rightEndPoint.Set(transform.position.x + rightPathOffset, transform.position.y, 0);
        }
    }

    public virtual void OnUpdate()
    {
        onPlayOffset.Set(offset.x * baseMove.faceDirection, offset.y, offset.z);
        onGCPlayOffset.Set(GCoffset.x * baseMove.faceDirection, GCoffset.y, GCoffset.z);
        nextIsGround = Physics2D.Linecast(transform.position + onGCPlayOffset, transform.position + onGCPlayOffset + Vector3.down, groundMask);
        FindPlayer();
        if (visiblePlayer)
        {
            TrackingPlayer();
        }
        else
        {
            Roming();
        }
    }
    protected void FindPlayer()
    {
        if (visiblePlayer || unit.isDaed) return;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position + onPlayOffset, viewBoxSize, 0);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != null && hitCollider.CompareTag("Player") && gameObject != hitCollider.gameObject)
            {
                visiblePlayer = hitCollider.transform;

            }

        }
    }

    //follow and Attack player in range
    protected virtual void TrackingPlayer()
    {
        if (!visiblePlayer||unit.isDaed) return;

        if (visiblePlayer.position.x > transform.position.x)
        {
            enemyMove.FaceTo(false);
        }
        else if (visiblePlayer.position.x < transform.position.x)
        {
            enemyMove.FaceTo(true);
        }

        if (nextIsGround)
        {
            if (Mathf.Abs(visiblePlayer.position.x - transform.position.x) > stopRange)
            {
                if (visiblePlayer.position.x > transform.position.x)
                {

                        enemyMove.moveHorizontal(1);
                        enemyMove.updateAnimatorValue(1);

                }
                else if (visiblePlayer.position.x < transform.position.x)
                {

                        enemyMove.moveHorizontal(-1);
                        enemyMove.updateAnimatorValue(1);
                }
            }
            //PreformAttack
            else
            {
                enemyMove.moveHorizontal(0);
                enemyMove.updateAnimatorValue(0);
                enemyMove.PrefromAttack();

            }
        }

    }

    public void Roming()
    {
        if (leftEndPoint != Vector3.zero || rightEndPoint != Vector3.zero)
        {

            //enemyMove.moveHorizontal(baseMove.moveDirection);
            if(!nextIsGround)
            {
                baseMove.faceDirection = -baseMove.faceDirection;
                
            }
            else if (transform.position.x < leftEndPoint.x )
            {
                baseMove.faceDirection = 1;
            }
            else if (transform.position.x > rightEndPoint.x )
            {
                baseMove.faceDirection = -1;
            }
            enemyMove.moveHorizontal(baseMove.faceDirection);
            enemyMove.updateAnimatorValue(1);
        }
    }
    //public void ReciveHitObject(GameObject hitObj)
    //{
    //    var dmgObj = hitObj.GetComponent<DamageObject>();
    //    if (dmgObj&& dmgObj.owner)
    //    {
    //        if (dmgObj.owner.CompareTag("Player"))
    //        {
    //            visiblePlayer = dmgObj.owner.transform;
    //        }
    //    }
    //}
    public void PrefromReciveHit(HitArea hitArea)
    {
        var dmgObj = (DamageObject)hitArea;
        if (dmgObj && dmgObj.owner)
        {
            if (dmgObj.owner.CompareTag("Player"))
            {
                visiblePlayer = dmgObj.owner.transform;
            }
        }
    }



}

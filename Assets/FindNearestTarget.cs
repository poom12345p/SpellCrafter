using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindNearestTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public string targetTag;
    public float speedFind, maxRange, initialSize;
    public int maxRepeat;
    public List<GameObject> beHitList;
    //public bool destroyAfterAllJump;
    public GameObject currentHitOjb;

    float size;
    bool isTrigger;
    int currentRepeat;
    void OnEnable()
    {
        beHitList.Clear();
        currentHitOjb = null;
        isTrigger = false;
        size = 0f;
        transform.localScale = new Vector3(initialSize, initialSize, initialSize);
        currentRepeat = maxRepeat;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.localScale.x);
        //if ((transform.localScale.x >= maxRange || (currentRepeat + 1) == 0) && destroyAfterAllJump)
        //{
        //    gameObject.SetActive(false);
        //}
        //else if (!isTrigger)
        //{
        //    transform.localScale += new Vector3(size * speedFind, size * speedFind, size * speedFind);
        //    size += 0.0001f;
        //}
        //else if ((currentRepeat + 1) > 0)
        //{
        //    size = 0f;
        //    transform.position = beHitList.Last().transform.position;
        //    transform.localScale = new Vector3(0, 0, 0);
        //    isTrigger = false;
        //}
        onUpdate();
    }

    //public void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == targetTag && !beHitList.Contains(col.gameObject))
    //    {
    //        currentRepeat--;
    //        isTrigger = true;
    //        beHitList.Add(col.gameObject);
    //        currentHitOjb = col.gameObject;
    //    }
    //}

    void onUpdate()
    {
        if (currentHitOjb == null)
        {
            float minDist = 0.0f;
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, maxRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider != null && hitCollider.CompareTag(targetTag) && gameObject != hitCollider.gameObject)
                {
                    if (currentHitOjb == null)
                    {
                        currentHitOjb = hitCollider.gameObject;
                        minDist = Vector3.Distance(transform.position, currentHitOjb.transform.position);
                    }
                    else if (Vector3.Distance(transform.position, hitCollider.transform.position) < minDist)
                    {
                        currentHitOjb = hitCollider.gameObject;
                        minDist = Vector3.Distance(transform.position, currentHitOjb.transform.position);
                    }
                        
                }

            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}

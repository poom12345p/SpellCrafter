using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventTrigererBox : MonoBehaviour
{
    [SerializeField]
    protected List<string> hitableTag;

    public bool isObjectOn;
    public bool isObjectActive;
    //[SerializeField]
    //protected float width;
    //[SerializeField]
    //protected float hight;
    //[SerializeField]
    //protected Vector3 hitAreaOffset;

    protected Vector2 hitBoxSize;

    public UnityEvent activeObject;
    public UnityEvent deActiceObject;

    [SerializeField]
    private List<GameObject> trigerObjs;

    private List<GameObject> interratToObjs;
    //void OnDrawGizmos()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireCube(transform.position + hitAreaOffset, new Vector2(hight, width));

    //    Gizmos.color = Color.green;
    //    foreach(var obj in trigerObjs)
    //    {
    //        Gizmos.DrawLine(transform.position, obj.transform.position);
    //    }

    //}
    // Start is called before the first frame update
    void Start()
    {
       //hitBoxSize = new Vector2(hight, width);

        interratToObjs = new List<GameObject>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hitableTag.Contains(collision.tag) )
        {
            interratToObjs.Add(collision.gameObject);

            if (!isObjectActive)
            {
                isObjectActive = true;

                activeObject.Invoke();
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interratToObjs.Contains(collision.gameObject))
        {
            interratToObjs.Remove(collision.gameObject);

            if (interratToObjs.Count <= 0)
            {
                isObjectActive = false;
                deActiceObject.Invoke();
            }
        }

       
    }
    void Update()
    {
        //isObjectOn = false;
        //Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position + hitAreaOffset, hitBoxSize, 0);
        //foreach (var hitCollider in hitColliders)
        //{
        //    if (hitCollider != null && hitableTag.Contains(hitCollider.tag) && gameObject != hitCollider.gameObject)
        //    {
        //        isObjectOn = true;

        //    }

        //}

        //if(isObjectOn&&!isObjectActive)
        //{
        //    isObjectActive = true;

        //    activeObject.Invoke();
        //}
        //else if(!isObjectOn&&isObjectActive)
        //{
        //    isObjectActive = false;
        //    deActiceObject.Invoke();
        //}
    }
   
}

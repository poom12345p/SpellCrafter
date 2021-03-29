using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ElementDetector : MonoBehaviour
{ 
    [SerializeField]
    List<Element> triggerElements;

    [SerializeField]
    UnityEvent triggerEvents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name+" is hit cld ");
        var dmgObj = collision.GetComponent<DamageObject>();
        if (dmgObj)
        {
            if(triggerElements != null && triggerElements.Contains(dmgObj.GetElement()))
            {
                triggerEvents.Invoke();
            }
        
        }
    }

}

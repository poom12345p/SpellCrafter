using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ReciveOnlyElementObj : BaseBody
{
    [SerializeField]
    protected List<Element> triggerElement;
    public UnityEvent AftertriggeredEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void ReciveHitAction(GameObject hitObj)
    {
        var dmgObj = hitObj.GetComponent<DamageObject>();
        if(dmgObj)
        {
            if(triggerElement.Contains(dmgObj.GetElement()))
            {

                AftertriggeredEvent.Invoke();
               // afterDeadEvent.Invoke();
            }
        }

        if (reciveHits != null)
        {
            foreach (var rh in reciveHits)
            {
                rh.PrefromReciveHit(hitObj.GetComponent<HitArea>());
            }
        }

        afterBeHitEvent.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaDrop : MonoBehaviour,PooledObject
{
    //Transform des;
    public float speed;
    public float stopDisEstimate;
    public GameObject player;
    ManaSystem manaSys;
    int manaAmount;
    // Start is called before the first frame update
    void Start()
    {
        if(manaSys == null)
        {
            manaSys = player.GetComponent<ManaSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var des = player.transform.position;
        if(Mathf.Abs(transform.position.x- des.x) > stopDisEstimate || Mathf.Abs(transform.position.y - des.y) > stopDisEstimate)
        {
            transform.position = Vector2.MoveTowards(transform.position, des, speed*Time.deltaTime);
        }
        else
        {
            GiveMana();

        }
    }

    public void OnSpawn()
    {

    }

    public void SetPlayer(GameObject p)
    {
        player = p;
        if (manaSys == null)
        {
            manaSys = player.GetComponent<ManaSystem>();
        }
    }

    public void SetSpawnValue(int amount)
    {
        // transform.position = pos;
        //speed += Random.Range(-2f, 2f);
        transform.position += new Vector3(Random.Range(-0.75f,0.75f), Random.Range(-0.75f, 0.75f), 0);
         manaAmount = amount;
    }
    public void GiveMana()
    {
        manaSys.gainMana(manaAmount);
        gameObject.SetActive(false);
    }

}

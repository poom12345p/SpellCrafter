using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public int BASE_MAX_MP, mpRegen, MP, upgrade;

    static int mpPerUpgrade = 10;
    public ProcessBar mpBar;
    public ObjectPooler mamaDropPool;
    void Start()
    {
        MAX_MP = BASE_MAX_MP;
        MP = MAX_MP;
        InvokeRepeating("MPRegen", 0f, 1f);
    }

    int MAX_MP;


    // Update is called once per frame
    void Update()
    {
        //need fix : call only when MP change 
       // MP = Mathf.Clamp(MP, 0, MAX_MP);
       // if(mpBar)mpBar.updateGauge(BASE_MAX_MP, MP);
        //Debug.Log("MP: " + MP + " / " + MAX_MP);
    }

    public void SpendMana(int mp)
    {
        MP -= mp;
        MP = Mathf.Clamp(MP, 0, MAX_MP);
        if (mpBar) mpBar.updateGauge(BASE_MAX_MP, MP);
    }

    public void gainMana(int mp)
    {
        MP += mp;
        MP = Mathf.Clamp(MP, 0, MAX_MP);
        if (mpBar) mpBar.updateGaugeImediate(BASE_MAX_MP, MP);
    }

    public void CreateManaDrop(int mpAmount,Vector3 pos)
    {
        while(mpAmount > 0)
        {
            var mpRe = mpAmount - 7 > 0 ? 7 : mpAmount;
            mpAmount -= 7;

            var mpDropObj = mamaDropPool.SpawnFromPool("ManaDrop", pos, Quaternion.identity);
            var mpDrop = mpDropObj.GetComponent<ManaDrop>();
            mpDrop.SetPlayer(gameObject);
            mpDrop.SetSpawnValue(mpRe);
           

        }
    }

    public void MPRegen()
    {
        MP += mpRegen;
        MP = Mathf.Clamp(MP, 0, MAX_MP);
        if (mpBar) mpBar.updateGauge(BASE_MAX_MP, MP);
    }
    public void SetMpBar(ProcessBar _mpBar)
    {
        mpBar = _mpBar;
        mpBar.updateGauge(BASE_MAX_MP, MP);
    }
    public void updateMp(Inventory inv)
    {
        MAX_MP = BASE_MAX_MP +( mpPerUpgrade * inv.manaUp);
        MP = MAX_MP;
        mpBar.updateGaugeImediate(MAX_MP,MP);
        Debug.Log(" maximun  Mana increse to " + MAX_MP);
    }

    public void FullMPRegen()
    {
        gainMana(MAX_MP);
    }
}

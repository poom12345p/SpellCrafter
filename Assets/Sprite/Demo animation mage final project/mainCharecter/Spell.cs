using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    // Start is called before the first frame update
    MainCharControl2 mcc;
    LittleCasterMove mcm;
    Spellcraft_2 sc2;
    Rigidbody2D rig;
    
    [System.Serializable]
    public struct Skill
    {
        public string tag;
        public float interval, knockback, CastTime, midAirFreezeTime;
        public float selfKnockForce;
        public int mp;
        public ParticleSystem ps;
        public GameObject spawnPoint;
        public AudioClip lauchSoundClip;

        // public Animator spawnPoint;
    }
    public List<Skill> skillPool;
    public Dictionary<string, Skill> skillDictionary;
    public GameObject character;

    [EnumNamedArray(typeof(Element))]
    public GameObject[] channellingSkill;
    [EnumNamedArray(typeof(Element))]
    public ParticleSystem[] psChannel;

    void Awake()
    {
        skillDictionary = new Dictionary<string, Skill>();
        foreach (var pool in skillPool) skillDictionary.Add(pool.tag, pool);
    }
    void Start()
    {
        mcc = character.GetComponent<MainCharControl2>();
        mcm = character.GetComponent<LittleCasterMove>();
        sc2 = character.GetComponent<Spellcraft_2>();
        rig = character.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //USE ONLY THIS FUNCTION
    //NO PS YET
    public void SingleCastSpell(Element ele) 
    {
        if (ele == Element.EARTH)
        {
            mcm.Cast(skillDictionary["Rock"], 1f, 1f, 0f);
            GetIntervalSkill("Rock");
            //GetKnockbackTime("Rock");
        }
        else if (ele == Element.FIRE)
        {
            mcm.Cast(skillDictionary["Pyroblast"], 3f, 3f, 0f);
            GetIntervalSkill("Pyroblast");
            //GetKnockbackTime("Pyroblast");
        }
        else if (ele == Element.ELECTRIC)
        {
            mcm.Cast(skillDictionary["Thunderbolt"], 1f, 1f, 0f);
            GetIntervalSkill("Thunderbolt");
            //GetKnockbackTime("Thunderbolt");
        }
        else if (ele == Element.LAVA)
        {
            mcm.Cast(skillDictionary["LavaLeak"], 1f, 1f, 0f);
            GetIntervalSkill("LavaLeak");
            //GetKnockbackTime("LavaLeak");
        }
        else if (ele == Element.SAND)
        {
            mcm.Cast(skillDictionary["Sandpool"], 1f, 1f, 0f);
            GetIntervalSkill("Sandpool");
            //GetKnockbackTime("Sandpool");
        }
    }

    public void HoldCastSpell(Element ele)
    {
        if (ele == Element.EARTH)
        {
            mcm.Cast(skillDictionary["Rock"], 1f, 1f, 0f);
            GetIntervalAttack("Rock");
            //GetKnockbackTime("Rock");
        }
        else if (ele == Element.FIRE)
        {
            mcm.Cast(skillDictionary["Pyroblast"], 5f, 5f, 0f);
            GetIntervalSkill("Pyroblast");
            //GetKnockbackTime("Pyroblast");
        }
        else if (ele == Element.WIND)
        {
            mcm.Cast(skillDictionary["WindUp"], 2f, 2f, 0f);
            GetIntervalSkill("WindUp");
            //ClearElement();
        }
        else if (ele == Element.WATER)
        {
            mcm.Cast(skillDictionary["Tsunami"], 2f, 2f, 0f);
            GetIntervalSkill("Tsunami");
            //ClearElement();
        }
    }

    public void NormalAttack(Element ele, int type)
    {
        if (type == 0)
        {
            if (ele == Element.NONE)
            {
                mcm.Cast(skillDictionary["Normal Attack"], 1f, 1f, 0f);
                GetIntervalAttack("Normal Attack");
            }
            else if (ele == Element.EARTH)
            {
                for (int i = 0; i < 3; i++) mcm.Cast(skillDictionary["EarthAttack"], 0.75f, 0.75f, (i - 1) * 20);
                GetIntervalAttack("EarthAttack");
            }
            else if (ele == Element.WATER)
            {
                mcm.Cast(skillDictionary["WaterAttack"], 1f, 1f, 0f);
                GetIntervalAttack("WaterAttack");
            }
            else if (ele == Element.WIND)
            {
                mcm.Cast(skillDictionary["WindAttack"], 1f, 1f, 0f);
                GetIntervalAttack("WindAttack");
            }
        }

        else if (type == 1)
        {
            if (ele == Element.NONE)
            {
                mcm.Cast(skillDictionary["Heavy Attack"], 1f, 1f, 0f);
                GetIntervalAttack("Heavy Attack");
            }
            else if (ele == Element.EARTH)
            {
                for (int i = 0; i < 4; i++) mcm.Cast(skillDictionary["EarthAttack"], 0.75f, 0.75f, (i - 1.5f) * 15);
                GetIntervalAttack("EarthAttack");
            }
            else if (ele == Element.WATER)
            {
                mcm.Cast(skillDictionary["WaterAttack"], 2f, 2f, 0f);
                GetIntervalAttack("WaterAttack");
            }
            else if (ele == Element.WIND)
            {
                for (int i = 0; i < 2; i++) mcm.Cast(skillDictionary["WindAttack"], 1f, 1f, (i - 0.5f) * 15);
                GetIntervalAttack("WindAttack");
            }
        }

        else if (type == 2)
        {
            if (ele == Element.NONE)
            {
                mcm.Cast(skillDictionary["Heavy Attack"], 1.5f, 1.5f, 0f);
                GetIntervalAttack("Heavy Attack");
            }
            else if (ele == Element.EARTH)
            {
                for (int i = 0; i < 5; i++) mcm.Cast(skillDictionary["EarthAttack"], 0.75f, 0.75f, (i - 2) * 10);
                GetIntervalAttack("EarthAttack");
            }
            else if (ele == Element.WATER)
            {
                mcm.Cast(skillDictionary["WaterAttack"], 3f, 3f, 0f);
                GetIntervalAttack("WaterAttack");
            }
            else if (ele == Element.WIND)
            {
                for (int i = 0; i < 3; i++) mcm.Cast(skillDictionary["WindAttack"], 1f, 1f, (i - 1) * 15f);
                GetIntervalAttack("WindAttack");
            }
        }
    }

    public void SpecialAttack(string spell, GameObject sp)
    {
        //mcm.Cast(skillDictionary[spell], 1f, 1f, 0f);
        mcm.SpecialCast(skillDictionary[spell], sp);
        GetIntervalSkill(spell);
    }

    public void ChannellingAttack(Element ele)
    {
        if (mcm.isChannelling)
        {
            for (int i = 0; i < channellingSkill.Length; i++)
            {
                if (i == (int)ele && channellingSkill[i] != null)
                {
                    channellingSkill[i].SetActive(true);
                    if (psChannel[i] != null)
                    {
                        if (!psChannel[i].isPlaying)
                        {
                            psChannel[i].Play();
                        }
                    }
                    //if (mcm.spriteRenderer.flipX) psChannel[i].gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                    //else psChannel[i].gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                    psChannel[i].gameObject.transform.rotation = new Quaternion(0, mcm.faceDirection < 0 ? 0.0f : 180.0f, 0, 0);
                }               
            }
        }
        else
        {
            foreach (GameObject j in channellingSkill) if (j != null) j.SetActive(false);
            foreach (ParticleSystem k in psChannel) if (k != null) k.Stop();
        }
    }

    public void GetIntervalSkill(string tag)
    {
        mcm.clickCastInterval = skillDictionary[tag].interval;
    }

    public void GetIntervalAttack(string tag)
    {
        mcm.clickAttackInterval = skillDictionary[tag].interval;
    }

    /*public void GetKnockbackTime(string tag)
    {
        mcm.knockbackTime = skillDictionary[tag].knockback;
    }*/

    public void ClearElement()
    {
        sc2.currentElement = Element.NONE;
    }
}

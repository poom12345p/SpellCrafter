using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdStatus : AbnormalStatus
{
    [SerializeField]
    SpriteRenderer bodySprite;
    EnemyMove enemyMove;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        enemyMove= unit.GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }
    public override void HideStatusEffect()
    {
        base.HideStatusEffect();
        if (enemyMove)
        {
            enemyMove.ReverseColor();
        }
    }

    public override void ShowStatusEffect()
    {
        base.ShowStatusEffect();
        if (enemyMove)
        {
            enemyMove.ReverseColor();
        }
       // bodySprite.color = new Color(0, 1, 1);
    }
}

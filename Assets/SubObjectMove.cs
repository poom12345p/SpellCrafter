using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubObjectMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float leftDistance, rightDistance, speed;
    public LittleCasterMove bm;
    public GameObject player;
    public Vector3 startPos, midairPos;

    Vector3 initialPos;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) bm = player.GetComponent<LittleCasterMove>();

        //if (!bm.Floating())
        //{
            if (bm.faceDirection == -1)
            {
                var newPos = startPos;
                newPos.x *= -1;
                transform.position = initialPos = player.transform.position + newPos;
            }
            else transform.position = initialPos = player.transform.position + startPos;
        //}
        /*else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            transform.position = player.transform.position + midairPos;
        }*/
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!bm.Floating())
        //{
            var pos = transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

            if (bm.faceDirection == -1) pos.x = Mathf.Clamp(transform.position.x, initialPos.x - rightDistance, initialPos.x + leftDistance);
            else pos.x = Mathf.Clamp(transform.position.x, initialPos.x - leftDistance, initialPos.x + rightDistance);

            transform.position = pos;
        //}
        /*else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            transform.position = player.transform.position + midairPos;
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawnPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            LittleCasterMove litMove = collision.GetComponent<LittleCasterMove>();
            litMove.SetReSpawnPos(transform.position);
            Debug.Log("Set Little Caster at " + gameObject.name + ":" + transform.position);
        }
    }
}

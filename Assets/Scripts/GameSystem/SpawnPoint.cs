using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public int number;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void SetPlayerOnSpawnPoint(GameObject mainCharMove)
    {
        mainCharMove.transform.position = transform.position;
    }
}

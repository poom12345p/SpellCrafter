using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectUnitArea : MonoBehaviour
{
    List<GameObject> objectsOnArea;
    List<ReciveSignal> observers;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsOnArea.Add(collision.gameObject);
        foreach (var ob in observers)
        {
            ob.RecivceSignal("PlayerUP");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsOnArea.Remove(collision.gameObject);
        foreach (var ob in observers)
        {
            ob.RecivceSignal("PlayerDown");
        }
    }

    public void AddObserver(ReciveSignal rd)
    {

            if (observers == null)
            {
                observers = new List<ReciveSignal>();
            }
           
            if(!observers.Contains(rd))
                observers.Add(rd);

    }

    public void RemoveObserver(ReciveSignal rd)
    {


        try
        {
            observers.Add(rd);
        }
        catch
        {
            Debug.LogError("false to remove " + rd);
        }

    }
}


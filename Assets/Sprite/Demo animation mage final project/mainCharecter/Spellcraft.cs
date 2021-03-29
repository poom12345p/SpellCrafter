using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spellcraft : MonoBehaviour
{
    // Start is called before the first frame update
    int MAX_ELEMENT_COUNT = 3;
    public Queue<Element> elementQueue = new Queue<Element>(3); 
    public GameObject[] elementBall;
    public Sprite[] elementSprite;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addElement(Element ele)
    {
        if (elementQueue.Count == MAX_ELEMENT_COUNT) elementQueue.Dequeue();
        if (elementQueue.Peek() != ele) elementQueue.Enqueue(ele);
        //setElementSprite(elementQueue);
    }

    public void setElementSprite(Queue<Element> ele)
    {
        //string A, B, C;
        Queue<Element> temp = new Queue<Element>(ele);

        elementBall[0].GetComponent<SpriteRenderer>().sprite = elementSprite[(int)temp.Peek()];
        //A = readQueue(temp);

        if (MAX_ELEMENT_COUNT >= 2)
        {
            temp.Dequeue();
            elementBall[1].GetComponent<SpriteRenderer>().sprite = elementSprite[(int)temp.Peek()];           
        }
        //B = readQueue(temp);

        if (MAX_ELEMENT_COUNT == 3)
        {
            temp.Dequeue();
            elementBall[2].GetComponent<SpriteRenderer>().sprite = elementSprite[(int)temp.Peek()];         
        }
        //C = readQueue(temp);

        //Debug.Log(A + " " + B + " " + C);
    }

    public string readQueue(Queue<Element> temp)
    {
        return temp.Peek().ToString();
    }
}

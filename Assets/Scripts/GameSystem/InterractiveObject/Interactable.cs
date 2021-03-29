using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    public string pointName;
    public bool isActive;
    public ushort state;
    public UnityEvent interactEvent;
    public SpriteRenderer icon;
    protected LittleCasterMove player;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        state = 0;
        ShowInteractIcon(false);
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowInteractIcon(true);
            collision.GetComponent<LittleCasterMove> ().SetInteract(this);
            player = collision.gameObject.GetComponent<LittleCasterMove>();
            player.SetInteract(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")&& player)
        {
            ShowInteractIcon(false);
            collision.GetComponent<LittleCasterMove>().ClearInteract();
            player.SetInteract(null);
            player = null;
        }
    }

    void ShowInteractIcon(bool val)
    {
        if (icon) icon.enabled = val;
    }

    virtual public void Interacted()
    {
        interactEvent.Invoke();
    }

    public void SetActiveFalse()
    {
        isActive = false;
       // GameManager.instance.SaveGame();
    }
    public void SetActiveTrue()
    {
        isActive = true;
       // GameManager.instance.SaveGame();
    }



    public void DisableInteract()
    {
        SetActiveFalse();
        if(GetComponent<Collider2D>()) GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
    }

  
    public virtual void SetState(ushort s)
    {
        state = s;
        Checkstate();
    }

    protected virtual void Checkstate()
    {

    }
    
}

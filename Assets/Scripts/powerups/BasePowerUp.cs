using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerUp : ColorChangeableObject, ColourRun.Interfaces.IPowerUp
{

    [SerializeField]
    protected float duration;
    protected float timer = 0;

    [SerializeField]
    protected bool activated = false;

    protected PowerUpContainer container;

    public override void Start()
    {
        FindObjectOfType<InputManager>().add(this);
        base.Start();
    }

    public virtual void Activate()
    {

    }

    public override void Trigger()
    {
        DestroyAndChange();
    }

    public void initialize(PowerUpContainer container)
    {
        this.container = container;
    }

    public void DestroyAndChange()
    {
        int index = 0;

        

        for(int i = 0; i < container.PowerUpList.Count; i++)
        {
            if(container.PowerUpList[i].CompareTag(gameObject.tag))
            {
                index = i;
            }

        }

        if (index == container.PowerUpList.Count -1)
            index = 0;
        else
            index++;


        container.PowerUp = Instantiate(container.PowerUpList[index], transform.parent.position, 
            Quaternion.identity, transform.parent);
        container.PowerUp.initialize(container);

        FindObjectOfType<InputManager>().remove(this);

        Destroy(gameObject);
    }

    public void Destroy()
    {
        FindObjectOfType<InputManager>().remove(this);
        Destroy(transform.parent.gameObject);
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            activated = true;
            Activate();
        }
    }
}

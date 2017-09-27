using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorChangeableObject : MonoBehaviour, IInputTrigger {
    protected int lastColor;
    protected int currentColor;

    private bool sameColorAsPlayer = false;

    public bool SameColorAsPlayer
    {
        get { return sameColorAsPlayer; }
        set { sameColorAsPlayer = value; }
    }

    public virtual void Start ()
    {
        if (transform.GetComponentInParent<RotationCube>() != null && transform.GetSiblingIndex() > 0)
        {
            Invoke("ForceSameColorAsOther", .1f);
        }
    }

    public virtual void Trigger() {
        ChangeColor();
    }

    public void ChangeColor()
    {
        if (sameColorAsPlayer)
        {
            lastColor = currentColor;

            SetColor(FindObjectOfType<PlayerScript>().GetCurrentColor());
        }
        else
        {
            int cc;

            if (lastColor > 0)
            {
                cc = lastColor;
                lastColor = 0;
            }
            else
            {
                cc = (currentColor + 1);
            }

            if (cc > (ColorManager.colors().Count - 1))
            {
                cc = 0;
            }

            SetColor(cc);
        }
    }
    
    public virtual void SetColor(int color) {
        currentColor = color;
        GetComponent<SpriteRenderer>().color = GetCurrentColor();
    }

    private void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    public Color GetCurrentColor()
    {
        return ColorManager.colors()[currentColor];
    }

    private void ForceSameColorAsOther()
    {
        if (transform.GetSiblingIndex() != 0)
        {
            ColorChangeableObject otherLaser = transform.parent.GetChild(0).GetComponent<ColorChangeableObject>();
            SetColor(ColorManager.colors().IndexOf(otherLaser.GetCurrentColor()));
        }
    }
}

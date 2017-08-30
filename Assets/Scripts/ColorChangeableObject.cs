using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeableObject : MonoBehaviour {

    public enum Colors
    {
        Blue, 
        Red,
        Yellow,
        Green
    }

    [SerializeField]
    protected Colors colorChanges = Colors.Blue;

    private Renderer colorRenderer;
    
    
    // Use this for initialization
    public virtual void Start ()
    {

        colorRenderer = GetComponent<Renderer>();
        ChangeColor(true);
	}

    public void ChangeColor(bool startUp = false)
    {
        switch(colorChanges)
        {
            case Colors.Blue:
                colorChanges = ApplyColors(Colors.Blue, Colors.Red, startUp);
                colorRenderer.material.color = ApplyColor(Color.blue, Color.red, startUp);
                break;
            case Colors.Red:
                colorChanges = ApplyColors(Colors.Red, Colors.Yellow, startUp);
                colorRenderer.material.color = ApplyColor(Color.red, Color.yellow, startUp);
                break;
            case Colors.Yellow:
                colorChanges = ApplyColors(Colors.Yellow, Colors.Green, startUp);
                colorRenderer.material.color = ApplyColor(Color.yellow, Color.green, startUp);
                break;
            case Colors.Green:
                colorChanges = ApplyColors(Colors.Green, Colors.Blue, startUp);
                colorRenderer.material.color = ApplyColor(Color.green, Color.blue, startUp);
                break;
        }
    }

    private Color ApplyColor(Color startColor, Color changeColor, bool start)
    {
        return start ? startColor : changeColor;
    }

    private Colors ApplyColors(Colors colorStart, Colors colorChange, bool start)
    {
        return start ? colorStart : colorChange;
    }
    
    public Colors GetCurrentColor()
    {
        return colorChanges;
    }
}

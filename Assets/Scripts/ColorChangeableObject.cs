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

    private SpriteRenderer colorRenderer;
    
    
    // Use this for initialization
    public virtual void Start ()
    {
        colorRenderer = GetComponent<SpriteRenderer>();
        ChangeColor(true);
	}

    public void ChangeColor(bool startUp = false)
    {

        Colors startColor = (Colors)((int)colorChanges +1);

        if (startUp)
        {
            startColor = (Colors)Random.Range((int)Colors.Blue, (int)Colors.Green + 1);
        }
        else
        {
            if (colorChanges == Colors.Blue)
                startColor = Colors.Red;
            else if (colorChanges == Colors.Red)
                startColor = Colors.Yellow;
            else if (colorChanges == Colors.Yellow)
                startColor = Colors.Green;
            else
                startColor = Colors.Blue;

        }

        switch (startColor)
        {
            case Colors.Blue:
                colorRenderer.color = Color.blue;
                break;
            case Colors.Red:
                colorRenderer.color = Color.red;
                break;
            case Colors.Yellow:
                colorRenderer.color = Color.yellow;
                break;
            case Colors.Green:
                colorRenderer.color = Color.green;
                break;
        }

        colorChanges = startColor;
    }
    
    public Colors GetCurrentColor()
    {
        return colorChanges;
    }
}

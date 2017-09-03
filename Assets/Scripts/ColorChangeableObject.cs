using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeableObject : MonoBehaviour {

    protected List<Color> colors = new List<Color> {
        Color.red,
        Color.green,
        Color.blue
    };

    protected int currentColor;

    private SpriteRenderer _renderer;
    
    // Use this for initialization
    public virtual void Start ()
    {
        _renderer = GetComponent<SpriteRenderer>();
        SetColor( UnityEngine.Random.Range(0, (colors.Count - 1)) );
	}

    public void ChangeColor(bool startUp = false)
    {
        int cc = (currentColor + 1);

        if (cc > (colors.Count - 1)) {
            cc = 0;
        }

        SetColor( cc );
    }
    
    private void SetColor(int color) {
        currentColor = color;
        _renderer.color = GetCurrentColor();
    }

    public Color GetCurrentColor()
    {
        return colors[currentColor];
    }
}

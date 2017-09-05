using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorChangeableObject : MonoBehaviour {
    protected int currentColor;
    private SpriteRenderer _renderer;

    private LevelManager _levelManager;
    
    private List<Color> _availableColors;

    public virtual void Start ()
    {
        _availableColors = ColorManager.colors();
        _renderer = GetComponent<SpriteRenderer>();
        _levelManager = FindObjectOfType<LevelManager>();

        if (_levelManager.forcedColor != null) {
            SetColor( (int) _levelManager.forcedColor );
        } else {
            SetColor( UnityEngine.Random.Range(0, (ColorManager.colors().Count - 1)) );
        }
        
	}

    public void ChangeColor(bool startUp = false)
    {
        int cc = (currentColor + 1);

        if (cc > (_availableColors.Count - 1)) {
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
        return _availableColors[currentColor];
    }
}

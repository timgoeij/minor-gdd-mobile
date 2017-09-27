using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorOrder : MonoBehaviour {

    [SerializeField]
    private Image singleColor;

    [SerializeField]
    private Image colorWithArrow;

    private List<Image> colorList = new List<Image>();
    
    // Use this for initialization
	void Start () {

        List<Color> listWithColors = ColorManager.colors();
        int index = 0;

        for(int i = 0; i < listWithColors.Count; i++)
        {
            index = i;

            if(index == 0)
            {
                InstantiateColor(Vector2.zero, listWithColors[index], colorWithArrow);
            }
            else if(index == 1)
            {
                Image lastAddColor = colorList[index - 1];
                InstantiateColor(GetNextPosition(lastAddColor, 0, true), listWithColors[index], colorWithArrow);
            }
            else if(index < listWithColors.Count -1)
            {
                Image lastAddColor = colorList[index - 1];

                InstantiateColor(GetNextPosition(lastAddColor, colorWithArrow.rectTransform.rect.width), 
                    listWithColors[index], colorWithArrow);
            }
            else
            {
                Image lastAddColor = colorList[index - 1];

                InstantiateColor(GetNextPosition(lastAddColor, singleColor.rectTransform.rect.width), 
                    listWithColors[index], singleColor);
            }
        }

        Invoke("UpdateColorOrder", .1f);
	}

    private Vector2 GetNextPosition(Image lastAddImage, float nextImageWidth = 0, bool second = false)
    {
        Vector2 lastAddPosition = lastAddImage.rectTransform.localPosition;
        float lastImageWidth = lastAddImage.rectTransform.rect.width;

        return new Vector2(lastAddPosition.x + ((second) ? lastImageWidth : ((lastImageWidth / 2) + (nextImageWidth / 2))), lastAddPosition.y);

    }

    private void InstantiateColor(Vector2 pos, Color color, Image prefab)
    {
        Image newColor = Instantiate(prefab,transform) as Image;

        newColor.rectTransform.localPosition = pos;
        newColor.color = color;

        colorList.Add(newColor);
    }

    public void OnNotifiedColorAdded(LevelManager levelManager)
    {
        int lastIndex = 0;

        for (int i = 0; i < colorList.Count; i++)
        {
            if(i == colorList.Count -1)
            {
                lastIndex = i;
                Image lastImage = colorList[i];
                colorList.Remove(lastImage);
                Destroy(lastImage.gameObject);

                i--;
            }
        }

        InstantiateColor(GetNextPosition(colorList[lastIndex - 1], colorWithArrow.rectTransform.rect.width), ColorManager.colors()[lastIndex], colorWithArrow);
        InstantiateColor(GetNextPosition(colorList[lastIndex], singleColor.rectTransform.rect.width), ColorManager.colors()[lastIndex + 1], singleColor);

        UpdateColorOrder();

        levelManager.notitfyAddingNewColor -= OnNotifiedColorAdded;
    }

    private void UpdateColorOrder()
    {
        int colorIndex = ColorManager.colors().IndexOf(FindObjectOfType<PlayerScript>().GetCurrentColor());

        int index = 0;

        if(colorIndex == ColorManager.colors().Count -1)
        {
            index = 0;
        }
        else
        {
            index += colorIndex + 1;
        }


        foreach(Image colorImage in colorList)
        {
            colorImage.color = ColorManager.colors()[index];

            if(index == ColorManager.colors().Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }
}

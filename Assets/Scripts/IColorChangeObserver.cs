using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColorChangeObserver {

    void add(ColorChangeableObject changeableObject);
    void remove(ColorChangeableObject changeableObject);

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Interfaces
{
    public interface IColorChangeObserver
    {

        void AddChangeableObject(ColorChangeableObject changeableObject);
        void RemoveChangeableObject(ColorChangeableObject changeableObject);

    }
}

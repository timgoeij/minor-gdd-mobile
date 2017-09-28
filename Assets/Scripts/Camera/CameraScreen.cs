using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraScreen {
  public static float width {
    get {
      return height * Screen.width/ Screen.height;
    }
  }
  public static float height {
    get {
      return Camera.main.orthographicSize * 2;
    }
  }

  public static bool ObjectIsBehindCamera(Transform t) {
    return (t.position.x + CameraScreen.width < Camera.main.transform.position.x);
  }
}
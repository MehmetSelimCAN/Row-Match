using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

    private void Start() {
        PrepareCamera();
    }

    private void PrepareCamera() {
		var cam = GetComponent<Camera>();
        float XPosition = (Grid.Cols - 1) / 2f;
        float YPosition = (Grid.Rows - 1) / 2f;
        cam.transform.position = new Vector3(XPosition, YPosition, -10);
	}
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public void setPosition(int x, int y) {
        transform.position = new Vector3(x, y, transform.position.z);
    }

}

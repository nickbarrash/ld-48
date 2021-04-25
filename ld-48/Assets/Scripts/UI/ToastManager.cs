using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastManager : MonoBehaviour
{
    public static ToastManager instance;

    public GameObject messagePrefab;
    int messageCount = 0;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        }

        instance = this;
    }

    public void showMessage(string message) {
        var newMessage = Instantiate(messagePrefab, transform);
        newMessage.name = $"ToastMessage_{++messageCount}";
        newMessage.GetComponent<ToastMessage>().setMessage(message);
    }
}

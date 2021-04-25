using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToastMessage : MonoBehaviour
{
    const float MESSAGE_TIME = 5;

    public TMP_Text messageLabel;

    public void setMessage(string message) {
        messageLabel.text = message;
        Debug.Log($"showing {message}");
        clearMessage();
    }

    public void clearMessage() {
        StartCoroutine(delayedClearMessage());
    }

    private IEnumerator delayedClearMessage() {
        yield return new WaitForSeconds(MESSAGE_TIME);
        Debug.Log("destroyed");
        Destroy(this.gameObject);
    }
}

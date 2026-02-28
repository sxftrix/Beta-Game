using UnityEngine;
using TMPro;
using System.Collections;

public class ErrorText : MonoBehaviour
{
    private TextMeshProUGUI message;
    private string stringCache;

    private void OnEnable()
    {
        message = GetComponent<TextMeshProUGUI>();
        message.text = "";
        SettlementBuilding.OnMessageTrigger += DisplayMessage;
    }

    private void DisplayMessage(string message)
    {
        if (message != null)
        {
            stringCache = message;
            StartCoroutine("ShowMessage");
        }
        
    }

    private IEnumerator ShowMessage()
    {
        message.text = stringCache;
        yield return new WaitForSeconds(1f);
        message.text = "";
        yield return null;
    }
}

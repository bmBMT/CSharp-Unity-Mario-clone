using UnityEngine;
using TMPro;

public class RecordChanger : MonoBehaviour
{
    private void Start() {
        GetComponent<TextMeshProUGUI>().text = "Record:" + PlayerPrefs.GetInt("score").ToString();
    }
}

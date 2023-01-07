using UnityEngine;
using TMPro;

public class TMP_Text_Change : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI worldText;
    [SerializeField] TextMeshProUGUI livesText;
    
    private void Update()
    {
        scoreText.text = GameManager.Instance.GetScore().ToString();
        coinsText.text = "x" + GameManager.Instance.GetCoins().ToString();
        worldText.text = GameManager.Instance.GetWorld();
        livesText.text = GameManager.Instance.GetLives().ToString();
    }
}

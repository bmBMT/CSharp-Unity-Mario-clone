using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    private float timer = 1;
    
    private void Update()
    {
        Time.timeScale = timer;

        if (Input.GetKeyDown(KeyCode.Escape) && pausePanel != null) {
            bool isActive = pausePanel.activeSelf;
            
            if (timer == 1) {
                timer = 0;
            }
            else timer = 1;

            pausePanel.SetActive(!isActive);
        }
    }

    public void ReturnTime() { timer = 1; }

    public void GoToMenu() {
        GameManager.Instance.DestroyManager();
        SceneManager.LoadScene("MainMenu");
    }
}

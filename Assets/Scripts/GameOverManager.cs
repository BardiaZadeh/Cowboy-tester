using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameOverCanvas;
    void Start()
    {
        gameOverCanvas.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowGameOver() {
        gameOverCanvas.SetActive(true);
    }
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

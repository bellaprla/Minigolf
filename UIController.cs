using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public void ToggleMusic() {
        AudioManager.Instance.ToggleMusic();
    }

    public void MenuButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
     public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }

}

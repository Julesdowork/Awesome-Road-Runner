using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject heroMenu;
    public Text starScoreText;
    public Image musicImg;
    public Sprite musicOff, musicOn;

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void HeroMenu()
    {
        heroMenu.SetActive(true);
    }

    public void HomeButton()
    {
        heroMenu.SetActive(false);
    }
}

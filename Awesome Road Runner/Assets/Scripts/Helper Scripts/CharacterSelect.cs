using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] availableHeroes;
    public Text selectedText;
    public GameObject starIcon;
    public Image selectBtnImage;
    public Sprite buttonGreen, buttonBlue;
    public Text starScoreText;

    private int currentIndex;
    private bool[] heroes;

    // Start is called before the first frame update
    void Start()
    {
        InitializeCharacters();
    }

    private void InitializeCharacters()
    {
        currentIndex = 0;

        for (int i = 0; i < availableHeroes.Length; i++)
        {
            availableHeroes[i].SetActive(false);
        }
        availableHeroes[currentIndex].SetActive(true);

        //heroes = GameManager
    }

    public void NextHero()
    {
        availableHeroes[currentIndex].SetActive(false);

        if (currentIndex + 1 == availableHeroes.Length)
            currentIndex = 0;
        else
            currentIndex++;

        availableHeroes[currentIndex].SetActive(true);
    }

    public void PreviousHero()
    {
        availableHeroes[currentIndex].SetActive(false);

        if (currentIndex - 1 == -1)
            currentIndex = availableHeroes.Length - 1;
        else
            currentIndex--;

        availableHeroes[currentIndex].SetActive(true);
    }
}

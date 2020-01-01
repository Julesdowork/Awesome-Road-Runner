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
        currentIndex = GameManager.instance.selectedIndex;

        for (int i = 0; i < availableHeroes.Length; i++)
        {
            availableHeroes[i].SetActive(false);
        }
        availableHeroes[currentIndex].SetActive(true);

        heroes = GameManager.instance.heroes;
    }

    public void NextHero()
    {
        availableHeroes[currentIndex].SetActive(false);

        if (currentIndex + 1 == availableHeroes.Length)
            currentIndex = 0;
        else
            currentIndex++;

        availableHeroes[currentIndex].SetActive(true);

        CheckIfCharacterIsUnlocked();
    }

    public void PreviousHero()
    {
        availableHeroes[currentIndex].SetActive(false);

        if (currentIndex - 1 == -1)
            currentIndex = availableHeroes.Length - 1;
        else
            currentIndex--;

        availableHeroes[currentIndex].SetActive(true);

        CheckIfCharacterIsUnlocked();
    }

    private void CheckIfCharacterIsUnlocked()
    {
        if (heroes[currentIndex])
        {
            // if the hero is unlocked
            starIcon.SetActive(false);

            if (currentIndex == GameManager.instance.selectedIndex)
            {
                selectBtnImage.sprite = buttonGreen;
                selectedText.text = "Selected";
            }
            else
            {
                selectBtnImage.sprite = buttonBlue;
                selectedText.text = "Select?";
            }
        }
        else
        {
            // if the hero is locked
            selectBtnImage.sprite = buttonBlue;
            starIcon.SetActive(true);
            selectedText.text = "1000";
        }
    }

    public void SelectHero()
    {
        if (!heroes[currentIndex])
        {
            if (currentIndex != GameManager.instance.selectedIndex)
            {
                if (GameManager.instance.starScore >= 1000)
                {
                    GameManager.instance.starScore -= 1000;

                    selectBtnImage.sprite = buttonGreen;
                    selectedText.text = "Selected";
                    starIcon.SetActive(false);

                    heroes[currentIndex] = true;

                    starScoreText.text = GameManager.instance.starScore.ToString();

                    GameManager.instance.selectedIndex = currentIndex;
                    GameManager.instance.heroes = heroes;

                    GameManager.instance.SaveGameData();
                }
                else
                {
                    print("Not enough star points to unlock the player");
                }
            }
        }
        else
        {
            selectBtnImage.sprite = buttonGreen;
            selectedText.text = "Selected";
            GameManager.instance.selectedIndex = currentIndex;

            GameManager.instance.SaveGameData();
        }
    }
}

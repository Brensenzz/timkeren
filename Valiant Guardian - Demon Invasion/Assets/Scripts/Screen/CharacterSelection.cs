﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//attach to CharacterSelectionCanvas
//do not rename the panel inside this canvas
public class CharacterSelection : MonoBehaviour {

    public Sprite[] HeroesThumb;    //face thumbnail for listing
    public Sprite[] EnemiesThumb;   //face thumbnail for listing

    public Sprite[] Heroes;         //full body image for preview
    public Sprite[] Enemies;        //full body image for preview

    private GameObject CharacterListPanel;
    private GameObject SelectedHeroPanel;
    private GameObject HeroesPositionPanel;
    private GameObject useFourHeroesPromptGO;

    private int selectedHero;
    private bool isFourHeroesSet = false;

    void Awake()
    {
        //to get character list panel
        CharacterListPanel = gameObject.transform.GetChild(0).FindChild("CharacterListPanel").gameObject;
        //to get selected hero panel
        SelectedHeroPanel = gameObject.transform.GetChild(0).FindChild("SelectedHeroPanel").gameObject;
        //to get heroes position panel
        HeroesPositionPanel = gameObject.transform.GetChild(0).FindChild("HeroesPositionPanel").gameObject;
        //set the initial character list
        //get the use 4 heroes prompt and disable it
        useFourHeroesPromptGO = GameObject.Find("useFourHeroesPromptBox");
        //use four heroes prompt box must be set active in editor!
        useFourHeroesPromptGO.SetActive(false);
        initHeroesList();
        initHeroPreview();
        initHeroesPositioning();
    }

    void initHeroesList()
    {
        for (int i = 0; i < HeroesThumb.Length; i++)
        {   //changing the sprite as array defined
            CharacterListPanel.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = HeroesThumb[i];
        }
    }

    void initHeroesPositioning()
    {
        for (int i = 0; i < 4; i++)
        {
            HeroesPositionPanel.transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
    }

    public void setHeroPosition() {
        for (int i = 0; i < 4; i++)
        {
            if (HeroesPositionPanel.transform.GetChild(i).GetComponent<Image>().enabled == false)
            {
                HeroesPositionPanel.transform.GetChild(i).GetComponent<Image>().sprite = Heroes[selectedHero];
                HeroesPositionPanel.transform.GetChild(i).GetComponent<Image>().enabled = true;
                break;
            }
        }
        //check if the fourth hero has been set, of course it is third index
        if (HeroesPositionPanel.transform.GetChild(3).GetComponent<Image>().enabled == true) {
            isFourHeroesSet = true;
        }
    }

    public void setSelectedHero(int index)
    {
        selectedHero = index;
        setPreviewCharacterToThis(index);
    }

    void initHeroPreview()
    {
        //set the first heroes in array as default preview
        SelectedHeroPanel.transform.GetChild(0).GetComponent<Image>().sprite = Heroes[0];
    }

    public void setPreviewCharacterToThis(int index)
    {
        //index sent by button's parameter
        SelectedHeroPanel.transform.GetChild(0).GetComponent<Image>().sprite = Heroes[index];
    }

    public void fourHeroUsedValidate()
    {
        //if four heroes is selected then go to survival
        if (isFourHeroesSet) {
            ScriptableObject.FindObjectOfType<Navigation>().GoToSurvival();
        }
        //if heroes selected less then 4, cannot continue
        else {
            showUseFourHeroesPrompt();
        }
    }

    public void showUseFourHeroesPrompt(){
        useFourHeroesPromptGO.SetActive(true);
    }

    public void hideUseFourHeroesPrompt(){
        useFourHeroesPromptGO.SetActive(false);
    }
}
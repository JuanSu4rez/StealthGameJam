using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private Button EasyModeBtn;
    [SerializeField]
    private Text EasyModeText;
    [SerializeField]
    private Button ReStartBtn;
    [SerializeField]
    private Button ExitGametBtn;
    [SerializeField]
    private Image panelWin;
    [SerializeField]
    private Text textWin;
    [SerializeField]
    private Image panelControls;
    [SerializeField]
    private Text textControls;
    [SerializeField]
    private Image panelGoal;
    [SerializeField]
    private Text testGoal;
    [SerializeField]
    private Image panelIntro;
    [SerializeField]
    private Text textIntro;

    public static GameUIController Instance = null;
    void Start() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
        ReStartBtn.onClick.AddListener(GameReStartBtnClick);
        ExitGametBtn.onClick.AddListener(ExitBtnClick);
        EasyModeBtn.onClick.AddListener(GameEasyModeBtnClick);
    }
    void Update() {
    
        if(Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
    void GameReStartBtnClick() {
        DamageConstants.Damage = DamageConstants.Normal_Damage;
        DamageConstants.Frecuency = DamageConstants.Normal_Frecuency;
        SceneManager.LoadScene(Scenes.SampleScene.ToString());
    }
    void GameEasyModeBtnClick() {
        DamageConstants.Damage = DamageConstants.Easy_Damage;
        DamageConstants.Frecuency = DamageConstants.Easy_Frecuency;
        SceneManager.LoadScene(Scenes.SampleScene.ToString());
    }
    void ExitBtnClick() {
        Application.Quit();
    }

    public void ShowWinMessge() {
        panelWin.gameObject.SetActive(true);
        textWin.gameObject.SetActive(true);
        HideControls();
    }

    public void ShowControls() {
        panelControls.gameObject.SetActive(true);
        textControls.gameObject.SetActive(true);
    }

    public void HideControls() {
        panelControls.gameObject.SetActive(false);
        textControls.gameObject.SetActive(false);
    }

    public void ShowIntro() {
        panelIntro.gameObject.SetActive(true);
        textIntro.gameObject.SetActive(true);
    }

    public void HideIntro() {
        panelIntro.gameObject.SetActive(false);
        textIntro.gameObject.SetActive(false);
    }

    public void ShowGoal() {
        panelGoal.gameObject.SetActive(true);
        testGoal.gameObject.SetActive(true);
    }

    public void HideGoal() {
        panelGoal.gameObject.SetActive(false);
        testGoal.gameObject.SetActive(false);
    }


    public void HideDefaultButtons() {
        ReStartBtn.gameObject.SetActive(false);
        ExitGametBtn.gameObject.SetActive(false);
    }

    public void ShowDefaultButtons() {
        ReStartBtn.gameObject.SetActive(true);
        ExitGametBtn.gameObject.SetActive(true);
    }

    public void ShowEasyModeButton() {
        EasyModeBtn.gameObject.SetActive(true);
    }
    public void ShowEasyMode() {
        EasyModeText.gameObject.SetActive(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private Button ReStartBtn;
    [SerializeField]
    private Button ExitGametBtn;
    [SerializeField]
    private Image panelWin;
    [SerializeField]
    private Text textWin;
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
    }
    void Update() {
    
        if(Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
    void GameReStartBtnClick() {
        SceneManager.LoadScene(Scenes.SampleScene.ToString());
    }
    void ExitBtnClick() {
        Application.Quit();
    }

    public void ShowWinMessge() {
        panelWin.gameObject.SetActive(true);
        textWin.gameObject.SetActive(true);
    }
}
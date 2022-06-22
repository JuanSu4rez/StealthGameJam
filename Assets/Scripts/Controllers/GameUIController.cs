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
    void Start() {
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
}
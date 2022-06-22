using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour{
    [SerializeField]
    private Button GameStartBtn;
    [SerializeField]
    private Button ExitGametBtn;
    void Start() {
        GameStartBtn.onClick.AddListener(GameStartBtnClick);
        ExitGametBtn.onClick.AddListener(ExitBtnClick);
    }
    void Update(){
        
    }
    void GameStartBtnClick() {
        SceneManager.LoadScene(Scenes.SampleScene.ToString());
    }
    void ExitBtnClick() {
        Application.Quit();
    }
}

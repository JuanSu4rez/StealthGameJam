using UnityEngine;
using System.Collections;

public class GameOverObserver : MonoBehaviour
{

    // Use this for initialization
    void Start() {
     
    }

    // Update is called once per frame
    void Update() {
        if(GameController.Instance != null && GameController.Instance.gameOver) {
           var disabler =  this.gameObject.GetComponent<IDisabler>();
            disabler?.Disable();
            this.enabled = false;
        }
    }
}

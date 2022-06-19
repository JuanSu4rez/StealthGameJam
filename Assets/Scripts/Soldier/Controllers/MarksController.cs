using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject marks;
    private SoldierMachineState _soldierMachineState;
    void Start()
    {
        marks.SetActive(false);
        _soldierMachineState = this.GetComponent<SoldierMachineState>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        marks.transform.position = transform.position;
        if(_soldierMachineState.ValidateState(SoldierStates.attacking)) {
            Attacking();
        } else if(_soldierMachineState.ValidateState(SoldierStates.searching)){
            Searching();
        } 
    }

    void Searching(){
        marks.gameObject.SetActive(true);
        marks.transform.GetChild(0).gameObject.SetActive(false);
        marks.transform.GetChild(1).gameObject.SetActive(false);
        marks.transform.GetChild(2).gameObject.SetActive(true);        
        Invoke("HideAllMarks", 3f);
    }

    void Attacking(){
        marks.gameObject.SetActive(true);
        marks.transform.GetChild(0).gameObject.SetActive(true);
        marks.transform.GetChild(1).gameObject.SetActive(false);
        marks.transform.GetChild(2).gameObject.SetActive(false);
        Invoke("HideAllMarks", 3f);
    }

    void ReturnToPatrol(){
        marks.gameObject.SetActive(true);
        marks.transform.GetChild(0).gameObject.SetActive(false);
        marks.transform.GetChild(1).gameObject.SetActive(true);
        marks.transform.GetChild(2).gameObject.SetActive(false);
        Invoke("HideAllMarks", 3f);
    }

    void HideAllMarks(){
        int childrenCount = marks.transform.childCount;
        for (int i = 0; i < childrenCount; i++){
            marks.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}

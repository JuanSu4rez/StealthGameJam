using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MarksPrefab;
    private GameObject marks;
    private SoldierMachineState _soldierMachineState;
    public bool IsSearching = false;
    private float searchingState = 1;
    void Start() {
        if(MarksPrefab)
            marks = Instantiate(MarksPrefab, MarksPrefab.transform.position, MarksPrefab.transform.rotation);
            marks.SetActive(false);
        _soldierMachineState = this.GetComponent<SoldierMachineState>();
    }

    // Update is called once per frame
    void LateUpdate() {
        if(marks)
            marks.transform.position = transform.position;
        if(_soldierMachineState.ValidateState(SoldierStates.attacking)) {
            Attacking();
        }
        else if( _soldierMachineState.ValidateState(SoldierStates.searching)) {
            if(IsSearching) {
                Searching();
            }
            else if(_soldierMachineState.SearchingState.FinishSearching) {
                ReturnToPatrol();
            }
        }
    }

    void Searching() {
        if(marks) {
            marks.SetActive(true);
            marks.transform.GetChild(0).gameObject.SetActive(false);
            marks.transform.GetChild(1).gameObject.SetActive(false);
            marks.transform.GetChild(2).gameObject.SetActive(true);
        }
        Invoke("HideAllMarks", 3f);
    }

    void Attacking() {
        if(marks) {
            marks.SetActive(true);
            marks.transform.GetChild(0).gameObject.SetActive(true);
            marks.transform.GetChild(1).gameObject.SetActive(false);
            marks.transform.GetChild(2).gameObject.SetActive(false);
        }
        Invoke("HideAllMarks", 3f);
    }

    public void ReturnToPatrol() {
        if(marks) {
            marks.SetActive(true);
            marks.transform.GetChild(0).gameObject.SetActive(false);
            marks.transform.GetChild(1).gameObject.SetActive(true);
            marks.transform.GetChild(2).gameObject.SetActive(false);
        }
        Invoke("HideAllMarks", 3f);
    }

    void HideAllMarks() {
        if(!marks)
            return;
        int childrenCount = marks.transform.childCount;
        for(int i = 0; i < childrenCount; i++) {
            marks.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}

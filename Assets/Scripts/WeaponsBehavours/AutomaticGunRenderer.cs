using UnityEngine;
using System.Collections;

public class AutomaticGunRenderer : MonoBehaviour
{
    private AutomaticGunBehaviour gunBehaviour;
    private LineRenderer LineRenderer;
    private Vector3[] initilapositions = null;
    // Use this for initialization
    void Start() {
        this.gunBehaviour = this.gameObject.GetComponent<AutomaticGunBehaviour>();
        if(!this.gunBehaviour) {
            this.enabled = false;
            return;
        }
        this.LineRenderer = gameObject.AddComponent<LineRenderer>();
        this.LineRenderer.startColor = new Color(255, 165, 0);
        this.LineRenderer.endColor = Color.yellow;
        this.LineRenderer.startWidth = 0.1f;
        this.LineRenderer.endWidth = 0.1f;

        LineRenderer.sortingOrder = 1;
        LineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        LineRenderer.material.color = Color.yellow;
        this.LineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        this.LineRenderer.enabled = gunBehaviour.IsActive && gunBehaviour.gameObject != null;
        if(this.LineRenderer.enabled) {
            var initialpos = this.transform.position;
            initialpos.y = 1.5f;
            var finallpos = gunBehaviour.TargetGameobject.transform.position;
            finallpos.y = 1.5f;
            var direction =  finallpos - initialpos;
            var distance = Vector3.Distance(initialpos, finallpos) ;
            Mathf.Clamp(distance, 0, distance);
            var randomvalue= Random.Range(distance * 0.2f, distance * 0.9f);
          
            this.LineRenderer.SetPosition(0, initialpos + ( direction.normalized * randomvalue ));
            this.LineRenderer.SetPosition(1, initialpos+( direction.normalized * (randomvalue + 0.4f) ));
        }
    }
}

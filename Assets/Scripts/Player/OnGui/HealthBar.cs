using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    private Texture2D textureHealt;
    private Texture2D textureCurrentHealt;
    private HealthBehaviour healthBehaviour;
    void Start() {
        healthBehaviour = this.GetComponent<HealthBehaviour>();
        this.enabled = healthBehaviour != null;
    }
    // Update is called once per frame
    void OnGUI() {
        if(healthBehaviour.IsAlive ) {
            Vector3 screenPos = UnityEngine.Camera.main.WorldToScreenPoint(this.transform.position);
            Vector2 v2 = new Vector2(screenPos.x, Screen.height - screenPos.y);
            UnityEngine.GUI.contentColor = Color.cyan;
            drawHealthGUIBar();
        }
    }

    public void drawHealthGUIBar() {
        if(textureCurrentHealt == null) {
            textureHealt = new Texture2D(1, 1);
            textureHealt.SetPixel(0, 0, Color.red);
            textureHealt.Apply();
            textureCurrentHealt = new Texture2D(1, 1);
            textureCurrentHealt.SetPixel(0, 0, Color.green);
            textureCurrentHealt.Apply();
        }
        var sqwidth = 15;
        var sqheight = 5;
        Vector3 screenPos = UnityEngine.Camera.main.WorldToScreenPoint(this.transform.position);
        screenPos += new Vector3(-( sqwidth / 2 ), 30);
        UnityEngine.GUI.DrawTexture(new Rect(screenPos.x, Screen.height - screenPos.y - 10, sqwidth, 2), textureHealt);
        var rect = new Rect(new Vector2(screenPos.x, Screen.height - screenPos.y - 10), new Vector2(sqwidth * ( healthBehaviour.Percentage ), 2));
        UnityEngine.GUI.DrawTexture(rect, textureCurrentHealt);
    }
}
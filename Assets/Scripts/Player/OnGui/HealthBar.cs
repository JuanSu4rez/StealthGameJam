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
        if(healthBehaviour.IsAlive) {
            if(UnityEngine.Camera.main != null) {
                Vector3 screenPos = UnityEngine.Camera.main.WorldToScreenPoint(this.transform.position);
                Vector2 v2 = new Vector2(screenPos.x, Screen.height - screenPos.y);
                UnityEngine.GUI.contentColor = Color.cyan;
                drawHealthGUIBar();
            }
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
        var sqwidth = Screen.width - ( Screen.width * 0.5f );
        var x = ( Screen.width * 0.25f );
        var sqheight = 10;
        Vector3 screenPos = UnityEngine.Camera.main.WorldToScreenPoint(this.transform.position);
        screenPos += new Vector3(-( sqwidth / sqheight ), 30);
        var paddingWidth = Screen.width * 0.5f;
        UnityEngine.GUI.DrawTexture(new Rect(x, Screen.height - 30, sqwidth, sqheight), textureHealt);
        var rect = new Rect(new Vector2(x, Screen.height - 30), new Vector2(sqwidth * ( healthBehaviour.Percentage ), sqheight));
        UnityEngine.GUI.DrawTexture(rect, textureCurrentHealt);
    }
}
using UnityEngine;
using DG.Tweening;

public class GridLightDarkMode : MonoBehaviour {
    
    private Color lightModeColor = new Color(0.88627f, 0.88627f, 0.88627f);
    private Color darkModeColor = new Color(0.23529f, 0.23529f, 0.23529f);

    void Start() {
        PreferencesScript.ColorChangeEvent += ToMode;
    }

    void OnDestroy() {
        PreferencesScript.ColorChangeEvent -= ToMode;
    }
    
    public void ToLightMode(float time) {
        foreach (Transform t in transform) {
            t.GetComponent<SpriteRenderer>().DOColor(lightModeColor, time);
        }
    }

    public void ToDarkMode(float time) {
        foreach (Transform t in transform) {
            t.GetComponent<SpriteRenderer>().DOColor(darkModeColor, time);
        }
    }

    public void ToMode(PreferencesScript.ColorMode mode, float time) {
        switch (mode) {
            case PreferencesScript.ColorMode.LIGHT:
                ToLightMode(time);
                break;
            case PreferencesScript.ColorMode.DARK:
                ToDarkMode(time);
                break;
        }
    }

    /// <summary>
    /// Returns this objects corresponding color to the colormode
    /// </summary>
    public Color GetCorrespondingColor(PreferencesScript.ColorMode colorMode) {
        switch (colorMode) {
            case PreferencesScript.ColorMode.LIGHT: return lightModeColor;
            case PreferencesScript.ColorMode.DARK: return darkModeColor;
            default: return Color.magenta;
        }
    }
	
}

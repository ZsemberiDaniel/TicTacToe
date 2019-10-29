using UnityEngine;
using System.Collections;

public class DoneGameScript : MonoBehaviour {

    // Height and width of the whole 
    public int height;
    public int width;
    
    public Cell.CellOcc winType;

    private GameObject signParent;

    private SpriteRenderer[] childrenSR;

    void Start () {
        signParent = transform.Find("Signs").gameObject;
        childrenSR = signParent.GetComponentsInChildren<SpriteRenderer>();

        UpdateBordercolor(SignResourceStorage.Instance.GetColorRelatedTo(winType));
        UpdateSignsColor(SignResourceStorage.Instance.xColor, SignResourceStorage.Instance.oColor, 0f);
    }

    void OnEnable() {
        PreferencesScript.ColorChangeEvent += ChangeToColorMode;
        PreferencesScript.ThemeChangeEvent += ChangeToColorTheme;
    }

    void OnDisable() {
        PreferencesScript.ColorChangeEvent -= ChangeToColorMode;
        PreferencesScript.ThemeChangeEvent -= ChangeToColorTheme;
    }

    public void ChangeToColorMode(PreferencesScript.ColorMode colorMode, float time) {
        StartCoroutine(UpdateSignsColor(SignResourceStorage.Instance.xColor, SignResourceStorage.Instance.oColor, time, Random.Range(0f, 0.5f)));
        UpdateBordercolor(SignResourceStorage.Instance.GetColorRelatedTo(winType));
    }

    public void ChangeToColorTheme(PreferencesScript.ColorTheme theme, float time) {
        ChangeToColorMode(PreferencesScript.ColorMode.LIGHT, time);
    }

    /// <summary>
    /// Updates  the signs' color in this domegame after delay
    /// </summary>
    IEnumerator UpdateSignsColor(Color xColor, Color oColor, float time, float delayTime) {
        yield return new WaitForSeconds(delayTime);

        foreach (SpriteRenderer sr in childrenSR) {
            sr.color = sr.name.StartsWith("X") ? xColor : oColor;
        }
    }

    /// <summary>
    /// Updates the signs' color in this domegame
    /// </summary>
    public void UpdateSignsColor(Color xColor, Color oColor, float time) {
        foreach (SpriteRenderer sr in childrenSR) {
            sr.color = sr.name.StartsWith("X") ? xColor : oColor;
        }
    }

    /// <summary>
    /// Updates this gamedone's border's color
    /// </summary>
    /// <param name="borderColor"></param>
    public void UpdateBordercolor(Color borderColor) {
        GameObject child = transform.Find("Border").gameObject;
        LineRenderer lr = child.GetComponent<LineRenderer>();
        Material mat = lr.material;
        mat.SetColor("_EmissionColor", borderColor);
        // transform.FindChild("Border").GetComponent<LineRenderer>().material.SetColor("_EmissionColor", borderColor);
    }
}

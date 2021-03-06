using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadImage : MonoBehaviour {

    bool loading = false;
    private Image image;

    void Awake() {
        image = GetComponent<Image>();
    }
	
	void Update () {
		if (loading) {
            transform.Rotate(0, 0, -2f);
        }
	}

    public void ToggleLoading() {
        if (loading) loading = false;
        else loading = true;
    }

    public void StartLoading() {
        loading = true;
        image.DOFade(0.5f, 0.2f);
        
    }
    public void StopLoading() {
        loading = false;
        image.DOFade(1f, 0.2f);
    }
}

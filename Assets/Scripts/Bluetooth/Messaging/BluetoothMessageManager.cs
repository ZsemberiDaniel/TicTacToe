using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class BluetoothMessageManager : MonoBehaviour {

    private static GameObject messageSpawnerObject;
    private static GameObject messagePrefab;

    private static float messageHeight = -1;

    private static List<RectTransform> messages = new List<RectTransform>();

    void Start() {
        messageSpawnerObject = GameObject.FindWithTag("BluetoothMessageSpawner");
        messagePrefab = Resources.Load<GameObject>("Prefabs/Bluetooth/Messaging/Message");
    }

    /// <summary>
    /// Make new message
    /// </summary>
    private static GameObject InstantiateMessage(bool ownMessage) {
        // Make new message and it goes directly to 0 0
        GameObject go = GameObject.Instantiate(messagePrefab);
        go.transform.SetParent(messageSpawnerObject.transform, false);
        go.SetActive(false);

        // If we don't know the size of the message yet store it
        if (messageHeight == -1) {
            messageHeight = go.GetComponent<RectTransform>().rect.height * (Camera.main.pixelHeight / 1080f);
        }

        // Move every message down
        for (int i = messages.Count - 1; i >= 0; i--) {
            if (messages[i] == null) { // it has been destroyed
                messages.RemoveAt(i);
            } else {
                messages[i].DOMoveY(messages[i].position.y - messageHeight, 0.4f);
            }
        }


        go.SetActive(true);
        go.GetComponent<BluetoothMessage>().Appear(ownMessage);

        // Set color according to colormode
        // go.GetComponent<SpriteRenderer>().color = preferences.currentMode == PreferencesScript.ColorMode.DARK ? darkColor : lightColor;

        // Add it to stack
        messages.Add(go.GetComponent<RectTransform>());

        return go;
    }

    public static void ShowEmojiMessage(Sprite emojiSprite, bool ownMessage = false) {
        GameObject messageObj = InstantiateMessage(ownMessage);

        Image img = messageObj.transform.GetChild(0).GetComponent<Image>();
        img.sprite = emojiSprite;
    }

}

public static class EmojiSprites {
    // These are here because I needed to expose them for achievements
    public const string UnicornEmoji = "unicornEmoji";
    public const string EggplantEmoji = "aubergineEmoji";
    public const string RektEmoji = "rektEmoji";
    public const string NoobEmoji = "noobEmoji";

    public static string[] emojiPaths = new string[] {
        "smilingEmoji",
        "angryEmoji",
        "bananaEmoji",
        "clownEmoji",
        "deathEmoji",
        "fightmeEmoji",
        "fireworkEmoji",
        "fistBumpEmoji",
        "muscleEmoji",
        "ohnoEmoji",
        "okEmoji",
        "scaredEmoji",
        "sleepingEmoji",
        "sunglassesEmoji",
        "thatpEmoji",
        "thinkingEmoji",
        "trophyEmoji",
        UnicornEmoji,
        "upsidedownEmoji",
        "cryingEmoji",
        "hundredEmoji",
        RektEmoji,
        NoobEmoji,
        "kissEmoji",
        "realAngryEmoji",
        "catEmoji",
        "ggEmoji",
        "gayEmoji",
        "heartEmoji",
        "brokenHeartEmoji",
        "niceEmoji",
        "sorryEmoji",
        "notEvenCloseEmoji",
        "thankYouEmoji",
        "myPleasureEmoji",
        "oopsEmoji",
        "hiEmoji",
        "helloEmoji",
        "facePalmEmoji",
        "deadEmoji",
        "nauseatedEmoji",
        "poopEmoji",
        "disappointedEmoji",
        "tearsOfJoyEmoji",
        "zzzEmoji",
        "droolingEmoji",
        EggplantEmoji,
        "toastEmoji",
        "clapEmoji",
        "likeEmoji",
        "dislikeEmoji",
        "praiseEmoji",
        "fingerCrossEmoji"
    };

    public static Dictionary<string, Sprite> emojis;

    public static void LoadEmojiSprites() {
        emojis = new Dictionary<string, Sprite>();

        string path = "Textures/GUI/Emojis/";

        foreach (string s in emojiPaths)
            emojis.Add(s, Resources.Load<Sprite>(path + s));
    }

    public static Sprite GetEmoji(string name) {
        Sprite emoji;
        emojis.TryGetValue(name, out emoji);
        return emoji;
    }
}

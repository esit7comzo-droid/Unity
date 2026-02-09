using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleGUI : MonoBehaviour
{
    public Canvas canvas;

    private GameObject loadingBar;
    private Text infoText;
    private Button exitButton;
    private Button gameButton;

    void Start()
    {
        // Canvas 없으면 생성
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvasObj.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            canvas = canvasObj.GetComponent<Canvas>();
        }

        // 로딩바 생성
        loadingBar = new GameObject("LoadingBar");
        loadingBar.transform.SetParent(canvas.transform);
        Image barImage = loadingBar.AddComponent<Image>();
        barImage.color = Color.green;
        RectTransform rt = loadingBar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(300, 30);
        rt.anchoredPosition = new Vector2(0, 0);

        // 텍스트 생성
        GameObject textObj = new GameObject("InfoText");
        textObj.transform.SetParent(canvas.transform);
        infoText = textObj.AddComponent<Text>();
        infoText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        infoText.alignment = TextAnchor.MiddleCenter;
        infoText.fontSize = 24;
        infoText.color = Color.white;
        RectTransform textRt = textObj.GetComponent<RectTransform>();
        textRt.sizeDelta = new Vector2(400, 50);
        textRt.anchoredPosition = new Vector2(0, 50);

        // 버튼 생성
        exitButton = CreateButton("Exit", new Vector2(-80, -50), () => { Application.Quit(); });
        gameButton = CreateButton("Game", new Vector2(80, -50), () => { infoText.text = "준비중"; });

        // 버튼 숨김
        exitButton.gameObject.SetActive(false);
        gameButton.gameObject.SetActive(false);

        // 로딩 시작
        StartCoroutine(LoadingCoroutine());
    }

    Button CreateButton(string text, Vector2 position, UnityEngine.Events.UnityAction action)
    {
        GameObject btnObj = new GameObject(text + "Button");
        btnObj.transform.SetParent(canvas.transform);
        Button btn = btnObj.AddComponent<Button>();
        Image img = btnObj.AddComponent<Image>();
        img.color = Color.gray;
        RectTransform rt = btnObj.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 40);
        rt.anchoredPosition = position;

        GameObject txtObj = new GameObject("Text");
        txtObj.transform.SetParent(btnObj.transform);
        Text btnText = txtObj.AddComponent<Text>();
        btnText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        btnText.text = text;
        btnText.alignment = TextAnchor.MiddleCenter;
        btnText.color = Color.black;
        RectTransform txtRt = txtObj.GetComponent<RectTransform>();
        txtRt.sizeDelta = rt.sizeDelta;
        txtRt.anchoredPosition = Vector2.zero;

        btn.onClick.AddListener(action);
        return btn;
    }

    IEnumerator LoadingCoroutine()
    {
        float duration = 5f;
        float elapsed = 0f;
        RectTransform rt = loadingBar.GetComponent<RectTransform>();
        Vector2 originalSize = rt.sizeDelta;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            rt.sizeDelta = new Vector2(originalSize.x * progress, originalSize.y);
            yield return null;
        }

        // 로딩 완료
        infoText.text = "by epsomm";
        loadingBar.SetActive(false);
        exitButton.gameObject.SetActive(true);
        gameButton.gameObject.SetActive(true);
    }
}

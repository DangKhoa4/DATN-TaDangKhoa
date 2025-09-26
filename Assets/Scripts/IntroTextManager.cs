using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroTextManager : MonoBehaviour
{
    public TextMeshProUGUI introText;   // Text hiển thị cốt truyện
    [TextArea(2, 5)]
    public string[] lines;              // Các dòng cốt truyện
    public float textSpeed = 0.05f;     // Tốc độ hiện từng chữ
    public string nextSceneName = "GameScene"; // Scene tiếp theo

    private int index;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private string displayedText = "";  // Lưu phần text đã hiện xong

    void Start()
    {
        index = 0;
        introText.text = "";
        StartTyping();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping && typingCoroutine != null)
            {
                // Skip dòng hiện tại
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;

                displayedText += (displayedText == "" ? "" : "\n") + lines[index];
                introText.text = displayedText;

                isTyping = false;
            }
            else
            {
                // Sang dòng tiếp theo
                NextLine();
            }
        }
    }

    void StartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        string currentLine = "";

        foreach (char c in lines[index])
        {
            currentLine += c;
            introText.text = displayedText + (displayedText == "" ? "" : "\n") + currentLine;
            yield return new WaitForSeconds(textSpeed);
        }

        // Kết thúc dòng → lưu lại
        displayedText += (displayedText == "" ? "" : "\n") + lines[index];

        isTyping = false;
        typingCoroutine = null;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartTyping();
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
    public void EndIntro()
    {
        // Chuyển về MenuScene
        SceneManager.LoadScene("MainMenu"); // đổi "MenuScene" thành tên scene menu
    }
}

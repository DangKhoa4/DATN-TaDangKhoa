using UnityEngine;

public class GrandPa : MonoBehaviour
{
    public GameObject winUI;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            audioManager.PlaySFX(audioManager.checkpoint);
            Time.timeScale = 0;
            winUI.SetActive(true);
            ScoreManager.instance.ShowScore();
        }
    }
}

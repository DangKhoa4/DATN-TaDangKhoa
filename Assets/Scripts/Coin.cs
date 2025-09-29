using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{

    public AudioClip coinClip;
    private TextMeshProUGUI coinText;
    AudioManager audioManager;

    private void Start()
    {
        coinText = GameObject.FindWithTag("CoinText").GetComponent<TextMeshProUGUI>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.coins += 1;
            audioManager.PlaySFX(audioManager.getcoin);
            coinText.text = player.coins.ToString();
            Destroy(gameObject);
        }
    }
}

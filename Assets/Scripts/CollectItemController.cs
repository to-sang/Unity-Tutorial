using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;


public class CollectItemController : MonoBehaviour
{
    [SerializeField] private AudioSource collectItemAudio;
    [SerializeField] private Text text;
    private void Start()
    {
        StaticClass.PreviousScore = StaticClass.Score;
        text.text = "Score: " + StaticClass.Score;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CollectItem"))
        {
            text.text = "Score: " + ++StaticClass.Score;
            collectItemAudio.Play();
            collectItemAudio.volume = collectItemAudio.volume + 0.1f;
            Destroy(collision.gameObject);
        }
    }
}

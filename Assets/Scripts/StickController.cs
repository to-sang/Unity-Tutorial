using UnityEngine;

public class StickController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") 
            && CompareTag("Untagged"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") 
            && CompareTag("Untagged"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}

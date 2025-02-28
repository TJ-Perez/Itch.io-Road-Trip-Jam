using UnityEngine;

public class outsideViewEnabler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<playerController>().isAbleViewOutside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<playerController>().isAbleViewOutside = false;
    }
}

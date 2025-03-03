using UnityEngine;
using UnityEngine.UI;

public class floatingHealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealthBar(float current, float max)
    {
        slider.value = current/max;
    }
}

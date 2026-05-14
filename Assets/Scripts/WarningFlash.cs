using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningFlash : MonoBehaviour
{
    [SerializeField] private float aliveTime = 2.5f; // Thời gian tồn tại của cảnh báo
    [SerializeField] private float flashSpeed = 0.5f; // Tốc độ nhấp nháy
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        if (image == null) { Debug.LogError("Need Image Component on Warning UI"); return; }
        
        StartCoroutine(FlashRoutine());
        // Tự hủy sau aliveTime
        Destroy(gameObject, aliveTime);
    }

    IEnumerator FlashRoutine()
    {
        while (true) 
        {
            Color c = image.color;
            c.a = 0; 
            image.color = c;
            yield return new WaitForSeconds(flashSpeed);

            c.a = 1; 
            image.color = c;
            yield return new WaitForSeconds(flashSpeed);
        }
    }
}
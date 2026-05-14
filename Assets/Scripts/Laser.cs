using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    [Header("Settings")]
    public float warningDuration = 2.5f; // Thời gian hiện tia nhỏ cảnh báo
    public float fireDuration = 2.5f;      // Thời gian phát hỏa gây sát thương
    public float laserWidth = 0.4f;      // Độ dày khi phát hỏa

    [Header("Points")]
    public Transform startPoint; // Điểm đầu (thường ở rìa màn hình trái)
    public Transform endPoint;   // Điểm cuối (thường ở rìa màn hình phải)

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        
        lineRenderer.enabled = false;
        edgeCollider.enabled = false;
    }

    void Start()
    {
        StartCoroutine(LaserRoutine());
    }

    IEnumerator LaserRoutine()
    {
        // 1. Gán vị trí cho Line Renderer
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);

        // 2. Trạng thái CẢNH BÁO (Tia laser rất mỏng, chưa có sát thương)
        lineRenderer.enabled = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        yield return new WaitForSeconds(warningDuration);

        // 3. Trạng thái PHÁT HỎA (Tia laser to ra, bật va chạm)
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        
        // Cập nhật Collider trùng với đường line
        UpdateCollider();
        edgeCollider.enabled = true;

        yield return new WaitForSeconds(fireDuration);

        // 4. Kết thúc và Tự hủy
        Destroy(gameObject);
    }

    void UpdateCollider()
    {
        Vector2[] points = new Vector2[2];
        points[0] = transform.InverseTransformPoint(startPoint.position);
        points[1] = transform.InverseTransformPoint(endPoint.position);
        edgeCollider.points = points;
    }
}
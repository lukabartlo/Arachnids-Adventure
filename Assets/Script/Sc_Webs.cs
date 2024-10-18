using UnityEngine;

public class Sc_Webs : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    void Start()
    {
        
    }

    void Update()
    {
        if (startPos != null && endPos != null) 
        {
            PlaceWeb(startPos, endPos);
        }
    }

    void PlaceWeb(Transform startPoint,Transform endPoint)
    {
        transform.position = ((startPoint.position + endPoint.position) / 2);
        float web_distance = Vector2.Distance(startPoint.position, endPoint.position);
        transform.localScale = new Vector3(web_distance, transform.localScale.y, transform.localScale.z);
        Vector2 web_direction = endPoint.position - startPoint.position;
        float web_rotation = Mathf.Atan2(web_direction.y, web_direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, web_rotation);
    }
}

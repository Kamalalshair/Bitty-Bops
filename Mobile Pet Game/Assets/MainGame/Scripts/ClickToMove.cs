using UnityEngine;
public class ClickToMove : MonoBehaviour
{
 
[SerializeField] Transform target;
float speed = 6f;
Vector2 targetPos;
 
private void Start()
{
    targetPos = transform.position;
}
 
void Update ()
{
    if(Input.GetMouseButtonDown(0))
    {
        targetPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.position = targetPos;
    }
    if((Vector2)transform.position != targetPos)
    {
       transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}
}
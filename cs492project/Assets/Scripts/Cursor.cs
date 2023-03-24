using UnityEngine;

public class Cursor : MonoBehaviour
{

    public void Start() 
    {
        UnityEngine.Cursor.visible = false;
    }

    public void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorPos.x + 0.40f, cursorPos.y - 0.40f);   
    }
    
}

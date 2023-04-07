using UnityEngine;

public class Scale : MonoBehaviour
{
    public void ScaleUp()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 0f);
    }

    public void ScaleDown()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}

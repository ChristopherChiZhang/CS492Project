using TMPro;
using UnityEngine;

public class ToDoListWindow : MonoBehaviour
{
    public void UpdateToDoList(TaskApp task)
    {
        transform.Find(task.name + "Summary").gameObject.GetComponent<TMP_Text>().fontStyle = FontStyles.Strikethrough;
    }
}

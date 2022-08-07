using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Text headerField;
    [SerializeField] private Text contentField;

    [SerializeField] private LayoutElement layoutElement;

    [SerializeField] private int maxCharacter;

    [SerializeField] private RectTransform rectTransform;

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.x / Screen.width;

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = mousePosition;
    }

    public void SetText(string content, string header = "")
    {
        if (header == "")
            headerField.gameObject.SetActive(false);
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > maxCharacter || contentLength > maxCharacter) ? true : false;
    }
}

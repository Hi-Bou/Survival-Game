using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    [SerializeField] private Tooltip tooltip;

    #region Singleton
    private static TooltipSystem _singleton;

    public static TooltipSystem Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(TooltipSystem)} instance already exists, destroying duplicated!");
                Destroy(value);
            }
        }
    }

    private void Awake()
    {
        Singleton = this;
    }
    #endregion Singleton

    public void Show(string content, string header = "")
    {
        tooltip.SetText(content, header);
        tooltip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        tooltip.gameObject.SetActive(false);
    }
}

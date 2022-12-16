using UnityEngine;
using UnityEngine.UIElements;

public class BaseMainView : MonoBehaviour
{
    protected UIDocument uiDocument;

    protected void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        uiDocument.enabled = false;
    }

    protected void Update()
    {
        // Close when pressing escape
        if(uiDocument.enabled && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    public virtual void CloseUI()
    {
        uiDocument.enabled = false;
        PauseMenu.GameIsPaused = false;
    }

    public virtual void OpenUI()
    {
        uiDocument.enabled = true;
        PauseMenu.GameIsPaused = true;
    }
}
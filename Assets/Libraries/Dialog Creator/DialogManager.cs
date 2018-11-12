using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public float charsPerSecond;
    GameObject dialogPanel;
    Text dialogText;
    Button[] buttons;

    Dialog currentDialog;

    private static DialogManager instance;
    public static DialogManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogManager>();
                if (instance == null)
                {
                    instance = new GameObject("new DialogManager Object").AddComponent<DialogManager>().GetComponent<DialogManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        dialogPanel = gameObject;
        dialogText = transform.GetChild(0).GetComponent<Text>();

        buttons = new Button[3];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = transform.GetChild(i + 1).GetComponent<Button>();
        }

        buttons[0].onClick.AddListener(delegate { OnOptionClicked(0); });
        buttons[1].onClick.AddListener(delegate { OnOptionClicked(1); });
        buttons[2].onClick.AddListener(delegate { OnOptionClicked(2); });
        dialogPanel.SetActive(false);
    }

    public void LoadDialog(Dialog d)
    {
        dialogPanel.SetActive(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        currentDialog = d;
        StartCoroutine(ReadDialogCoroutine(currentDialog.GetDialogLine()));
        EventManager.Instance.DispatchEvent(EventID.DIALOG_STARTED);
    }

    public void NextDialog(int i = 0)
    {
        LoadDialog(currentDialog.next[0]);
    }

    IEnumerator ReadDialogCoroutine(string dialog)
    {
        dialogText.text = "";
        for (int i = 0; i < dialog.Length; i++)
        {
            dialogText.text += dialog[i];
            yield return new WaitForSecondsRealtime(1f / charsPerSecond);
        }
        ReadOptions(currentDialog.next);
    }

    void ReadOptions(List<Dialog> options)
    {
        if (options.Count == 0)
        {
            buttons[0].gameObject.SetActive(true);
            buttons[0].GetComponentInChildren<Text>().text = "[End]";
        }

        else
        {
            for (int i = 0; i < options.Count; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<Text>().text = options[i].textOptions[0];
            }
        }
    }

    void OnOptionClicked(int buttonIndex)
    {
        if (currentDialog.next.Count <= 0) EndConversation();
        else LoadDialog(currentDialog.next[buttonIndex].next[0]);
    }

    void EndConversation()
    {
        dialogPanel.SetActive(false);
        EventManager.Instance.DispatchEvent(EventID.DIALOG_ENDED);
    }
}
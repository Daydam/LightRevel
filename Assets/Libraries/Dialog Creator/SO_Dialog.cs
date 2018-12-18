using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/New Dialog")]
public class SO_Dialog : ScriptableObject
{
    [SerializeField]
    Node_DialogLine[] dialogNodes;
    public Node_DialogLine[] DialogNodes { get { return dialogNodes; } set { dialogNodes = value; } }
    [SerializeField]
    Node_Response[] responseNodes;
    public Node_Response[] ResponseNodes { get { return responseNodes; } set { responseNodes = value; } }
    public Rect dragScreen;

    [SerializeField]
    DialogLine[] dialogs;
    public DialogLine[] Dialogs { get { return dialogs; } set { dialogs = value; } }
    [SerializeField]
    Response[] responses;
    public Response[] Responses { get { return responses; } set { responses = value; } }

    public string dialogCondition;
    public bool repeats;

    //We'll have a DialogLine array and a Response array.
    //When a DialogLine comes up, the options are 
        //responses.Where(a => a.ComesFrom = currentText && ConditionManager.Instance.CheckCondition(a.Condition));
    //When we click a Response, the next DialogLine will be 
        //dialogs.Where(a => a.ComesFrom = currentText && ConditionManager.Instance.CheckCondition(a.Condition)).First;

    public DialogLine GetDialog(int responseID)
    {
        return dialogs.Where(a => a.ComesFrom == responseID && ConditionManager.Instance.CheckCondition(a.Conditions)).FirstOrDefault();
    }

    public Response[] GetResponses(int dialogID)
    {
        return responses.Where(a => a.ComesFrom == dialogID && ConditionManager.Instance.CheckCondition(a.Conditions)).ToArray();
    }
}

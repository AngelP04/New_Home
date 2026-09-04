using Articy.Unity;
using Articy.Unity.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BranchChoice : MonoBehaviour
{
    [SerializeField]
    private TMP_Text buttonText;

    private Branch branch;
    private ArticyFlowPlayer flowPlayer;

    public void AssignBranch(ArticyFlowPlayer aFlowPlayer, Branch aBranch)
    {
        branch = aBranch;
        flowPlayer = aFlowPlayer;
        buttonText.text = string.Empty;
        var objectWithMenuText = aBranch.Target as IObjectWithLocalizableMenuText;
        if (objectWithMenuText != null)
        {
            buttonText.text = objectWithMenuText.MenuText;
        }

        if (string.IsNullOrEmpty(buttonText.text))
        {
            buttonText.text = ">>>";
        }
    }

    public void OnBranchSelected()
    {
        DialogManager.instance.ClearAllBranches();
        DialogManager.instance.continueButton = true;
        flowPlayer.Play(branch);
        flowPlayer.Play();
    }
}

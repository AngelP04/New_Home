using Articy.New_Home;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Playables;

public class DialogManager : MonoBehaviour, IArticyFlowPlayerCallbacks
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText, continueText;
    public TextMeshProUGUI npcName;
    public bool dialogActive;
    public bool continueButton;
    private bool dialogFinished, haveBranch, isCinematic;
    [SerializeField]
    private RectTransform branchLayoutPanel;
    [SerializeField]
    private GameObject branchPrefab;
    [SerializeField]
    private PlayableDirector playableDirector;
    [SerializeField]
    private ArticyRef myRef;

    public string[] dialogLines;
    public int currentDialogLine;

    private PlayerMovement player;

    public static DialogManager instance { get; private set; }

    private ArticyFlowPlayer flowPlayer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        flowPlayer = GetComponent<ArticyFlowPlayer>();
    }
    void Update()
    {
        if(dialogActive)
        {
            if (dialogFinished && Input.GetMouseButtonDown(0))
            {
                EndDialog();
            }
            if (!haveBranch && Input.GetMouseButtonDown(0))
            {
                ClearAllBranches();
                if (!dialogFinished)
                {
                    flowPlayer.Play();
                }
                if (dialogFinished)
                {
                    EndDialog();
                }
            }
        }
        /*if (dialogActive && Input.GetMouseButtonDown(0))
        {
            flowPlayer.Play();
            //currentDialogLine++;
        }

        if (currentDialogLine >= dialogLines.Length)
        {
            dialogActive = false;
            dialogBox.SetActive(false);
            currentDialogLine = 0;
        }
        else
        {
            dialogText.text = dialogLines[currentDialogLine];
        }
        */
    }

    public void ContinueDialog()
    {
        flowPlayer.Play();
    }

    public void ShowDialog(string[] lines, string name=null)
    {
        dialogActive = true;
        dialogBox.SetActive(true);
        currentDialogLine = 0;
        dialogLines = lines;
        npcName.text = name;
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        dialogText.text = string.Empty;
        var objectWithText = aObject as IObjectWithLocalizableText;
        if (objectWithText != null)
        {
            dialogText.text = objectWithText.Text;
            var objectWithSpeaker = aObject as IObjectWithSpeaker;
            if (objectWithSpeaker != null)
            {
                var speakerEntity = objectWithSpeaker.Speaker as Entity;
                if(speakerEntity != null)
                {
                    npcName.text = speakerEntity.DisplayName;
                }
            }
        }
        continueButton = false;
    }

    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
        dialogFinished = true;
        haveBranch = aBranches.Count > 1;
        continueText.gameObject.SetActive(!haveBranch);

        foreach (var branch in aBranches)
        {
            if (branch.Target is IDialogueFragment)
            {
                dialogFinished = false;
                break;
            }
        }
        if (!dialogFinished && haveBranch)
        {
            foreach (var branch in aBranches)
            {
                GameObject btn = Instantiate(branchPrefab, branchLayoutPanel);
                btn.GetComponent<BranchChoice>().AssignBranch(flowPlayer, branch);
            }
        }
    }

    public void ClearAllBranches()
    {
        foreach (Transform child in branchLayoutPanel)
        {
            Destroy(child.gameObject);
        }
    }

    private void EndDialog()
    {
        dialogBox.SetActive(false);
        flowPlayer.FinishCurrentPausedObject();
        ClearAllBranches();
        dialogActive = false;
        if(isCinematic)
        {
            playableDirector.GetComponent<PlayableDirector>().playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }

    public void StartDialog(ArticyObject aObject)
    {
        flowPlayer.StartOn = aObject;
    }

    public void DialogCinematic()
    {
        if(playableDirector.GetComponent<PlayableDirector>().state == PlayState.Playing)
        {
            var dialogCinematic = myRef.GetObject();
            if (dialogCinematic != null)
            {
                dialogActive = true;
                StartDialog(dialogCinematic);
                playableDirector.GetComponent<PlayableDirector>().playableGraph.GetRootPlayable(0).SetSpeed(0);
                isCinematic = true;
            }
        }
    }
}

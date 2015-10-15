using System;
using UnityEngine;
using UnityEngine.UI;
using Constants;

public class WWWProgress : MonoBehaviour
{
    public int idx;
    public int total;
    public WWW curWWW;
    public bool update = false;
    public string prestr = "";
    public string endstr = "";
    public Slider pBar;
    public Text pLab;

    //[UIAutoFind]
    public GameObject ConfirmPop;
    //[UIAutoFind]
    public Button OK;
    //[UIAutoFind]
    public Button Cancel;
    //[UIAutoFind]
    public Text ContentLabel;

    private Action OKCallBack;
    private Action CancelCallBack;
    //private string txt;

    public void OnEnable()
    {
        Messenger.AddListener<string, Action, Action>(ApplicationEvents.RES_UPDATE_WARNING, OnUpdateWarn);
    }

    private void OnUpdateWarn(string content, Action arg1, Action arg2)
    {
        ConfirmPop.SetActive(true);
        OKCallBack = arg1;
        CancelCallBack = arg2;
        //txt = content;
        ContentLabel.text = content;
    }

    public void OnDisable()
    {
        Messenger.RemoveListener<string, Action, Action>(ApplicationEvents.RES_UPDATE_WARNING, OnUpdateWarn);
    }

    protected void SetData()
    {
    }

    void Awake()
    {
        OK.onClick.AddListener(OnOK);
        Cancel.onClick.AddListener(OnCancel);

        ConfirmPop.SetActive(false);
    }

    void OnDestroy()
    {
        OK.onClick.RemoveListener(OnOK);
        Cancel.onClick.RemoveListener(OnCancel);
    }


    private void OnCancel()
    {
        ConfirmPop.SetActive(false);
        CancelCallBack();
        CancelCallBack = null;
    }

    private void OnOK()
    {
        ConfirmPop.SetActive(false);
        OKCallBack();
        OKCallBack = null;
    }

    public void StartUpdate(WWW www, string pre = "", int index = 1, int totalCount = 1)
    {
        idx = index;
        total = totalCount;
        curWWW = www;
        prestr = pre;
        update = true;
    }
    public void StopUpdate()
    {
        curWWW = null;
        update = false;
    }

    public void SetEndStr(string str)
    {
        endstr = str;
    }
    public void SetText(string str)
    {
        if (pLab != null)
            pLab.text = str;
    }

    public void SetProgress(float v)
    {
        if (pBar != null)
            pBar.value = v;
    }

    void FixedUpdate()
    {
        if (!update)
            return;
        float p = Persent;
        if (pBar != null)
            pBar.value = p;
        if (pLab != null)
            pLab.text = prestr + idx + "/" + total + endstr;
    }

    public float Persent
    {
        get
        {
            if (total < 0)
                total = 1;
            if (idx <= 0)
                idx = 1;
            float p = 1f;
            if (curWWW != null)
                p = curWWW.progress;
            return p;
        }
    }

}

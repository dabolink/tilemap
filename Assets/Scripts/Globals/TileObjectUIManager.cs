using System;
using UnityEngine;
using UnityEngine.UI;

public class TileObjectUIManager : MonoBehaviour
{
    public TileObject CurrentTO;
    public CanvasGroup canvasGroup;
    
    private InputManager currentInputManager;
    private OutputManager currentOutputManager;

    private Toggle IOToggle;

    public bool isInput;

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentTO(null);
        IOToggle = GetToggle(4);
        SetIO(IOToggle.isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentTO(TileObject tileObject)
    {
        canvasGroup.gameObject.SetActive(tileObject != null);
        CurrentTO = tileObject;
        if(tileObject == null)
        {
            return;
        }

        currentOutputManager = CurrentTO.GetComponent<OutputManager>();
        currentInputManager = CurrentTO.GetComponent<InputManager>();

        if(currentOutputManager == null && currentInputManager == null)
        {
            canvasGroup.gameObject.SetActive(false);
        } 
        else if(currentInputManager == null)
        {
            IOToggle.interactable = false;
            SetIO(false);
        }
        else if(currentOutputManager == null)
        {
            IOToggle.interactable = false;
            SetIO(true);
        } else
        {
            IOToggle.interactable = true;
        }
        UpdateIOValues();
    }

    private void UpdateIOValues()
    {
        for (int i = 0; i < 4; i++)
        {
            UpdateIOValue(i);
        }
    }

    private void UpdateIOValue(int dir)
    {
        if (isInput)
        {
            if(currentInputManager == null)
            {
                return;
            }
            GetToggle(dir).isOn = currentInputManager.Status[dir];
        }
        else
        {
            if (currentOutputManager == null)
            {
                return;
            }
            GetToggle(dir).isOn = currentOutputManager.Status[dir];
        }
        
    }

    public void SetIOValue(int dir)
    {
        if (isInput)
        {
            currentInputManager.SetStatus(dir, GetToggle(dir).isOn);
        } 
        else
        {
            currentOutputManager.SetStatus(dir, GetToggle(dir).isOn);
        }
    }

    public void SetAllIOValues(bool val)
    {
        for (int i = 0; i < 4; i++)
        {
            GetToggle(i).isOn = val;
            SetIOValue(i);
        }
    }

    public void SetIO(bool value)
    {
        if(isInput == value)
        {
            return;
        }
        if(!IOToggle.isOn == value)
        {
            IOToggle.isOn = value;
        }
        
        isInput = value;
        UpdateIOValues();
    }

    public Toggle GetToggle(int dir)
    {
        return canvasGroup.transform.GetChild(dir).GetComponent<Toggle>();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{

    [SerializeField] private float _moveTime = 0.1f;

    private GameObject selectIcon;

    private void Start()
    {
        selectIcon = transform.Find("Select").gameObject;
    }

    private IEnumerator SelectButton(bool startingAnimation)
    {
        float elapsedTime = 0f;
        while (elapsedTime < _moveTime)
        {
            elapsedTime += Time.deltaTime;
            if (startingAnimation)
            {
                selectIcon.SetActive(true);
                //
            }
            else
            {
                selectIcon.SetActive(false);
                //
            }

            yield return null;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(SelectButton(true));

        MenuController.instance.LastSelected = gameObject;
        for (int i = 0; i < MenuController.instance.Buttons.Length; i++)
        {
            MenuController.instance.LastSelectedIndex = i;
        }
        return;

    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(SelectButton(false));
    }
}

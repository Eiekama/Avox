using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonSelectionController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,ISelectHandler,IDeselectHandler
{

    [SerializeField] private float _verticalMoveAmount = 0f;
    [SerializeField] private float _moveTime = 0.1f;
    [Range(0f, 2f), SerializeField] private float _scaleAmount = 1.2f;

    private Vector3 _startPos;
    private Vector3 _startScale;

    Scene scene;

    private void Start()
    {
         scene = SceneManager.GetActiveScene();
        _startPos = transform.position;
        _startScale = transform.localScale;

    }

    private IEnumerator MoveButton(bool startingAnimation)
    {
        Vector3 endPosition;
        Vector3 endScale;

        float elapsedTime = 0f;

        while (elapsedTime < _moveTime)
        {
            elapsedTime += Time.deltaTime;
            if (startingAnimation)
            {
                endPosition = _startPos + new Vector3(0f, _verticalMoveAmount, 0f);
                endScale = _startScale * _scaleAmount;

            }
            else
            {
                endPosition = _startPos;
                endScale = _startScale;
            }

            Vector3 lerpedPos = Vector3.Lerp(transform.position, endPosition, (elapsedTime / _moveTime));
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / _moveTime));

            transform.position = lerpedPos;
            transform.localScale = lerpedScale;

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
        StartCoroutine(MoveButton(true));

        if (scene.name == "EscMenu")
        {
            PauseMenuController.instance.LastSelected = gameObject;
            for (int i = 0; i < PauseMenuController.instance.Buttons.Length; i++)
            {
                PauseMenuController.instance.LastSelectedIndex = i;
            }
            return;
        }
        else if (scene.name == "mainMenu")
        {
            MainMenuController.instance.LastSelected = gameObject;
            for (int i = 0; i < MainMenuController.instance.Buttons.Length; i++)
            {
                MainMenuController.instance.LastSelectedIndex = i;
            }
            return;
        }
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(MoveButton(false));
    }
}


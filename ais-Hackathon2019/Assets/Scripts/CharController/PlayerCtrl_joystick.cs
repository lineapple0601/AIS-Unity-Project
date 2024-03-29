﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*作成者：MOON*/
// IDragHandler`、IPointerUpHandler、IPointerDownHandlerを追加する
public class PlayerCtrl_joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //joystickの背景をもらう　→　joystickBG
    private Image bgImg;
    //直接動くjoystickをもらう  →　joystickCtrl
    private Image joystickImg;
    //各移動するVectorの値をもらう
    private Vector3 inputVector;

    public bool drag_Check = false;

    // Start is called before the first frame update
    void Start()
    {
        //最初に実行するとき、各値をもらう
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /* 
     * bgImgの領域の中でタッチが発生した場合(「RectTransformUtility.ScreenPointToLocalPointInRectangle」これがTureとき)、
     * タッチされた位置の値をposにあげ、bgImgの直四角形のsizeDeltaでわけ、pos.xは0 ~ -1、pos.yは0 ~ 1の値で作る
     * joystickImgを基準で左右で動いたとき、pos.xは-1 ~ 1の間の値で、上下で動いた場合pos.y-は1 ~ 1の間の値で変換するため
     * pos.x * 2 + 1、pos.y * 2 - 1で処理する。
     * その値をinputVectorに代入し、単位Vectorで作る。最後にjoystickImgをタッチした位置で移動する。
     */
    public void OnDrag(PointerEventData eventData)
    {
        drag_Check = true;
        Vector2 pos;
        
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform,
                                                                    eventData.position,
                                                                    eventData.pressEventCamera,
                                                                    out pos))
        {

            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2 + 1, pos.y * 2 - 1, 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // 赤いj\Joystickの移動
            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3),
                                                                     inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3));
        }
    }

    /* 
     * タッチしているとき、発生するOnPointerDown(PointerEventData eventData)メッソードでOnDrag(eventData)実行するようにする。
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        drag_Check = true;
        OnDrag(eventData);
    }

    /* 
     * タッチを中止したとき、発生するOnPointerUp(PointerEventData eventData)メッソードでinputVectorとjoystickImgの位置を初期化する。
     */
    public void OnPointerUp(PointerEventData eventData)
    {
        drag_Check = false;
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    /* 
     * inputVactor値をPlayerControllerスクリプトに渡すため、使用するGetHorizontalValue
     * PlayerControllerスクリプトにinputVector.x値をもらうため使用するメッソード
     */
    public float GetHorizontalValue()
    {
        return inputVector.x;
    }

    /* 
     * inputVactor値をPlayerControllerスクリプトに渡すため、使用するGetVerticalValue
     * PlayerControllerスクリプトにinputVector.y値をもらうため使用するメッソード
     */
    public float GetVerticalValue()
    {
        return inputVector.y;
    }

}

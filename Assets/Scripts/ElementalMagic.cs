using System;
using System.Collections;
using UnityEngine;

namespace Magic
{
    //NAME OF ELEMENTS OF ENUMS IS IMPORTANT!
    public enum Elements
    {
        Fire,
        Frost
    }
    public enum Shapes
    {
        Stream,
        Projectile
    }
    public enum Targets
    {
        Forward,
        Around
    }
}

public class ElementalMagic : MonoBehaviour
{
    ///<summary> накастованная комбинация </summary>
    private int chosenElement, chosenShape, chosenTarget;
    ///<summary> состояние скрипта </summary>
    private ScriptStates state = ScriptStates.waitingStart;
    ///<summary> кнопка активации скрипта (TODO: пересмотреть алгоритм, чтобы избавиться от кнопки активации) </summary>
    public KeyCode activateButton = KeyCode.G;
    ///<summary> кнопка применения заклинания </summary>
    public KeyCode releaseButton = KeyCode.Mouse0;
    ///<summary> массив управляющих кнопок для выбора комбинаций </summary>
    public KeyCode[] binds;
    enum ScriptStates
    {
        waitingStart,
        waitingElement,
        waitingShape,
        waitingTarget,
        readyToUse
    }

    void Update()
    {
        //Debug.Log(state);
        //Debug it
        if( state == ScriptStates.waitingStart && Input.GetKeyDown(activateButton))
        {
            state++;
            StartCoroutine(CastSpell());
        }
        else if(state == ScriptStates.readyToUse && Input.GetKeyDown(releaseButton))
        {
            //Создание экземпляра класса выбранного элемента магии
            Element element = (Element)Type.GetType(Enum.GetName(typeof(Magic.Elements), chosenElement))
                .GetConstructor(new Type[1] { typeof(GameObject) })
                .Invoke(new object[] { gameObject });

            element.ParseCast((Magic.Shapes)chosenShape, (Magic.Targets)chosenTarget);
            chosenElement = chosenShape = chosenTarget = -1;
            state = ScriptStates.waitingStart;
        }
    }

    IEnumerator CastSpell()
    {
        while(state != ScriptStates.readyToUse || state != ScriptStates.waitingStart)
        {
            InputMode();
            yield return null;
        }
        yield break;
    }

    void InputMode()
    {
        int keyIndex;
        for(keyIndex = 0; keyIndex < binds.Length; keyIndex++)
        {
            if (Input.GetKeyDown(binds[keyIndex]))
                break;
        }
        if (keyIndex == binds.Length)
            return;
        //TODO: добавить ограничение keyIndex
        //TODO: придумать что-нибудь вместо switch
        //TODO: обработать cancel button
        switch(state)
        {
            case ScriptStates.waitingElement:
                chosenElement = keyIndex;
                break;
            case ScriptStates.waitingShape:
                chosenShape = keyIndex;
                break;
            case ScriptStates.waitingTarget:
                chosenTarget = keyIndex;
                break;
        }
        state++;
    }
}

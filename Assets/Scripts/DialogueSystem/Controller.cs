using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class Controller : MonoBehaviour
    {

        #region Public Variables
        [Header("Buttons")]
        public List<Button> answerButtonList;
        #endregion

        #region Private Variables
        private LinesScriptable.Answer answer;
        #endregion

        #region Unity Events
        private void Awake()
        {
            MessageDispatcher.AddListener(this);
        }
        #endregion

        #region Private Methods
        private void EnableButton(int buttonCount)
        {
            for (int i = 0; i < buttonCount; i++)
            {
                answerButtonList[i].gameObject.SetActive(true);
                answerButtonList[i].GetComponentInChildren<Text>().text = answer.answersList[i];
                var i1 = i;
                answerButtonList[i].onClick.AddListener(delegate { InvokeEvents(answer.answerActions[i1]); });
            }
        }

        private void InvokeEvents(UnityEvent ev)
        {
            if (ev == null) return;
            ev.Invoke();
        }

        private void DisableButton(int buttonCount)
        {
            for (int i = 0; i < buttonCount; i++)
            {
                answerButtonList[i].gameObject.SetActive(false);
                if (answerButtonList[i].onClick.GetPersistentEventCount() != 0)
                    answerButtonList[i].onClick.RemoveAllListeners();
            }
        }

        #endregion

        #region Message Based Methods
        private void GetAnswer(Messages.Dialogue.GetAnswer message)
        {
            answer = message.answer;
            EnableButton(answer.answersList.Count);
        }
        private void StopDialogue(Messages.Dialogue.Stop message)
        {
            if(answer != null)
                DisableButton(answer.answersList.Count);
        }
        #endregion
    }
}


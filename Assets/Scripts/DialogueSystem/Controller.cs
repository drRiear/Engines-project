using UnityEngine;
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
        private Lines.Answer answer;
        #endregion

        #region Unity Events
        private void Awake()
        {
            MessageDispatcher.AddListener(this);
        }
        private void Update()
        {
            
            if (answer != null)
                GetAnswer();
        }
        #endregion

        #region Private Methods
        private void GetAnswer()
        {
            switch (answer.answersList.Count)
            {
                case 1:
                    EnableButton(0);
                    break;
                case 2:
                    EnableButton(0);
                    EnableButton(1);
                    break;
                case 3:
                    EnableButton(0);
                    EnableButton(1);
                    EnableButton(2);
                    break;
                case 4:
                    EnableButton(0);
                    EnableButton(1);
                    EnableButton(2);
                    EnableButton(3);
                    break;
            }
        }

        private void EnableButton(int index)
        {
            answerButtonList[index].gameObject.SetActive(true);
            answerButtonList[index].GetComponentInChildren<Text>().text = answer.answersList[index];
        }

        #endregion

        #region Message Based Methods
        private void GetAnswer(Messages.Dialogue.GetAnswer message)
        {
            answer = message.answer;
        }
        private void StartDialogue(Messages.Dialogue.Start message)
        {

        }
        #endregion
    }
}


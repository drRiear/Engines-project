﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DialogueSystem
{
    public class Displayer : MonoBehaviour
    {
        #region Public Variables

        public LinesScriptable _linesScriptable;
        [Space]
        public float timeToPrintLetter = 0.05f;
        [Header("UI Elements")]
        public Canvas dialogueCanvas;
        public Text phraseText;
        #endregion

        #region Private Variables
        private IEnumerator currentCoroutine;
        private GameObject thisNPC;
        #endregion

        #region Unity Events
        private void Awake()
        {
            MessageDispatcher.AddListener(this);
            thisNPC = transform.parent.gameObject;
        }
        #endregion

        #region Private Methods
        private void Talk()
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            phraseText.text = "";

            if (_linesScriptable.queue.Count == 0)
            {
                DialogueEnd();
                return;
            }

            currentCoroutine = PrintPhrase(_linesScriptable.queue[0]);
            StartCoroutine(currentCoroutine);
        }

        private void CheckLineForAnswer()
        {
            int currentLineIndex = _linesScriptable.linesList.IndexOf(_linesScriptable.queue[0]);
            foreach (var answer in _linesScriptable.answers)
                if (answer.lineIndex == currentLineIndex)
                    MessageDispatcher.Send(new Messages.Dialogue.GetAnswer(answer));
        }

        private void DialogueEnd()
        {
            MessageDispatcher.Send(new Messages.Dialogue.Stop(thisNPC));

        }

        private IEnumerator PrintPhrase(string phrase)
        {
            CheckLineForAnswer();

            _linesScriptable.UnQueue(phrase);

            foreach (var c in phrase)
            { 
                phraseText.text += c;
                yield return new WaitForSeconds(timeToPrintLetter);
            }
        }
        #endregion

        #region Message Based Methods
        private void StartDialogue(Messages.Dialogue.Start message)
        {
            if (message.npc != thisNPC) return;

            if (!dialogueCanvas.gameObject.activeInHierarchy)
                dialogueCanvas.gameObject.SetActive(true);
            
            Talk();
        }
        private void StopDialogue(Messages.Dialogue.Stop message)
        {
            _linesScriptable.SetQueue();
            dialogueCanvas.gameObject.SetActive(false);
        }
        #endregion
    }
}
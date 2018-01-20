﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DialogueSystem
{
    public class Dialogue : MonoBehaviour
    {
        #region Public Variables
        public string textFileName;
        [Space]
        public float timeToPrintLetter = 0.05f;
        [Header("UI Elements")]
        public Canvas dialogueCanvas;
        public Text phraseText;
        #endregion

        #region Private Variables
        private Lines _lines;
        private IEnumerator currentCoroutine;
        private GameObject thisNPC;
        #endregion


        #region Unity Events
        private void Awake()
        {
            MessageDispatcher.AddListener(this);
            thisNPC = transform.parent.gameObject;
            LoadText();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
                LoadText();
        }
        #endregion

        #region Private Methods
        private void LoadText()
        {
            DataManager dm = new DataManager(textFileName);
            
            _lines = dm.LoadFromJSON<Lines>();
            _lines.SetQueue();
        }

        private void Talk()
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            phraseText.text = "";

            if (_lines.queue.Count == 0)
            {
                DialogueEnd();
                return;
            }
            
            currentCoroutine = PrintPhrase(_lines.queue[0]);
            StartCoroutine(currentCoroutine);
        }

        private void DialogueEnd()
        {
            _lines.SetQueue();
            dialogueCanvas.gameObject.SetActive(false);
        }

        private IEnumerator PrintPhrase(string phrase)
        {
            _lines.UnQueue(phrase);

            foreach (var c in phrase)
            { 
                phraseText.text += c;
                yield return new WaitForSeconds(timeToPrintLetter);
            }
        }
        #endregion

        #region Private Methods
        private void StartDialogue(Messages.DialogueStart message)
        {
            if (message.npc != thisNPC) return;
            

            if (!dialogueCanvas.gameObject.activeInHierarchy)
                dialogueCanvas.gameObject.SetActive(true);
            
            Talk();
        }
        private void StopDialogue(Messages.DialogueStops message)
        {
            if (message.npc != thisNPC) return;

            DialogueEnd();
        }
        #endregion
    }
}
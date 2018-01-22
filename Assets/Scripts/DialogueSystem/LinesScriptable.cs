using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Lines", menuName = "Dialogue/Lines", order = 1)]
    public class LinesScriptable : ScriptableObject
    {
        public List<string> linesList = new List<string>();
        [HideInInspector] public List<string> queue = new List<string>();
        public List<Answer> answers = new List<Answer>();

        public void SetQueue()
        {
            if (linesList == null || linesList.Count < 1) return;

            queue = new List<string>(linesList);
        }

        public void UnQueue(string phrase)
        {
            if (queue == null || queue.Count < 1) return;

            queue.Remove(phrase);
        }

        [System.Serializable]
        public class Answer
        {
            public int lineIndex;
            public List<string> answersList;
            public List<UnityEvent> answerActions;
        }
    }
}

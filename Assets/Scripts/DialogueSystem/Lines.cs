using System.Collections.Generic;

namespace DialogueSystem
{
    [System.Serializable]
    public class Lines
    {
        public List<string> linesList = new List<string>();
        public List<string> queue = new List<string>();
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
        }
    }
}

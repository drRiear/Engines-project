using System.Collections.Generic;

namespace DialogueSystem
{
    [System.Serializable]
    public class Lines
    {
        public List<string> linesList;
        public List<string> queue;

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
    }
}

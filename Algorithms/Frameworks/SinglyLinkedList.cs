using Algorithms.Patterns.SinglyLinkedList;

namespace Algorithms.Frameworks
{
    /// <summary>
    /// Односвязный список
    /// </summary>
    internal class SinglyLinkedList
    {
        private readonly TechniquePartialReversals techniquePartialReversals;
        private readonly DummyNode dummyNode;

        ListNode Reverse(ListNode head)
        {
            ListNode prev = null;
            var current = head;

            while (current != null)
            {
                var next = current.Next;

                current.Next = prev;

                prev = current;
                current = next;
            }

            return prev;
        }
    }

    class ListNode
    {
        public int Value;
        public ListNode Next;

        public ListNode(int value)
        {
            Value = value;
        }
    }
}
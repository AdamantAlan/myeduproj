using Algorithms.Frameworks;

namespace Algorithms.Patterns.SinglyLinkedList
{
    /// <summary>
    /// Техника Частичных Разворотов
    /// </summary>
    internal class TechniquePartialReversals
    {
        bool IsPalindrome(ListNode head)
        {
            var slow = head;
            var fast = head;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            var right = Reverse(slow);
            var left = head;

            while (right != null)
            {
                if (left.Value != right.Value)
                    return false;

                left = left.Next;
                right = right.Next;
            }

            return true;
        }

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class ListUtilities
    {
        public static Stack<T> CreateShuffledStack<T>(IList<T> values) where T : Object
        {
            var stack = new Stack<T>();
            var list = new List<T>(values);

            // Remove values from the list and add to the stack as long as there are
            // items left on the list - list gets smalleras the stack gets bigger.
            while(list.Count > 0)
            {
                //get the next item at a random index
                var randomIndex = Random.Range(0, list.Count - 1);
                var randomItem = list[randomIndex];
                // Remove from the list and add to the stack
                list.RemoveAt(randomIndex);
                stack.Push(randomItem);
            }
            return stack;
        }

    }

}

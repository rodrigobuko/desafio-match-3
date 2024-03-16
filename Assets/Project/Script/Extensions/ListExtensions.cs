using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3.Extensions
{
    public static class ListExtensions 
    {
        // Extension to check if there is any true value
        public static bool ContainsAnyTrue(this List<List<bool>> listOfLists)
        {
            foreach (var innerList in listOfLists)
            {
                foreach (var value in innerList)
                {
                    if (value)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

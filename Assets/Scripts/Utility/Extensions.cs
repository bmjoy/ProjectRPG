using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


static class Extensions
{
    //Return a random element from an array
    public static T GetRandom<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T GetRandom<T>(this T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public static List<T> GetRandom<T>(this List<T> list, int amount)
    {
        List<T> items = new List<T>();

        for (int i = 0; i < amount; i++)
        {
            var randomElement = list[UnityEngine.Random.Range(0, amount + 1)];

            if (!items.Contains(randomElement))
                items.Add(randomElement);
            else i--;
        }

        return items;
    }
}


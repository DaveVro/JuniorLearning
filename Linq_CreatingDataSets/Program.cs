﻿using System;
using System.Collections;
using System.Collections.Generic;


namespace CreatingDataSets
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            static IEnumerable<string> Suits()
            {
                yield return "clubs";
                yield return "diamonds";
                yield return "hearts";
                yield return "spades";
            }

            static IEnumerable<string> Ranks()
            


            {
                yield return "two";
                yield return "three";
                yield return "four";
                yield return "five";
                yield return "six";
                yield return "seven";
                yield return "eight";
                yield return "nine";
                yield return "ten";
                yield return "jack";
                yield return "queen";
                yield return "king";
                yield return "ace";
            }
        }
    }
}

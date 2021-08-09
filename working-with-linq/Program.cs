using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqFaroShuffle
{
    class Program
    {
        /// <summary>
        /// Entry point of the app
        /// This app creates a deck of cards using a LINQ query and performs a faro shuffle
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Linq query using query syntax to build the deck of cards
            var startingDeck = (from s in Suits().LogQuery("Suit Generation")
                               from r in Ranks().LogQuery("Rank Generation")
                               select new { Suit = s, Rank = r }).LogQuery("Starting Deck").ToArray();

            // Method syntax version of Linq query
            //var startingDeck = Suits().SelectMany(suit => Ranks().Select(rank => new { Suit = suit, Rank = rank }));

            foreach (var card in startingDeck)
            {
                Console.WriteLine(card);
            }

            // 52 cards in a deck, so 52 / 2 = 26
            //var top = startingDeck.Take(26);
            //var bottom = startingDeck.Skip(26);
            //var shuffle = top.InterleaveSequenceWith(bottom);

            //foreach (var card in shuffle)
            //{
            //    Console.WriteLine(card);
            //}

            var times = 0;

            var shuffle = startingDeck;

            // Shuffles the cards until they return to the original sequence
            do
            {
                // Dev note, using ToArray switchs the evaluation from lazy evaluation to eager evaluation

                // Out shuffle, light query with 8 iterations
                //shuffle =
                //    shuffle.Take(26).LogQuery("Top half")
                //    .InterleaveSequenceWith(shuffle.Skip(26).LogQuery("Bottom half"))
                //    .LogQuery("Shuffle")
                //    .ToArray();

                // In shuffle, intensive query with 52 iterations
                shuffle =
                    shuffle.Skip(26).LogQuery("Bottom half")
                    .InterleaveSequenceWith(shuffle.Take(26).LogQuery("Top half"))
                    .LogQuery("Shuffle")
                    .ToArray();

                foreach (var card in shuffle)
                {
                    Console.WriteLine(card);
                }

                Console.WriteLine();
                times++;
            }
            while (!startingDeck.SequenceEquals(shuffle));

            Console.WriteLine(times);

            static IEnumerable<string> Suits()
            {
                yield return "Clubs";
                yield return "Diamonds";
                yield return "Hearts";
                yield return "Spades";
            }

            static IEnumerable<string> Ranks()
            {
                yield return "Two";
                yield return "Three";
                yield return "Four";
                yield return "Five";
                yield return "Six";
                yield return "Seven";
                yield return "Eight";
                yield return "Nine";
                yield return "Ten";
                yield return "Jack";
                yield return "Queen";
                yield return "King";
                yield return "Ace";
            }
        }
    }
}

﻿using System.Collections.Generic;
using System.Linq;

namespace CaseStudy
{
    public static class StringSplitting
    {
        // split :: Char -> String -> [String]
        public static IEnumerable<string> Split(char c, string s)
        {
            // split c [] = []
            if (s.Length == 0) return EmptyEnumerableOfString;

            // split c xs = xs' : if null xs'' then [] else split c (tail xs'')
            //     where xs' = takeWhile (/=c) xs
            //           xs''= dropWhile (/=c) xs
            var xs1 = new string(s.TakeWhile(x => x != c).ToArray());
            var xs2 = new string(s.SkipWhile(x => x != c).ToArray());
            return Cons(xs1, (xs2.Length == 0) ? EmptyEnumerableOfString : Split(c, Tail(xs2)));
        }

        private static IEnumerable<string> EmptyEnumerableOfString
        {
            get { return Enumerable.Empty<string>(); }
        }

        private static IEnumerable<T> Cons<T>(T x, IEnumerable<T> xs)
        {
            return new[] {x}.Concat(xs);
        }

        private static string Tail(string s)
        {
            return s.Substring(1);
        }
    }
}

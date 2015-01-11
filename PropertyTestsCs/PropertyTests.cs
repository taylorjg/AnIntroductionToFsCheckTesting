using System.Linq;
using FsCheck;
using FsCheck.NUnit;
using FsCheck.Fluent;
using CaseStudy;
using FsCheckUtils;
using System;
using Microsoft.FSharp.Core;

namespace PropertyTestsCs
{
    using SS = StringSplitting;

    internal class PropertyTests
    {
        private static readonly Config MyConfig = Config.QuickThrowOnFailure;
        private static readonly Configuration MyConfiguration = MyConfig.ToConfiguration();

        private static readonly Func<char, string, bool> PropJoinSplit =
            (c, xs) => SS.Join(c, SS.Split(c, xs)) == xs;

        [Property]
        public void JoinOfSplitGivesOriginalString()
        {
            Spec
                .ForAny(PropJoinSplit)
                .Check(MyConfiguration);
        }

        private static readonly Func<char, string, string> Collect = (c, xs) =>
        {
            var ys = SS.Split(c, xs);
            return ys == null ? "(null)" : Convert.ToString(ys.Count());
        };

        [Property]
        public void JoinOfSplitGivesOriginalStringWithCollect()
        {
            Spec
                .ForAny(PropJoinSplit)
                .Collect(Collect)
                .Check(MyConfiguration);
        }

        private static readonly Func<string, Gen<Rose<Result>>> PropJoinSplitTick =
            xs => Prop.forAll(Arb.fromGen(Gen.elements(xs)), FSharpFunc<char, bool>.FromConverter(c => PropJoinSplit(c, xs)));

        [Property]
        public void JoinOfSplitGivesOriginalStringWhereSepCharIsTakenFromNonEmptyString()
        {
            var arb = Arb.Default.NonEmptyString();
            var body = FSharpFunc<NonEmptyString, Gen<Rose<Result>>>.FromConverter(nes => PropJoinSplitTick(nes.Get));
            Check.One(MyConfig, Prop.forAll(arb, body));
        }
    }
}

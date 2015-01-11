using FsCheck;
using FsCheck.NUnit;
using FsCheck.Fluent;
using CaseStudy;
using FsCheckUtils;
using System;

namespace PropertyTestsCs
{
    using SS = StringSplitting;

    internal class PropertyTests
    {
        private static readonly Config MyConfig = Config.VerboseThrowOnFailure;
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
    }
}

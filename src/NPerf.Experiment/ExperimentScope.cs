﻿namespace NPerf.Experiment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NPerf.Framework.Interfaces;

    internal class ExperimentScope
    {
        public static void Start(SartParameters startParameters)
        {
            using (var testObserver = new TestObserver(startParameters.ChannelName))
            {
                var suite = AssemblyLoader.CreateInstance<IPerfTestSuite>(
                    startParameters.SuiteAssembly, startParameters.SuiteType);

                var subject = AssemblyLoader.CreateInstance(
                    startParameters.SubjectAssembly, startParameters.SubjectType);

                var start = startParameters.Start;
                var step = startParameters.Step;
                var end = startParameters.End;

                if (suite != null && subject != null)
                {
                    var test = suite.Tests.First(x => x.TestMethodName == startParameters.TestMethod);
                    var runner = new TestRunner(
                        delegate(int idx) { suite.SetUp(idx, subject); },
                        delegate { test.Test(subject); },
                        delegate { suite.TearDown(subject); },
                        delegate(int idx) { return suite.GetRunDescriptor(idx); },
                        string.IsNullOrEmpty(start) ? 0 : int.Parse(start),
                        string.IsNullOrEmpty(step) ? 1 : int.Parse(step),
                        string.IsNullOrEmpty(end) ? suite.Tests.Length - 1 : int.Parse(end));

                    runner.Subscribe(testObserver);
                }

                testObserver.OnCompleted();
            }
        }
    }
}

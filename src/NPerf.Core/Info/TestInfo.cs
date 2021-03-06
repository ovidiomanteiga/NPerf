﻿namespace NPerf.Core.Info
{
    using System;

    public class TestInfo
    {

        public Guid TestId { get; set; }

        public string TestMethodName { get; set; }

        public string TestDescription { get; set; }

        public Type TestedType { get; set; }

        public TestSuiteInfo Suite { get; set; }

        public override int GetHashCode()
        {
            return TestId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var testInfo = obj as TestInfo;
            if (testInfo == null)
            {
                return false;
            }

            return this.TestId.Equals(testInfo.TestId);
        }

        public override string ToString()
        {
            return string.Format(
                "{0} for {1}", string.IsNullOrEmpty(TestDescription) ? TestMethodName : TestDescription, TestedType);
        }
    }
}

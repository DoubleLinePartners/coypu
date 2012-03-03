using System;
using Coypu.Actions;

namespace Coypu.Queries
{
    internal class ActionSatisfiesPredicateQuery : Query<bool>
    {
        private readonly DriverAction tryThis;
        private readonly Query<bool> until;
        public TimeSpan RetryInterval { get; private set; }

        public TimeSpan Timeout { get; private set; }

        internal ActionSatisfiesPredicateQuery(DriverAction tryThis, Query<bool> until, TimeSpan overallTimeout, TimeSpan retryInterval)
        {
            this.tryThis = tryThis;
            this.until = until;
            RetryInterval = retryInterval;
            Timeout = overallTimeout;
        }

        public void Run()
        {
            tryThis.Act();
            until.Run();
            Result = until.Result;
        }

        public object ExpectedResult
        {
            get { return true; }
        }

        public bool Result { get; private set; }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace FileLog.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Writer()
        {
            Logger.Writer(Model.LogLevel.Error, "test");
        }

        [Fact]
        public void ThreadWriter()
        {
            var threadStart = new ThreadStart(Writer);

            var manualResetEvents = new List<ManualResetEvent>();
            ThreadPool.SetMaxThreads(10, 10);
            for (int i = 0; i < 64; i++)
            {
                var mre = new ManualResetEvent(false);
                manualResetEvents.Add(mre);
                ThreadPool.QueueUserWorkItem(t =>
                {
                    Logger.Writer(Model.LogLevel.Error, "±àºÅ" + Thread.CurrentThread.ManagedThreadId);
                    mre.Set();
                });
            }

            WaitHandle.WaitAll(manualResetEvents.ToArray());

        }
    }
}

// Tools Factory

// A set of professional and user-friendly tools aimed at 
// editing all major aspects of Pokémon GBA games.

// Copyright (C) 2010  HackMew

// This file is part of Tools Factory.
// Tools Factory is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Tools Factory is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Tools Factory.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Threading;
using System.ComponentModel;

// References:
// http://weblogs.asp.net/rosherove/pages/BackgroundWorkerEx.aspx
// ---

// Originally created by Roy Osherove, Team Agile
// Blog: www.ISerializable.com
// Roy@TeamAgile.com

namespace HackMew.ToolsFactory
{

    /// <summary>
    /// Replaces the standard BackgroundWorker Component in .NET 2.0 Winforms
    /// To support the ability of aborting the thread the worker is using, 
    /// and supporting the fast proporgation of ProgressChanged events without locking up
    /// </summary>
    /// <remarks></remarks> 
    public class BackgroundWorkerEx : Component
    {
        // Events
        public event DoWorkEventHandler DoWork
        {
            add
            {
                base.Events.AddHandler(BackgroundWorkerEx.doWorkKey, value);
            }
            remove
            {
                base.Events.RemoveHandler(BackgroundWorkerEx.doWorkKey, value);
            }
        }
        public event ProgressChangedEventHandler ProgressChanged
        {
            add
            {
                base.Events.AddHandler(BackgroundWorkerEx.progressChangedKey, value);
            }
            remove
            {
                base.Events.RemoveHandler(BackgroundWorkerEx.progressChangedKey, value);
            }
        }
        public event RunWorkerCompletedEventHandler RunWorkerCompleted
        {
            add
            {
                base.Events.AddHandler(BackgroundWorkerEx.runWorkerCompletedKey, value);
            }
            remove
            {
                base.Events.RemoveHandler(BackgroundWorkerEx.runWorkerCompletedKey, value);
            }
        }

        // Methods
        static BackgroundWorkerEx()
        {
            BackgroundWorkerEx.doWorkKey = new object();
            BackgroundWorkerEx.runWorkerCompletedKey = new object();
            BackgroundWorkerEx.progressChangedKey = new object();
        }

        public BackgroundWorkerEx()
        {
            threadStart = new WorkerThreadStartDelegate(WorkerThreadStart);
            operationCompleted = new SendOrPostCallback(AsyncOperationCompleted);
            progressReporter = new SendOrPostCallback(ProgressReporter);
        }

        private void AsyncOperationCompleted(object arg)
        {
            isRunning = false;
            cancellationPending = false;
            OnRunWorkerCompleted((RunWorkerCompletedEventArgs)arg);
        }

        public void CancelAsync()
        {
            if (!WorkerSupportsCancellation)
                throw new InvalidOperationException("BackgroundWorker_WorkerDoesntSupportCancellation");

            cancellationPending = true;
        }

        protected virtual void OnDoWork(DoWorkEventArgs e)
        {
            mThread = Thread.CurrentThread;
            DoWorkEventHandler workStartDelegate =
                (DoWorkEventHandler)base.Events[BackgroundWorkerEx.doWorkKey];

            if (workStartDelegate != null)
            {
                try
                {
                    workStartDelegate(this, e);
                }
                catch (ThreadAbortException)
                {
                    Thread.ResetAbort();
                }
            }
        }

        private object tempLock = new object();

        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChangedEventHandler progressChangedDelegate =
                (ProgressChangedEventHandler)base.Events[BackgroundWorkerEx.progressChangedKey];

            if (progressChangedDelegate != null)
                progressChangedDelegate(this, e);
        }

        protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompletedEventHandler workderCompletedDelegate =
                (RunWorkerCompletedEventHandler)base.Events[BackgroundWorkerEx.runWorkerCompletedKey];

            if (workderCompletedDelegate != null)
                workderCompletedDelegate(this, e);
        }

        private void ProgressReporter(object arg)
        {
            OnProgressChanged((ProgressChangedEventArgs)arg);
        }

        public void ReportProgress(int percentProgress)
        {
            ReportProgress(percentProgress, null);
        }

        public void ReportProgress(int percentProgress, object userState)
        {
            if (!WorkerReportsProgress)
            {
                throw new InvalidOperationException("BackgroundWorker_WorkerDoesntReportProgress");
            }

            ProgressChangedEventArgs progressArgs = new ProgressChangedEventArgs(percentProgress, userState);
            object lockTarget = new object();

            if (asyncOperation != null)
            {
                asyncOperation.Post(progressReporter, progressArgs);
                // Thread.Sleep(10);
            }
            else
            {
                progressReporter(progressArgs);
            }
        }

        public void RunWorkerAsync()
        {
            RunWorkerAsync(null);
        }

        private Thread mThread;

        public void StopImmediately()
        {
            if (!isRunning || mThread == null)
            {
                return;
            }
            else
            {
                mThread.Abort();
                //there is no need to catch a threadAbortException 
                //since we are catching it and resetting it inside the OnDoWork method
            }

            RunWorkerCompletedEventArgs completedArgs =
                new RunWorkerCompletedEventArgs(null, null, true);

            try
            {

                if (asyncOperation != null)
                {
                    //invoke operation on the correct thread
                    asyncOperation.PostOperationCompleted(operationCompleted, completedArgs);
                }
                else
                {
                    //invoke operation directly
                    operationCompleted(completedArgs);
                }
            }
            catch (InvalidOperationException)
            {
                isRunning = false;
                cancellationPending = false;
            }
        }

        public void RunWorkerAsync(object argument)
        {
            if (isRunning)
                throw new InvalidOperationException("BackgroundWorker_WorkerAlreadyRunning");

            isRunning = true;
            cancellationPending = false;
            asyncOperation = AsyncOperationManager.CreateOperation(null);
            threadStart.BeginInvoke(argument, null, null);
        }

        private void WorkerThreadStart(object userState)
        {
            object result = null;
            Exception workerException = null;
            bool cancel = false;

            try
            {
                DoWorkEventArgs workArgs = new DoWorkEventArgs(userState);
                OnDoWork(workArgs);

                if (workArgs.Cancel)
                {
                    cancel = true;
                }
                else
                {
                    result = workArgs.Result;
                }
            }
            catch (Exception ex)
            {
                workerException = ex;
            }

            try
            {
                RunWorkerCompletedEventArgs completedArgs = new RunWorkerCompletedEventArgs(result, workerException, cancel);

                if (isRunning)
                    asyncOperation.PostOperationCompleted(operationCompleted, completedArgs);
            }
            catch (InvalidOperationException)
            {
                isRunning = false;
                cancellationPending = false;
            }
        }

        // Properties
        public bool CancellationPending
        {
            get { return cancellationPending; }
        }

        public bool IsBusy
        {
            get { return isRunning; }
        }

        public bool WorkerReportsProgress
        {
            get { return workerReportsProgress; }
            set { workerReportsProgress = value; }
        }

        public bool WorkerSupportsCancellation
        {
            get { return canCancelWorker; }
            set { canCancelWorker = value; }
        }

        // Fields
        private AsyncOperation asyncOperation;
        private bool canCancelWorker;
        private bool cancellationPending;
        private static readonly object doWorkKey;
        private bool isRunning;
        private readonly SendOrPostCallback operationCompleted;
        private static readonly object progressChangedKey;
        private readonly SendOrPostCallback progressReporter;
        private static readonly object runWorkerCompletedKey;
        private readonly WorkerThreadStartDelegate threadStart;
        private bool workerReportsProgress;

        // Nested Types
        private delegate void WorkerThreadStartDelegate(object argument);
    }
}


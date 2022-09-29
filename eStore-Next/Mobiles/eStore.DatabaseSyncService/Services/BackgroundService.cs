using System.ComponentModel;

namespace eStore.DatabaseSyncService.Services
{
    public abstract class BackgroundService
    {
        private static BackgroundWorker Worker;

        public BackgroundWorker GetInstance => Worker;

        public void InitService()
        {
            Worker = new BackgroundWorker();
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
            Worker.DoWork += new DoWorkEventHandler(DoWork);
            Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            Worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
        }

        public abstract void DoWork(object sender, DoWorkEventArgs e);

        //public abstract Object Jobt(Object obj, BackgroundWorker worker, DoWorkEventArgs e);

        // This event handler deals with the results of the
        // background operation.
        public abstract void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);

        public abstract void ProgressChanged(object sender, ProgressChangedEventArgs e);

        //{
        //    resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
        //}
    }
}
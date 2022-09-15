using AKS.Shared.Commons.Ops;
using CommunityToolkit.Maui.Alerts;
using eStore_MauiLib.RemoteService;
using Microsoft.Maui.Platform;
using System.ComponentModel;

namespace eStore_MauiLib.Services.BackgroundServices
{
    public class SyncDownService : BackgroundService
    {
        public override void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            //e.Result = Job((int)e.Argument, worker, e);
            e.Result = Job((LocalSync)e.Argument, worker, e);
            
        }

        public override void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // MessageBox.Show(e.Error.Message);
                //handle error
                Toast.Make($"Error: {e.Error.Message}", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            }
            else if (e.Cancelled)
            {
                //resultLabel.Text = "Canceled";
                //handle when user canceled job
                Toast.Make("Sync Local database is canceled!", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            }
            else
            {
                // Finally, handle the case where the operation
                // succeeded.
                //resultLabel.Text = e.Result.ToString();
                if ((bool)e.Result == true)
                {
                    CurrentSession.LocalStatus = true;
                    Toast.Make("Local database is sync with remote current data", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                }
                else
                {
                    Toast.Make("It failed to sync Local database with remote latest data", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                }
            }
            Toast.Make($" ObjectSender:{sender.ToString()}", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();

            //TODO: basic cleanup or reset main thread ui
        }

        public  bool Job(LocalSync sync,  BackgroundWorker worker, DoWorkEventArgs e)
        {
            bool result=false;
            switch (sync)
            {
                case LocalSync.Initial:
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        result = false;
                        break;
                    }
                    else
                    {
                        worker.ReportProgress(10);
                        result = DatabaseStatus.SyncInitial();
                        worker.ReportProgress(90);
                        if (result) CurrentSession.LocalStatus = true;
                        worker.ReportProgress(100);
                    }
                    break;
                case LocalSync.Accounting:
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        result = false;
                        break;
                    }
                    else
                    {
                        worker.ReportProgress(20);
                        result = DatabaseStatus.SyncAccounting();
                        worker.ReportProgress(100);

                    }
                    break;
                case LocalSync.Inventory:
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        result = false;
                        break;
                    }
                    else
                    {
                        worker.ReportProgress(20);
                        result = DatabaseStatus.SyncInventory();
                        worker.ReportProgress(100);
                    }
                    break;
                case LocalSync.InitialAccounting:
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        result = false;
                        break;
                    }
                    else
                    {
                        worker.ReportProgress(10);
                        result = DatabaseStatus.SyncInitial();
                     
                        if (result)
                        {
                            worker.ReportProgress(40);
                            CurrentSession.LocalStatus = true;
                            worker.ReportProgress(50);
                            if (worker.CancellationPending == true)
                            {
                                e.Cancel = true;
                                result = false;
                                break;
                            }
                            else
                            {
                                worker.ReportProgress(60);
                                result = DatabaseStatus.SyncAccounting();
                                worker.ReportProgress(100);
                            }
                        }
                    }
                    break;
                case LocalSync.InitialInventory:
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        result = false;
                        break;
                    }
                    else
                    {
                        worker.ReportProgress(10);
                        result = DatabaseStatus.SyncInitial();
                        if (result)
                        {
                            worker.ReportProgress(25);
                            CurrentSession.LocalStatus = true;
                            worker.ReportProgress(50);
                            if (worker.CancellationPending == true)
                            {
                                e.Cancel = true;
                                result = false;
                                break;
                            }
                            else
                            {
                                worker.ReportProgress(60);
                                result = DatabaseStatus.SyncInventory();
                                worker.ReportProgress(100);
                            }
                        }
                    }
                    break;
                case LocalSync.All:
                    worker.ReportProgress(1);
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        result = false;
                        break;
                    }
                    else
                    {
                        worker.ReportProgress(10);
                        result = DatabaseStatus.SyncInitial();
                        if (result)
                        {
                            worker.ReportProgress(20);
                            CurrentSession.LocalStatus = true;
                            worker.ReportProgress(30);
                            if (worker.CancellationPending == true)
                            {
                                e.Cancel = true;
                                result = false;
                                break;
                            }
                            else
                            {
                                worker.ReportProgress(40);
                                result = DatabaseStatus.SyncAccounting();
                                worker.ReportProgress(60);
                            }
                            if (worker.CancellationPending == true)
                            {
                                e.Cancel = true;
                                result = false;
                                break;
                            }
                            else
                            {
                                worker.ReportProgress(80);
                                result = DatabaseStatus.SyncInventory();
                                worker.ReportProgress(100);
                            }
                        }
                    }
                    break;
                default:
                    worker.ReportProgress(10);
                    result = DatabaseStatus.SyncInitial();
                    worker.ReportProgress(70);
                    if (result) CurrentSession.LocalStatus = true;
                    worker.ReportProgress(100);
                    break;
            }
            return result;
        }

        public override void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Toast.Make($"Progress:{e.ProgressPercentage}% Completed,Sender:{sender.ToString()} ").Show();
        }
    }
}

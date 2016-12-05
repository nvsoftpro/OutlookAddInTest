using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using SecuredMail.DataGen;
using SecuredMail.DisposeImp;
using SecuredMail.Logger;

namespace SecuredMail
{
    /// <summary>
    /// Outlook.MailItem wrapper
    /// </summary>
    public class OutlookMailItem : DisposableObject
    {
        private ILogger logger;
        private static readonly object lockObject = new object();
        private readonly MailItem _mailItem;
        public OutlookMailItem(MailItem mailItem)
        {
            Debug.WriteLine($"Outlookmail ctor {mailItem.GetHashCode()}");
            logger.Message("Outlookmail ctor:",mailItem.GetHashCode());
            _mailItem = mailItem;
        }

        /// <summary>
        /// Changing an Email Subject and an Email Body with Windows data 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ChangeEmailSubjectAndBodyWithWindowsDataAsync()
        {
            Debug.WriteLine("Change mailItem. EntryId:" + _mailItem.EntryID);
            logger.Message("Change mailItem. EntryId:", _mailItem.EntryID);
            _mailItem.Subject = "Controls text and value you can see below";

            List<string> content = await GetWindowContentAsync();

            List<Task> tasks = new List<Task>();
            ChangeEmailBody($"START:{DateTime.Now}");
            logger.Message($"START:{DateTime.Now}");
            foreach (var controlData in content)
            {
                var temp = controlData; //avoid closure variable effect
                Task task = Task.Run(() => { ChangeEmailBody(temp); });
                tasks.Add(task);
                try
                {
                    task.Wait(2000);
                }
                catch (AggregateException ae)
                {
                    var flatten = ae.Flatten();
                    Debug.WriteLine("Task throwing exception " + flatten.Message); // better way to put this into window journal events
                    logger.Message("Task throwing exception ", flatten.Message);
                    throw;
                }
                await Task.Delay(500);
            }


            Task t = Task.WhenAll(tasks);
            try
            {
                t.Wait();
                ChangeEmailBody($"DONE:{DateTime.Now}");
                logger.Message("DONE:", DateTime.Now);
            }
            catch { }

            if (t.Status == TaskStatus.RanToCompletion)
                logger.Message("Tasks were completed");
            else if (t.Status == TaskStatus.Faulted)
                logger.Message("Tasks were failed");

            return true;
        }

        /// <summary>
        /// Add the Eratosthenes sequence to an Email body 
        /// </summary>
        /// <param name="maxNumber">
        /// Ceiling number for calculating EratosthenesSieveNumbers. 
        /// It should be more then 3
        /// </param>
        /// <returns></returns>
        public async Task<bool> ChangeEmailBodyWithEratosthenesSieveNumbers(int maxNumber, IProgress<int> progress )
        {
            if (maxNumber < 3)
            {
                return false;
            }

            IEnumerable<int> primes = await GetEratosthenesNumberAsync(maxNumber);
            if (primes == null)
            {
                logger.Message("Sequence is not produced");
                return false;
            }

            logger.Message($"START COUNTING:", DateTime.Now);
            int primesAll = primes.Count();
            Barrier barrier = new Barrier(primesAll);
            foreach (var prime in primes)
            {
                var temp = prime; //avoid closure effect
                var task = Task.Run(() =>
                {
                    ChangeEmailBody(temp);
                  
                    barrier.SignalAndWait();
                });
                try
                {
                    task.Wait(1000);
                }
                catch (AggregateException ae)
                {
                    Debug.WriteLine("Task throw exception " + ae.Message); // better way to put this into window journal events or file
                    logger.Message("Task throw exception ", ae.Message);
                    throw;
                }

                await Task.Delay(1000); // a slow pace and display numbers

                progress?.Report(prime);
            }

            progress?.Report(maxNumber);

            logger.Message($"END COUNTING:", DateTime.Now);

            return true;
        }

        /// <summary>
        /// Clear an Email body
        /// </summary>
        public void ClearEmailBody()
        {
            logger.Message("Clearing the Email body");
            lock (lockObject)
            {
                _mailItem.Body = "";
            }
        }

        /// <summary>
        /// Changing an Email body by object value
        /// </summary>
        /// <param name="value">It should be the object has well realization ToString function</param>
        public void ChangeEmailBody(object value)
        {
            logger.Message("Value:", value);
            lock (lockObject)
            {
                _mailItem.Body += value;
            }
        }

        /// <summary>
        /// Asynchronous function to get the Windows content of Outlook app
        /// </summary>
        /// <returns></returns>
        private Task<List<string>> GetWindowContentAsync()
        {
            var content = new WindowsContent();
            return Task.Run(() => content.GetWindowsText());
        }

        /// <summary>
        /// Asynchronous function to get the Eratosthenes sequence
        /// </summary>
        /// <param name="maxNumber">
        /// Ceiling number for calculating EratosthenesSieveNumbers. 
        /// It should be more then 3
        /// </param>
        /// <returns></returns>
        private Task<IEnumerable<int>> GetEratosthenesNumberAsync(int maxNumber)
        {
            return Task.Run(() => Calculation.RunEratosthenesSieve(maxNumber));
        }
    }
}

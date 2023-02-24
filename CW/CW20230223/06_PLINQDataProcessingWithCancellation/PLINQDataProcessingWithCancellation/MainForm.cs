using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLINQDataProcessingWithCancellation
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource cancelToken;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            // Start a new "task" to process the files. 
            Task.Factory.StartNew(() =>
            {
                ProcessIntData();
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (cancelToken != null)
                cancelToken.Cancel();
        }

        private void ProcessIntData()
        {
            // Get a very large array of integers. 
            int[] source = Enumerable.Range(1, 10000000).ToArray();

            // Find the numbers where num % 3 == 0 is true, returned
            // in descending order. 
            int[] modThreeIsZero = null;
            cancelToken = new CancellationTokenSource();

            try
            {
                this.Invoke((Action)(() =>
                {
                    this.Text = "Fun with PLINQ";
                }));

                var watch = new Stopwatch();

                watch.Start();

                //Var1 - Обычный LINQ - запрос
                // Сиснтаксис запросов
                modThreeIsZero = (from num in source
                                  where num % 3 == 0
                                  orderby num ascending
                                  select num).ToArray();

                //// Сиснтаксис методов
                //modThreeIsZero = source.Where(num => num % 3 == 0)
                //                  .OrderBy(num => num)
                //                  .Select(num => num).ToArray();
                ////Var1 - Обычный LINQ - запрос



                ////Var2 - PLINQ - запрос
                ////Сиснтаксис запросов
                //modThreeIsZero = (from num in source
                //                  .AsParallel()
                //                  .AsOrdered()
                //                  .WithCancellation(cancelToken.Token)
                //                  where num % 3 == 0
                //                  select num)
                //                  .ToArray();

                //// Сиснтаксис методов
                //modThreeIsZero = source.AsParallel()
                //                       .AsOrdered()
                //                       .WithCancellation(cancelToken.Token)
                //                       //.WithDegreeOfParallelism(Environment.ProcessorCount)
                //                       .Where(num => num % 3 == 0)
                //                       .Select(num => num)
                //                       .ToArray();
                ////Var2 - PLINQ - запрос

                watch.Stop();

                MessageBox.Show(string.Format($"Затраченное время {watch.ElapsedMilliseconds} мс.\nFound {modThreeIsZero.Count()} numbers that match query!"));
            }
            catch (OperationCanceledException ex)
            {
                this.Invoke((Action)delegate
                {
                    this.Text = ex.Message;
                });
            }
            finally
            {
                cancelToken.Dispose();
                cancelToken = null;
            }
        }
    }
}

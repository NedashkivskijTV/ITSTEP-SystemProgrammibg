using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataParallelismWithForEach
{
    public partial class MainForm : Form
    {
        // New Form level variable.
        private CancellationTokenSource cancelToken;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnProcessImages_Click(object sender, EventArgs e)
        {
            // Start a new "task" to process the files. 
            Task.Factory.StartNew(() =>
            {
                //Var1 - обычный способ 
                ProcessFilesNoParallel();

                ////Var2 - параллельный способ 
                //ProcessFilesParallel();
            });
        }


        private void ProcessFilesNoParallel()
        {
            txtInputArea.Text = "";
            // Load up all *.jpg files, and make a new folder for the modified data.
            string[] files = Directory.GetFiles(@"C:\Temp\Pic", "*.jpg",
                SearchOption.AllDirectories);
            string newDir = @"C:\Temp\ModifiedPic";
            Directory.CreateDirectory(newDir);

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                //  Process the image data in a no parallel manner! 
                foreach (var currentFile in files)
                {
                    string filename = Path.GetFileName(currentFile);
                    using (Bitmap bitmap = new Bitmap(currentFile))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir, filename));

                        //this.Text = string.Format("Processing {0} on thread {1}", filename,
                        //  Thread.CurrentThread.ManagedThreadId);

                        // We need to ensure that the secondary threads access controls
                        // created on primary thread in a safe manner.
                        this.Invoke((Action)delegate
                        {
                            txtInputArea.Text += string.Format($"Обработка файла  {filename} в потоке {Thread.CurrentThread.ManagedThreadId}" + Environment.NewLine);
                        });
                    }
                };

                watch.Stop();
                this.Invoke((Action)delegate
                {
                    txtInputArea.Text += string.Format($"Затраченное время {watch.ElapsedMilliseconds} мс.");
                });
            }
            catch (OperationCanceledException ex)
            {
                this.Invoke((Action)delegate
                {
                    Text = ex.Message;
                });
            }
        }

        private void ProcessFilesParallel()
        {
            // Use ParallelOptions instance to store the CancellationToken
            cancelToken = new CancellationTokenSource();

            ParallelOptions parOpts = new ParallelOptions();
            parOpts.CancellationToken = cancelToken.Token; // Возможность отмены операции
            parOpts.MaxDegreeOfParallelism = Environment.ProcessorCount; //Число процессоров на текущем компьютере

            this.Invoke((Action)(() =>
            {
                Text = "";
            })); 
            
            // Load up all *.jpg files, and make a new folder for the modified data.
            string[] files = Directory.GetFiles(@"C:\Temp\Pic", "*.jpg",
                SearchOption.AllDirectories);
            string newDir = @"C:\Temp\ModifiedPic";
            Directory.CreateDirectory(newDir);

            try
            {
                this.Invoke((Action)(() =>
                {
                    Text = "Fun with the TPL";
                }));

                var watch = new Stopwatch();

                watch.Start();

                //  Process the image data in a parallel manner! 
                Parallel.ForEach(files, parOpts, currentFile =>
                {
                    parOpts.CancellationToken.ThrowIfCancellationRequested();

                    string filename = Path.GetFileName(currentFile);
                    using (Bitmap bitmap = new Bitmap(currentFile))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir, filename));

                        // We need to ensure that the secondary threads access controls
                        // created on primary thread in a safe manner.
                        this.Invoke((Action)(() =>
                        {
                            txtInputArea.Text += string.Format($"Обработка файла {filename}  в потоке {Thread.CurrentThread.ManagedThreadId}" + Environment.NewLine);
                        }));

                        //// Раскомментировать для проверки отмены операции
                        //Thread.Sleep(300);
                    }
                }
                );

                watch.Stop();

                this.Invoke((Action)(() =>
                {
                    txtInputArea.Text += string.Format($"Процессоров {Environment.ProcessorCount} Затраченное время {watch.ElapsedMilliseconds} мс.");
                }));
            }
            catch (OperationCanceledException ex)
            {
                this.Invoke((Action)(() => 
                {
                    this.Text = ex.Message;
                }));
            }
            finally
            {
                cancelToken.Dispose();
                cancelToken = null;
            }
        }

        private void btnCancelTask_Click(object sender, EventArgs e)
        {
            // This will be used to tell all the worker threads to stop!
            if (cancelToken != null)
                cancelToken.Cancel();
        }
    }
}

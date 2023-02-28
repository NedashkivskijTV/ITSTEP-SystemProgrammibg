using System.Diagnostics;

namespace OptimizedApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Counter.count = 0; // обнулення лічильника

            var watch = new Stopwatch();
            watch.Start();

            //---------------------------------------------------------------
            Thread[] threads = new Thread[10];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(() =>
                {
                    for (int j = 1; j <= 1000000; ++j)
                        Interlocked.Increment(ref Counter.count);
                });
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();
            //---------------------------------------------------------------

            watch.Stop();

            ShowInfoInTextBox("The result of the work of nonoptimized code", watch.ElapsedMilliseconds);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Counter.count = 0; // обнулення лічильника

            var watch = new Stopwatch();
            watch.Start();

            //---------------------------------------------------------------
            Parallel.Invoke(() => // конструкція дає змогу зменшити час виконання коду з ~208 мс до ~77 мс  (виграш становить близько 63%)
            {
                RunMyMethod(10);
            });
            //---------------------------------------------------------------

            watch.Stop();

            ShowInfoInTextBox("Result after code optimization", watch.ElapsedMilliseconds);
        }

        private void RunMyMethod(int n)
        {
            for (int i = 0; i < n; ++i)
            {
                MyMethod();
            }
        }

        private void MyMethod()
        {
            for (int j = 1; j <= 1000000; ++j)
                Interlocked.Increment(ref Counter.count);
        }

        private void ShowInfoInTextBox(string text, long timeInMc)
        {
            textBox1.Text = text + Environment.NewLine;
            textBox1.Text += $"Counter.count = {Counter.count}" + Environment.NewLine;
            textBox1.Text += $"time = {timeInMc} ms";
        }
    }

    class Counter
    {
        public static int count;
    }
}
﻿using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyEBookReader
{
    public partial class MainForm : Form
    {
        private string theEBook = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            // The Project Gutenberg EBook of A Tale of Two Cities, by Charles Dickens
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += (s, eArgs) => 
            { 
                theEBook = eArgs.Result;
                txtBook.Text = theEBook;
            };
            wc.DownloadStringAsync(new Uri("https://www.gutenberg.org/cache/epub/4238/pg4238.txt"));            
            //wc.DownloadStringAsync(new Uri("http://www.gutenberg.org/files/2009/2009.txt"));
        }

        #region Do work in parallel!
        private void btnGetStats_Click(object sender, EventArgs e)
        {
            // Get the words from the e-book.
            string[] words = theEBook.Split(new char[] { ' ', '\u000A', ',', '.', ';', ':', '-', '?', '/' },
                StringSplitOptions.RemoveEmptyEntries);
            string[] tenMostCommon = null;
            string longestWord = string.Empty;

            var watch = new Stopwatch();
            watch.Start();

            ////Var1 - обычный способ
            //// Now, find the ten most common words.
            //tenMostCommon = FindTenMostCommon(words);
            ////Get the longest word. 
            //longestWord = FindLongestWord(words);
            ////Var1 - обычный способ

            //Var2 - Параллельное выполнение
            Parallel.Invoke(
                () =>
                {
                    // Now, find the ten most common words.
                    tenMostCommon = FindTenMostCommon(words);
                },
                () =>
                {
                    // Get the longest word. 
                    longestWord = FindLongestWord(words);
                }
                );
            //Var2 - Параллельное выполнение

            watch.Stop();

            // Now that all tasks are complete, build a string to show all
            // stats in a message box.
            StringBuilder bookStats = new StringBuilder($"Затраченное время {watch.ElapsedMilliseconds } мс.\n\n");
            bookStats.AppendLine("Десять наиболее употребляемых слов: ");

            foreach (string s in tenMostCommon)
            {
                bookStats.AppendLine(s);
            }
            bookStats.AppendFormat($"\nСамое длинное слово: {longestWord}");
            bookStats.AppendLine();
            MessageBox.Show(bookStats.ToString(), "Информация о книге");
        }
        #endregion

        #region Task methods.
        private string[] FindTenMostCommon(string[] words)
        {
            var frequencyOrder = from word in words
                                 where word.Length > 6
                                 group word by word into g
                                 orderby g.Count() descending
                                 select g.Key;

            string[] commonWords = (frequencyOrder.Take(10)).ToArray();
            return commonWords;
        }

        private string FindLongestWord(string[] words)
        {
            return (from w in words orderby w.Length descending select w).FirstOrDefault();
        }
        #endregion
    }
}

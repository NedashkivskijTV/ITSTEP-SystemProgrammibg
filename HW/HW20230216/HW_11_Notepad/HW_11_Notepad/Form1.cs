using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace HW_11_Notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabelTime.Text = DateTime.Now.ToLongTimeString();
            printDocument1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            printDocument1.DefaultPageSettings = new System.Drawing.Printing.PageSettings();

            StatusInf();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) // зберігає файл
            {
                string fileName = saveFileDialog1.FileName;
                string str = JsonConvert.SerializeObject(tabControl1.SelectedTab.Controls.OfType<TextBox>().First().Text);
                File.WriteAllText(fileName, str);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) // відкриває файл
            {
                string fileName = openFileDialog1.FileName;
                if (File.Exists(fileName))
                {
                    string str = File.ReadAllText(fileName);

                    TabPage tabPage = new TabPage
                    {
                        Text = "Новий файл " + ++countFile
                    };
                    string text = JsonConvert.DeserializeObject<string>(str);
                    TextBox textBox = new TextBox
                    { // підглянуто у конструктора - створення TabPage2
                        Text = text,
                        //Text = temp.Text,
                        Dock = System.Windows.Forms.DockStyle.Fill,
                        Location = new System.Drawing.Point(3, 3),
                        Multiline = true,
                        ScrollBars = ScrollBars.Both,
                        AcceptsReturn = true,
                        AcceptsTab = true
                    };
                    tabPage.Controls.Add(textBox);
                    tabControl1.TabPages.Add(tabPage);
                    tabControl1.SelectedTab = tabPage; // призначення активним щойно створеного вікна
                }
            }
        }

        int countFile = 1;
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = new TabPage
            {
                Text = "Новий файл " + ++countFile
            };
            TextBox textBox = new TextBox
            { // підглянуто у конструктора - створення TabPage2
                Dock = System.Windows.Forms.DockStyle.Fill,
                Location = new System.Drawing.Point(3, 3),
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                AcceptsReturn = true,
                AcceptsTab = true
            };
            tabPage.Controls.Add(textBox);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage; // призначення активним щойно створеного вікна
        }

        private void вирізатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Controls[0] is TextBox tb)
            {
                tb.Cut();
            }
        }

        private void копіюватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Controls[0] is TextBox tb)
            {
                tb.Copy();
            }
        }

        private void вставитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Controls[0] is TextBox tb)
            {
                tb.Paste();
            }
        }

        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Controls[0] is TextBox tb)
            {
                int start = tb.SelectionStart;
                int length = tb.SelectionLength;
                int textLenght = tb.Text.Length;
                Text = $"start = {start}, length = {length}, textLenght = {textLenght}";
                if (length == 0 && start < tb.Text.Length)
                {
                    tb.Text = tb.Text.Remove(tb.SelectionStart, 1);
                    tb.SelectionStart = start;
                }
                else if (length > 0)
                {
                    tb.Text = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength);
                    tb.SelectionStart = start;
                }
            }
        }

        private void toolStripMenuItemAa_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                tabControl1.SelectedTab.Controls.OfType<TextBox>().First().ForeColor = colorDialog1.Color; // функціонал аналогічний верхньому із застосуванням LINQ
            }
        }

        private void toolStripMenuItemBack_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                tabControl1.SelectedTab.Controls.OfType<TextBox>().First().Font = fontDialog1.Font; // функціонал аналогічний верхньому із застосуванням LINQ
                tabControl1.SelectedTab.Controls.OfType<TextBox>().First().ForeColor = fontDialog1.Color;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelTime.Text = DateTime.Now.ToLongTimeString();
            StatusInf();
        }

        private void closetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.Controls.Count > 1)
            {
                int ind = tabControl1.SelectedIndex;
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);// закриває активну вкладку
                if (ind > 0)
                    tabControl1.SelectedIndex = ind - 1; // вибір активної вкладки - попередня відносно видаленої
            }
        }

        TextBox textBox;
        string textToPrint;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (textBox != null)
            {
                e.Graphics.MeasureString(textToPrint, // текст
                    textBox.Font,                      // налаштування тексту - шрифт...
                    e.MarginBounds.Size,                // розміри сторінки
                    StringFormat.GenericTypographic,     // формат - типографічний формат
                    out int charOnPage,                  // кількість символів на сторінку
                    out int linesOnPage                  // кількість рядків на сторінці
                    ); // міряє скільки потрібно рядків, щоб вписати текст

                SolidBrush textToPrintBrush = new SolidBrush(textBox.ForeColor); // витягує колір з налаштувань тексту що друкується та створює обєкт БРАШ для передачі в принтер

                e.Graphics.DrawString(textToPrint,
                    textBox.Font,
                    textToPrintBrush,
                    e.MarginBounds,
                    StringFormat.GenericTypographic); // роздрук

                textToPrint = textToPrint.Substring(charOnPage); // відрізає від тексту, що потрібно надрукувати кусок, що вже надруковано
                e.HasMorePages = (textToPrint.Length > 0); // показує чи є ще сторінки для друку

                if (!e.HasMorePages)
                {
                    textToPrint = textBox.Text;
                }
            }

        }
        private void роздрукуватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Text = "Роздрукувати";
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox = tabControl1.SelectedTab.Controls.OfType<TextBox>().First(); // збереження посилання на текстбокс - потім воно стане джерелом усієї потрібної для друку інф - потребує застосування конструкції перевірки на null
                textToPrint = textBox.Text;
                printDocument1.Print();
            }
        }
        private void налаштуанняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Text = "Налаштування принтера";
            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings = pageSetupDialog1.PrinterSettings;
                printDocument1.DefaultPageSettings = pageSetupDialog1.PageSettings;
            }

        }
        private void попередньоПереглянутиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Text = "Попередньо переглянути";
            textBox = tabControl1.SelectedTab.Controls.OfType<TextBox>().First(); // збереження посилання на текстбокс - потім воно стане джерелом усієї потрібної для друку інф - потребує застосування конструкції перевірки на null
            textToPrint = textBox.Text;
            printPreviewDialog1.ShowDialog(); // роздрук документа з попереднім ппереглядом
        }

        private void StatusInf()
        {
            if (tabControl1.SelectedTab.Controls[0] is TextBox tb)
            {
                // Поз.курсора: в рядку _ в документі _
                int cursorPosition = tb.SelectionStart;
                toolStripStatusLabelPosKursor.Text = $"Поз.: в рядку {cursorPosition - tb.GetFirstCharIndexFromLine(tb.GetLineFromCharIndex(tb.SelectionStart))} в док. {cursorPosition}";

                var pos = tb.GetLineFromCharIndex(tb.SelectionStart);
                var pos2 = tb.GetLineFromCharIndex(tb.Text.Length);
                toolStripStatusLabelLines.Text = $"Рядок-{pos + 1} Рядків-{pos2 + 1}";
            }
        }

        private void Стат_Click(object sender, EventArgs e)
        {
            using (FormStatText formST = new FormStatText()) // використання блоку using для звільнення ресурсів (економії) - using відправить форму на знищення після завершення роботи з нею
            {

                string text_stat = "";
                if (tabControl1.SelectedTab.Controls[0] is TextBox tb && tb.Text.Length > 0)
                {
                    text_stat = "\nКількість символів в тексті - " + tb.Text.Length;
                    formST.StatInf = text_stat; // передача тексту до додаткової форми через властивість 

                    string[] word_count = tb.Text.Split(",!?;\\t\\n: .".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    text_stat = "\nКількість слів в тексті - " + word_count.Length;
                    formST.StatInf = text_stat; // передача тексту до додаткової форми через властивість 

                    formST.StatInf = "Слова, що повторюються більше 2 раз:";

                    int count = 0;
                    List<string> list_word = new List<string>();
                    for (int i = 0; i < word_count.Length; i++)
                    {
                        count = 0;
                        for (int j = 0; j < word_count.Length; j++)
                        {
                            if (word_count[i].Equals(word_count[j], StringComparison.CurrentCultureIgnoreCase))
                                ++count;
                        }
                        if (count > 2 && !list_word.Contains(word_count[i]))
                        {
                            list_word.Add(word_count[i]);
                        }
                    }

                    for (int k = word_count.Length - 1; k >= 2; --k)
                    {
                        foreach (string item in list_word)
                        {
                            count = 0;
                            for (int j = 0; j < word_count.Length; j++)
                            {
                                if (item.Equals(word_count[j], StringComparison.CurrentCultureIgnoreCase))
                                    ++count;
                            }
                            if (count == k)
                                formST.StatInf = $"{item} - {count}";
                        }
                    }
                }
                formST.ShowDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

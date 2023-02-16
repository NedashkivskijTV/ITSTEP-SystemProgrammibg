using System;
using System.Drawing;
using System.Windows.Forms;

namespace TextDrawer
{
    public partial class TextDrawerForm : Form
    {
        //Отображаемый текст
        string SourceText = "No text was added!";
        //Шрифт отображаемого текста
        Font DrawingFont;

        public TextDrawerForm()
        {
            InitializeComponent();

            DrawingFont = new Font("Arial", 25);
            panel1.Paint += Panel1_Paint;
            this.Paint += Form1_Paint;
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            Panel1_Paint(panel1, new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle));
        }

        public void SetText(string text)
        {
            /*сохраняем новое значение переменной SourceText*/
            SourceText = text;

            /*инициируем перерисовку окна*/
            Panel1_Paint(panel1, new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle));
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (SourceText.Length > 0)
            {
                /*создаём буферное изображение, основываясь на размерах 
                 * клиентской части элемента управления Panel*/
                Image img = new Bitmap(panel1.ClientRectangle.Width, panel1.ClientRectangle.Height);

                /*получаем графический контектс созданого нами изображения*/
                Graphics imgDC = Graphics.FromImage(img);

                /*очищаем изображение используя цвет фона окна*/
                imgDC.Clear(BackColor);

                /*прорисовываем на элементе кправления Panel текст используя выбранный шрифт*/
                imgDC.DrawString(SourceText, DrawingFont, Brushes.Brown, ClientRectangle, new StringFormat(StringFormatFlags.NoFontFallback));

                /*прорисовываем изображение на элементе управления Panel*/
                e.Graphics.DrawImage(img, 0, 0);
            }
        }

        private void selectColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*создаём объект стандартного диалога FontDialog*/
            FontDialog dlg = new FontDialog();

            /*инициализируем объект шрифта для диалога*/
            dlg.Font = DrawingFont;

            /*открываем диалог модально*/
            if (dlg.ShowDialog() == DialogResult.OK)
                /*если была нажата кнопка OK, сохраняем выбранные настройки*/
                DrawingFont = dlg.Font;

            /*инициируем перерисовку элемента управления Panel*/
            Panel1_Paint(panel1, new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle));
        }

        public new void Move(Point newLocation, int width)
        {
            /*устанавливаем новое значение позиции окна*/
            this.Location = newLocation;

            /*устанавливаем новое значение ширины окна*/
            this.Width = width;
        }
    }
}

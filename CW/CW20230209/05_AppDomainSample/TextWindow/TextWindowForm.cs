using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
namespace TextWindow
{
    public partial class TextWindowForm : Form
    {
        /*ссылка на модуль, из которого быдем вызывать методы*/
        Module DrawerModule { get; set; }
        /*объект форма от которого будем вызывать методы*/
        object TextDrawer;
        
        public TextWindowForm(Module drawer, object targetWindow)
        {
            DrawerModule = drawer;
            TextDrawer = targetWindow;

            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*вызываем метд SetText главного окна приложения TextDrawer*/
            DrawerModule.GetType("TextDrawer.TextDrawerForm").GetMethod("SetText").Invoke(TextDrawer, new object[]{textBox1.Text});
            
            /*вызываем метд Move главного окна приложения TextDrawer*/
            DrawerModule.GetType("TextDrawer.TextDrawerForm").GetMethod("Move").Invoke(TextDrawer, new object[] { new Point(this.Location.X, this.Location.Y + this.Height), this.Width });
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            /*вызываем метд Move главного окна приложения TextDrawer*/
            DrawerModule.GetType("TextDrawer.TextDrawerForm").GetMethod("Move").Invoke(TextDrawer, new object[] { new Point(this.Location.X, this.Location.Y + this.Height), this.Width });
        }
    }
}

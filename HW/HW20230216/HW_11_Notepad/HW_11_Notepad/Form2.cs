using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_11_Notepad
{
    public partial class FormStatText : Form
    {
        //public SubjectMark subject // властивість - забезпечує доступ до текстових полів форми під час додавання/редагування оцінок
        //{
        //    get
        //    {
        //        int m;
        //        if (!int.TryParse(textBoxMark.Text, out m))
        //            m = 0;
        //        return new SubjectMark { SubjectName = textBoxSubject.Text, Mark = m };
        //    }
        //    set
        //    {
        //        if (value is SubjectMark sm)
        //        {
        //            textBoxSubject.Text = sm.SubjectName;
        //            textBoxMark.Text = "" + sm.Mark;
        //        }
        //    }
        //}

        public string StatInf 
        {
            //get; 
            set
            {
                //textBoxStatText.Text = value;
                listBox1.Items.Add(value);
            }
        }

        public FormStatText()
        {
            InitializeComponent();
        }
    }
}

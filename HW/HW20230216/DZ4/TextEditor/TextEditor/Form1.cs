using System.Text;

namespace TextEditor
{
    public partial class Form1 : Form
    {

        private string fileName = "";

        public Form1()
        {
            InitializeComponent();

            BtnSaveState(false); // ����������� ������ ���������� ������
        }

        async private void btnOpenFile_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName; // ���������� ������� ���� �����
                //Text = fileName;
                Text = openFileDialog1.SafeFileName; // ��������� ���� ����� � ��������� �����

                if (File.Exists(fileName)) // �������� �������� ������� �� �����
                {
                    string str = await File.ReadAllTextAsync(fileName); // ������������ �����
                    richTextBox1.Text = str;
                    //richTextBox1.LoadFile(fileName); // ������������ rtf �����

                    BtnSaveState(false); // ����������� ������ ���������� ������
                }
                else
                {
                    ShowFileNotFoundMessage(); // ��������� ����������� ��� ��������� �����
                }
            }
        }

        async private void btnSaveText_Click(object sender, EventArgs e)
        {

            if (fileName.Length == 0) // �������� �������� ����������� ���� ����� (�� ��� ������������)
            {
                MessageBox.Show("You must select the file", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (File.Exists(fileName)) // �������� �������� ������� �� �����
            {
                if (DialogResult.Yes == MessageBox.Show("Do you want to save the text ?",
                    "Attention",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1)) // ��������� ���� ������������ ���������� ������ � ����
                {
                    string text = richTextBox1.Text;
                    await File.WriteAllTextAsync(fileName, text); // ���������� �����
                    
                    BtnSaveState(false); // ����������� ������ ���������� ������
                }
            }
            else
            {
                ShowFileNotFoundMessage(); // ��������� ����������� ��� ��������� �����
            }
        }

        private void ShowFileNotFoundMessage()
        {
            MessageBox.Show("File not found", "Attention", MessageBoxButtons.OK); // ��������� ����������� ��� ��������� �����
        }

        private void RichTextBoxTextChanged(object sender, EventArgs e)
        {
            if (fileName.Length > 0)
            {
                BtnSaveState(true); // ������ֲ� ������ ���������� ������ (���� �������� ���)
            }
        }

        private void BtnSaveState(bool isEnabled) // ���������/���������� ������ ���������� ���� �������� ��� � �����
        {
            btnSaveText.Enabled = isEnabled; 
            saveToolStripMenuItem.Enabled = isEnabled;
        }

    }
}

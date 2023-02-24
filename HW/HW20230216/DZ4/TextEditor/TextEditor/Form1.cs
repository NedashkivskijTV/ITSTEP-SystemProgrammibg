using System.Text;

namespace TextEditor
{
    public partial class Form1 : Form
    {

        private string fileName = "";

        public Form1()
        {
            InitializeComponent();

            BtnSaveState(false); // деактивація кнопок збереження тексту
        }

        async private void btnOpenFile_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName; // збереження повного імені файлу
                //Text = fileName;
                Text = openFileDialog1.SafeFileName; // виведення імені файлу у заголовку форми

                if (File.Exists(fileName)) // перевірка наявності доступу до файлу
                {
                    string str = await File.ReadAllTextAsync(fileName); // завантаження даних
                    richTextBox1.Text = str;
                    //richTextBox1.LoadFile(fileName); // завантаження rtf файлу

                    BtnSaveState(false); // деактивація кнопок збереження тексту
                }
                else
                {
                    ShowFileNotFoundMessage(); // виведення повідомлення про відсутність файлу
                }
            }
        }

        async private void btnSaveText_Click(object sender, EventArgs e)
        {

            if (fileName.Length == 0) // перевірка наявності збереженого імені файлу (не був завантажений)
            {
                MessageBox.Show("You must select the file", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (File.Exists(fileName)) // перевірка наявності доступу до файлу
            {
                if (DialogResult.Yes == MessageBox.Show("Do you want to save the text ?",
                    "Attention",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1)) // виведення вікна підтвердження збереження тексту у файл
                {
                    string text = richTextBox1.Text;
                    await File.WriteAllTextAsync(fileName, text); // збереження даних
                    
                    BtnSaveState(false); // деактивація кнопок збереження тексту
                }
            }
            else
            {
                ShowFileNotFoundMessage(); // виведення повідомлення про відсутність файлу
            }
        }

        private void ShowFileNotFoundMessage()
        {
            MessageBox.Show("File not found", "Attention", MessageBoxButtons.OK); // виведення повідомлення про відсутність файлу
        }

        private void RichTextBoxTextChanged(object sender, EventArgs e)
        {
            if (fileName.Length > 0)
            {
                BtnSaveState(true); // АКТИВАЦІЯ кнопок збереження тексту (після внесення змін)
            }
        }

        private void BtnSaveState(bool isEnabled) // включення/виключення кнопки збереження після внесення змін у тексті
        {
            btnSaveText.Enabled = isEnabled; 
            saveToolStripMenuItem.Enabled = isEnabled;
        }

    }
}

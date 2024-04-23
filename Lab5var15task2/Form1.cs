using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab5var15task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.Checked)
            {
                if (radioButton == radioButton1)
                {
                    browseButton.Text = "Вибрати папку";
                }
                else if (radioButton == radioButton2)
                {
                    browseButton.Text = "Вибрати файл";
                }
            }
        }

        private void browseButton_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                    {
                        folderPathTextBox.Text = dialog.SelectedPath;
                        DisplayFolderInfo(dialog.SelectedPath);
                    }
                }
            }
            else if (radioButton2.Checked)
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.CheckFileExists = true;
                    dialog.CheckPathExists = true;
                    dialog.Multiselect = false;

                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    {
                        folderPathTextBox.Text = dialog.FileName;
                        if (Directory.Exists(dialog.FileName))
                        {
                            DisplayFolderInfo(dialog.FileName);
                        }
                        else if (File.Exists(dialog.FileName))
                        {
                            DisplayFileInfo(dialog.FileName);
                        }
                        else
                        {
                            MessageBox.Show("Виберіть існуючу папку або файл.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void DisplayFolderInfo(string folderPath)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

                string folderName = directoryInfo.Name;
                long folderSize = GetDirectorySize(directoryInfo);
                DateTime lastModified = directoryInfo.LastWriteTime;

                richTextBox1.Clear();
                richTextBox1.AppendText($"Ім'я папки: {folderName}\n");
                richTextBox1.AppendText($"Розмір: {folderSize} байт\n");
                richTextBox1.AppendText($"Остання зміна: {lastModified}\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка отримання інформації про папку: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayFileInfo(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                string fileName = fileInfo.Name;
                long fileSize = fileInfo.Length;
                DateTime lastModified = fileInfo.LastWriteTime;

                richTextBox1.Clear();
                richTextBox1.AppendText($"Ім'я файлу: {fileName}\n");
                richTextBox1.AppendText($"Розмір: {fileSize} байт\n");
                richTextBox1.AppendText($"Остання зміна: {lastModified}\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка отримання інформації про файл: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            long size = 0;

            FileInfo[] files = directoryInfo.GetFiles();

            foreach (FileInfo file in files)
            {
                size += file.Length;
            }

            DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();
            foreach (DirectoryInfo subDirectory in subDirectories)
            {
                size += GetDirectorySize(subDirectory);
            }
            return size;
        }
    }
}

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

namespace Lab5var15task3
{
    public partial class Form1 : Form
    {
        string[] photoPaths;
        int currentPhotoIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadPhotos(string folderPath)
        {
            listBox1.Items.Clear();
            pictureBox1.Image = null;

            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show("Папка не знайдена.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

            photoPaths = Directory.GetFiles(folderPath)
                                   .Where(file => validExtensions.Contains(Path.GetExtension(file).ToLower()))
                                   .ToArray();

            foreach (string photoPath in photoPaths)
            {
                listBox1.Items.Add(Path.GetFileName(photoPath));
            }
        }

        private void selectFolderButton_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog.SelectedPath;
                LoadPhotos(selectedFolderPath);
                currentPhotoIndex = 0;
            }
        }

        private void displayButton_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                string selectedPhotoPath = photoPaths[listBox1.SelectedIndex];
                pictureBox1.Image = System.Drawing.Image.FromFile(selectedPhotoPath);
                currentPhotoIndex = listBox1.SelectedIndex;
            }
        }

        private void nextPhotoButton_Click_1(object sender, EventArgs e)
        {
            currentPhotoIndex++;
            if (currentPhotoIndex >= photoPaths.Length)
            {
                currentPhotoIndex = 0;
            }
            pictureBox1.Image = System.Drawing.Image.FromFile(photoPaths[currentPhotoIndex]);
            listBox1.SelectedIndex = currentPhotoIndex;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            string defaultFolderPath = @"..\..\PhotosTask3";
            LoadPhotos(defaultFolderPath);

            if (photoPaths.Length > 0)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(photoPaths[0]);
                listBox1.SelectedIndex = 0;
            }
        }
    }
}

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

namespace Lab5var15task4
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
            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show("Папка не знайдена.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

            photoPaths = Directory.GetFiles(folderPath)
                                   .Where(file => validExtensions.Contains(Path.GetExtension(file).ToLower()))
                                   .ToArray();

            if (photoPaths.Length > 0)
            {
                DisplayPhoto(photoPaths[currentPhotoIndex]);
            }
            else
            {
                MessageBox.Show("В папці відсутні фотографії з підтримуваними форматами.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayPhoto(string photoPath)
        {
            pictureBox1.Image = System.Drawing.Image.FromFile(photoPath);
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            currentPhotoIndex++;
            if (currentPhotoIndex >= photoPaths.Length)
            {
                currentPhotoIndex = 0;
            }
            DisplayPhoto(photoPaths[currentPhotoIndex]);
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            currentPhotoIndex--;
            if (currentPhotoIndex < 0)
            {
                currentPhotoIndex = photoPaths.Length - 1;
            }
            DisplayPhoto(photoPaths[currentPhotoIndex]);
        }

        private void rotateLeftButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox1.Refresh();
            }
        }

        private void rotateRightButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Refresh();
            }
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog.SelectedPath;
                LoadPhotos(selectedFolderPath);
                currentPhotoIndex = 0;
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            string defaultFolderPath = @"..\..\PhotosTask4";
            LoadPhotos(defaultFolderPath);
        }
    }
}

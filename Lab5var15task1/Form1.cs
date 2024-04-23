using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5var15task1
{
    public partial class Form1 : Form
    {
        private double SmallRoomPrice;
        private double MediumRoomPrice;
        private double LargeRoomPrice;

        public Form1()
        {
            InitializeComponent();
            SmallRoomPrice = Convert.ToDouble(firstPriceTextBox.Text);
            MediumRoomPrice = Convert.ToDouble(secondPriceTextBox.Text);
            LargeRoomPrice = Convert.ToDouble(thirdPriceTextBox.Text);
            countSmallRoomUpDown.ValueChanged += UpdateOrderCost;
            countMediumRoomUpDown.ValueChanged += UpdateOrderCost;
            countLargeRoomUpDown.ValueChanged += UpdateOrderCost;
            smallRoomBox.CheckedChanged += UpdateOrderCostAndPictureBox;
            mediumRoomBox.CheckedChanged += UpdateOrderCostAndPictureBox;
            largeRoomBox.CheckedChanged += UpdateOrderCostAndPictureBox;
            firstPriceTextBox.TextChanged += UpdateRoomPrices;
            secondPriceTextBox.TextChanged += UpdateRoomPrices;
            thirdPriceTextBox.TextChanged += UpdateRoomPrices;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
        }

        private void UpdateOrderCostAndPictureBox(object sender, EventArgs e)
        {
            UpdateOrderCost(sender, e);
            UpdatePictureBoxes();
        }

        private void UpdatePictureBoxes()
        {
            pictureBox1.Visible = smallRoomBox.Checked;
            pictureBox2.Visible = mediumRoomBox.Checked;
            pictureBox3.Visible = largeRoomBox.Checked;
        }

        private void UpdateOrderCost(object sender, EventArgs e)
        {
            int countSmallRooms = (smallRoomBox.Checked) ? (int)countSmallRoomUpDown.Value : 0;
            int countMediumRooms = (mediumRoomBox.Checked) ? (int)countMediumRoomUpDown.Value : 0;
            int countLargeRooms = (largeRoomBox.Checked) ? (int)countLargeRoomUpDown.Value : 0;

            double orderCost = 0;

            if (smallRoomBox.Checked)
                orderCost += countSmallRooms * SmallRoomPrice;
            if (mediumRoomBox.Checked)
                orderCost += countMediumRooms * MediumRoomPrice;
            if (largeRoomBox.Checked)
                orderCost += countLargeRooms * LargeRoomPrice;

            int totalRooms = countSmallRooms + countMediumRooms + countLargeRooms;
            if (totalRooms > 10)
                orderCost *= 0.7;

            outputTextBox.Text = $"Вартість замовлення: {orderCost:N2}";
        }

        private void UpdateRoomPrices(object sender, EventArgs e)
        {
            SmallRoomPrice = Convert.ToDouble(firstPriceTextBox.Text);
            MediumRoomPrice = Convert.ToDouble(secondPriceTextBox.Text);
            LargeRoomPrice = Convert.ToDouble(thirdPriceTextBox.Text);

            UpdateOrderCostAndPictureBox(sender, e);
        }
    }
}
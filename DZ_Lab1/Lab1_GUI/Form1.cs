using DZ_Lab1;
using System;
using System.Windows.Forms;

namespace Lab1_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void seed_input_TextChanged(object sender, EventArgs e)
        {


            if (long.TryParse(seed_input.Text, out long value))
            {
                if (value > Int32.MaxValue)
                {
                    MessageBox.Show($"The number must be less than {Int32.MaxValue}.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    seed_input.Text = Int32.MaxValue.ToString();
                }
            }
            else
            {
                seed_input.Text = "";
            }
        }

        private void seed_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void number_of_items_input_TextChanged(object sender, EventArgs e)
        {
            if (long.TryParse(number_of_items_input.Text, out long value))
            {
                if (value > Int32.MaxValue)
                {
                    MessageBox.Show($"The number must be less than {Int32.MaxValue}.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    number_of_items_input.Text = Int32.MaxValue.ToString();
                }
            }
            else
            {
                number_of_items_input.Text = "";
            }
        }

        private void number_of_items_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }



        private void backpack_size_input_TextChanged(object sender, EventArgs e)
        {
            if (long.TryParse(backpack_size_input.Text, out long value))
            {
                if (value > Int32.MaxValue)
                {
                    MessageBox.Show($"The number must be less than {Int32.MaxValue}.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    backpack_size_input.Text = Int32.MaxValue.ToString();
                }
            }
            else
            {
                backpack_size_input.Text = "";
            }
        }

        private void backpack_size_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isSeedValid = int.TryParse(seed_input.Text, out int seed);
            bool isNumberOfItemsValid = int.TryParse(number_of_items_input.Text, out int numberOfItems);
            bool isBackpackSizeValid = int.TryParse(backpack_size_input.Text, out int backpackSize);

            if (isSeedValid && isNumberOfItemsValid && isBackpackSizeValid)
            {
                Backpack backpack = new Backpack(seed, backpackSize, numberOfItems);

                backpack.Solve();

                Result_output.Clear();
                Result_output.Text = backpack.ToString();

                Items_output.Clear();
                Items_output.Text = backpack.Items_string();

            }
            else
                MessageBox.Show("Something went wrong try again", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

     
    }
}

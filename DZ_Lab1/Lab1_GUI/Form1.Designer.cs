namespace Lab1_GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            seed_label = new Label();
            seed_input = new TextBox();
            number_of_items_label = new Label();
            number_of_items_input = new TextBox();
            backpack_size_label = new Label();
            backpack_size_input = new TextBox();
            button1 = new Button();
            Results_label = new Label();
            Result_output = new RichTextBox();
            Items_label = new Label();
            Items_output = new RichTextBox();
            SuspendLayout();
            // 
            // seed_label
            // 
            seed_label.AutoSize = true;
            seed_label.Location = new Point(12, 9);
            seed_label.Name = "seed_label";
            seed_label.Size = new Size(31, 15);
            seed_label.TabIndex = 0;
            seed_label.Text = "seed";
            // 
            // seed_input
            // 
            seed_input.Location = new Point(12, 27);
            seed_input.Name = "seed_input";
            seed_input.Size = new Size(100, 23);
            seed_input.TabIndex = 1;
            seed_input.TextChanged += seed_input_TextChanged;
            seed_input.KeyPress += seed_input_KeyPress;
            // 
            // number_of_items_label
            // 
            number_of_items_label.AutoSize = true;
            number_of_items_label.Location = new Point(12, 53);
            number_of_items_label.Name = "number_of_items_label";
            number_of_items_label.Size = new Size(95, 15);
            number_of_items_label.TabIndex = 2;
            number_of_items_label.Text = "number of items";
            // 
            // number_of_items_input
            // 
            number_of_items_input.Location = new Point(12, 71);
            number_of_items_input.Name = "number_of_items_input";
            number_of_items_input.Size = new Size(100, 23);
            number_of_items_input.TabIndex = 3;
            number_of_items_input.TextChanged += number_of_items_input_TextChanged;
            number_of_items_input.KeyPress += number_of_items_input_KeyPress;
            // 
            // backpack_size_label
            // 
            backpack_size_label.AutoSize = true;
            backpack_size_label.Location = new Point(12, 97);
            backpack_size_label.Name = "backpack_size_label";
            backpack_size_label.Size = new Size(79, 15);
            backpack_size_label.TabIndex = 4;
            backpack_size_label.Text = "backpack size";
            // 
            // backpack_size_input
            // 
            backpack_size_input.Location = new Point(12, 115);
            backpack_size_input.Name = "backpack_size_input";
            backpack_size_input.Size = new Size(100, 23);
            backpack_size_input.TabIndex = 5;
            backpack_size_input.TextChanged += backpack_size_input_TextChanged;
            backpack_size_input.KeyPress += backpack_size_input_KeyPress;
            // 
            // button1
            // 
            button1.Location = new Point(12, 144);
            button1.Name = "button1";
            button1.Size = new Size(100, 26);
            button1.TabIndex = 6;
            button1.Text = "Run";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Results_label
            // 
            Results_label.AutoSize = true;
            Results_label.Location = new Point(482, 9);
            Results_label.Name = "Results_label";
            Results_label.Size = new Size(44, 15);
            Results_label.TabIndex = 8;
            Results_label.Text = "Results";
            // 
            // Result_output
            // 
            Result_output.Location = new Point(482, 27);
            Result_output.Name = "Result_output";
            Result_output.ReadOnly = true;
            Result_output.ScrollBars = RichTextBoxScrollBars.Vertical;
            Result_output.Size = new Size(306, 411);
            Result_output.TabIndex = 9;
            Result_output.Text = "";
            // 
            // Items_label
            // 
            Items_label.AutoSize = true;
            Items_label.Location = new Point(154, 9);
            Items_label.Name = "Items_label";
            Items_label.Size = new Size(36, 15);
            Items_label.TabIndex = 10;
            Items_label.Text = "Items";
            // 
            // Items_output
            // 
            Items_output.Location = new Point(154, 27);
            Items_output.Name = "Items_output";
            Items_output.ReadOnly = true;
            Items_output.ScrollBars = RichTextBoxScrollBars.Vertical;
            Items_output.Size = new Size(322, 411);
            Items_output.TabIndex = 11;
            Items_output.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Items_output);
            Controls.Add(Items_label);
            Controls.Add(Result_output);
            Controls.Add(Results_label);
            Controls.Add(button1);
            Controls.Add(backpack_size_input);
            Controls.Add(backpack_size_label);
            Controls.Add(number_of_items_input);
            Controls.Add(number_of_items_label);
            Controls.Add(seed_input);
            Controls.Add(seed_label);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label seed_label;
        private TextBox seed_input;
        private Label number_of_items_label;
        private TextBox number_of_items_input;
        private Label backpack_size_label;
        private TextBox backpack_size_input;
        private Button button1;
        private Label Results_label;
        private RichTextBox Result_output;
        private Label Items_label;
        private RichTextBox Items_output;
    }
}

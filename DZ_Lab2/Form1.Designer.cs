namespace DZ_Lab2
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
            textBoxRates = new TextBox();
            LoadData = new Button();
            Date_list = new ListBox();
            Currency_to_exchange = new ListBox();
            Exchanged_currency = new ListBox();
            Calculate_exchange = new Button();
            Exchange_from = new TextBox();
            Exchange_to = new TextBox();
            SuspendLayout();
            // 
            // textBoxRates
            // 
            textBoxRates.Location = new Point(10, 9);
            textBoxRates.Margin = new Padding(3, 2, 3, 2);
            textBoxRates.Multiline = true;
            textBoxRates.Name = "textBoxRates";
            textBoxRates.ReadOnly = true;
            textBoxRates.ScrollBars = ScrollBars.Vertical;
            textBoxRates.Size = new Size(267, 297);
            textBoxRates.TabIndex = 0;
            textBoxRates.Tag = "textBoxRates";
            textBoxRates.TextChanged += textBoxRates_TextChanged;
            // 
            // LoadData
            // 
            LoadData.Location = new Point(160, 311);
            LoadData.Name = "LoadData";
            LoadData.Size = new Size(117, 23);
            LoadData.TabIndex = 1;
            LoadData.Text = "Load Newest Data";
            LoadData.UseVisualStyleBackColor = true;
            LoadData.Click += LoadData_Click;
            // 
            // Date_list
            // 
            Date_list.FormattingEnabled = true;
            Date_list.ItemHeight = 15;
            Date_list.Location = new Point(10, 311);
            Date_list.Name = "Date_list";
            Date_list.Size = new Size(144, 19);
            Date_list.TabIndex = 2;
            Date_list.SelectedIndexChanged += Date_list_SelectedIndexChanged;
            // 
            // Currency_to_exchange
            // 
            Currency_to_exchange.FormattingEnabled = true;
            Currency_to_exchange.ItemHeight = 15;
            Currency_to_exchange.Location = new Point(340, 9);
            Currency_to_exchange.Name = "Currency_to_exchange";
            Currency_to_exchange.Size = new Size(120, 79);
            Currency_to_exchange.TabIndex = 3;
            Currency_to_exchange.SelectedIndexChanged += Currency_to_exchange_SelectedIndexChanged;
            // 
            // Exchanged_currency
            // 
            Exchanged_currency.FormattingEnabled = true;
            Exchanged_currency.ItemHeight = 15;
            Exchanged_currency.Location = new Point(572, 9);
            Exchanged_currency.Name = "Exchanged_currency";
            Exchanged_currency.Size = new Size(120, 79);
            Exchanged_currency.TabIndex = 4;
            Exchanged_currency.SelectedIndexChanged += Exchanged_currency_SelectedIndexChanged;
            // 
            // Calculate_exchange
            // 
            Calculate_exchange.Location = new Point(480, 38);
            Calculate_exchange.Name = "Calculate_exchange";
            Calculate_exchange.Size = new Size(75, 22);
            Calculate_exchange.TabIndex = 5;
            Calculate_exchange.Text = "Calculate";
            Calculate_exchange.UseVisualStyleBackColor = true;
            Calculate_exchange.Click += Calculate_exchange_Click;
            // 
            // Exchange_from
            // 
            Exchange_from.Location = new Point(466, 9);
            Exchange_from.Name = "Exchange_from";
            Exchange_from.Size = new Size(100, 23);
            Exchange_from.TabIndex = 6;
            Exchange_from.KeyPress += Exchange_from_KeyPress;
            // 
            // Exchange_to
            // 
            Exchange_to.Location = new Point(466, 66);
            Exchange_to.Name = "Exchange_to";
            Exchange_to.ReadOnly = true;
            Exchange_to.Size = new Size(100, 23);
            Exchange_to.TabIndex = 7;
            Exchange_to.TextChanged += Exchange_to_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(Exchange_to);
            Controls.Add(Exchange_from);
            Controls.Add(Calculate_exchange);
            Controls.Add(Exchanged_currency);
            Controls.Add(Currency_to_exchange);
            Controls.Add(Date_list);
            Controls.Add(LoadData);
            Controls.Add(textBoxRates);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxRates;
        private Button LoadData;
        private ListBox Date_list;
        private ListBox Currency_to_exchange;
        private ListBox Exchanged_currency;
        private Button Calculate_exchange;
        private TextBox Exchange_from;
        private TextBox Exchange_to;
    }


}

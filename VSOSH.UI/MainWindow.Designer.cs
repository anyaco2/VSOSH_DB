namespace VSOSH.UI
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			comboBox1 = new System.Windows.Forms.ComboBox();
			button3 = new System.Windows.Forms.Button();
			comboBox2 = new System.Windows.Forms.ComboBox();
			button4 = new System.Windows.Forms.Button();
			downloadProtocolButton = new System.Windows.Forms.Button();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			SuspendLayout();
			// 
			// comboBox1
			// 
			comboBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
			comboBox1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
			comboBox1.FormattingEnabled = true;
			comboBox1.Items.AddRange(new object[]
			{
				"искусство",
				"астрономия",
				"биология",
				"химия",
				"китайский язык",
				"информатика",
				"экология",
				"экономика",
				"английский язык",
				"французкий язык",
				"основы безопасности и  зашиты родины",
				"география",
				"немецкий язык",
				"история",
				"право",
				"литература",
				"математика",
				"физическая культура",
				"физика",
				"русский язык",
				"обществознание",
				"труды"
			});
			comboBox1.Location = new System.Drawing.Point(91, 289);
			comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new System.Drawing.Size(295, 30);
			comboBox1.TabIndex = 2;
			// 
			// button3
			// 
			button3.BackColor = System.Drawing.Color.DarkViolet;
			button3.Cursor = System.Windows.Forms.Cursors.Hand;
			button3.FlatAppearance.BorderColor = System.Drawing.Color.Cyan;
			button3.FlatAppearance.BorderSize = 100;
			button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Cyan;
			button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Cyan;
			button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			button3.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
			button3.ForeColor = System.Drawing.SystemColors.ButtonFace;
			button3.Location = new System.Drawing.Point(395, 269);
			button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(161, 69);
			button3.TabIndex = 3;
			button3.Text = "Скачать";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_ClickAsync;
			// 
			// comboBox2
			// 
			comboBox2.BackColor = System.Drawing.SystemColors.InactiveBorder;
			comboBox2.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
			comboBox2.FormattingEnabled = true;
			comboBox2.Items.AddRange(new object[]
			{
				"Общий отчёт",
				"По классам и предметам",
				"За более старший класс"
			});
			comboBox2.Location = new System.Drawing.Point(584, 285);
			comboBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			comboBox2.Name = "comboBox2";
			comboBox2.Size = new System.Drawing.Size(295, 30);
			comboBox2.TabIndex = 4;
			// 
			// button4
			// 
			button4.BackColor = System.Drawing.Color.DarkViolet;
			button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			button4.Cursor = System.Windows.Forms.Cursors.Hand;
			button4.FlatAppearance.BorderColor = System.Drawing.Color.White;
			button4.FlatAppearance.BorderSize = 10;
			button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			button4.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
			button4.ForeColor = System.Drawing.SystemColors.ButtonFace;
			button4.Location = new System.Drawing.Point(888, 269);
			button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(161, 71);
			button4.TabIndex = 5;
			button4.Text = "Скачать";
			button4.UseVisualStyleBackColor = false;
			button4.Click += button4_ClickAsync;
			// 
			// downloadProtocolButton
			// 
			downloadProtocolButton.BackColor = System.Drawing.Color.DarkViolet;
			downloadProtocolButton.Cursor = System.Windows.Forms.Cursors.Hand;
			downloadProtocolButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			downloadProtocolButton.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
			downloadProtocolButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
			downloadProtocolButton.Location = new System.Drawing.Point(614, 83);
			downloadProtocolButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			downloadProtocolButton.Name = "downloadProtocolButton";
			downloadProtocolButton.Size = new System.Drawing.Size(187, 108);
			downloadProtocolButton.TabIndex = 6;
			downloadProtocolButton.Text = "Загрузить протокол";
			downloadProtocolButton.UseVisualStyleBackColor = false;
			downloadProtocolButton.Click += downloadProtocolButton_ClickAsync;
			// 
			// openFileDialog1
			// 
			openFileDialog1.FileName = "openFileDialog1";
			// 
			// label1
			// 
			label1.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
			label1.Location = new System.Drawing.Point(84, 83);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(509, 48);
			label1.TabIndex = 7;
			label1.Text = "Всероссийская олимпиада школьников";
			// 
			// label2
			// 
			label2.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
			label2.Location = new System.Drawing.Point(83, 131);
			label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(336, 60);
			label2.TabIndex = 8;
			label2.Text = "ШКОЛЬНЫЙ ЭТАП";
			// 
			// label3
			// 
			label3.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
			label3.Location = new System.Drawing.Point(85, 249);
			label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(205, 35);
			label3.TabIndex = 9;
			label3.Text = "Проходные баллы";
			// 
			// label4
			// 
			label4.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
			label4.Location = new System.Drawing.Point(579, 249);
			label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(301, 35);
			label4.TabIndex = 10;
			label4.Text = "Статистика школьного этапа";
			// 
			// button1
			// 
			button1.BackColor = System.Drawing.Color.DarkViolet;
			button1.Cursor = System.Windows.Forms.Cursors.Hand;
			button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			button1.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
			button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
			button1.Location = new System.Drawing.Point(840, 83);
			button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(187, 108);
			button1.TabIndex = 11;
			button1.Text = "Загруженные протоколы";
			button1.UseVisualStyleBackColor = false;
			button1.Click += button1_Click;
			// 
			// MainWindow
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.SystemColors.ButtonFace;
			ClientSize = new System.Drawing.Size(1103, 502);
			Controls.Add(button1);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(downloadProtocolButton);
			Controls.Add(button4);
			Controls.Add(comboBox2);
			Controls.Add(button3);
			Controls.Add(comboBox1);
			Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			Text = "Form3";
			ResumeLayout(false);
		}

		private System.Windows.Forms.Button button1;
		#endregion
		private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button downloadProtocolButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
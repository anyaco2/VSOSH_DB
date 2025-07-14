using System.ComponentModel;

namespace VSOSH.UI;

partial class Protocols
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private IContainer components = null;

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
		dataGridView1 = new System.Windows.Forms.DataGridView();
		button1 = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
		SuspendLayout();
		// 
		// dataGridView1
		// 
		dataGridView1.ColumnHeadersHeight = 29;
		dataGridView1.Location = new System.Drawing.Point(34, 24);
		dataGridView1.Name = "dataGridView1";
		dataGridView1.RowHeadersWidth = 51;
		dataGridView1.Size = new System.Drawing.Size(338, 314);
		dataGridView1.TabIndex = 0;
		// 
		// button1
		// 
		button1.BackColor = System.Drawing.Color.DarkViolet;
		button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		button1.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Bold);
		button1.ForeColor = System.Drawing.SystemColors.Control;
		button1.Location = new System.Drawing.Point(409, 24);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(161, 69);
		button1.TabIndex = 1;
		button1.Text = "Удалить";
		button1.UseVisualStyleBackColor = false;
		button1.Click += button1_Click;
		// 
		// Protocols
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		ClientSize = new System.Drawing.Size(582, 366);
		Controls.Add(button1);
		Controls.Add(dataGridView1);
		MaximumSize = new System.Drawing.Size(600, 413);
		MinimumSize = new System.Drawing.Size(600, 413);
		Text = "Protocols";
		((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
		ResumeLayout(false);
	}

	private System.Windows.Forms.Button button1;

	private System.Windows.Forms.DataGridView dataGridView1;
	#endregion
}


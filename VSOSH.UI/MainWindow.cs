using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSOSH.Contracts;
using VSOSH.HttpClient;

namespace VSOSH.UI;

public partial class MainWindow : Form
{
	private readonly ApiClient _apiClient;

	public MainWindow()
	{
		_apiClient = new ApiClient(new System.Net.Http.HttpClient());
		InitializeComponent();
	}


	//вызов
	private async void button3_ClickAsync(object sender, EventArgs e)
	{
		var subject = comboBox1.Text switch
		{
			"искусство" => Subject.Art,
			"астрономия" => Subject.Astronomy,
			"биология" => Subject.Biology,
			"химия" => Subject.Chemistry,
			"китайский язык" => Subject.Chinese,
			"информатика" => Subject.ComputerScience,
			"экология" => Subject.Ecology,
			"экономика" => Subject.Economy,
			"английский язык" => Subject.English,
			"французкий язык" => Subject.French,
			"основы безопасности и  зашиты родины" => Subject.FundamentalsLifeSafety,
			"география" => Subject.Geography,
			"немецкий язык" => Subject.German,
			"история" => Subject.History,
			"право" => Subject.Law,
			"литература" => Subject.Literature,
			"математика" => Subject.Math,
			"физическая культура" => Subject.PhysicalEducation,
			"физика" => Subject.Physic,
			"русский язык" => Subject.Russian,
			"обществознание" => Subject.SocialStudies,
			"труды" => Subject.Technology
		};
		var bytes = await _apiClient.GetPassingPointsAsync(subject);
		saveFileDialog1.ShowDialog();
		saveFileDialog1.Title = "Save";
		saveFileDialog1.Filter = "Microsoft Excel (*.xls*)|*.xls*";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			try
			{
				// Сохраняем массив байтов в файл
				File.WriteAllBytes($"{saveFileDialog1.FileName}.xlsx", bytes);
				MessageBox.Show("Файл успешно сохранён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка сохранения файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}

	private async void button4_ClickAsync(object sender, EventArgs e) //Статистика
	{
		switch (comboBox2.Text)
		{
			case "Общий отчёт":
				var bytes = await _apiClient.GetGeneralReportAsync();
				saveFileDialog1.ShowDialog();
				saveFileDialog1.Title = "Save";
				saveFileDialog1.Filter = "Microsoft Excel (*.xls*)|*.xls*";
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					try
					{
						// Сохраняем массив байтов в файл
						File.WriteAllBytes($"{saveFileDialog1.FileName}.xlsx", bytes);
						MessageBox.Show("Файл успешно сохранён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Ошибка сохранения файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				break;
			case "По классам и предметам":
				bytes = await _apiClient.GetQuantitativeDataAsync();
				saveFileDialog1.ShowDialog();
				saveFileDialog1.Title = "Save";
				saveFileDialog1.Filter = "Microsoft Excel (*.xls*)|*.xls*";
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					try
					{
						// Сохраняем массив байтов в файл
						File.WriteAllBytes($"{saveFileDialog1.FileName}.xlsx", bytes);
						MessageBox.Show("Файл успешно сохранён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Ошибка сохранения файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				break;
			case "За более старший класс":
				bytes = await _apiClient.GetGreaterClassDataAsync();
				saveFileDialog1.ShowDialog();
				saveFileDialog1.Title = "Save";
				saveFileDialog1.Filter = "Microsoft Excel (*.xls*)|*.xls*";
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					try
					{
						// Сохраняем массив байтов в файл
						File.WriteAllBytes($"{saveFileDialog1.FileName}.xlsx", bytes);
						MessageBox.Show("Файл успешно сохранён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Ошибка сохранения файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				break;
		}
	}

	private async void downloadProtocolButton_ClickAsync(object sender, EventArgs e)
	{
		var fileDialog1 = new OpenFileDialog
		{
			DefaultExt = "*.xls;*.xlsx",
			InitialDirectory = @"C:\",
			Filter = "Microsoft Excel (*.xls*)|*.xls*",
			FilterIndex = 2,
			RestoreDirectory = true
		};


		if (fileDialog1.ShowDialog() == DialogResult.OK)
		{
			var excelFileName = fileDialog1.FileName;
			try
			{
				await _apiClient.UploadParserResultAsync(new FileStream(excelFileName, FileMode.Open), excelFileName);
				MessageBox.Show("Данные успешно добавлены.");
			}
			catch (Exception)
			{
				MessageBox.Show("Что-то пошло не так.");
			}
		}
	}
}
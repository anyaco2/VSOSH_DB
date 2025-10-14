using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSOSH.Domain;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;

namespace VSOSH.UI;

public partial class MainWindow : Form
{
	#region Data
	#region Fields
	private readonly IGeneralReportService _generalReportService;
	private readonly IGreaterClassService _greaterClassService;
	private readonly IParser _parser;
	private readonly IPassingPointsService _passingPointsService;
	private readonly IProtocolRepository _protocolRepository;
	private readonly IQuantitativeDataService _quantitativeDataService;
	#endregion
	#endregion

	#region .ctor
	public MainWindow(IParser parser,
					  IGeneralReportService generalReportService,
					  IGreaterClassService greaterClassService,
					  IPassingPointsService passingPointsService,
					  IQuantitativeDataService quantitativeDataService,
					  IProtocolRepository protocolRepository)
	{
		InitializeComponent();
		_parser = parser;
		_generalReportService = generalReportService;
		_greaterClassService = greaterClassService;
		_passingPointsService = passingPointsService;
		_quantitativeDataService = quantitativeDataService;
		_protocolRepository = protocolRepository;
		comboBox1.Items.Clear();
		comboBox1.Items.AddRange(
			"астрономия, биология, география, информатика, искусство, история, испанский язык, китайский язык, литература, математика, основы безопасности и защиты родины, обществознание, право, русский язык, экономика, экология, химия, физика, французский язык, английский язык, немецкий язык, физическая культура, труды"
				.Split(", "));
	}
	#endregion

	#region Private
	private void button1_Click(object sender, EventArgs e)
	{
		var protocols = new Protocols(_protocolRepository);
		protocols.Show();
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
			"труды" => Subject.Technology,
			"испанский язык" => Subject.Spanish,
			_ => throw new ArgumentOutOfRangeException()
		};
		await using var fileStream = await _passingPointsService.GetPassingPoints(subject);
		saveFileDialog1.Title = @"Save";
		saveFileDialog1.Filter = @"Microsoft Excel (*.xls*)|*.xls*";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			try
			{
				// Сохраняем массив байтов в файл
				File.Copy(fileStream.Name, $"{saveFileDialog1.FileName}.xlsx");
				MessageBox.Show(@"Файл успешно сохранён!", @"Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(@$"Ошибка сохранения файла: {ex.Message}", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}

	private async void button4_ClickAsync(object sender, EventArgs e) //Статистика
	{
		switch (comboBox2.Text)
		{
			case "Общий отчёт":
				await GeneralReportAsync();
				break;
			case "По классам и предметам":
				await GetQuantitativeDataAsync();
				break;
			case "За более старший класс":
				await GetGreaterClassDataAsync();
				break;
		}
	}

	private async void downloadProtocolButton_ClickAsync(object sender, EventArgs e)
	{
		var fileDialog1 = new OpenFileDialog
		{
			DefaultExt = "*.xls;*.xlsx",
			InitialDirectory = @"C:\",
			Filter = @"Microsoft Excel (*.xls*)|*.xls*",
			FilterIndex = 2,
			RestoreDirectory = true,
			Multiselect = true
		};

		if (fileDialog1.ShowDialog() == DialogResult.OK)
		{
			var excelFileNames = fileDialog1.FileNames;
			foreach (var excelFileName in excelFileNames)
			{
				try
				{
					await _parser.ParseAndSaveAsync(new FileStream(excelFileName, FileMode.Open));
					MessageBox.Show($@"Данные файла {excelFileName} успешно добавлены.");
				}
				catch (Exception)
				{
					MessageBox.Show($@"Что-то пошло не так у файла {excelFileName}.");
				}
			}
		}
	}

	private async Task GeneralReportAsync()
	{
		await using var fileStream = await _generalReportService.GetGeneralReport();
		saveFileDialog1.Title = @"Save";
		saveFileDialog1.Filter = @"Microsoft Excel (*.xls*)|*.xls*";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			try
			{
				// Сохраняем массив байтов в файл
				File.Copy(fileStream.Name, $"{saveFileDialog1.FileName}.xlsx");
				MessageBox.Show(@"Файл успешно сохранён!", @"Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(@$"Ошибка сохранения файла: {ex.Message}", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}

	private async Task GetGreaterClassDataAsync()
	{
		await using var fileStream = await _greaterClassService.GetGreaterClass();
		saveFileDialog1.Title = @"Save";
		saveFileDialog1.Filter = @"Microsoft Excel (*.xls*)|*.xls*";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			try
			{
				// Сохраняем массив байтов в файл
				File.Copy(fileStream.Name, $"{saveFileDialog1.FileName}.xlsx");
				MessageBox.Show(@"Файл успешно сохранён!", @"Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(@$"Ошибка сохранения файла: {ex.Message}", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}

	private async Task GetQuantitativeDataAsync()
	{
		await using var fileStream = await _quantitativeDataService.GetQuantitativeData();
		saveFileDialog1.Title = @"Save";
		saveFileDialog1.Filter = @"Microsoft Excel (*.xls*)|*.xls*";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			try
			{
				// Сохраняем массив байтов в файл
				File.Copy(fileStream.Name, $"{saveFileDialog1.FileName}.xlsx");
				MessageBox.Show(@"Файл успешно сохранён!", @"Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(@$"Ошибка сохранения файла: {ex.Message}", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
	#endregion
}

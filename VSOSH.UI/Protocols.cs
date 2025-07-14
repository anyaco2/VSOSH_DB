using System;
using System.Linq;
using System.Windows.Forms;
using Serilog;
using VSOSH.Domain.Entities;
using VSOSH.Domain.Repositories;

namespace VSOSH.UI;

public partial class Protocols : Form
{
	#region Data
	#region Static
	private static readonly ILogger Log = Serilog.Log.ForContext<Protocols>();
	#endregion

	#region Fields
	private readonly IProtocolRepository _protocolRepository;
	private Protocol[] _protocols;
	#endregion
	#endregion

	#region .ctor
	public Protocols(IProtocolRepository protocolRepository)
	{
		InitializeComponent();
		_protocolRepository = protocolRepository;
		_protocols = _protocolRepository.FindAll();

		SetDataSource();
	}
	#endregion

	#region Private
	private void button1_Click(object sender, EventArgs e)
	{
		for (var i = 0; i < dataGridView1.SelectedRows.Count; i++)
		{
			var selected = dataGridView1.SelectedRows[i].DataBoundItem as Proto;
			try
			{
				_protocolRepository.Remove(_protocols.FirstOrDefault(p => p.Id == selected.Идентификатор));
				_protocols = _protocolRepository.FindAll();
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"Произошла ошибка при удалении протокола: {selected.Идентификатор}");
				MessageBox.Show($"Произошла ошибка при удалении протокола: {selected.Название}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		SetDataSource();
	}

	private void SetDataSource()
	{
		dataGridView1.DataSource = _protocols.Select(p => new Proto(p.Name, p.Id))
											 .ToArray();
		dataGridView1.Columns[1].Visible = false;
	}
	#endregion

	private record Proto(string Название, Guid Идентификатор);
}

using System;
using System.Windows.Forms;

namespace VSOSH.UI
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        //вызов
        private void
            button3_Click(object sender,
                EventArgs e) //Функция создает запрос на основе выбора предмета из выпадающего списка
        {
        }

        private void button4_Click(object sender, EventArgs e) //Статистика
        {
            switch (comboBox2.Text)
            {
                case "Общий отчёт":
                    break;
                case "По классам и предметам":
                    break;
                case "За более старший класс":
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e) //Загрузка исходных файлов EXCEL
        {
        }
    }
}
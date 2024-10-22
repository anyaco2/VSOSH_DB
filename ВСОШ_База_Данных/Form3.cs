using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using Microsoft.Office.Interop.Excel;
using System.Windows.Input;
using System.ComponentModel.Design;

namespace ВСОШ_База_Данных
{
    public partial class Form3 : Form
    {
        public static string connectionString = "Server=localhost;Port=5432;Database=VOSH_DB;User Id = postgres;Password=2789;";
        private const int BatchSize = 1000; // Размер пакета данных для чтения
        public Form3()
        {
            InitializeComponent();
            FillComboBox("select * from public.subject_area", "subject_area_name", comboBox1);//Подгрузка предметов из БД
        }

        public class Stroka
        {
            public List<string> Stolbets { get; set; } = new List<string>();
        }
        static void SavingARowToTheDatabase(Stroka stroka)//сохранение строки в базе данных
        {
            //Подключение к базе
            NpgsqlConnection sqlConnection = new NpgsqlConnection(connectionString);
            sqlConnection.Open();
            //Создание sql-запроса
            NpgsqlCommand comand = new NpgsqlCommand();
            comand.Connection = sqlConnection;
            comand.CommandType = CommandType.Text;
            comand.CommandText = "INSERT INTO public.\"Results\" VALUES (";//начало запроса
            for (int i = 0; i < stroka.Stolbets.Count; i++)//добавляем данные в запрос, по ячейкам
            {
                if (int.TryParse(stroka.Stolbets[i], out int number))//если передаваемый параметр число
                {
                    comand.CommandText += stroka.Stolbets[i];
                }
                else
                {//если не число добавляем кавыки
                    comand.CommandText += "\'" + stroka.Stolbets[i] + "\'";
                }
                if (i != stroka.Stolbets.Count - 1) //если это не последний параметр добавляем запятую и пробел
                {
                    comand.CommandText += ", ";
                }
                else
                { //если последний то );
                    comand.CommandText += ");";
                }
            }
            //передаем запрос в БД
            NpgsqlDataReader comandDataReader = comand.ExecuteReader();
        }
        private void importFromExcel()//Загружаем данные из таблицы Excel
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.DefaultExt = "*.xls;*.xlsx";
            openFileDialog1.Filter = "Microsoft Excel (*.xls*)|*.xls*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                string xlFileName = openFileDialog1.FileName;
                using (var stream = File.Open(xlFileName, FileMode.Open, FileAccess.Read))
                {
                    // Создание объекта IExcelDataReader для чтения данных
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        // Список для хранения объектов Stroka
                        List<Stroka> stroki = new List<Stroka>();

                        // Цикл для обработки всех листов в файле
                        do
                        {
                            bool flag = false;//флаг проверки первая ли это строка (для исключения заголовков)
                                              // Цикл для обработки каждой строки в текущем листе
                            while (reader.Read())
                            {
                                // Создание объекта Stroka для текущей строки
                                Stroka stroka = new Stroka();
                                if (flag)
                                {

                                    // Цикл для обработки каждого столбца в текущей строке
                                    for (int column = 0; column < reader.FieldCount; column++)
                                    {
                                        // Запись значения в список столбцов объекта Stroka
                                        stroka.Stolbets.Add(reader.GetValue(column)?.ToString());
                                    }
                                    // Заполнение dataGridView данными из текущей строки
                                    SavingARowToTheDatabase(stroka);
                                }
                                else
                                {//игнорируем первую строку (заголовки) на каждом листе
                                    flag = true;
                                }
                            }
                        } while (reader.NextResult()); // Переход к следующему листу

                    }
                }
            }
            // Открытие Excel файла для чтения
        }
        private void exportFromDataBase(String commanda)//Передаем данные из БД в Excel
        {
            // Подключение к PostgreSQL
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Список SQL-запросов
                string[] sqlQueries = {
                    commanda //пока так, чтобы полностью структуру не перестраивать. Но запрос всегда будет один, поэтому массив больше не понадобится
                };
                
                // Создание Excel-файла
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = excelApp.Workbooks.Add();

                // Цикл по запросам
                for (int queryIndex = 0; queryIndex < sqlQueries.Length; queryIndex++)
                {
                    // Получение текущего запроса
                    string sql = sqlQueries[queryIndex];

                    // Выборка данных из таблицы
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        // Получение схемы таблицы
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            System.Data.DataTable schemaTable = reader.GetSchemaTable();

                            // Создание нового листа, если необходим
                            if (queryIndex + 1 > workbook.Worksheets.Count)
                            {
                                workbook.Worksheets.Add();
                            }
                            // Получение листа для текущего запроса
                            Worksheet worksheet = (Worksheet)workbook.Worksheets[1]; // Индекс листа начинается с 1

                            // Запись заголовков столбцов из схемы таблицы
                            int column = 1;
                            foreach (DataRow row1 in schemaTable.Rows)
                            {
                                worksheet.Cells[1, column] = row1["ColumnName"].ToString();
                                column++;
                            }
                            // Запись данных в Excel по пакетам
                            int row = 2; // Начинаем запись данных со второй строки
                            while (reader.Read())
                            {
                                // Запись текущей строки в Excel
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    worksheet.Cells[row, i + 1] = reader[i].ToString();
                                }
                                row++;

                                // Проверка, достигнут ли размер пакета
                                if (row % BatchSize == 0)
                                {
                                    // Сохранение изменений в Excel (необязательно, можно сохранить в конце)
                                    workbook.Save();

                                    // Очистка памяти
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                                    worksheet = null;
                                    System.GC.Collect(); // Сборка мусора

                                    // Получение листа для текущего запроса (необязательно, если уже получен)
                                    worksheet = (Worksheet)workbook.Worksheets[queryIndex + 1];
                                }
                            }
                        }
                    }
                }

                // Сохранение изменений в Excel после завершения чтения всех запросов
                //Сохранение Excel-файла

                saveFileDialog1.ShowDialog();
                saveFileDialog1.Title = "Save";
                saveFileDialog1.Filter = "Microsoft Excel (*.xls*)|*.xls*";
                workbook.SaveAs(saveFileDialog1.FileName);

                // Закрытие Excel
                excelApp.Quit();
            }
        }

       

        public bool FillComboBox(string query, string nameColumn, ComboBox comboBox)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(reader.GetOrdinal(nameColumn)))
                                {
                                    comboBox.Items.Add(reader[nameColumn].ToString());
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка SQL при заполнении {comboBox}:\r\n {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при заполнении {comboBox}:\r\n {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        //вызов
        private void button3_Click(object sender, EventArgs e) //Функция создает запрос на основе выбора предмета из выпадающего списка
        {
            NpgsqlConnection sqlConnection = new NpgsqlConnection(connectionString);
            sqlConnection.Open();
            NpgsqlCommand comand = new NpgsqlCommand();
            comand.Connection = sqlConnection;
            comand.CommandType = CommandType.Text;
            switch (comboBox1.Text) //Т.к. олимпиады по предметам начинаются в разных классах, то пока так
            {
                case "литература":
                case "английский язык":
                case "немецкий язык":
                case "французский язык":
                case "китайский язык":
                case "география":
                case "технология":
                case "астрономия":
                case "экология":
                case "биология":
                case "история":
                case "экономика":
                case "физическая культура":
                case "информатика и ИКТ":
                    string[] sqlQueries =
                    {
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=5", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=6", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=7", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=8", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=9", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=10", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=11", comboBox1.Text)
                    };
                    exportFromDataBase3(sqlQueries);
                    break;

                case "МХК":
                case "физика":
                case "химия":
                case "право":
                case "ОБЖ":
                    string[] sqlQueries2 =
                    {
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=7", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=8", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=9", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=10", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=11", comboBox1.Text)
                    };
                    exportFromDataBase3(sqlQueries2);
                    break;

                case "русский язык":
                case "математика":
                    string[] sqlQueries1 =
                    {
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=4", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=5", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=6", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=7", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=8", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=9", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=10", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=11", comboBox1.Text)
                    };
                    exportFromDataBase3(sqlQueries1);
                    break;

                case "обществознание":
                    string[] sqlQueries3 =
                    {
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=6", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=7", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=8", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=9", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=10", comboBox1.Text),
                        comand.CommandText = String.Format("SELECT * FROM public.\"Results\" where subject_name=('{0}') AND \"student_class-new\"=11", comboBox1.Text)
                    };
                    exportFromDataBase3(sqlQueries3);
                    break;
            }
            
            //передаем запрос в функцию экспорта
        }

        private void exportFromDataBase3(string[] sqlQueries)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Список SQL-запросов

                // Создание Excel-файла
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = excelApp.Workbooks.Add();

                // Цикл по запросам
                for (int queryIndex = 0; queryIndex < sqlQueries.Length; queryIndex++)
                {
                    // Получение текущего запроса
                    string sql = sqlQueries[queryIndex];

                    // Выборка данных из таблицы
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        // Получение схемы таблицы
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            int column = 7;
                            System.Data.DataTable schemaTable = reader.GetSchemaTable();
                            
                            // Создание нового листа, если необходим
                            if (queryIndex + 1 > workbook.Worksheets.Count)
                            {
                                workbook.Worksheets.Add();
                                
                            }
                            // Получение листа для текущего запроса

                            Worksheet worksheet = (Worksheet)workbook.Worksheets[1]; // Индекс листа начинается с 1
                            // Запись заголовков столбцов из схемы таблицы
                            column = 1;
                            foreach (DataRow row1 in schemaTable.Rows)
                            {
                                worksheet.Cells[1, column] = row1["ColumnName"].ToString();
                                column++;
                            }
                            // Запись данных в Excel по пакетам
                            int row = 2; // Начинаем запись данных со второй строки
                            while (reader.Read())
                            {
                                // Запись текущей строки в Excel
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    worksheet.Cells[row, i + 1] = reader[i].ToString();
                                }
                                row++;
                                string className = reader[7].ToString(); // Предполагаем, что номер класса находится в седьмом столбце
                                (workbook.Sheets[1] as Worksheet).Name = className + " класс"; // Установка имени листа
                                // Проверка, достигнут ли размер пакета
                                if (row % BatchSize == 0)
                                {
                                    // Сохранение изменений в Excel (необязательно, можно сохранить в конце)
                                    workbook.Save();

                                    // Очистка памяти
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                                    worksheet = null;
                                    System.GC.Collect(); // Сборка мусора

                                    // Получение листа для текущего запроса (необязательно, если уже получен)
                                    worksheet = (Worksheet)workbook.Worksheets[queryIndex + 1];
                                    
                                }
                            }
                            
                        }
                    }
                }

                // Сохранение изменений в Excel после завершения чтения всех запросов
                //Сохранение Excel-файла

                saveFileDialog1.ShowDialog();
                saveFileDialog1.Title = "Save";
                saveFileDialog1.Filter = "Microsoft Excel (*.xls*)|*.xls*";
                workbook.SaveAs(saveFileDialog1.FileName);

                // Закрытие Excel
                excelApp.Quit();
            }
        }

        private void button4_Click(object sender, EventArgs e) //Статистика
        {
            string report = comboBox2.Text;
            switch (report)
            {
                case "Общий отчёт":
                    generalReport();
                    break;
                case "По классам и предметам":
                    ReportByClassAndSubject();
                    break;
                case "За более старший класс":
                    ReportForOlderClass();
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e) //Загрузка исходных файлов EXCEL
        {
            importFromExcel();
        }

        private void ReportForOlderClass() //За старший класс
        {
            string comanda = "SELECT subject_name as Предмет, student_surname as Фамилия, student_name as Имя, student_patronimic as Отчество, " +
                "\"student_class-new\" as \"Класс, за который выступает\", student_class as \"Класс, в котором обучается\" FROM public.\"Results\" " +
                "WHERE \"student_class-new\" > student_class ORDER BY student_surname ASC";
            exportFromDataBase(comanda);
        }
        private void ReportByClassAndSubject() //По классам и предметам
        {
            exportFromDataBase1();
        }


        private void generalReport() //Общий отчет
        {
            string comanda = "SELECT Count(*) as \"Кол-во фактов участия\", (Select Count(DISTINCT student_surname) as \"Кол-во уникальных участников\" " +
                "from public.\"Results\"),\r\n\t(SELECT COUNT(*) as \"Кол-во дипломов победителей\" FROM public.\"Results\" where student_status = 'Победитель'), " +
                "\r\n\t(SELECT COUNT(Distinct student_surname) as \"Кол-во уникальных победителей\" FROM public.\"Results\" where student_status = 'Победитель'),\r\n\t(SELECT COUNT(*) " +
                "as \"Кол-во дипломов призеров\" FROM public.\"Results\" where student_status = 'Призер'), \r\n\t(SELECT COUNT(Distinct student_surname) as \"Кол-во уникальных призеров\" " +
                "FROM public.\"Results\" where student_status = 'Призер'),\r\n\t(SELECT COUNT(Distinct student_surname) as \"Кол-во ун. победителей и призеров\" FROM public.\"Results\" " +
                "where student_status <> 'ЛОХ')\r\n\tFROM public.\"Results\"";
            exportFromDataBase(comanda);
        }
        private void exportFromDataBase1()//Передаем данные из БД в Excel
        {
            // Подключение к PostgreSQL
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Список SQL-запросов
                string[] sqlQueries = {
                    "SELECT subject_name, COUNT(CASE WHEN student_class = 4 THEN 1 END) AS \"4 класс\",\r\n\tCOUNT(CASE WHEN student_class = 5 THEN 1 END) AS \"5 класс\", " +
                    "COUNT(CASE WHEN student_class = 6 THEN 1 END) AS \"6 класс\",\r\n\tCOUNT(CASE WHEN student_class = 7 THEN 1 END) AS \"7 класс\", " +
                    "COUNT(CASE WHEN student_class = 8 THEN 1 END) AS \"8 класс\",\r\n\tCOUNT(CASE WHEN student_class = 9 THEN 1 END) AS \"9 класс\", " +
                    "COUNT(CASE WHEN student_class = 10 THEN 1 END) AS \"10 класс\",\r\n\tCOUNT(CASE WHEN student_class = 11 THEN 1 END) AS \"11 класс\"\r\nFROM \r\n    " +
                    "public.\"Results\"\r\nGROUP BY \r\n    subject_name\r\nORDER BY \r\n    subject_name;",

                    "SELECT subject_name, COUNT(CASE WHEN student_class = 4 AND student_status = 'Победитель' THEN 1 END) AS \"4 класс\",\r\n\t" +
                    "COUNT(CASE WHEN student_class = 5 AND student_status = 'Победитель' THEN 1 END) AS \"5 класс\", COUNT(CASE WHEN student_class = 6 AND student_status = 'Победитель' THEN 1 END) AS \"6 класс\"," +
                    "\r\n\tCOUNT(CASE WHEN student_class = 7 AND student_status = 'Победитель' THEN 1 END) AS \"7 класс\", " +
                    "COUNT(CASE WHEN student_class = 8 AND student_status = 'Победитель' THEN 1 END) AS \"8 класс\",\r\n\tCOUNT(CASE WHEN student_class = 9 AND student_status = 'Победитель' THEN 1 END) AS \"9 класс\"," +
                    " COUNT(CASE WHEN student_class = 10 AND student_status = 'Победитель' THEN 1 END) AS \"10 класс\",\r\n\t" +
                    "COUNT(CASE WHEN student_class = 11 AND student_status = 'Победитель' THEN 1 END) AS \"11 класс\"\r\nFROM \r\n    " +
                    "public.\"Results\"\r\nGROUP BY \r\n    subject_name\r\nORDER BY \r\n    subject_name;",

                    "SELECT subject_name, COUNT(CASE WHEN student_class = 4 AND student_status = 'Призер' THEN 1 END) AS \"4 класс\"," +
                    "\r\n\tCOUNT(CASE WHEN student_class = 5 AND student_status = 'Призер' THEN 1 END) AS \"5 класс\", " +
                    "COUNT(CASE WHEN student_class = 6 AND student_status = 'Призер' THEN 1 END) AS \"6 класс\",\r\n\tCOUNT(CASE WHEN student_class = 7 AND student_status = 'Призер' THEN 1 END) AS \"7 класс\"," +
                    " COUNT(CASE WHEN student_class = 8 AND student_status = 'Призер' THEN 1 END) AS \"8 класс\",\r\n\tCOUNT(CASE WHEN student_class = 9 AND student_status = 'Призер' THEN 1 END) AS \"9 класс\", " +
                    "COUNT(CASE WHEN student_class = 10 AND student_status = 'Призер' THEN 1 END) AS \"10 класс\"," +
                    "\r\n\tCOUNT(CASE WHEN student_class = 11 AND student_status = 'Призер' THEN 1 END) AS \"11 класс\"\r\nFROM \r\n    " +
                    "public.\"Results\"\r\nGROUP BY \r\n    subject_name\r\nORDER BY \r\n    subject_name;"
                };

                // Создание Excel-файла
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = excelApp.Workbooks.Add();

                // Цикл по запросам
                for (int queryIndex = 0; queryIndex < sqlQueries.Length; queryIndex++)
                {
                    // Получение текущего запроса
                    string sql = sqlQueries[queryIndex];

                    // Выборка данных из таблицы
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        // Получение схемы таблицы
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            System.Data.DataTable schemaTable = reader.GetSchemaTable();

                            // Создание нового листа, если необходим
                            if (queryIndex + 1 > workbook.Worksheets.Count)
                            {
                                workbook.Worksheets.Add();
                            }
                            // Получение листа для текущего запроса
                            Worksheet worksheet = (Worksheet)workbook.Worksheets[1]; // Индекс листа начинается с 1

                            // Запись заголовков столбцов из схемы таблицы
                            int column = 1;
                            foreach (DataRow row1 in schemaTable.Rows)
                            {
                                worksheet.Cells[1, column] = row1["ColumnName"].ToString();
                                column++;
                            }
                            // Запись данных в Excel по пакетам
                            int row = 2; // Начинаем запись данных со второй строки
                            while (reader.Read())
                            {
                                // Запись текущей строки в Excel
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    worksheet.Cells[row, i + 1] = reader[i].ToString();
                                }
                                row++;

                                // Проверка, достигнут ли размер пакета
                                if (row % BatchSize == 0)
                                {
                                    // Сохранение изменений в Excel (необязательно, можно сохранить в конце)
                                    workbook.Save();

                                    // Очистка памяти
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                                    worksheet = null;
                                    System.GC.Collect(); // Сборка мусора

                                    // Получение листа для текущего запроса (необязательно, если уже получен)
                                    worksheet = (Worksheet)workbook.Worksheets[queryIndex + 1];
                                }
                            }
                        }
                    }
                }

                // Сохранение изменений в Excel после завершения чтения всех запросов
                //Сохранение Excel-файла

                saveFileDialog1.ShowDialog();
                saveFileDialog1.Title = "Save";
                saveFileDialog1.Filter = "Microsoft Excel (*.xls*)|*.xls*";
                workbook.SaveAs(saveFileDialog1.FileName);

                // Закрытие Excel
                excelApp.Quit();
            }
        }
    }
}

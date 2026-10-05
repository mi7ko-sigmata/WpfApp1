using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // Динамичен списък, който автоматично обновява DataGrid при промяна
        private ObservableCollection<Student> studentsList = new ObservableCollection<Student>();

        public MainWindow()
        {
            InitializeComponent();

            // Свързваме списъка с таблицата
            StudentsDataGrid.ItemsSource = studentsList;
        }

        // 1. Бутон: Добавяне на нов ученик към таблицата
        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput(out int number, out int grade))
            {
                Student newStudent = new Student
                {
                    Number = number,
                    Name = NameBox.Text,
                    Grade = grade
                };

                studentsList.Add(newStudent);
                ClearInputs();
                StatusTextBlock.Text = "Ученикът е добавен!";
            }
        }

        // 2. Бутон: Корекция на избрания от таблицата ученик
        private void EditStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDataGrid.SelectedItem is Student selectedStudent)
            {
                if (ValidateInput(out int number, out int grade))
                {
                    selectedStudent.Number = number;
                    selectedStudent.Name = NameBox.Text;
                    selectedStudent.Grade = grade;

                    // Обновяваме изгледа на таблицата
                    StudentsDataGrid.Items.Refresh();
                    StatusTextBlock.Text = "Данните са коригирани!";
                }
            }
            else
            {
                StatusTextBlock.Text = "Моля, изберете ученик от таблицата!";
            }
        }

        // 3. Бутон: Изтриване на избран ученик
        private void DeleteStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDataGrid.SelectedItem is Student selectedStudent)
            {
                studentsList.Remove(selectedStudent);
                ClearInputs();
                StatusTextBlock.Text = "Ученикът е изтрит!";
            }
            else
            {
                StatusTextBlock.Text = "Моля, изберете ученик за изтриване!";
            }
        }

        // При избор на ред от таблицата — данните автоматично се попълват в полетата за редактиране
        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudentsDataGrid.SelectedItem is Student selectedStudent)
            {
                NumberBox.Text = selectedStudent.Number.ToString();
                NameBox.Text = selectedStudent.Name;
                GradeBox.Text = selectedStudent.Grade.ToString();
            }
        }

        // Проверка дали са въведени валидни данни
        private bool ValidateInput(out int number, out int grade)
        {
            number = 0;
            grade = 0;

            if (!int.TryParse(NumberBox.Text, out number))
            {
                StatusTextBlock.Text = "Моля, въведете валиден номер!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                StatusTextBlock.Text = "Моля, въведете име!";
                return false;
            }

            if (!int.TryParse(GradeBox.Text, out grade))
            {
                StatusTextBlock.Text = "Моля, въведете валидна оценка!";
                return false;
            }

            return true;
        }

        // Изчистване на полетата след операция
        private void ClearInputs()
        {
            NumberBox.Clear();
            NameBox.Clear();
            GradeBox.Clear();
            StudentsDataGrid.UnselectAll();
        }
    }
}
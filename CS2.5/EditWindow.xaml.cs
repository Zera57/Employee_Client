using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CS2._5
{
	/// <summary>
	/// Логика взаимодействия для EditWindow.xaml
	/// </summary>
	public partial class EditWindow : Window
	{
		public List<Department> departments { get; set; }

		public Employee emploee { get; set; }

		private Department startDepartment;

		public EditWindow(IEnumerable<Department> _departments, Department _startDepartment, Employee _emploee)
		{
			InitializeComponent();

			departments = new List<Department>(_departments);
			emploee = _emploee;
			startDepartment = _startDepartment;

			Set_Information(emploee);

			combo.ItemsSource = departments;
			combo.SelectedItem = _startDepartment;
		}

		private void Set_Information(Employee emploee)
		{
			if (emploee != null)
			{
				txtBox_Name.Text = emploee.ToString();
				txtBox_Position.Text = emploee.Position;
				txtBox_Age.Text = emploee.Age.ToString();
				txtBox_BaseSalary.Text = emploee.BaseSalary.ToString();
				txtBox_Hours.Text = emploee.Hours.ToString();
			}
		}

		private void ButtonSave_Click(object sender, RoutedEventArgs e)
		{
			Department newdepartment = (Department)combo.SelectedItem;

			emploee.DepartmentId = combo.SelectedIndex + 1;
			emploee.Position = txtBox_Position.Text;
			emploee.FirstName = txtBox_Name.Text.Split(' ')[0];
			emploee.SecondName = txtBox_Name.Text.Split(' ')[1];
			emploee.Age = Convert.ToInt32(txtBox_Age.Text);
			emploee.BaseSalary = Convert.ToInt32(txtBox_BaseSalary.Text);
			emploee.Hours = Convert.ToInt32(txtBox_Hours.Text);

			this.DialogResult = true;
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to close EditWindow? All unsaved progress will disappear.", "Close", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
			{
				this.DialogResult = false;
			}
		}
	}
}

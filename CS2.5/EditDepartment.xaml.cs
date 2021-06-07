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
	/// Логика взаимодействия для AddDepartment.xaml
	/// </summary>
	public partial class EditDepartment : Window
	{
		Department department;

		public EditDepartment(Department _department)
		{
			InitializeComponent();

			department = _department;

			Set_Information(department);
		}

		private void Set_Information(Department department)
		{
			if (department != null)	txtBox_Name.Text = department.Name;
		}

		private void ButtonAdd_Click(object sender, RoutedEventArgs e)
		{
			department.Name = txtBox_Name.Text;

			this.DialogResult = true;
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to close EditWindow? All unsaved progress will disappear.", "Close", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
				this.DialogResult = false;
		}
	}
}

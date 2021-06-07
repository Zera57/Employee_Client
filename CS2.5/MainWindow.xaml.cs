using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CS2._5
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>


	public partial class MainWindow : Window
	{
		public Department lastDepartment;

		public Model model;

		public MainWindow()
		{
			InitializeComponent();

			model = new Model();

			combo.ItemsSource = model.CurrentDepartments;
		}

		private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Button_AddEmploee.Height == 0)
			{
				Grid_Buttons.Height = Double.NaN;
				Button_AddEmploee.Height = Double.NaN;
			}
			ComboBox comboBox = (ComboBox)sender;
			Department department = (Department)comboBox.SelectedItem;
			if (department != null)
			{
				if (!department.Name.Equals("Add new Department +"))
					list.ItemsSource = model.GetEmploees(department.Id);
				else
				{
					list.ItemsSource = null;
					Button_AddEmploee.Height = 0;
					Grid_Buttons.Height = 0;
					Department newDepartment = new Department();
					EditDepartment editDepartment = new EditDepartment(newDepartment);
					bool? checkDialog = true;
					if (editDepartment.ShowDialog() == checkDialog)
					{
						model.InsertDepartment(newDepartment);
						combo.ItemsSource = null;
						combo.ItemsSource = model.GetDepartments();
						combo.SelectedIndex = combo.Items.Count - 2;
					}
				}
			}
		}

		private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Plug.Height != 0) Plug.Height = 0;

			ListBox listBox = (ListBox)sender;
			Employee emploee = (Employee)listBox.SelectedItem;
			Set_Information(emploee);

			lastDepartment = (Department)combo.SelectedItem;
		}

		public void Set_Information(Employee emploee)
		{
			if (emploee != null)
			{
				txt_Name.Text = emploee.ToString();
				txt_Position.Text = "Position: " + emploee.Position;
				txt_Age.Text = "Age: " + emploee.Age.ToString();
				txt_Salary.Text = "Salary: " + emploee.Salary.ToString();
			}
		}

		private void Button_EditDepartment_Click(object sender, RoutedEventArgs e)
		{
			Department newDepartment = (Department)combo.SelectedItem;
			EditDepartment editDepartment = new EditDepartment(newDepartment);
			bool? checkDialog = true;
			if (editDepartment.ShowDialog() == checkDialog)
			{
				model.UpdateDepartment(newDepartment);
				combo.ItemsSource = model.GetDepartments();
			}
		}

		private void Button_DeleteDepartment_Click(object sender, RoutedEventArgs e)
		{
			Department deleteDepartment = (Department)combo.SelectedItem;

			model.DeleteDepartment(deleteDepartment);
			combo.ItemsSource = null;
			combo.ItemsSource = model.GetDepartments();
			Button_AddEmploee.Height = 0;
			Grid_Buttons.Height = 0;
		}

		private void Button_AddEmploee_Click(object sender, RoutedEventArgs e)
		{
			Employee emploee = new Employee();
			EditWindow editWindow = new EditWindow(model.CurrentDepartments, (Department)combo.SelectedItem, emploee);
			bool? checkDialog = true;
			if (editWindow.ShowDialog() == checkDialog)
			{
				model.InsertEmploee(emploee);
				list.ItemsSource = model.GetEmploees(emploee.DepartmentId);
			}
		}

		private void ButtonAddHours_Click(object sender, RoutedEventArgs e)
		{
			Employee emploee = (Employee)list.SelectedItem;
			emploee?.AddHours();
			Set_Information(emploee);
			model.UpdateEmploee(emploee);
		}

		private void Button_EditEmploee_Click(object sender, RoutedEventArgs e)
		{
			Employee emploee = (Employee)list.SelectedItem;
			EditWindow editWindow = new EditWindow(model.CurrentDepartments, (Department)combo.SelectedItem, emploee);
			bool? checkDialog = true;
			if (editWindow.ShowDialog() == checkDialog)
			{
				model.UpdateEmploee(emploee);				
			}
		}

		private void Button_DeleteEmploee_Click(object sender, RoutedEventArgs e)
		{
			model.DeleteEmploee((Employee)list.SelectedItem);
			//lastDepartment.RemoveEmploee((Emploee)list.SelectedItem);
			if (Plug.Height == 0) Plug.Height = Double.NaN;
		}
	}

}

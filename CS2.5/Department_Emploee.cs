using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace CS2._5
{
	public class Model
	{
		public ObservableCollection<Department> CurrentDepartments;

		HttpClient http = new HttpClient();

		public Model()
		{
			CurrentDepartments = new ObservableCollection<Department>();
			GetDepartments();
		}

		public ObservableCollection<Department> GetDepartments()
		{
			string url = "http://localhost:50605/getDepartments";
			try
			{
				CurrentDepartments = JsonConvert.DeserializeObject<ObservableCollection<Department>>(http.GetStringAsync(url).Result);
			}
			catch
			{

			}
			CurrentDepartments.Add(new Department() { Name = "Add new Department +" });
			return CurrentDepartments;
		}

		public ObservableCollection<Employee> GetEmploees(int DepartmentId)
		{
			string url = "http://localhost:50605/getEmployee/list/" + DepartmentId;

			ObservableCollection<Employee> CurrentEmploees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(http.GetStringAsync(url).Result);

			return CurrentEmploees;
		}

		public void InsertDepartment(Department department)
		{
			string url = "http://localhost:50605/addDepartment";
			StringContent content = new StringContent(JsonConvert.SerializeObject(department), Encoding.UTF8, "application/json");
			http.PostAsync(url, content);
		}

		public void UpdateDepartment(Department department)
		{
			string url = "http://localhost:50605/updateDepartment";
			StringContent content = new StringContent(JsonConvert.SerializeObject(department), Encoding.UTF8, "application/json");
			http.PostAsync(url, content);
		}

		public void DeleteDepartment(Department department)
		{
			string url = "http://localhost:50605/deleteDepartment";
			StringContent content = new StringContent(JsonConvert.SerializeObject(department), Encoding.UTF8, "application/json");
			http.PostAsync(url, content);
		}

		public void InsertEmploee(Employee emploee)
		{
			string url = "http://localhost:50605/addEmployee";
			StringContent content = new StringContent(JsonConvert.SerializeObject(emploee), Encoding.UTF8, "application/json");
			http.PostAsync(url, content);
		}

		public void UpdateEmploee(Employee emploee)
		{
			string url = "http://localhost:50605/updateEmployee";
			StringContent content = new StringContent(JsonConvert.SerializeObject(emploee), Encoding.UTF8, "application/json");
			http.PostAsync(url, content);
		}

		public void DeleteEmploee(Employee emploee)
		{
			string url = "http://localhost:50605/deleteEmployee";
			StringContent content = new StringContent(JsonConvert.SerializeObject(emploee), Encoding.UTF8, "application/json");
			http.PostAsync(url, content);
		}
	}

	public class Department
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}


	public class Employee
	{
		public string	FirstName { get; set; }
		public string	SecondName { get; set; }

		public string	Position { get; set; }

		public int		Age { get; set; }
		public int		Hours { get; set; }
		public int		BaseSalary { get; set; }
		public int		Salary => BaseSalary * Hours;
		public int		Id { get; set; }
		public int		DepartmentId { get; set; }

		public void AddHours()
		{
			Hours += 8;
		}

		public override string ToString()
		{
			return FirstName + " " + SecondName;
		}

		public DataRow UpdateRow(DataRow emploeeRow)
		{
			emploeeRow["Id"] = this.Id;
			emploeeRow["DepartmentId"] = this.DepartmentId;
			emploeeRow["FirstName"] = this.FirstName;
			emploeeRow["SecondName"] = this.SecondName;
			emploeeRow["Position"] = this.Position;
			emploeeRow["Age"] = this.Age;
			emploeeRow["BaseSalary"] = this.BaseSalary;
			emploeeRow["Hours"] = this.Hours;

			return emploeeRow;
		}
	}
}

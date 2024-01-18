/*
 * Created by SharpDevelop.
 * User: ILHAM POHAN
 * Date: 1/18/2024
 * Time: 3:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace STAApp
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		MySqlConnection connection = new MySqlConnection("Server=localhost;Database=sta_database;Uid=root;Pwd=;");
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			try
			{
				connection.Open();
				
				string query = "SELECT * FROM karyawan";
				MySqlCommand cmd = new MySqlCommand(query, connection);
				
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				adapter.Fill(dataTable);
				
				dataGridView1.DataSource = dataTable;
			}
			catch (Exception ex)
			{
			    
			}
			finally
			{
			    connection.Close();
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			try
			{
				connection.Open();
				
				var dateFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
				var dateTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss");
				
				var ageFrom = numericUpDown1.Value.ToString();
				var ageTo = numericUpDown1.Value.ToString();
				
				string query = "SELECT * FROM karyawan";
				bool existWhere = false;
				if (dateFrom != "" && dateTo != "") {
					query += " WHERE (TglMasukKerja BETWEEN '"+dateFrom+"' AND '"+dateTo+"')";
					existWhere = true;
				} else if(dateFrom != ""){
					query += " WHERE (TglMasukKerja >= '"+dateFrom+"')";
					existWhere = true;
				} else if(dateTo != ""){
					query += " WHERE (TglMasukKerja <= '"+dateTo+"')";
					existWhere = true;
				}
				
				if (ageFrom != "" && ageTo != "") {
					if (existWhere){
						query += " AND (Usia BETWEEN '"+ageFrom+"' AND '"+ageTo+"')";
					} else{
						query += " WHERE (Usia BETWEEN '"+ageFrom+"' AND '"+ageTo+"')";	
					}
				} else if(ageFrom != ""){
					if (existWhere){
						query += " AND (Usia >= '"+ageFrom+"')";
					} else{
						query += " WHERE (Usia >= '"+ageFrom+"')";
					}
				} else if(ageTo != ""){
					if (existWhere){
						query += " AND (Usia <= '"+ageTo+"')";
					} else{
						query += " WHERE (Usia <= '"+ageTo+"')";
					}
				}
				
				MySqlCommand cmd = new MySqlCommand(query, connection);
				
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				adapter.Fill(dataTable);
				
				dataGridView1.DataSource = dataTable;
			}
			catch (Exception ex)
			{
			    
			}
			finally
			{
			    connection.Close();
			}
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			if (dataGridView1.SelectedRows.Count > 0)
        	{
				MessageBox.Show(dataGridView1.SelectedRows.Count.ToString());
				return;
			}
			
			DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
			
			var selectedID = selectedRow.Cells["IDKaryawan"].Value.ToString();
			MessageBox.Show(selectedID);
			try
			{
				connection.Open();
				
				string query = "SELECT * FROM karyawan";
				MySqlCommand cmd = new MySqlCommand(query, connection);
				
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				adapter.Fill(dataTable);
				
				dataGridView1.DataSource = dataTable;
			}
			catch (Exception ex)
			{
			    
			}
			finally
			{
			    connection.Close();
			}	
		}
	}
}

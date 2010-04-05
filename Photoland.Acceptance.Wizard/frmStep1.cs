using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Photoland.Security;
using Photoland.Security.User;
using Photoland.Forms.Interface;
using Photoland.Lib;

namespace Photoland.Acceptance.Wizard
{
	public partial class frmStep1 : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;
		public DataTable tblOrder = new DataTable("Order");

		private SqlCommand db_command = new SqlCommand();
		private SqlDataReader db_reader;
		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();


        //////////////////////////////////////////////////////////////////////////
        private bool CheckState(SqlConnection c)
        {
            bool r = false;
            if (!TestCommand(c))
            {
                frmWaitConnection w = new frmWaitConnection();
                while (!TestCommand(c))
                {
                    Application.DoEvents();
                    w = new frmWaitConnection();
                    w.ShowDialog();
                    if (w.DialogResult == DialogResult.Cancel)
                    {
                        r = false;
                        break;
                    }
                }
                if (TestCommand(c))
                    r = true;
            }
            else
            {
                r = true;
            }
            return r;
        }

        private bool TestCommand(SqlConnection c)
        {
            bool r = false;
            try
            {
                if (c.State != ConnectionState.Open)
                    c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select getdate()";
                DateTime _r = (DateTime)cmd.ExecuteScalar();
                r = true;
            }
            catch (Exception ex)
            {
                r = false;
            }
            return r;
        }
        //////////////////////////////////////////////////////////////////////////

        public frmStep1()
		{
			InitializeComponent();
			this.Text = "Мастер приемки заказов: шаг 1";

			tblOrder.Columns.Add("check", System.Type.GetType("System.Boolean"));
			tblOrder.Columns.Add("Name", System.Type.GetType("System.String"));
			tblOrder.Columns.Add("Count", System.Type.GetType("System.Int32"));
			tblOrder.Columns.Add("Folder", System.Type.GetType("System.String"));
			tblOrder.Columns.Add("id", System.Type.GetType("System.String"));
		}

		private void frmStep1_Load(object sender, EventArgs e)
		{
			ReBildTable();
			lblUserInfo.Text = usr.Name + "\n" + usr.Post + "\n" + usr.Point;
			tmr.Interval = prop.Dir_rescan * 1000;
			tmr.Start();
		}

		private void ReBildTable()
		{
			tmr.Stop();
            if (CheckState(db_connection))
            {
                db_command.Connection = db_connection;
                if (prop.ShortGoodsListInWizard)
                    db_command.CommandText = "SELECT [id_good] ,[name] ,[description] ,[folder] ,[type] ,[checked] FROM [vwWizardStep1Good1] WHERE [type]=1 ORDER BY [name]";
                else
                    db_command.CommandText = "SELECT [id_good] ,[name] ,[description] ,[folder] ,[type] ,[checked] FROM [vwWizardStep1Good] WHERE [type]=1 ORDER BY [name]";
                db_reader = db_command.ExecuteReader();
                // очищаем таблицу
                GridOrder.Rows.Count = 1;
                // задаем высоту строки
                GridOrder.Rows.DefaultSize = 30;
                // проверяем значение пути к папкам для печати и обработки
                while ((prop.Dir_edit == "") || (prop.Dir_print == ""))
                {
                    MessageBox.Show("Не определены каталоги для заказов на печатьи на обработку! Проверьте настройки программы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    frmOptions fOption = new frmOptions();
                    fOption.ShowDialog();
                }
                // Подпапки Год-месяц-день
                string tmp_subdirs = DateTime.Now.Year.ToString() + "\\";
                if (DateTime.Now.Month.ToString().Length == 1)
                    tmp_subdirs += "0";
                tmp_subdirs += DateTime.Now.Month.ToString() + "\\";
                if (DateTime.Now.Day.ToString().Length == 1)
                    tmp_subdirs += "0";
                tmp_subdirs += DateTime.Now.Day.ToString();

                // Об'ект папки для печати
                if (!Directory.Exists(prop.Dir_print + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text))
                    Directory.CreateDirectory(prop.Dir_print + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text);
                DirectoryInfo DirPrint = new DirectoryInfo(prop.Dir_print + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text);

                if (!Directory.Exists(prop.Dir_edit + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text))
                    Directory.CreateDirectory(prop.Dir_edit + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text);
                DirectoryInfo DirEdit = new DirectoryInfo(prop.Dir_edit + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text);

                while (db_reader.Read())
                {
                    string _tmp_id_good;
                    string _tmp_name;
                    string _tmp_description;
                    string _tmp_folder;
                    string _tmp_type;
                    bool _tmp_checked;

                    if (!db_reader.IsDBNull(0))
                        _tmp_id_good = db_reader.GetString(0);
                    else
                        _tmp_id_good = "0";
                    if (!db_reader.IsDBNull(1))
                        _tmp_name = db_reader.GetString(1);
                    else
                        _tmp_name = "";
                    if (!db_reader.IsDBNull(2))
                        _tmp_description = db_reader.GetString(2);
                    else
                        _tmp_description = "";
                    if (!db_reader.IsDBNull(3))
                        _tmp_folder = db_reader.GetString(3);
                    else
                        _tmp_folder = "";
                    if (!db_reader.IsDBNull(4))
                        _tmp_type = db_reader.GetString(4);
                    else
                        _tmp_type = "";
                    if (!db_reader.IsDBNull(5))
                        _tmp_checked = db_reader.GetBoolean(5);
                    else
                        _tmp_checked = false;

                    // создаем папку с именем формата если он отмечена галкой
                    if (_tmp_checked)
                    {
                        // если папка для печати с форматом не существует, то создаем ее
                        if (!Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
                        {
                            Directory.CreateDirectory(DirPrint.FullName + "\\" + _tmp_folder);
                        }

                        // если папка для обработки с форматом не существует, то создаем ее
                        if (!Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
                        {
                            Directory.CreateDirectory(DirEdit.FullName + "\\" + _tmp_folder);
                        }
                    }
                    else
                    {
                        // для печати: если существует, то проверяем, если она пустая, то удаляем ее, если не пустая, то спрашиваем
                        if (Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
                        {
                            DirectoryInfo tmpDir = new DirectoryInfo(DirPrint.FullName + "\\" + _tmp_folder);
                            if (fso.Directory_Size(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true) == 0)
                            {
                                try
                                {
                                    if (!prop.DenyDelete)
                                    {
                                        if (prop.QueryForDelete)
                                        {
                                            if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                                tmpDir.Delete(true);
                                        }
                                        else
                                        {
                                            tmpDir.Delete(true);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorNfo.WriteErrorInfo(ex);
                                }
                            }
                            else
                            {
                                if (MessageBox.Show("Внимание! Вы отменили в заказе формат " + _tmp_name.Trim() + ", направленный на печать, но в нем находятся файлы (" + fso.Count_of_files(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true).ToString() + " шт.)!\nУдалить?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    try
                                    {
                                        if (!prop.DenyDelete)
                                        {
                                            if (prop.QueryForDelete)
                                            {
                                                if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                                    tmpDir.Delete(true);
                                            }
                                            else
                                            {
                                                tmpDir.Delete(true);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorNfo.WriteErrorInfo(ex);
                                    }
                                }
                                else
                                {
                                    _tmp_checked = true;
                                }
                            }
                        }

                        // для обработки: если существует, то проверяем, если она пустая, то удаляем ее, если не пустая, то спрашиваем
                        if (Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
                        {
                            DirectoryInfo tmpDir = new DirectoryInfo(DirEdit.FullName + "\\" + _tmp_folder);
                            if (fso.Directory_Size(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true) == 0)
                            {
                                try
                                {
                                    if (!prop.DenyDelete)
                                    {
                                        if (prop.QueryForDelete)
                                        {
                                            if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                                tmpDir.Delete(true);
                                        }
                                        else
                                        {
                                            tmpDir.Delete(true);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorNfo.WriteErrorInfo(ex);
                                }
                            }
                            else
                            {
                                if (MessageBox.Show("Внимание! Вы отменили в заказе формат " + _tmp_name.Trim() + ", направленный на обработку, но в нем находятся файлы (" + fso.Count_of_files(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true).ToString() + " шт.)!\nУдалить?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    try
                                    {
                                        if (!prop.DenyDelete)
                                        {
                                            if (prop.QueryForDelete)
                                            {
                                                if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                                    tmpDir.Delete(true);
                                            }
                                            else
                                            {
                                                tmpDir.Delete(true);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorNfo.WriteErrorInfo(ex);
                                    }
                                }
                                else
                                {
                                    _tmp_checked = true;
                                }
                            }
                        }
                    }

                    // выводим строку с галкой, форматом и количеством фотографий в папке
                    int tmp_count_files = 0;
                    if (Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
                        tmp_count_files += fso.Count_of_files(DirPrint.FullName + "\\" + _tmp_folder, prop.List_of_files, prop.Search_SubDir, true);
                    if (Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
                        tmp_count_files += fso.Count_of_files(DirEdit.FullName + "\\" + _tmp_folder, prop.List_of_files, prop.Search_SubDir, true);

                    GridOrder.AddItem("\t" + _tmp_checked + "\t" + _tmp_name + "\t" + tmp_count_files.ToString() + "\t" + _tmp_folder + "\t" + _tmp_id_good);
                }
                db_reader.Close();
                UpdateTable();
            }
			tmr.Start();
		}

		private void UpdateTable()
		{
			while ((prop.Dir_edit == "") || (prop.Dir_print == ""))
			{
				MessageBox.Show("Не определены каталоги для заказов на печатьи на обработку! Проверьте настройки программы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				frmOptions fOption = new frmOptions();
				fOption.ShowDialog();
			}
			// Подпапки Год-месяц-день
			string tmp_subdirs = DateTime.Now.Year.ToString() + "\\";
			if (DateTime.Now.Month.ToString().Length == 1)
				tmp_subdirs += "0";
			tmp_subdirs += DateTime.Now.Month.ToString() + "\\";
			if (DateTime.Now.Day.ToString().Length == 1)
				tmp_subdirs += "0";
			tmp_subdirs += DateTime.Now.Day.ToString();

			// Об'ект папки для печати
			if (!Directory.Exists(prop.Dir_print + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text))
				Directory.CreateDirectory(prop.Dir_print + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text);
			DirectoryInfo DirPrint = new DirectoryInfo(prop.Dir_print + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text);

			if (!Directory.Exists(prop.Dir_edit + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text))
				Directory.CreateDirectory(prop.Dir_edit + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text);
			DirectoryInfo DirEdit = new DirectoryInfo(prop.Dir_edit + "\\" + tmp_subdirs + "\\" + lblOrderNo.Text);

			// Очищаем таблицу для передачи
			tblOrder.Rows.Clear();
			for (int i = 1; i < GridOrder.Rows.Count; i++)
			{
				string _tmp_name = GridOrder.GetData(i, 2).ToString();
				string _tmp_folder = GridOrder.GetData(i, 4).ToString();
				bool _tmp_checked = Convert.ToBoolean(GridOrder.GetData(i, 1));
				// создаем папку с именем формата если он отмечена галкой
				if (_tmp_checked)
				{
					// если папка для печати с форматом не существует, то создаем ее
					if (!Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
					{
						Directory.CreateDirectory(DirPrint.FullName + "\\" + _tmp_folder);
					}

					// если папка для обработки с форматом не существует, то создаем ее
					if (!Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
					{
						Directory.CreateDirectory(DirEdit.FullName + "\\" + _tmp_folder);
					}
				}
				else
				{
					// для печати: если существует, то проверяем, если она пустая, то удаляем ее, если не пустая, то спрашиваем
					if (Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
					{
						DirectoryInfo tmpDir = new DirectoryInfo(DirPrint.FullName + "\\" + _tmp_folder);
						if (fso.Directory_Size(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true) == 0)
						{
							try
							{
								if (!prop.DenyDelete)
								{
									if (prop.QueryForDelete)
									{
										if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
											tmpDir.Delete(true);
									}
									else
									{
										tmpDir.Delete(true);
									}
								}
							}
							catch(Exception ex)
							{
								ErrorNfo.WriteErrorInfo(ex);
							}
						}
						else
						{
							if (MessageBox.Show("Внимание! Вы отменили в заказе формат " + _tmp_name.Trim() + ", направленный на печать, но в нем находятся файлы (" + fso.Count_of_files(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true).ToString() + " шт.)!\nУдалить?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								try
								{
									if (!prop.DenyDelete)
									{
										if (prop.QueryForDelete)
										{
											if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
												tmpDir.Delete(true);
										}
										else
										{
											tmpDir.Delete(true);
										}
									}
								}
								catch(Exception ex)
								{
									ErrorNfo.WriteErrorInfo(ex);
								}
							}
							else
							{
								_tmp_checked = true;
							}
						}
					}

					// для обработки: если существует, то проверяем, если она пустая, то удаляем ее, если не пустая, то спрашиваем
					if (Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
					{
						DirectoryInfo tmpDir = new DirectoryInfo(DirEdit.FullName + "\\" + _tmp_folder);
						if (fso.Directory_Size(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true) == 0)
						{
							try
							{
								if (!prop.DenyDelete)
								{
									if (prop.QueryForDelete)
									{
										if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
											tmpDir.Delete(true);
									}
									else
									{
										tmpDir.Delete(true);
									}
								}
							}
							catch(Exception ex)
							{
								ErrorNfo.WriteErrorInfo(ex);
							}
						}
						else
						{
							if (MessageBox.Show("Внимание! Вы отменили в заказе формат " + _tmp_name.Trim() + ", направленный на обработку, но в нем находятся файлы (" + fso.Count_of_files(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true).ToString() + " шт.)!\nУдалить?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								try
								{
									if (!prop.DenyDelete)
									{
										if (prop.QueryForDelete)
										{
											if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
												tmpDir.Delete(true);
										}
										else
										{
											tmpDir.Delete(true);
										}
									}
								}
								catch(Exception ex)
								{
									ErrorNfo.WriteErrorInfo(ex);
								}
							}
							else
							{
								_tmp_checked = true;
							}
						}
					}
				}

				// выводим строку с галкой, форматом и количеством фотографий в папке
				int tmp_count_files = 0;
				if (Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
					tmp_count_files += fso.Count_of_files(DirPrint.FullName + "\\" + _tmp_folder, prop.List_of_files, prop.Search_SubDir, true);
				if (Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
					tmp_count_files += fso.Count_of_files(DirEdit.FullName + "\\" + _tmp_folder, prop.List_of_files, prop.Search_SubDir, true);

				GridOrder.SetData(i, 1, _tmp_checked);
				GridOrder.SetData(i, 3, tmp_count_files);
			
				object[] row = new object[5];
				row[0] = GridOrder.GetData(i, 1);
				row[1] = GridOrder.GetData(i, 2);
				row[2] = GridOrder.GetData(i, 3);
				row[3] = GridOrder.GetData(i, 4);
				row[4] = GridOrder.GetData(i, 5);
				tblOrder.Rows.Add(row);
			}
			UpdateForm();
		}

		private void UpdateForm()
		{
			try
			{
				decimal cnt = 0;
				for (int i = 1; i < GridOrder.Rows.Count; i++)
				{
					cnt += decimal.Parse(GridOrder.GetData(i, 3).ToString());
				}
				if (((radioPapperType1.Checked) || (radioPapperType2.Checked)) && ((radioCrop1.Checked) || (radioCrop2.Checked) ||
					(radioCrop3.Checked)) && (cnt > 0))
					btnNext.Enabled = true;
				else
					btnNext.Enabled = false;
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Отменить заказ?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				this.DialogResult = DialogResult.Cancel;
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			UpdateTable();
		}

		private void tmr_Tick(object sender, EventArgs e)
		{
			UpdateTable();
		}

		private void radioPapperType1_CheckedChanged(object sender, EventArgs e)
		{
			UpdateForm();
		}

		private void radioPapperType2_CheckedChanged(object sender, EventArgs e)
		{
			UpdateForm();
		}

		private void radioCrop1_CheckedChanged(object sender, EventArgs e)
		{
			UpdateForm();
		}

		private void radioCrop2_CheckedChanged(object sender, EventArgs e)
		{
			UpdateForm();
		}

		private void radioCrop3_CheckedChanged(object sender, EventArgs e)
		{
			UpdateForm();
		}

	}
}
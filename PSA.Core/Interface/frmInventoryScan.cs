using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface
{
	public partial class frmInventoryScan : PSA.Lib.Interface.Templates.frmTDialog
	{
		public List<string> numbers = new List<string>();
		public List<string> oldnumbers = new List<string>();
		public string status_title = "";
	
		public frmInventoryScan()
		{
			InitializeComponent();
			this.Title = "Сканирование штрих-кодов";
		    txtStatus.Text = status_title;
			txtScan.Text = "";
			lstScan.Items.Clear();
		}

		private void frmInventoryScan_Load(object sender, EventArgs e)
		{
			CheckAccess();
            txtStatus.Text = status_title;
		}

		private void CheckAccess()
		{
			lblCount.Text = "Всего: " + lstScan.Items.Count + "шт.";
			if(lstScan.Items.Count == 0)
			{
				btnClear.Enabled = false;
				btnOk.Enabled = false;
				btnDeleteList.Enabled = false;
			}
			else
			{
				btnClear.Enabled = true;
				btnOk.Enabled = true;
				btnDeleteList.Enabled = true;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			frmDialogYesNo f = new frmDialogYesNo();
			f.Message = "Закрыть окно для добавления заказов?\nДанные сканирования будут утеряны.";
			f.Title = "Отмена сканирования";
			f.ShowDialog();
			if(f.DialogResult == DialogResult.Yes)
			{
				this.Close();
			}
		}

		private void frmInventoryScan_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))
			{
				if (e.KeyChar == (char)Keys.Return)
				{
					try
					{
						if(txtScan.Text.Length == 12)
						{
							bool add = true;
							for(int i=0;i<lstScan.Items.Count;i++)
							{
								if(lstScan.Items[i].ToString() == txtScan.Text)
								{
									add = false;
									break;
								}
							}
							foreach (string _oldscan in oldnumbers)
							{
								if (_oldscan.Split(':')[0] == txtScan.Text)
								{
									add = false;
									MessageBox.Show("Заказ с номером " + txtScan.Text + " уже добавлен ранее в статусе " + _oldscan.Split(':')[1] + ",\nчто бы добавить заказ с новым статусом веринтесь в документ инвентаризации и удалите из списка этот номер.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
									break;
								}
							}
							if (add)
							{
								lstScan.Items.Insert(0, txtScan.Text);
								
								CheckAccess();
							}
							txtScan.Text = "";
						}
						e.Handled = true;
					}
					catch (Exception ex)
					{
					}
				}
				else if (e.KeyChar == (char)Keys.Escape)
				{
					txtScan.Text = "";
					e.Handled = true;
				}
				else
				{
					if(txtScan.Text.Length < 12)
					{
						txtScan.Text += e.KeyChar.ToString();
						e.Handled = false;
					}
				}
			}
			else
			{
				e.Handled = true;
			}
			label2.Focus();

		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			frmDialogYesNo f = new frmDialogYesNo();
			f.Message = "Вы действительно хотите очистить список заказов?";
			f.Title = "Очистка списка";
			f.ShowDialog();
			if(f.DialogResult == DialogResult.Yes)
			{
				lstScan.Items.Clear();
			}
			CheckAccess();
		}

		private void btnDeleteList_Click(object sender, EventArgs e)
		{
			if(lstScan.SelectedIndex >= 0)
			{
				frmDialogYesNo f = new frmDialogYesNo();
				f.Message = "Вы действительно хотите удалить выбранный номер заказа?";
				f.Title = "Удаление из списка";
				f.ShowDialog();
				if (f.DialogResult == DialogResult.Yes)
				{
					lstScan.Items.RemoveAt(lstScan.SelectedIndex);
				}
			}
			CheckAccess();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			frmDialogYesNo f = new frmDialogYesNo();
			f.Message = "Завершить сканирование и вернуться к документу?";
			f.Title = "Завершение сканирования";
			f.ShowDialog();
			if (f.DialogResult == DialogResult.Yes)
			{
				for(int i = 0; i < lstScan.Items.Count; i++)
				{
					numbers.Add(lstScan.Items[i].ToString());
				}
				this.Close();
			}
			
		}
	}
}

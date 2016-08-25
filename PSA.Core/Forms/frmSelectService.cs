using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Components.FilterRow;
using Photoland.Lib;
using System.IO;

namespace Photoland.Forms.Interface
{
	public partial class frmSelectService : Form
	{
		private SqlConnection db_connection;
		private FilterRow fRow;
		private SqlDataAdapter db_adapter;
		private SqlCommand db_command;
		private DataTable tbl;
		private bool add_to_order;
		private string orderno;
		PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		private string _id_serv = "0";
        public string id_serv
		{
			get { return _id_serv; }
		}

		private string _text_serv;
		public string text_serv
		{
			get { return _text_serv; }
		}

		private string _stext_serv;
		public string stext_serv
		{
			get { return _stext_serv; }
		}

		private List<string> _content = new List<string>();
		public List<string> Content
		{
			get { return _content; }
		}

		private decimal _content_count = 0;
		public decimal Content_count
		{
			get { return _content_count; }
		}

		private bool _defect = false;
		public bool Defect
		{
			get { return _defect; }
		}

		private string _goodslist;
		public string goodsList
		{
			get { return _goodslist; }
		}


		private decimal _defect_count = 0;
		public decimal Defect_count
		{
			get { return _defect_count; }
		}

		private int _defect_type = 0;
		public int Defect_type
		{
			get { return _defect_type; }
		}

		private int _defect_user_id = 0;
		public int Defect_user_id
		{
			get { return _defect_user_id; }
		}

		private string _defect_user;
		public string Defect_user
		{
			get { return _defect_user; }
		}

		private string Date = "";

		public frmSelectService(SqlConnection db_connection, bool add_to_order, string OrderNo)
		{
			InitializeComponent();
			this.Text = "Выбор услуги";
			this.db_connection = db_connection;
			this.add_to_order = add_to_order;
			this.orderno = OrderNo;
			ReBild();
			fRow = new FilterRow(gridServices);
		}

		public frmSelectService(SqlConnection db_connection, bool add_to_order, string OrderNo, string Date)
		{
			InitializeComponent();
			this.Text = "Выбор услуги";
			this.db_connection = db_connection;
			this.add_to_order = add_to_order;
			this.orderno = OrderNo;
			ReBild();
			fRow = new FilterRow(gridServices);
			this.Date = Date;
		}

		public frmSelectService(SqlConnection db_connection, bool add_to_order, string OrderNo, bool defect, string goodslist)
		{
			InitializeComponent();
			this.Text = "Выбор услуги";
			this.db_connection = db_connection;
			this.add_to_order = add_to_order;
			this.orderno = OrderNo;
			this._defect = defect;
			this._goodslist = goodslist;
			ReBild();
			fRow = new FilterRow(gridServices);
		}

		private void ReBild()
		{
			if(Defect)
                db_command = new SqlCommand("SELECT [id_good], [guid], [name], [description], [type], [apply_form], [sign] FROM [vwGoodList] WHERE [id_good] IN (" + goodsList + ") ORDER BY [type], [apply_form], [name]", db_connection);
			else
                db_command = new SqlCommand("SELECT [id_good], [guid], [name], [description], [type], [apply_form], [sign] FROM [vwGoodList] ORDER BY [type], [apply_form], [name]", db_connection);
			db_adapter = new SqlDataAdapter(db_command);
			tbl = new DataTable("Services");
			db_adapter.Fill(tbl);

			tbl.Columns["name"].ColumnName = "Наименование услуги";

			gridServices.DataSource = tbl;

			gridServices.Cols[1].Visible = false;
			gridServices.Cols[2].Visible = false;
			gridServices.Cols[4].Visible = false;
			gridServices.Cols[5].Visible = false;
			gridServices.Cols[6].Visible = false;
			gridServices.Cols[7].Visible = false;

			gridServices.Cols[3].Width = 565;
			gridServices.Cols[3].AllowDragging = false;
			gridServices.Cols[3].AllowEditing = false;
			gridServices.Cols[3].AllowMerging = false;
			gridServices.Cols[3].AllowResizing = true;
			gridServices.Cols[3].AllowSorting = true;
			
			gridServices.Rows.DefaultSize = 20;

            for (int i = 0; i < gridServices.Rows.Count; i++)
            {
                if (gridServices.GetData(i, 5) != null)
                {
                    if (gridServices.GetData(i, 5).ToString().Trim() == "1")
                    {
                        if (gridServices.GetData(i, 6) != null)
                        {
                            if (gridServices.GetData(i, 6).ToString().Trim() == "00001")
                                gridServices.Rows[i].Style = gridServices.Styles["Color1"];
                            if (gridServices.GetData(i, 6).ToString().Trim() == "00002")
                                gridServices.Rows[i].Style = gridServices.Styles["Color2"];
                            if (gridServices.GetData(i, 6).ToString().Trim() == "00003")
                                gridServices.Rows[i].Style = gridServices.Styles["Color3"];
                            if (gridServices.GetData(i, 6).ToString().Trim() == "")
                                gridServices.Rows[i].Style = gridServices.Styles["Color10"];
                        }
                    }
                    if (gridServices.GetData(i, 5).ToString().Trim() == "2")
                    {
                        if (gridServices.GetData(i, 6) != null)
                        {
                            if (gridServices.GetData(i, 6).ToString().Trim() == "00001")
                                gridServices.Rows[i].Style = gridServices.Styles["Color4"];
                            if (gridServices.GetData(i, 6).ToString().Trim() == "00002")
                                gridServices.Rows[i].Style = gridServices.Styles["Color5"];
                            if (gridServices.GetData(i, 6).ToString().Trim() == "00003")
                                gridServices.Rows[i].Style = gridServices.Styles["Color6"];
                            if (gridServices.GetData(i, 6).ToString().Trim() == "")
                                gridServices.Rows[i].Style = gridServices.Styles["Color10"];
                        }
                    }
                    if (gridServices.GetData(i, 5).ToString().Trim() == "3")
                    {
                        if (gridServices.GetData(i, 6) != null)
                        {
                            if (gridServices.GetData(i, 6).ToString().Trim() == "00001")
                                gridServices.Rows[i].Style = gridServices.Styles["Color7"];
                            if (gridServices.GetData(i, 6).ToString().Trim() == "00002")
                                gridServices.Rows[i].Style = gridServices.Styles["Color8"];
                            if (gridServices.GetData(i, 6).ToString().Trim() == "00003")
                                gridServices.Rows[i].Style = gridServices.Styles["Color9"];
                            if (gridServices.GetData(i, 6).ToString().Trim() == "")
                                gridServices.Rows[i].Style = gridServices.Styles["Color10"];
                        }
                    }
                }
            }

		}

        private void SelectServ()
		{
			if (!Defect)
			{
				if (gridServices.GetData(gridServices.Row, 1) != null)
				{
					if (this.add_to_order)
					{
						switch (gridServices.GetData(gridServices.Row, 6).ToString())
						{
							case "00001":
								{
									frmApplyService fApply;
									if(Date!="")
										fApply = new frmApplyService(this.orderno, Date);
									else
										fApply = new frmApplyService(this.orderno);
									fApply.ShowDialog();
									if (fApply.DialogResult == DialogResult.OK)
									{
										this._content = fApply.Content;
										this._content_count = (decimal)this._content.Count;
									}
									else
									{
										frmQueryCount fCount = new frmQueryCount();
										fCount.ShowDialog();
										if (fCount.DialogResult == DialogResult.OK)
										{
											this._content_count = fCount.Count;
										}
										else
										{
											this._content_count = 0;
										}
									}
									fApply.Close();
									break;
								}
							case "00002":
								{
									frmSelectFrame fApply = new frmSelectFrame();
									fApply.ShowDialog();
									if (fApply.DialogResult == DialogResult.OK)
									{
										_content = fApply.Content;
										_content_count = fApply.Content_count;
									}
									else
									{
										frmQueryCount fCount = new frmQueryCount();
										fCount.ShowDialog();
										if (fCount.DialogResult == DialogResult.OK)
										{
											_content_count = fCount.Count;
										}
										else
										{
											_content_count = 0;
										}
									}
									fApply.Close();
									break;
								}
                            case "00003":
                                {
                                    frmQueryFrameParam fApply = new frmQueryFrameParam();
                                    string dir = "";
                                    try
                                    {
                                        orderno = orderno.Trim();
                                        Order.OrderInfo ord = new Photoland.Order.OrderInfo(db_connection, orderno);
                                        string date = "";
                                        if (ord.Datein != null)
                                            date = ord.Datein;
                                        else
                                            date = DateTime.Now.ToShortDateString();
                                        if (!Directory.Exists(prop.Dir_print + "\\" + fso.GetDateSubFolders(date) + "\\" + orderno))
                                            Directory.CreateDirectory(prop.Dir_print + "\\" + fso.GetDateSubFolders(date) + "\\" + orderno);
                                        if (!Directory.Exists(prop.Dir_print + "\\" + fso.GetDateSubFolders(date) + "\\" + orderno + "\\" + gridServices.GetData(gridServices.Row, 7).ToString().Trim() + "_noscan"))
                                            Directory.CreateDirectory(prop.Dir_print + "\\" + fso.GetDateSubFolders(date) + "\\" + orderno + "\\" + gridServices.GetData(gridServices.Row, 7).ToString().Trim() + "_noscan");
                                        dir = prop.Dir_print + "\\" + fso.GetDateSubFolders(date) + "\\" + orderno + "\\" + gridServices.GetData(gridServices.Row, 7).ToString().Trim() + "_noscan";

                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                    fApply.lblPath.Text = dir;
                                    fApply.ShowDialog();
                                    if (fApply.DialogResult == DialogResult.OK)
                                    {
                                        _content.Add("\r\nИнформация к заказу:\r\nФайл: " + fApply.txtFile.Text + "\r\nШирина: " + fApply.txtW.Text + "\r\nВысота: " + fApply.txtH.Text + "\r\nПлощадь: " + fApply.txtS.Text + "\r\nКомментарий: \r\n" + fApply.txtComment.Text);
                                        if (prop.Round3)
                                            _content_count = decimal.Round(decimal.Parse(fApply.txtS.Text), 3);
                                        else
                                            _content_count = decimal.Round(decimal.Parse(fApply.txtS.Text), 2);
                                    }
                                    else
                                    {
                                        _content_count = 0;
                                    }
                                    fApply.Close();
                                    break;
                                }
                            case "00004":
                                {
                                    frmQueryFrameParam2 fApply = new frmQueryFrameParam2();
                                    string dir = "";
                                    fApply.lblPath.Text = dir;
                                    fApply.ShowDialog();
                                    if (fApply.DialogResult == DialogResult.OK)
                                    {
                                        _content.Add("\r\nИнформация к заказу:\r\nФайл: " + fApply.txtFile.Text + "\r\nШирина: " + fApply.txtW.Text + "\r\nВысота: " + fApply.txtH.Text + "\r\nПериметр: " + fApply.txtS.Text + "\r\nКомментарий: \r\n" + fApply.txtComment.Text);
                                        if (prop.Round3)
                                            _content_count = decimal.Round(decimal.Parse(fApply.txtS.Text), 3);
                                        else
                                            _content_count = decimal.Round(decimal.Parse(fApply.txtS.Text), 2);
                                    }
                                    else
                                    {
                                        _content_count = 0;
                                    }
                                    fApply.Close();
                                    break;
                                }
                            default:
								{
									frmQueryCount fCount = new frmQueryCount();
									fCount.ShowDialog();
									if (fCount.DialogResult == DialogResult.OK)
									{
										this._content_count = fCount.Count;
									}
									else
									{
										this._content_count = 0;
									}
									break;
								}
						}
					}
				}
				_id_serv = gridServices.GetData(gridServices.Row, 1).ToString();
				_text_serv = gridServices.GetData(gridServices.Row, 3).ToString();
				_stext_serv = gridServices.GetData(gridServices.Row, 7).ToString();
				if (this.add_to_order)
				{
					if ((this._id_serv != "0") && (this._content_count > 0))
						this.DialogResult = DialogResult.OK;
					else
						this.DialogResult = DialogResult.Cancel;
				}
				else
				{
					if (this._id_serv != "0")
						this.DialogResult = DialogResult.OK;
					else
						this.DialogResult = DialogResult.Cancel;
				}
			}
			else
			{
				frmApplyDefect f = new frmApplyDefect();
				f.db_connection = db_connection;
				f.OrderNo = orderno;
				f.ShowDialog();
				if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
				{
					_id_serv = gridServices.GetData(gridServices.Row, 1).ToString();
					_text_serv = gridServices.GetData(gridServices.Row, 3).ToString();
					_stext_serv = gridServices.GetData(gridServices.Row, 7).ToString();
					_defect_count = decimal.Parse(f.txtCount.Text);
					_defect_type = int.Parse(f.txtType.SelectedValue.ToString());
					_defect_user = f.txtUser.Text;
					_defect_user_id = int.Parse(f.txtUser.SelectedValue.ToString());

					this.DialogResult = System.Windows.Forms.DialogResult.OK;
				}
				else
				{
					this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
				}
			}
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			ReBild();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			SelectServ();
		}

		private void gridServices_DoubleClick(object sender, EventArgs e)
		{
			SelectServ();
		}

		private void frmSelectService_Load(object sender, EventArgs e)
		{
			_id_serv = "0";
			_text_serv = "";
			_stext_serv = "";
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}
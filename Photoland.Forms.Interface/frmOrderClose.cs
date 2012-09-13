using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security;
using Photoland.Security.Client;
using Photoland.Security.Discont;
using Photoland.Security.User;
using Photoland.Lib;
using Photoland.Acceptance.NumGenerator;
using Photoland.Order;
using System.IO;
using PSA.Lib.Interface;
using PSA.Lib.Util;
using System.IO;
using System.Net;

namespace Photoland.Forms.Interface
{
	public partial class frmOrderClose : Form
	{

		public SqlConnection db_connection;
		public OrderInfo order;
	    private OrderInfo etalon_order;
		public bool NewOrder;
		public UserInfo usr;
		public bool fixDouble = false;


		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
		private DataTable tblOrder = new DataTable("Order");
		private DataTable tblOldOrder = new DataTable("Old");
		private DataTable tblNewOrder = new DataTable("New");
		private DataTable tblDefectOrder = new DataTable("defect");
	    private bool paymentonload = false;
	    private decimal sum_vozvrat = 0;

		private bool AddedFinalPayment = false;

		private decimal maxBonusSum = 0;
		private string BonusInfo = "";
		private string how = "";
		private bool newdcard = false;

		
		public frmOrderClose(SqlConnection db_connection, UserInfo usr, int id_order)
		{
			InitializeComponent();
			this.db_connection = db_connection;
			this.usr = usr;

            initOrder(id_order);
            
    	}

        private void initOrder(int id_order)
        {
            this.order = new OrderInfo(db_connection, id_order);
            etalon_order = new OrderInfo(db_connection, order.Id);
            this.Text = "Работа с заказом";

            try
            {
                if (order.AutoExport != 0)
                {
                    SqlCommand db_command = new SqlCommand("SELECT [name] FROM [place] WHERE [id_place] = " + order.Place.ToString(), db_connection);
                    if (order.AutoExport == -1)
                    {
                        lblAdvStatus.Text = "Заказ отправлен в " + db_command.ExecuteScalar().ToString().Trim();
                        lblAdvStatus.ForeColor = Color.Red;
                    }
                    if (order.AutoExport > 0)
                    {
                        lblAdvStatus.Text = "Заказ отправляется в " + db_command.ExecuteScalar().ToString().Trim();
                        lblAdvStatus.ForeColor = Color.Blue;
                    }
                }
            }
            catch { }

            // номер
            lblOrderNo.Text = order.Orderno;
            // дата поступления заказа
            lblDateOrderInput.Text = order.Datein + " " + order.Timein;
            // дата выдачи заказа
            txtOrderDateOutput.Value = DateTime.Parse(order.Dateout);

            double t = prop.Time_begin_work;
            txtOrderTimeOutput.Items.Clear();
            while (t <= double.Parse(prop.Time_end_work.ToString()))
            {
                if (t.ToString().LastIndexOf(",5") > 0)
                {
                    txtOrderTimeOutput.Items.Add(t.ToString().Replace(",5", "") + ":30");
                    if (order.Timeout == t.ToString().Replace(",5", "") + ":30")
                        txtOrderTimeOutput.Text = t.ToString().Replace(",5", "") + ":30";
                }
                else
                {
                    txtOrderTimeOutput.Items.Add(t.ToString() + ":00");
                    if (order.Timeout == t.ToString() + ":00")
                        txtOrderTimeOutput.Text = t.ToString() + ":00";
                }
                t += 0.5;
            }
            // клиент
            if (order.Client != null)
                lblClientName.Text = order.Client.Name;
            else
                lblClientName.Text = "Клиент не определен";
            switch (order.Crop)
            {
                case 1:
                    {
                        radioCrop1.Checked = true;
                        radioCrop2.Checked = false;
                        radioCrop3.Checked = false;
                        break;
                    }
                case 2:
                    {
                        radioCrop2.Checked = true;
                        radioCrop1.Checked = false;
                        radioCrop3.Checked = false;
                        break;
                    }
                case 3:
                    {
                        radioCrop3.Checked = true;
                        radioCrop1.Checked = false;
                        radioCrop2.Checked = false;
                        break;
                    }
            }
            //формат бумаги
            switch (order.Type)
            {
                case 1:
                    {
                        radioPapperType1.Checked = true;
                        radioPapperType2.Checked = false;
                        break;
                    }
                case 2:
                    {
                        radioPapperType2.Checked = true;
                        radioPapperType1.Checked = false;
                        break;
                    }
            }
            //скидка
            if (order.Discont != null)
            {
                lblOrderDiscont.Text = order.Discont.Discserv + "%";
                if (order.Discont.Code_dcard.Trim() != "")
                {
                    btnDescont.Enabled = false;
                    btnDescont.Visible = false;
                    btnDescontClear.Enabled = false;
                    btnDescontClear.Visible = false;
                    btnBonusAdd.Enabled = false;
                    btnBonusAdd.Visible = false;
                    btnBonusDel.Enabled = false;
                    btnBonusDel.Visible = false;
                    newdcard = false;
                }
                else
                {
                    newdcard = true;
                }
            }
            // предоплата
            lblOrderAdvancedPayment.Text = order.AdvancedPayment.ToString();

            //коментрарий
            txtComment.Text = order.Comment;

            if (order.Preview == 1)
                checkPreview.Checked = true;
            else
                checkPreview.Checked = false;

            if (order.FinalPayment == 0)
                paymentonload = false;
            else
                paymentonload = true;

			if (fixDouble)
			{
				btnDescont.Enabled = false;
				btnDescontClear.Enabled = false;
				btnBonusAdd.Enabled = false;
				btnBonusDel.Enabled = false;
				btnFinalPayment.Enabled = false;
				btnFinalPaymentClear.Enabled = false;
			}

			SqlCommand _ptype_cmd = new SqlCommand();
			_ptype_cmd.Connection = db_connection;
			_ptype_cmd.CommandText = "SELECT * FROM PTYPE WHERE ([DEL] = 0) OR (ID_PTYPE = " + order.PType + ") ORDER BY ID_PTYPE";
			_ptype_cmd.CommandTimeout = 9000;
			SqlDataAdapter _ptype_da = new SqlDataAdapter(_ptype_cmd);
			DataTable _ptype_tbl = new DataTable();
			_ptype_da.Fill(_ptype_tbl);
			DataRow _ptype_r = _ptype_tbl.NewRow();
			_ptype_r["id_ptype"] = -1;
			_ptype_r["name_ptype"] = "Не выбрано!";
			_ptype_tbl.Rows.InsertAt(_ptype_r, 0);
			txtPType.DataSource = _ptype_tbl;
			txtPType.DisplayMember = "name_ptype";
			txtPType.ValueMember = "id_ptype";
			txtPType.SelectedValue = -1;

			txtPType.SelectedValue = order.PType;

            if (order.Konvert == 1)
                checkKonvert.Checked = true;
            else
                checkKonvert.Checked = false;



                tmr.Stop();
            tmr.Interval = prop.UpdateOrderTableInAcceptance * 1000;
            tmr.Start();
        }

		private void frmOrderClose_Load(object sender, EventArgs e)
		{

			gridOrder.Rows.DefaultSize = 25;

            tblOrder.Columns.Clear();
			tblOrder.Columns.Add("check", Type.GetType("System.Boolean"));				//  0
			tblOrder.Columns.Add("Name", Type.GetType("System.String"));				//  1
			tblOrder.Columns.Add("id", Type.GetType("System.String"));					//  2
			tblOrder.Columns.Add("Count", Type.GetType("System.Decimal"));				//  3
			tblOrder.Columns.Add("ActCount", Type.GetType("System.Decimal"));			//  4
			tblOrder.Columns.Add("Price", Type.GetType("System.Decimal"));				//  5
			tblOrder.Columns.Add("Sum", Type.GetType("System.Decimal"));				//  6
			tblOrder.Columns.Add("ActSum", Type.GetType("System.Decimal"));				//  7
			tblOrder.Columns.Add("Folder", Type.GetType("System.String"));				//  8
			tblOrder.Columns.Add("Sign", Type.GetType("System.String"));				//  9
			tblOrder.Columns.Add("old", Type.GetType("System.Boolean"));				// 10
			tblOrder.Columns.Add("Content", Type.GetType("System.Object"));				// 11
			tblOrder.Columns.Add("guid", Type.GetType("System.String"));				// 12


			tblOrder.Columns.Add("datework", Type.GetType("System.String"));			// 13
			tblOrder.Columns.Add("id_user_work", Type.GetType("System.Int32"));			// 14
			tblOrder.Columns.Add("name_work", Type.GetType("System.String"));			// 15
			tblOrder.Columns.Add("defect_quantity", Type.GetType("System.Decimal"));	// 16
			tblOrder.Columns.Add("id_user_defect", Type.GetType("System.Int32"));		// 17
			tblOrder.Columns.Add("user_defect", Type.GetType("System.String"));			// 18
			tblOrder.Columns.Add("tech_defect", Type.GetType("System.Int32"));			// 19
			tblOrder.Columns.Add("mashine", Type.GetType("System.String"));				// 20
            tblOrder.Columns.Add("material", Type.GetType("System.String"));			// 21
            tblOrder.Columns.Add("add_user", Type.GetType("System.String"));			// 22
            tblOrder.Columns.Add("comment", Type.GetType("System.String"));			    // 23
            tblOrder.Rows.Clear();

            tblNewOrder.Columns.Clear();
			tblNewOrder.Columns.Add("id_serv", Type.GetType("System.String"));	//0
			tblNewOrder.Columns.Add("Name", Type.GetType("System.String"));		//1
			tblNewOrder.Columns.Add("SName", Type.GetType("System.String"));	//2
			tblNewOrder.Columns.Add("Count", Type.GetType("System.Decimal"));	//3
			tblNewOrder.Columns.Add("Content", Type.GetType("System.Object"));	//4
            tblNewOrder.Columns.Add("guid", Type.GetType("System.String"));		//5
            tblNewOrder.Columns.Add("comment", Type.GetType("System.String"));	//6
            tblNewOrder.Rows.Clear();

            tblDefectOrder.Columns.Clear();
			tblDefectOrder.Columns.Add("id_serv", Type.GetType("System.String"));	//0
			tblDefectOrder.Columns.Add("Name", Type.GetType("System.String"));		//1
			tblDefectOrder.Columns.Add("type", Type.GetType("System.Int32"));		//2
			tblDefectOrder.Columns.Add("Count", Type.GetType("System.Decimal"));	//3
			tblDefectOrder.Columns.Add("defuser", Type.GetType("System.String"));	//4
			tblDefectOrder.Columns.Add("defuserid", Type.GetType("System.Int32"));	//5
			tblDefectOrder.Columns.Add("guid", Type.GetType("System.String"));		//6
			tblDefectOrder.Columns.Add("mashine", Type.GetType("System.String"));	//7
            tblDefectOrder.Columns.Add("material", Type.GetType("System.String"));	//8
			tblDefectOrder.Columns.Add("comment", Type.GetType("System.String"));	//9
			tblDefectOrder.Columns.Add("price", Type.GetType("System.Decimal"));	//10
            tblDefectOrder.Rows.Clear();

			tblOldOrder = order.OrderBody; 
			ReBild();

			FillQuickButtons();
		}

		private void ReBild()
		{
			tmr.Stop();
			ReBildTable();
			ReCalcOrder();
			CheckStatus();
			tmr.Start();
		}

		private void CheckStatus()
		{
            hideActions.Visible = false;

            btnQuickServ1.Enabled = false;
            btnQuickServ2.Enabled = false;
            btnQuickServ3.Enabled = false;
            btnQuickServ4.Enabled = false;
            btnQuickServ5.Enabled = false;
            btnQuickServ6.Enabled = false;
            btnQuickServ7.Enabled = false;
            btnQuickServ8.Enabled = false;
            btnQuickServ9.Enabled = false;
            btnQuickServ10.Enabled = false;
            btnOK.Enabled = false;
            btnOrderDesigner.Enabled = false;
            btnOrderEnd.Enabled = false;
            btnOrderEndZ.Enabled = false;
            btnOrderEndZ.Enabled = false;
            btnOrderEnd.Visible = false;
            btnOrderEndZ.Visible = false;
            btnOrderPrint.Enabled = false;
            btnToWork.Enabled = false;
            btnOrderCancel.Enabled = false;
            btnAddServ.Enabled = false;
            btnDelServ.Enabled = false;
		    btnOrderEndF.Enabled = false;
            btnFinalPayment.Enabled = false;
            btnFinalPaymentClear.Enabled = false;
			txtPType.Enabled = false;
            checkKonvert.Enabled = false;

            lblStatus.Text = "Статус не определен";
            
            switch (order.Distanation.Trim())
			{
                // Готово после печати
                case "000111":
                    {
                        btnDelServ.Enabled = false;
                        btnOK.Enabled = false;
                        lblStatus.Text = "Готово после печати";
                        btnHow.Enabled = true;
                        hideActions.Visible = true;

                        break;
                    }
                // Готово после обработки
                case "000211":
                    {
                        btnDelServ.Enabled = false;
                        btnOK.Enabled = false;
                        lblStatus.Text = "Готово после обработки";
                        btnHow.Enabled = true;
                        hideActions.Visible = true;

                        break;
                    }
                // Готово после обработки
                case "000212":
                    {
                        btnDelServ.Enabled = false;
                        btnOK.Enabled = false;
                        lblStatus.Text = "Готово после обработки, предпросмотр";
                        btnHow.Enabled = true;
                        hideActions.Visible = true;
                        checkKonvert.Enabled = true;

                        break;
                    }
                //В очереди на печать
                case "000100":
                    {
                        hideActions.Visible = false;
                        btnDelServ.Enabled = true;
						btnAddServ.Enabled = true;
                        btnReCalc.Enabled = true;
                        btnOK.Enabled = true;
                        checkKonvert.Enabled = true;
                        //checkPreview.Enabled = true;
						if (Decimal.Parse(lblOrderSum.Text) == 0)
							btnOrderCancel.Enabled = true;
						else
							btnOrderCancel.Enabled = false;

                        lblStatus.Text = "В очереди на печать";

                        break;
                    }
                // У дизайнера
				case "000200":
					{
                        hideActions.Visible = false;
                        btnAddServ.Enabled = true;
						btnDelServ.Enabled = true;
                        checkKonvert.Enabled = true;
						if (prop.Qbtn01_id != "0")
							btnQuickServ1.Enabled = true;
						else
							btnQuickServ1.Enabled = false;
						if (prop.Qbtn02_id != "0")
							btnQuickServ2.Enabled = true;
						else
							btnQuickServ2.Enabled = false;
						if (prop.Qbtn03_id != "0")
							btnQuickServ3.Enabled = true;
						else
							btnQuickServ3.Enabled = false;
						if (prop.Qbtn04_id != "0")
							btnQuickServ4.Enabled = true;
						else
							btnQuickServ4.Enabled = false;
						if (prop.Qbtn05_id != "0")
							btnQuickServ5.Enabled = true;
						else
							btnQuickServ5.Enabled = false;
						if (prop.Qbtn06_id != "0")
							btnQuickServ6.Enabled = true;
						else
							btnQuickServ6.Enabled = false;
						if (prop.Qbtn07_id != "0")
							btnQuickServ7.Enabled = true;
						else
							btnQuickServ7.Enabled = false;
						if (prop.Qbtn08_id != "0")
							btnQuickServ8.Enabled = true;
						else
							btnQuickServ8.Enabled = false;
						if (prop.Qbtn09_id != "0")
							btnQuickServ9.Enabled = true;
						else
							btnQuickServ9.Enabled = false;
						if (prop.Qbtn10_id != "0")
							btnQuickServ10.Enabled = true;
						else
							btnQuickServ10.Enabled = false;
						btnReCalc.Enabled = true;
						btnOK.Enabled = true;
						//checkPreview.Enabled = true;
						if (Decimal.Parse(lblOrderSum.Text) == 0)
							btnOrderCancel.Enabled = true;
						else
							btnOrderCancel.Enabled = false;

						lblStatus.Text = "В очереди на обработку";

						break;
					}
				//В процессе печати
				case "000110":
					{
                        hideActions.Visible = false;

						lblStatus.Text = "В процессе печати";
                        checkKonvert.Enabled = true;
						break;
					}
				//В процессе обработки дизайнером
				case "000210":
					{
                        hideActions.Visible = false;

						lblStatus.Text = "В процессе обработки дизайнером";
                        checkKonvert.Enabled = true;
						break;
					}
				// На предпросмотре
				case "000010":
					{
                        hideActions.Visible = false;
                        
                        btnAddServ.Enabled = true;
						btnDelServ.Enabled = true;
						if (prop.Qbtn01_id != "0")
							btnQuickServ1.Enabled = true;
						else
							btnQuickServ1.Enabled = false;
						if (prop.Qbtn02_id != "0")
							btnQuickServ2.Enabled = true;
						else
							btnQuickServ2.Enabled = false;
						if (prop.Qbtn03_id != "0")
							btnQuickServ3.Enabled = true;
						else
							btnQuickServ3.Enabled = false;
						if (prop.Qbtn04_id != "0")
							btnQuickServ4.Enabled = true;
						else
							btnQuickServ4.Enabled = false;
						if (prop.Qbtn05_id != "0")
							btnQuickServ5.Enabled = true;
						else
							btnQuickServ5.Enabled = false;
						if (prop.Qbtn06_id != "0")
							btnQuickServ6.Enabled = true;
						else
							btnQuickServ6.Enabled = false;
						if (prop.Qbtn07_id != "0")
							btnQuickServ7.Enabled = true;
						else
							btnQuickServ7.Enabled = false;
						if (prop.Qbtn08_id != "0")
							btnQuickServ8.Enabled = true;
						else
							btnQuickServ8.Enabled = false;
						if (prop.Qbtn09_id != "0")
							btnQuickServ9.Enabled = true;
						else
							btnQuickServ9.Enabled = false;
						if (prop.Qbtn10_id != "0")
							btnQuickServ10.Enabled = true;
						else
							btnQuickServ10.Enabled = false;
						btnFinalPayment.Enabled = false;
						btnFinalPaymentClear.Enabled = false;
						btnOrderDesigner.Enabled = false;
						btnOrderPrint.Enabled = false;
						btnToWork.Enabled = true;
						btnOrderEnd.Enabled = false;
                        btnDescont.Enabled = false;
						//btnDescontClear.Enabled = true;
						btnReCalc.Enabled = true;
                        checkKonvert.Enabled = true;
						

						btnOrderEnd.Enabled = false;
						btnOrderEndF.Enabled = false;
                        btnOrderEndZ.Enabled = false;

						CheckStatus2();

						lblStatus.Text = "На предпросмотре";

						break;
					}
				// В ожидании оплаты (безнал)
				case "000001":
					{
                        hideActions.Visible = false;

                        btnAddServ.Enabled = true;
						btnDelServ.Enabled = true;
						if (prop.Qbtn01_id != "0")
							btnQuickServ1.Enabled = true;
						else
							btnQuickServ1.Enabled = false;
						if (prop.Qbtn02_id != "0")
							btnQuickServ2.Enabled = true;
						else
							btnQuickServ2.Enabled = false;
						if (prop.Qbtn03_id != "0")
							btnQuickServ3.Enabled = true;
						else
							btnQuickServ3.Enabled = false;
						if (prop.Qbtn04_id != "0")
							btnQuickServ4.Enabled = true;
						else
							btnQuickServ4.Enabled = false;
						if (prop.Qbtn05_id != "0")
							btnQuickServ5.Enabled = true;
						else
							btnQuickServ5.Enabled = false;
						if (prop.Qbtn06_id != "0")
							btnQuickServ6.Enabled = true;
						else
							btnQuickServ6.Enabled = false;
						if (prop.Qbtn07_id != "0")
							btnQuickServ7.Enabled = true;
						else
							btnQuickServ7.Enabled = false;
						if (prop.Qbtn08_id != "0")
							btnQuickServ8.Enabled = true;
						else
							btnQuickServ8.Enabled = false;
						if (prop.Qbtn09_id != "0")
							btnQuickServ9.Enabled = true;
						else
							btnQuickServ9.Enabled = false;
						if (prop.Qbtn10_id != "0")
							btnQuickServ10.Enabled = true;
						else
							btnQuickServ10.Enabled = false;
						btnFinalPayment.Enabled = true;
						btnFinalPaymentClear.Enabled = true;
						btnOrderDesigner.Enabled = false;
						btnOrderPrint.Enabled = false;
						btnToWork.Enabled = true;
						btnOrderEnd.Enabled = false;
                        btnDescont.Enabled = false;
						//btnDescontClear.Enabled = true;
						btnReCalc.Enabled = true;
                        checkKonvert.Enabled = true;

                        if (decimal.Parse(lblOrderSumFinal.Text) == 0)
                        {
                            btnFinalPayment.Enabled = false;
                            btnOrderEnd.Enabled = false;
                            btnOrderEndF.Enabled = false;
                            btnOrderEndZ.Enabled = false;
                        }
                        else
                        {

                            btnOrderEnd.Enabled = true;
                            btnOrderEndF.Enabled = true;
                            btnOrderEndZ.Enabled = true;
                        }

						if (int.Parse(txtPType.SelectedValue.ToString()) == -1)
							txtPType.Enabled = true;

						CheckStatus2();

						lblStatus.Text = "В ожидании оплаты (безнал)";
                        break;
                    }
				// на выдаче
				case "000000":
					{
                        hideActions.Visible = false;

                        btnDelServ.Enabled = true;
						btnDescont.Enabled = true;
						btnDescontClear.Enabled = true;
						btnFinalPayment.Enabled = true;
						btnFinalPaymentClear.Enabled = false;
						btnReCalc.Enabled = true;
						try
						{
							if (decimal.Parse(lblOrderSumFinal.Text) <= 0)
							{
								btnOrderEnd.Enabled = true;
								btnOrderEndF.Enabled = true;
							}
							else
							{
								btnOrderEnd.Enabled = false;
								btnOrderEndF.Enabled = false;
							}

							if (decimal.Parse(lblOrderSumFinal.Text) == 0)
							{
								btnFinalPayment.Enabled = false;
								btnDescont.Enabled = false;
								btnDescontClear.Enabled = false;
							}
							else
							{
								btnFinalPayment.Enabled = true;
								btnDescont.Enabled = true;
								btnDescontClear.Enabled = true;
							}
						}
						catch (Exception ex)
						{
							ErrorNfo.WriteErrorInfo(ex);
						}
						btnOK.Enabled = true;
						btnReCalc.Enabled = true;
						lblStatus.Text = "На выдаче";

						btnOrderEnd.Enabled = true;
						btnOrderEndZ.Enabled = true;

						if (int.Parse(txtPType.SelectedValue.ToString()) == -1)
							txtPType.Enabled = true;

						CheckStatus2();

						break;
					}
				// Выдано
				case "100000":
					{
                        hideActions.Visible = false;

                        btnDelServ.Enabled = true;
						btnOK.Enabled = true;
						lblStatus.Text = "Выдано";
						btnHow.Enabled = true;
					    btnReCalc.Enabled = true;

						break;
					}
                // Отменено
                case "010000":
                    {
                        hideActions.Visible = false;

                        btnDelServ.Enabled = false;
                        btnOK.Enabled = false;
                        lblStatus.Text = "Отменено";
                        btnHow.Enabled = true;

                        break;
                    }
				// Утеряно
				case "300000":
					{
						hideActions.Visible = false;

						btnDelServ.Enabled = false;
						btnOK.Enabled = false;
						lblStatus.Text = "Утеряно";
						btnHow.Enabled = true;

						break;
					}
				// Списано
				case "400000":
					{
						hideActions.Visible = false;

						btnDelServ.Enabled = false;
						btnOK.Enabled = false;
						lblStatus.Text = "Списано";
						btnHow.Enabled = true;

						break;
					}
				// Выдано (не оплачено)
                case "200000":
                    {
                        hideActions.Visible = false;
                        btnReCalc.Enabled = true;

                        btnDelServ.Enabled = true;
                        btnOK.Enabled = true;
                        lblStatus.Text = "Выдано (не оплачено)";
                        btnOrderEnd.Enabled = true;
                        btnOrderEndZ.Enabled = false;
                        try
                        {
                            if (decimal.Parse(lblOrderSumFinal.Text) == 0)
                            {
                                btnFinalPayment.Enabled = false;
                            }
                            else
                            {
                                btnFinalPayment.Enabled = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorNfo.WriteErrorInfo(ex);
                        }

                        break;
                    }
                default:
					{
                        hideActions.Visible = false;

                        btnQuickServ1.Enabled = false;
						btnQuickServ2.Enabled = false;
						btnQuickServ3.Enabled = false;
						btnQuickServ4.Enabled = false;
						btnQuickServ5.Enabled = false;
						btnQuickServ6.Enabled = false;
						btnQuickServ7.Enabled = false;
						btnQuickServ8.Enabled = false;
						btnQuickServ9.Enabled = false;
						btnQuickServ10.Enabled = false;
						btnOK.Enabled = false;
						btnOrderDesigner.Enabled = false;
						btnOrderEnd.Enabled = false;
						btnOrderEndZ.Enabled = false;
						btnOrderEndZ.Enabled = false;
						btnOrderEnd.Visible = false;
						btnOrderEndZ.Visible = false;
						btnOrderPrint.Enabled = false;
						btnToWork.Enabled = false;
						btnOrderCancel.Enabled = false;
						btnAddServ.Enabled = false;
						btnDelServ.Enabled = false;
						txtPType.Enabled = false;

						lblStatus.Text = "Статус не определен";

						break;
					}
			}
            if (!prop.DontLockExported)
            {
                if (order.AutoExport != 0)
                {
                    hideActions.Visible = false;

                    btnQuickServ1.Enabled = false;
                    btnQuickServ2.Enabled = false;
                    btnQuickServ3.Enabled = false;
                    btnQuickServ4.Enabled = false;
                    btnQuickServ5.Enabled = false;
                    btnQuickServ6.Enabled = false;
                    btnQuickServ7.Enabled = false;
                    btnQuickServ8.Enabled = false;
                    btnQuickServ9.Enabled = false;
                    btnQuickServ10.Enabled = false;
                    btnOK.Enabled = false;
                    btnOrderDesigner.Enabled = false;
                    btnOrderEnd.Enabled = false;
                    btnOrderEndZ.Enabled = false;
                    btnOrderEndZ.Enabled = false;
                    btnOrderEnd.Visible = false;
                    btnOrderEndZ.Visible = false;
                    btnOrderPrint.Enabled = false;
                    btnToWork.Enabled = false;
                    btnOrderCancel.Enabled = false;
                    btnAddServ.Enabled = false;
                    btnDelServ.Enabled = false;
                    txtPType.Enabled = false;
                }
            }


			//if (("_" + order.Client.Category_name.ToLower()).IndexOf("оптов") > 0) 
            if (order.Client != null)
            {
                if ((order.Client.Name.ToUpper().Substring(0, 2) == "БН") || (("_" + order.Client.Category_name.ToLower()).IndexOf("оптов") > 0))
                {
                    //btnOrderEnd.Enabled = true;
                    //btnOrderEndZ.Enabled = true;
                    //btnOrderEndF.Enabled = false;
                    btnOrderEnd.Visible = true;
                    btnOrderEndZ.Visible = true;
                    btnOrderEndF.Visible = false;
                }
                else
                {
                    //btnOrderEnd.Enabled = false;
                    //btnOrderEndZ.Enabled = false;
                    //btnOrderEndF.Enabled = true;
                    btnOrderEnd.Visible = false;
                    btnOrderEndZ.Visible = false;
                    btnOrderEndF.Visible = true;
                }
            }
            else
            {
                btnOrderEnd.Visible = false;
                btnOrderEndZ.Visible = false;
                btnOrderEndF.Visible = true;
            }


			if (((order.Orderno.Substring(0, 1) == prop.Order_terminal_prefics.Substring(0, 1)) && (prop.Terminal_control_worker)) &&
				((order.Distanation.Trim() != "010000")  && (order.Distanation.Trim() != "300000") && (order.Distanation.Trim() != "400000"))
				)
			{
				bool bad = false;
				for (int i = 0; i < tblOldOrder.Rows.Count; i++)
				{
					if (tblOldOrder.Rows[i]["name_work"].ToString().Trim() == "")
					{
						bad = true;
						break;
					}
				}
				if (bad)
				{
					hideWorker.Visible = true;
					try
					{
						if (decimal.Parse(lblOrderSumFinal.Text) <= 0)
						{
							btnOrderEnd.Enabled = true;
							btnOrderEndF.Enabled = true;
						}
						else
						{
							btnOrderEnd.Enabled = false;
							btnOrderEndF.Enabled = false;
						}

						if (decimal.Parse(lblOrderSumFinal.Text) == 0)
						{
							btnFinalPayment.Enabled = false;
							btnDescont.Enabled = false;
							btnDescontClear.Enabled = false;
							//button3.Enabled = false;
						}
						else
						{
							btnFinalPayment.Enabled = true;
							btnDescont.Enabled = true;
							btnDescontClear.Enabled = true;
							button3.Enabled = true;
						}
						if (decimal.Parse(lblOrderAdvancedPayment.Text) > 0)
						{
							btnFinalPayment.Enabled = false;
							btnDescont.Enabled = false;
							btnDescontClear.Enabled = false;
							button3.Enabled = false;
						}
					}
					catch (Exception ex)
					{
						ErrorNfo.WriteErrorInfo(ex);
					}
				}
				else
				{
					hideWorker.Visible = false;
				}
			}
			else
			{
				hideWorker.Visible = false;
			}

			if (fixDouble)
			{
				panelFixDouble.Visible = true;
				hideActions.Visible = false;
				btnQuickServ1.Enabled = false;
				btnQuickServ2.Enabled = false;
				btnQuickServ3.Enabled = false;
				btnQuickServ4.Enabled = false;
				btnQuickServ5.Enabled = false;
				btnQuickServ6.Enabled = false;
				btnQuickServ7.Enabled = false;
				btnQuickServ8.Enabled = false;
				btnQuickServ9.Enabled = false;
				btnQuickServ10.Enabled = false;
				btnOK.Enabled = false;
				btnOrderDesigner.Enabled = false;
				btnOrderEnd.Enabled = false;
				btnOrderEndZ.Enabled = false;
				btnOrderEndZ.Enabled = false;
				btnOrderEnd.Visible = false;
				btnOrderEndZ.Visible = false;
				btnOrderPrint.Enabled = false;
				btnToWork.Enabled = false;
				btnOrderCancel.Enabled = false;
				btnAddServ.Enabled = false;
				btnDelServ.Enabled = false;

				btnDescont.Enabled = false;
				btnDescontClear.Enabled = false;
				btnBonusAdd.Enabled = false;
				btnBonusDel.Enabled = false;
				btnFinalPayment.Enabled = false;
				btnFinalPaymentClear.Enabled = false;
				if ((order.AdvancedPayment > 0) || (order.FinalPayment > 0))
				{
					btnFixDouble.Enabled = false;
					lblFixDouble.Text = "Нельзя отменить заказ, по которому уже принимались деньги!";
				}
				else
				{
					btnFixDouble.Enabled = true;
					lblFixDouble.Text = "При отмене заказу будет присвоен новый номер с префиксом " + (order.Orderno.Substring(0, 1) == prop.Order_terminal_prefics.Substring(0, 1) ? "49" : "59");
				}
			}
            if (!usr.prmCanLoginAdmin)
                btnDelServ.Enabled = false;

		}

		private void CheckStatus2()
		{
            if (order.Distanation.Trim() == "000000")
            {
                if (order.Discont != null)
                {
                    if (order.Discont.Name_dcard.Trim() != "")
                    {
                        btnDescont.Enabled = false;
                        btnDescontClear.Enabled = true;
                    }
                    else
                    {
                        btnDescont.Enabled = true;
                        btnDescontClear.Enabled = false;
                    }
                    if (order.Discont.Bonus > 0)
                    {
                        btnBonusAdd.Enabled = true;
                        btnBonusDel.Enabled = false;
                    }
                    else
                    {
                        btnBonusAdd.Enabled = false;
                        btnBonusDel.Enabled = false;
                    }
                }
                else
                {
                    btnDescont.Enabled = true;
                    btnDescontClear.Enabled = false;
                    btnBonusAdd.Enabled = false;
                    btnBonusDel.Enabled = false;
                }

                if (order.Bonus > 0)
                {
                    btnBonusAdd.Enabled = false;
                    btnBonusDel.Enabled = true;
                }

                if (decimal.Parse(lblOrderSumFinal.Text) <= 0)
                {
                    btnDescont.Enabled = false;
                    btnDescontClear.Enabled = false;
                    btnBonusAdd.Enabled = false;
                    btnBonusDel.Enabled = false;
                }
                if(!paymentonload)
                {
                    btnFinalPaymentClear.Enabled = true;
                }
                else
                {
                    btnFinalPaymentClear.Enabled = false;
                }
            }

			if (fixDouble)
			{
				btnDescont.Enabled = false;
				btnDescontClear.Enabled = false;
				btnBonusAdd.Enabled = false;
				btnBonusDel.Enabled = false;
				btnFinalPayment.Enabled = false;
				btnFinalPaymentClear.Enabled = false;
			}
		}

		// Пересчет заказа
		private void ReCalcOrder()
		{
			how = "Как получилсь такие цифры:\n\n";
			if (this.order != null)
			{

				decimal sum = 0;

                DataTable _t = new DataTable("calc");
                _t.Columns.Add("check");                //0
                _t.Columns.Add("Name");                 //1
                _t.Columns.Add("id");                   //2
                _t.Columns.Add("count");                //3
                _t.Columns.Add("ActCount");             //4
                _t.Columns.Add("Price");                //5
                _t.Columns.Add("Sum");                  //6
                _t.Columns.Add("ActSum");               //7
                _t.Columns.Add("folder");               //8
                _t.Columns.Add("sign");                 //9
                _t.Columns.Add("old");                  //10
                _t.Columns.Add("Content");              //11
                _t.Columns.Add("guid");                 //12
                _t.Columns.Add("datework");             //13
                _t.Columns.Add("id_user_work");         //14
                _t.Columns.Add("name_work");            //15
                _t.Columns.Add("defect_quantity");      //16
                _t.Columns.Add("id_user_defect");       //17
                _t.Columns.Add("user_defect");          //18
                _t.Columns.Add("tech_defect");          //19
                _t.Columns.Add("mashine");              //20
                _t.Columns.Add("material");             //21

				// сбор данных
				for (int i = 0; i < this.order.OrderBody.Rows.Count; i++)
				{
					bool found = false;
					for (int j = 0; j < _t.Rows.Count; j++)
					{
						// если эта строка уже скопирована
						if (order.OrderBody.Rows[i][2].ToString() == _t.Rows[j][2].ToString())
						{
							// суммируем количество
							// обнуляем стоимость
							// ставим флаг дублежа
							_t.Rows[j][3] = decimal.Parse(_t.Rows[j][3].ToString()) + decimal.Parse(order.OrderBody.Rows[i][3].ToString());
							_t.Rows[j][4] = decimal.Parse(_t.Rows[j][4].ToString()) + decimal.Parse(order.OrderBody.Rows[i][4].ToString());
							_t.Rows[j][5] = decimal.Parse(order.OrderBody.Rows[i][5].ToString());
							found = true;
						}
					}
					if (!found)
					{
						_t.ImportRow(order.OrderBody.Rows[i]);
					}
				}

				maxBonusSum = 0;
				// заполняем прайс по фактическому количеству и выгружаем назад
				for (int i = 0; i < _t.Rows.Count; i++)
				{
					if (prop.Order_terminal_prefics.Substring(0, 1) != order.Orderno.Substring(0, 1))
						_t.Rows[i][5] = GetPrice(_t.Rows[i][2].ToString(), decimal.Parse(_t.Rows[i][4].ToString()));
					_t.Rows[i][6] = decimal.Parse(_t.Rows[i][5].ToString()) * decimal.Parse(_t.Rows[i][3].ToString());
					_t.Rows[i][7] = decimal.Parse(_t.Rows[i][5].ToString()) * decimal.Parse(_t.Rows[i][4].ToString());
					how += "-----------------------------------------------\n";
					how += "Услуга: " + _t.Rows[i][1].ToString().Trim() + "\n";
					how += "Количество (выполнено): " + decimal.Parse(_t.Rows[i][4].ToString()).ToString("N2") + "\n";
					how += "Стоимость с учетом скидки по кол-ву: " + decimal.Parse(_t.Rows[i][5].ToString()).ToString("N2") + "\n";
					how += "Сумма по услуге: " + decimal.Parse(_t.Rows[i][7].ToString()).ToString("N2") + "\n";
					if (CanPayBonus(_t.Rows[i][2].ToString()))
					{
						maxBonusSum += decimal.Parse(_t.Rows[i][5].ToString()) * decimal.Parse(_t.Rows[i][4].ToString());
					}
					for (int j = 0; j < order.OrderBody.Rows.Count; j++)
					{
						if (order.OrderBody.Rows[j][2].ToString() == _t.Rows[i][2].ToString())
						{
							order.OrderBody.Rows[j][5] = _t.Rows[i][5];
							order.OrderBody.Rows[j][6] = decimal.Parse(order.OrderBody.Rows[j][5].ToString()) * decimal.Parse(order.OrderBody.Rows[j][4].ToString());
							order.OrderBody.Rows[j][7] = decimal.Parse(order.OrderBody.Rows[j][5].ToString()) * decimal.Parse(order.OrderBody.Rows[j][4].ToString());
						}
					}
				}
				how += "-----------------------------------------------\n";
				//gridOrder.DataSource = _t;

				for (int i = 0; i < this.order.OrderBody.Rows.Count; i++)
				{
					// проверяем, что бы код исполнителя был хоть чем-то, а не null
					if (this.order.OrderBody.Rows[i][14].ToString() != "")
					{
						// значит есть исполнитель, то есть смотрит факт
						if ((int)this.order.OrderBody.Rows[i][14] > 0)
						{
							sum += (decimal)this.order.OrderBody.Rows[i][7];
						}
						// исполнителя нет, значит смотрим заказаное количество
						else
						{
							sum += (decimal)this.order.OrderBody.Rows[i][6];
						}
					}
					// если все таки null, то смотрим заказаное количество
					else
					{
						sum += (decimal)this.order.OrderBody.Rows[i][6];
					}
				}
				how += "Стоимость заказа: " + decimal.Round(sum, 2).ToString() + "\n";
                // если стоимость заказа = 0, то заказ можно отменить
                if (sum == 0)
                    btnOrderCancel.Enabled = true;
                else
                    btnOrderCancel.Enabled = false;
				how += "Оплатить бонусами можно: " + maxBonusSum.ToString() + "\n";
				how += "-----------------------------------------------\n";
				lblOrderSum.Text = sum.ToString("N2");
				if (this.order.Discont != null)
				{
					if (order.Discont.Bonus > 0)
					{
						btnBonusAdd.Enabled = true;
					}
					else
					{
						btnBonusAdd.Enabled = false;
						btnBonusDel.Enabled = false;
					}
					lblOrderDiscont.Text = this.order.Discont.Discserv.ToString() + "%";
					//sum -= sum * (this.order.Discont.Discserv / 100);
					//btnDescont.Enabled = false;
					//btnDescontClear.Enabled = true;
					lblBonusSize.Text = "" + order.Discont.Bonus.ToString("N0");
					BonusInfo = "НА КАРТЕ " + order.Discont.Bonus.ToString("N0") + " БОНУСОВ (РУБ).\n\nОБЩАЯ СТОИМОСТЬ УСЛУГ, ЗА КОТОРЫЕ МОЖНО РАСЧИТАТЬСЯ БОНУСАМИ " + maxBonusSum.ToString("N0") + " РУБ.";
				}
				else
				{
					//btnBonusAdd.Enabled = false;
					//btnBonusDel.Enabled = false;
					//btnDescont.Enabled = true;
					//btnDescontClear.Enabled = false;
					order.Bonus = 0;
					lblBonus.Text = "";
					lblBonusSize.Text = "0";
				}
				if (order.Bonus > 0)
				{
					//btnBonusDel.Enabled = true;
					//btnBonusAdd.Enabled = false;
					how += "Получено бонусов: " + order.Bonus.ToString("N2") + "\n";
				}
				sum = sum - order.Bonus;
				how += "К оплате наличными: " + sum.ToString() + "\n";
				
				if (this.order.Discont != null)
				{
                    if (((order.Discont.BonusType.ToUpper().Trim().IndexOf('G') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('H') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('I') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('J') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('K') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('L') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('M') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('N') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('O') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('P') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('Q') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('R') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('S') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('T') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('U') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('V') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('W') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('X') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('Y') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('Z') > -1)))
					{
						how += "Скидка по купону (сертификату): " + order.Discont.Discserv.ToString("N0") + "% (" +
							   (maxBonusSum * (this.order.Discont.Discserv / 100)).ToString("N2") + " руб)\n";
						sum = (sum - (maxBonusSum * (this.order.Discont.Discserv / 100)));
					}
					else
					{
						how += "Скидка по дисконтной карте: " + order.Discont.Discserv.ToString("N0") + "% (" +
						       (sum*(this.order.Discont.Discserv/100)).ToString("N2") + " руб)\n";
						sum = (sum - (sum*(this.order.Discont.Discserv/100)));
					}
				}
				switch (prop.ModelRound)
				{
					case 0:
						{
							break;
						}
					case 1:
						{
							if (((sum - ((int)sum)) <= (decimal)0.25) && ((sum - ((int)sum)) > 0))
								sum = ((int)sum);
							else if (((sum - ((int)sum)) > (decimal)0.25) && ((sum - ((int)sum)) <= (decimal)0.75))
								sum = ((int)sum) + (decimal)0.5;
							else if ((sum - ((int)sum)) > (decimal)0.75)
								sum = ((int)sum) + 1;
							break;
						}
					case 2:
						{
							if (((sum - ((int)sum)) <= (decimal)0.45) && ((sum - ((int)sum)) > 0))
								sum = ((int)sum);
							else if (((sum - ((int)sum)) > (decimal)0.45) && ((sum - ((int)sum)) <= (decimal)0.95))
								sum = ((int)sum) + (decimal)0.5;
							else if ((sum - ((int)sum)) > (decimal)0.95)
								sum = ((int)sum) + 1;
							break;
						}
					case 3:
						{
							if (((sum - ((int)sum)) <= (decimal)0.15) && ((sum - ((int)sum)) > 0))
								sum = (int)sum;
							else if (((sum - ((int)sum)) > (decimal)0.15) && ((sum - ((int)sum)) <= (decimal)0.65))
								sum = ((int)sum) + (decimal)0.5;
							else if ((sum - ((int)sum)) > (decimal)0.65)
								sum = ((int)sum) + 1;
							break;
						}
					case 4:
						{
							if (((sum - ((int)sum)) <= (decimal)0.49) && ((sum - ((int)sum)) > 0))
								sum = (int)sum;
							else if ((sum - ((int)sum)) > (decimal)0.49)
								sum = ((int)sum) + 1;
							break;
						}
					case 5:
						{
							if (((sum - ((int)sum)) <= (decimal)0.5) && ((sum - ((int)sum)) > 0))
								sum = ((int)sum) + (decimal)0.5;
							else if ((sum - ((int)sum)) > (decimal)0.5)
								sum = ((int)sum) + 1;
							break;
						}
				}
                order.Order_price = sum;
				sum = sum - order.AdvancedPayment;
                how += "Предоплата: " + this.order.AdvancedPayment.ToString() + "\n";
				how += "-----------------------------------------------\n";
				
				how += "К оплате: " + sum.ToString() + "\n";
				lblOrderAdvancedPayment.Text = this.order.AdvancedPayment.ToString("N2");
				//sum -= this.order.AdvancedPayment;
				//sum -= this.order.FinalPayment;
				sum -= this.order.FinalPayment;
			    how += "Всего получено: " + (order.FinalPayment + order.AdvancedPayment).ToString() + "\n";
                if (sum < 0)
                    how += "Возврат: " + (sum * (-1)).ToString() + "\n";
			    sum_vozvrat = sum;
				lblOrderSumFinal.Text = sum.ToString("N2");
				decimal tmp = this.order.AdvancedPayment + this.order.FinalPayment;
				lblTotalPayment.Text = tmp.ToString("N2");
				lblBonus.Text = order.Bonus.ToString("N2");
			}
		}

		private bool CanPayBonus(string p)
		{
			bool ret = false;
			if(order.Discont != null)
			{
				if(order.Discont.BonusType != "")
				{
					SqlCommand tmp_bonus = new SqlCommand("SELECT COUNT(*) FROM [good] WHERE [id_good] = '" + p.Trim() + "' AND [bonustype] LIKE '%" + order.Discont.BonusType.Trim() + "%'", db_connection);
					int cn = (int)tmp_bonus.ExecuteScalar();
					if (cn > 0)
						ret = true;
					else
						ret = false;
				}
			}
			return ret;
		}

		private void ReBildTable()
		{
			tblOrder.Rows.Clear();

			gridOrder.DataSource = tblOrder;
			for (int i = 0; i < tblOldOrder.Rows.Count; i++)
			{
				object[] r = new object[24];
				r[0] = tblOldOrder.Rows[i][0];
				r[1] = tblOldOrder.Rows[i][1];
				r[2] = tblOldOrder.Rows[i][2];
				r[3] = tblOldOrder.Rows[i][3];
				r[4] = tblOldOrder.Rows[i][4];
				r[5] = tblOldOrder.Rows[i][5];
				r[6] = tblOldOrder.Rows[i][6];
				r[7] = tblOldOrder.Rows[i][7];
				r[8] = "";
				r[9] = tblOldOrder.Rows[i][8];
				r[10] = tblOldOrder.Rows[i][9];
				r[12] = tblOldOrder.Rows[i][10];
				r[13] = tblOldOrder.Rows[i][11];
				r[14] = tblOldOrder.Rows[i][12];
				r[15] = tblOldOrder.Rows[i][13];
				r[16] = tblOldOrder.Rows[i][14];
				r[17] = tblOldOrder.Rows[i][15];
				r[18] = tblOldOrder.Rows[i][16];
				r[19] = tblOldOrder.Rows[i][17];
                r[22] = tblOldOrder.Rows[i][18];
                r[23] = tblOldOrder.Rows[i][19];

				tblOrder.Rows.Add(r);
			}

			gridOrder.DataSource = tblOrder;

			for (int i = 0; i < tblNewOrder.Rows.Count; i++)
			{
				object[] r = new object[24];
				r[0] = false;
				r[1] = tblNewOrder.Rows[i][1];
				r[2] = tblNewOrder.Rows[i][0];
				r[3] = tblNewOrder.Rows[i][3];
				r[4] = (decimal)0;
				r[5] = GetPrice(tblNewOrder.Rows[i][0].ToString(), decimal.Parse(r[3].ToString()));
				r[6] = (decimal)r[3] * (decimal)r[5];
				r[7] = (decimal)0;
				r[8] = "";
				r[9] = "+";
				r[10] = false;
				r[11] = tblNewOrder.Rows[i][4];
                r[12] = tblNewOrder.Rows[i][5];

				r[13] = 0;
				r[14] = 0;
				r[15] = "";
				r[16] = 0;
				r[17] = 0;
				r[18] = "";
				r[19] = 0;
				r[20] = "";
				r[21] = "";
                r[22] = usr.Name;
                r[23] = tblNewOrder.Rows[i][6];
                /// todo: !!!

				tblOrder.Rows.Add(r);
			}

			for (int i = 0; i < tblDefectOrder.Rows.Count; i++)
			{
				object[] r = new object[24];
				r[0] = false;
				r[1] = tblDefectOrder.Rows[i][1];
				r[2] = tblDefectOrder.Rows[i][0];
				r[4] = (decimal)tblDefectOrder.Rows[i][3] * -1;
				r[3] = (decimal)0;
				if (order.Orderno.Substring(0, 1) == prop.Order_terminal_prefics.Substring(0, 1))
					r[5] = decimal.Parse(tblDefectOrder.Rows[i][10].ToString());
				else
					r[5] = GetPrice(tblDefectOrder.Rows[i][0].ToString(), decimal.Parse(r[4].ToString()));
				r[6] = (decimal)r[3] * (decimal)r[5];
				r[7] = (decimal)r[4] * (decimal)r[5];
				r[8] = "";
				r[9] = "-";
				r[10] = false;
				r[12] = tblDefectOrder.Rows[i][6];

				r[13] = tblDefectOrder.Rows[i][0];
				r[14] = (int)tblDefectOrder.Rows[i][5];
				r[15] = tblDefectOrder.Rows[i][4];

				if ((decimal)tblDefectOrder.Rows[i][3] < 0)
					r[16] = (decimal)tblDefectOrder.Rows[i][3] * -1;
				else 
					r[16] = (decimal)tblDefectOrder.Rows[i][3];

				r[17] = tblDefectOrder.Rows[i][5];
				r[18] = tblDefectOrder.Rows[i][4];
				r[19] = tblDefectOrder.Rows[i][2];
				r[20] = tblDefectOrder.Rows[i][7];
                r[21] = tblDefectOrder.Rows[i][8];
                r[22] = usr.Name;
                r[23] = tblDefectOrder.Rows[i][9];

				tblOrder.Rows.Add(r);
			}
			for (int i = 1; i < gridOrder.Rows.Count; i++)
			{
				try
				{
					if (!(bool) gridOrder.Rows[i][11])
						gridOrder.Rows[i].Style = gridOrder.Styles["Green"];
					if ((int) gridOrder.Rows[i][20] > 0)
						gridOrder.Rows[i].Style = gridOrder.Styles["Red"];
				}
				catch(Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);
				}
			}

			this.order.OrderBody = tblOrder;

			gridOrder.Cols[1].Visible = false;
			gridOrder.Cols[2].Visible = false;
			gridOrder.Cols[3].Visible = false;
			gridOrder.Cols[4].Visible = false;
			gridOrder.Cols[5].Visible = false;
			gridOrder.Cols[6].Visible = false;
			gridOrder.Cols[7].Visible = false;
			gridOrder.Cols[8].Visible = false;
			gridOrder.Cols[9].Visible = false;
			gridOrder.Cols[10].Visible = false;
			gridOrder.Cols[11].Visible = false;
			gridOrder.Cols[12].Visible = false;
			gridOrder.Cols[13].Visible = false;
			gridOrder.Cols[14].Visible = false;
			gridOrder.Cols[15].Visible = false;
			gridOrder.Cols[16].Visible = false;
			gridOrder.Cols[17].Visible = false;
			gridOrder.Cols[18].Visible = false;
			gridOrder.Cols[19].Visible = false;
			gridOrder.Cols[20].Visible = false;
			gridOrder.Cols[21].Visible = false;
            gridOrder.Cols[22].Visible = false;
            gridOrder.Cols[23].Visible = false;
            gridOrder.Cols[24].Visible = false;

            

			gridOrder.Cols[2].Visible = true;
			gridOrder.Cols[2].Width = 527;
			gridOrder.Cols[2].AllowDragging = false;
			gridOrder.Cols[2].AllowEditing = false;
			gridOrder.Cols[2].AllowMerging = false;
			gridOrder.Cols[2].AllowResizing = true;
			gridOrder.Cols[2].AllowSorting = true;
			gridOrder.Cols[2].Caption = "Название услуги";

			gridOrder.Cols[4].Visible = true;
			gridOrder.Cols[4].Width = 100;
			gridOrder.Cols[4].AllowDragging = false;
			gridOrder.Cols[4].AllowEditing = false;
			gridOrder.Cols[4].AllowMerging = false;
			gridOrder.Cols[4].AllowResizing = true;
			gridOrder.Cols[4].AllowSorting = true;
			gridOrder.Cols[4].Caption = "Количество";
			if (prop.Round3)
				gridOrder.Cols[4].Format = "N3";
			else
				gridOrder.Cols[4].Format = "N2";

			gridOrder.Cols[5].Visible = true;
			gridOrder.Cols[5].Width = 100;
			gridOrder.Cols[5].AllowDragging = false;
			gridOrder.Cols[5].AllowEditing = false;
			gridOrder.Cols[5].AllowMerging = false;
			gridOrder.Cols[5].AllowResizing = true;
			gridOrder.Cols[5].AllowSorting = true;
			gridOrder.Cols[5].Caption = "Факт. кол.";
			if (prop.Round3)
				gridOrder.Cols[5].Format = "N3";
			else
				gridOrder.Cols[5].Format = "N2";

			gridOrder.Cols[6].Visible = true;
			gridOrder.Cols[6].Width = 100;
			gridOrder.Cols[6].AllowDragging = false;
			gridOrder.Cols[6].AllowEditing = false;
			gridOrder.Cols[6].AllowMerging = false;
			gridOrder.Cols[6].AllowResizing = true;
			gridOrder.Cols[6].AllowSorting = true;
			gridOrder.Cols[6].Caption = "Стоимость";
			gridOrder.Cols[6].Format = "N2";

			gridOrder.Cols[7].Visible = false;
			gridOrder.Cols[7].Width = 100;
			gridOrder.Cols[7].AllowDragging = false;
			gridOrder.Cols[7].AllowEditing = false;
			gridOrder.Cols[7].AllowMerging = false;
			gridOrder.Cols[7].AllowResizing = true;
			gridOrder.Cols[7].AllowSorting = true;
			gridOrder.Cols[7].Caption = "Сумма";
			gridOrder.Cols[7].Format = "N2";

            gridOrder.Cols[8].Visible = true;
            gridOrder.Cols[8].Width = 100;
            gridOrder.Cols[8].AllowDragging = false;
            gridOrder.Cols[8].AllowEditing = false;
            gridOrder.Cols[8].AllowMerging = false;
            gridOrder.Cols[8].AllowResizing = true;
            gridOrder.Cols[8].AllowSorting = true;
            gridOrder.Cols[8].Caption = "Сумма";
            gridOrder.Cols[8].Format = "N2";

            gridOrder.Cols[23].Visible = true;
            gridOrder.Cols[23].Width = 300;
            gridOrder.Cols[23].AllowDragging = false;
            gridOrder.Cols[23].AllowEditing = false;
            gridOrder.Cols[23].AllowMerging = false;
            gridOrder.Cols[23].AllowResizing = true;
            gridOrder.Cols[23].AllowSorting = true;
            gridOrder.Cols[23].Caption = "Добавил";

            gridOrder.Cols[16].Visible = true;
            gridOrder.Cols[16].Width = 300;
            gridOrder.Cols[16].AllowDragging = false;
            gridOrder.Cols[16].AllowEditing = false;
            gridOrder.Cols[16].AllowMerging = false;
            gridOrder.Cols[16].AllowResizing = true;
            gridOrder.Cols[16].AllowSorting = true;
            gridOrder.Cols[16].Caption = "Выполнил/Списание";

            gridOrder.Cols[24].Visible = true;
            gridOrder.Cols[24].Width = 500;
            gridOrder.Cols[24].AllowDragging = false;
            gridOrder.Cols[24].AllowEditing = false;
            gridOrder.Cols[24].AllowMerging = false;
            gridOrder.Cols[24].AllowResizing = true;
            gridOrder.Cols[24].AllowSorting = true;
            gridOrder.Cols[24].Caption = "Комментарии";

            //gridOrder.Cols.Move(23, 9);
            //gridOrder.Cols.Move(15, 10);
            //gridOrder.Cols.Move(18, 11);
            //gridOrder.Cols.Move(24, 12);

        }

		// Добавляет услугу из быстрой кнопки
		private void SelectQuickService(string id)
		{
			if (prop.Order_terminal_prefics.Substring(0, 1) != order.Orderno.Substring(0, 1))
			{
				tmr.Stop();
				List<string> _content = new List<string>();
				decimal _content_count = 0;
				string _id_serv = "";
				string _text_serv = "";
				string _stext_serv = "";

				SqlCommand db_command = new SqlCommand("SELECT [id_good], [guid], [name], [description], [type], [apply_form], [sign] FROM [vwGoodList] WHERE [id_good] = '" + id + "'", db_connection);
				SqlDataReader db_reader = db_command.ExecuteReader();
				if (db_reader.Read())
				{
					if (!db_reader.IsDBNull(5))
					{
						switch (db_reader.GetString(5))
						{
							case "00001":
								{
									frmApplyService fApply = new frmApplyService(order.Orderno, order.Datein);
									fApply.ShowDialog();
									if (fApply.DialogResult == DialogResult.OK)
									{
										_content = fApply.Content;
										_content_count = (decimal)_content.Count;
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
							default:
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
									break;
								}
						}
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
					if (!db_reader.IsDBNull(0))
						_id_serv = db_reader.GetString(0);
					if (!db_reader.IsDBNull(2))
						_text_serv = db_reader.GetString(2);
					if (!db_reader.IsDBNull(6))
						_stext_serv = db_reader.GetString(6);
				}
				if ((_id_serv != "") && (_content_count > 0))
				{
					AddToServTable(_id_serv, _text_serv, _stext_serv, _content_count, _content);
				}

				db_reader.Close();

				tmr.Start();
			}
			else
			{
				MessageBox.Show("Нельзя добавлять услуги в данный тип заказов!");
			}
		}

		// Получить стоимость по коду
        private decimal GetPrice(string id, decimal cnt)
        {
            if (order.Client != null)
            {
                string tmp_id_good = id;
                int tmp_id_client_category = int.Parse(order.Client.Id_category.ToString());
                decimal pr = 0;
                bool err = false;
                if (cnt < 0) cnt *= -1;
                //SqlCommand db_command = new SqlCommand("SELECT [id_good], [id_category], [amount] FROM [vwPriceFull] WHERE [id_good] = '" + tmp_id_good + "' AND [id_category] = " + tmp_id_client_category.ToString(), db_connection);
                SqlCommand db_command = new SqlCommand("spAdvPrice", db_connection);
                db_command.Parameters.Add(new SqlParameter("@id_good", id));
                db_command.Parameters.Add(new SqlParameter("@id_category", tmp_id_client_category));
                db_command.Parameters.Add(new SqlParameter("@threshold", cnt));
                db_command.Parameters.Add(new SqlParameter("@ondate", DateTime.Parse(order.Datein)));
                db_command.CommandType = CommandType.StoredProcedure;
                decimal price;
                try
                {
                    price = (decimal)db_command.ExecuteScalar();
                    {
                        if (price > 0)
                        {
                            pr = price;
                        }
                        else
                        {
                            SqlCommand tmp =
                                new SqlCommand("SELECT CAST([zero] as BIT) AS [zero] FROM [good] WHERE [id_good] = '" + tmp_id_good + "'",
                                               db_connection);
                            bool z = (bool)tmp.ExecuteScalar();
                            if (!z)
                            {
                                MessageBox.Show("Внимание! Не найдена цена в прайсе!\nПроверьте заполние прайса!", "Ошибка получения цены",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                err = true;
                            }
                            else
                            {
                                err = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SqlCommand tmp =
                        new SqlCommand("SELECT CAST([zero] as BIT) AS [zero] FROM [good] WHERE [id_good] = '" + tmp_id_good + "'",
                                       db_connection);
                    bool z = (bool)tmp.ExecuteScalar();
                    if (!z)
                    {
                        MessageBox.Show("Внимание! Не найдена цена в прайсе!\nПроверьте заполние прайса!", "Ошибка получения цены",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        err = true;
                    }
                    else
                    {
                        err = false;
                    }
                }
                if (!err)
                    return pr;
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }



		private void btnQuickServ1_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn01_id);
		}

		private void btnQuickServ2_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn02_id);
		}

		private void btnQuickServ3_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn03_id);
		}

		private void btnQuickServ4_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn04_id);
		}

		private void btnQuickServ5_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn05_id);
		}

		private void btnQuickServ6_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn06_id);
		}

		private void btnQuickServ7_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn07_id);
		}

		private void btnQuickServ8_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn08_id);
		}

		private void btnQuickServ9_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn09_id);
		}

		private void btnQuickServ10_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn10_id);
		}

		// Добавляет строку в таблицу услуг
		private void AddToServTable(string id, string name, string sname, decimal count, List<string> content)
		{
			tmr.Stop();
			object[] row = new object[7];
			row[0] = id;
			row[1] = name;
			row[2] = sname;
			row[3] = count;
			row[4] = content;
			row[5] = Guid.NewGuid().ToString();
            foreach (string s in content)
                row[6] += s.Replace("\r", "").Replace("\n", " ").Trim() + " ";
            if (row[6] != null)
            {
                if (row[6].ToString().Length > 1020)
                    row[6] = row[6].ToString().Substring(0, 1020);
            }
            else
            {
                row[6] = "";
            }
			tblNewOrder.Rows.Add(row);
			tmr.Start();
			
		}

		// Добавляет строку в таблицу списание
		private void AddToDefTable(string id, string name, int type, decimal count, string user, int user_id, string mashine_id, string material_id, string comment)
		{
			tmr.Stop();
			object[] row = new object[11];
			row[0] = id;
			row[1] = name;
			row[2] = type;
			row[3] = count;
			row[4] = user;
			row[5] = user_id;
			row[6] = Guid.NewGuid().ToString();
			row[7] = mashine_id;
			row[8] = material_id;
			if (comment.Length > 1020)
				comment = comment.Substring(0, 1020);
			row[9] = comment;
			row[10] = 0;
			tblDefectOrder.Rows.Add(row);
			tmr.Start();
		}

		// Добавляет строку в таблицу списание
		private void AddToDefTable(string id, string name, int type, decimal count, string user, int user_id, string mashine_id, string material_id, string comment, decimal Price)
		{
			tmr.Stop();
			object[] row = new object[11];
			row[0] = id;
			row[1] = name;
			row[2] = type;
			row[3] = count;
			row[4] = user;
			row[5] = user_id;
			row[6] = Guid.NewGuid().ToString();
			row[7] = mashine_id;
			row[8] = material_id;
			if (comment.Length > 1020)
				comment = comment.Substring(0, 1020);
			row[9] = comment;
			row[10] = Price;
			tblDefectOrder.Rows.Add(row);
			tmr.Start();
		}

		// Привязка кнопок быстрого вызова
		private void FillQuickButtons()
		{
			btnQuickServ1.Text = prop.Qbtn01_stext.Trim();
			btnQuickServ2.Text = prop.Qbtn02_stext.Trim();
			btnQuickServ3.Text = prop.Qbtn03_stext.Trim();
			btnQuickServ4.Text = prop.Qbtn04_stext.Trim();
			btnQuickServ5.Text = prop.Qbtn05_stext.Trim();
			btnQuickServ6.Text = prop.Qbtn06_stext.Trim();
			btnQuickServ7.Text = prop.Qbtn07_stext.Trim();
			btnQuickServ8.Text = prop.Qbtn08_stext.Trim();
			btnQuickServ9.Text = prop.Qbtn09_stext.Trim();
			btnQuickServ10.Text = prop.Qbtn10_stext.Trim();

			if (prop.Qbtn01_id == "0")
				btnQuickServ1.Enabled = false;

            if (prop.Qbtn02_id == "0")
				btnQuickServ2.Enabled = false;

            if (prop.Qbtn03_id == "0")
				btnQuickServ3.Enabled = false;

            if (prop.Qbtn04_id == "0")
				btnQuickServ4.Enabled = false;

            if (prop.Qbtn05_id == "0")
				btnQuickServ5.Enabled = false;

            if (prop.Qbtn06_id == "0")
				btnQuickServ6.Enabled = false;

            if (prop.Qbtn07_id == "0")
				btnQuickServ7.Enabled = false;

            if (prop.Qbtn08_id == "0")
				btnQuickServ8.Enabled = false;

            if (prop.Qbtn09_id == "0")
				btnQuickServ9.Enabled = false;

            if (prop.Qbtn10_id == "0")
				btnQuickServ10.Enabled = false;

		}

		private void btnReCalc_Click(object sender, EventArgs e)
		{
			ReBild();
		}

		// Добавляет услугу из списка
		private void SelectService()
		{
			if (prop.Order_terminal_prefics.Substring(0, 1) != order.Orderno.Substring(0, 1))
			{
				tmr.Stop();
				frmSelectService fSelServ = new frmSelectService(db_connection, true, order.Orderno, order.Datein);
				fSelServ.ShowDialog();
				if (fSelServ.DialogResult == DialogResult.OK)
				{
					AddToServTable(fSelServ.id_serv, fSelServ.text_serv, fSelServ.stext_serv, fSelServ.Content_count, fSelServ.Content);
				}
				tmr.Start();
			}
			else
			{
				MessageBox.Show("Нельзя добавлять услуги в данный тип заказов!");
			}
		}

		// Добавляет услугу для списания из списка
		private void SelectServiceDefect()
		{
			tmr.Stop();
			string tmplst = "";
            List<string> tlst = new List<string>();
			for (int i = 0; i < tblOrder.Rows.Count; i++)
			{
				if (i == (tblOrder.Rows.Count - 1))
					tmplst += "'" + tblOrder.Rows[i][2] + "'";
				else
					tmplst += "'" + tblOrder.Rows[i][2] + "',";
                tlst.Add(tblOrder.Rows[i]["id"].ToString().Trim() + ";" + tblOrder.Rows[i]["ActCount"].ToString());
			}

			frmApplyDefect f = new frmApplyDefect();
		    f.goodsfromorder = tlst;
			f.db_connection = db_connection;
			f.OrderNo = order.Orderno;
			f.GoodsFilter = tmplst;
			f.status = this.order.Distanation.Trim();
			f.ShowDialog();
			if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				/*
				_id_serv = gridServices.GetData(gridServices.Row, 1).ToString();
				_text_serv = gridServices.GetData(gridServices.Row, 3).ToString();
				_defect_count = decimal.Parse(f.txtCount.Text);
				_defect_type = int.Parse(f.txtType.SelectedValue.ToString());
				_defect_user = f.txtUser.Text;
				_defect_user_id = int.Parse(f.txtUser.SelectedValue.ToString());
				*/
				if (order.Orderno.Substring(0, 1) == prop.Order_terminal_prefics.Substring(0, 1))
				{
					decimal defPrice = 0;
					for (int i = 0; i < tblOrder.Rows.Count; i++)
					{
						if (tblOrder.Rows[i]["id"].ToString().Trim() == f.txtGood.SelectedValue.ToString().Trim())
						{
							defPrice = Decimal.Parse(tblOrder.Rows[i]["Price"].ToString());
						}
					}
					AddToDefTable(f.txtGood.SelectedValue.ToString(), f.txtGood.Text, int.Parse(f.txtType.SelectedValue.ToString()),
						decimal.Parse(f.txtCount.Text), f.txtUser.Text, int.Parse(f.txtUser.SelectedValue.ToString()),
						f.txtMashine.SelectedValue.ToString(), f.txtPaper.SelectedValue.ToString(), f.txtComment.Text, defPrice);
				}
				else
				{
					AddToDefTable(f.txtGood.SelectedValue.ToString(), f.txtGood.Text, int.Parse(f.txtType.SelectedValue.ToString()),
						decimal.Parse(f.txtCount.Text), f.txtUser.Text, int.Parse(f.txtUser.SelectedValue.ToString()),
						f.txtMashine.SelectedValue.ToString(), f.txtPaper.SelectedValue.ToString(), f.txtComment.Text);
				}

                if ((f.txtType.SelectedValue.ToString().Trim() == "10") && (order.Orderno.Substring(0, 1) != prop.Order_terminal_prefics.Substring(0, 1)))
                {
                    bool _tmp = false;
                    while (!_tmp)
                    {
                        if (
                            MessageBox.Show("Возврат <" + f.txtGood.Text.Trim() + "> отправляется на переделку?\n\nВНИМАНИЕ! ЗАКАЗ БУДЕТ АВТОМАТИЧЕСКИ СОХРАНЕН С НОВЫМИ СТРОЧКАМИ И ПЕРЕНЕСЕН В СТАТУС \"ПРЕДВАРИТЕЛЬНЫЙ ПРОСМОТР\"\n\n",
                                            "Возврат", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            frmQueryCount fcount = new frmQueryCount();
                            fcount.Count = decimal.Parse(f.txtCount.Text);
                            fcount.ShowDialog();
                            if (fcount.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
								AddEvent("Блокирование строчек");
                                _tmp = true;
                                List<string> _tmp_comment = new List<string>();
                                _tmp_comment.Add("Переделка возврата");
                                AddToServTable(f.txtGood.SelectedValue.ToString(), f.txtGood.Text, "", fcount.Count, _tmp_comment);
                                order.AdvancedPayment += order.FinalPayment;
                                order.FinalPayment = 0;
                                ReBild();
                                if(order.UpdateOrder(usr, false, false, false))
                                {
                                    AddEvent("Сделан возврат и переделка");
                                    initOrder(order.Id);
                                    frmOrderClose_Load(this, new EventArgs());
                                    order.Distanation = "000010";
									if (!order.UpdateOrder(usr, false, false, false))
                                    {
                                        MessageBox.Show("При назначении нового статуса произошла ошибка!\n" + order.Err, "Ошибка",
                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        AddEvent("Заказу присвоен новый статус");
                                        initOrder(order.Id);
                                        frmOrderClose_Load(this, new EventArgs());
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Произошла ошибка во время сохранения заказа!", "Ошибка",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                _tmp = false;
                            }
                        }
                        else
                        {
                            _tmp = true;
                        }
                    }
                }

			}
			tmr.Start();

		}

		private void button11_Click(object sender, EventArgs e)
        {
            SelectService();
            ReBild();
        }

        private void gridOrder_DoubleClick(object sender, EventArgs e)
        {
            DelFromServTable(gridOrder.Row - 1);
        }

        // Удаляет из указанной позиции строку
        private void DelFromServTable(int row)
        {
            tmr.Stop();
            try
            {
                string tmp = tblOrder.Rows[row][12].ToString();
                if (!bool.Parse(tblOrder.Rows[row][10].ToString()))
                {
                    if (MessageBox.Show("Удалить: " + tblOrder.Rows[row][1].ToString().Trim() + "?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        tblOrder.Rows.RemoveAt(row);
						for (int i = 0; i < tblNewOrder.Rows.Count; i++)
						{
							if (tblNewOrder.Rows[i][5].ToString() == tmp)
							{
								tblNewOrder.Rows.RemoveAt(i);
							}
						}
						for (int i = 0; i < tblDefectOrder.Rows.Count; i++)
						{
							if (tblDefectOrder.Rows[i][6].ToString() == tmp)
							{
								tblDefectOrder.Rows.RemoveAt(i);
							}
						}
						ReBild();
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя удалить эту строку!\nКоррекция возможна только через возврат/списание.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
				ErrorNfo.WriteErrorInfo(ex);
            }
            tmr.Start();
        }

		private void button12_Click(object sender, EventArgs e)
		{
            if (prop.PasswordClass1.Trim() != "")
            {
                frmGetPassword f = new frmGetPassword();
                f.ShowDialog();
                if ((f.DialogResult == System.Windows.Forms.DialogResult.OK) && (f.Password == prop.PasswordClass1))
                {
                    SelectServiceDefect();
                }
                f.Close();
            }
            else
            {
                SelectServiceDefect();
            }
		    ReBild();
		}

		private void tmr_Tick(object sender, EventArgs e)
		{
			ReBild();
		}

		private void btnFinalPayment_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			this.order.FinalPayment = 0;
			ReCalcOrder();
			frmFinalPayment fPayment = new frmFinalPayment(decimal.Parse(lblOrderSumFinal.Text));
			if (order.Orderno.Substring(0, 1) == prop.Order_terminal_prefics.Substring(0, 1))
				fPayment.size = 0;
			fPayment.ShowDialog();
			this.order.FinalPayment = fPayment.Payment;
			if (order.FinalPayment > 0)
				AddedFinalPayment = true;
			else
				AddedFinalPayment = false;
			ReBild();
			tmr.Start();
		}

		private void btnDescont_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			frmGetDiscont fGetDiscont = new frmGetDiscont();
			fGetDiscont.db_connection = db_connection;
			fGetDiscont.ShowDialog();
			if (fGetDiscont.DialogResult == DialogResult.OK)
			{
				if (fGetDiscont.discont.Code_dcard != "")
				{
					this.order.Discont = fGetDiscont.discont;
					order.Bonus = 0;
				}
				else
				{
					MessageBox.Show("Дисконтная карта не найдена в базе!", "Скидка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.order.Discont = null;
				}
			}
			ReBild();
			tmr.Start();
		}

		private void btnDescontClear_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			this.order.Discont = null;
			lblOrderDiscont.Text = "0%";
			order.Bonus = 0;
			lblBonus.Text = "";
			btnBonusAdd.Enabled = false;
			ReBild();
			tmr.Start();
		}

		private void btnFinalPaymentClear_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			this.order.FinalPayment = 0;
			ReBild();
			tmr.Start();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			try
			{
				string y = DateTime.Parse(lblDateOrderInput.Text).Year.ToString();
				string m = DateTime.Parse(lblDateOrderInput.Text).Month < 10
							? "0" + DateTime.Parse(lblDateOrderInput.Text).Month.ToString()
							: DateTime.Parse(lblDateOrderInput.Text).Month.ToString();
				string d = DateTime.Parse(lblDateOrderInput.Text).Day < 10
							? "0" + DateTime.Parse(lblDateOrderInput.Text).Day.ToString()
							: DateTime.Parse(lblDateOrderInput.Text).Day.ToString();
				string f = prop.Dir_print + "\\" + y + "\\" + m + "\\" + d + "\\" + lblOrderNo.Text.Trim() + "\\" +
						   lblOrderNo.Text.Trim() + ".txt";
				if (File.Exists(f))
				{
					System.Diagnostics.Process.Start(f);
				}
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Ошибка при открытии файла информации", "Модуль приемки", MessageBoxButtons.OK,
								MessageBoxIcon.Warning);
			}

		}

		private void button6_Click(object sender, EventArgs e)
		{
			try
			{
				string y = DateTime.Parse(lblDateOrderInput.Text).Year.ToString();
				string m = DateTime.Parse(lblDateOrderInput.Text).Month < 10
							? "0" + DateTime.Parse(lblDateOrderInput.Text).Month.ToString()
							: DateTime.Parse(lblDateOrderInput.Text).Month.ToString();
				string d = DateTime.Parse(lblDateOrderInput.Text).Day < 10
							? "0" + DateTime.Parse(lblDateOrderInput.Text).Day.ToString()
							: DateTime.Parse(lblDateOrderInput.Text).Day.ToString();
				string f = prop.Dir_edit + "\\" + y + "\\" + m + "\\" + d + "\\" + lblOrderNo.Text.Trim() + "\\" +
						   lblOrderNo.Text.Trim() + ".txt";
				if (File.Exists(f))
				{
					System.Diagnostics.Process.Start(f);
				}
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Ошибка при открытии файла информации", "Модуль приемки", MessageBoxButtons.OK,
								MessageBoxIcon.Warning);
			}

		}

		private void btnOK_Click(object sender, EventArgs e)
		{
            if (order.Distanation.Trim() == "100000")
            {
                if (MessageBox.Show("ВНИМАНИЕ! ВЫ ДЕЙСТВИТЕЛЬНО ХОТИТЕ СОХРАНИТЬ ИЗМЕНЕНИЯ В ВЫДАННОМ ЗАКАЗЕ?", "ВНИМАНИЕ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveOrder(false);
                }

            }
            else
            {
                SaveOrder(false);
            }
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void SaveOrder(bool UseAdvPayment)
		{
			tmr.Stop();
			order.Dateout = txtOrderDateOutput.Value.ToShortDateString() + " " + txtOrderTimeOutput.Text;
			order.Timeout = txtOrderTimeOutput.Text;
			order.PType = int.Parse(txtPType.SelectedValue.ToString());
            order.Konvert = ((checkKonvert.Checked) ? 1 : 0);
			try
			{
				if (((order.Distanation.Trim() == "100000") || (order.Distanation.Trim() == "200000")) && (int.Parse(txtPType.SelectedValue.ToString()) == -1))
				{
					MessageBox.Show("Перед выдачей заказа необходимо указать тип оплаты!", "Закрытие заказа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtPType.Focus();
				}
				else
				{

					OrderInfo etalon_order2 = new OrderInfo(db_connection, etalon_order.Id);
					if (etalon_order.IsSame(etalon_order2))
					{
						bool ok = true;
						int vozvrat = 0;
						if ((sum_vozvrat <= (-1)) && (order.Distanation.Trim() == "100000"))
						{
							ok = false;
							bool _tmp = false;
							while (!_tmp)
							{
								if (MessageBox.Show(
										"Внимание! Необходимо сделать возврат на сумму " +
										(decimal.Round(sum_vozvrat, 2) * (-1)).ToString() + "р.\nПровести возврат?",
										"Возврат", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
								{
									frmQueryStorno fstorno = new frmQueryStorno();
									fstorno.Title = "Вариант возврата";
									if (order.Discont != null)
										fstorno.infoBonus = order.Bonus;
									else
										fstorno.infoBonus = 0;
									fstorno.infoMoney = order.AdvancedPayment + order.FinalPayment;
									fstorno.infoSum = sum_vozvrat * (-1);
									fstorno.goods = false;
									for (int i = 1; i < order.OrderBody.Rows.Count; i++)
									{
										if (CanPayBonus(order.OrderBody.Rows[i][2].ToString().Trim()))
										{
											fstorno.goods = true;
											break;
										}
									}
									if (order.Discont != null)
									{
										if (((order.Discont.BonusType.ToUpper().Trim().IndexOf('G') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('H') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('I') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('J') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('K') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('L') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('M') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('N') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('O') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('P') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('Q') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('R') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('S') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('T') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('U') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('V') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('W') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('X') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('Y') > -1) || (order.Discont.BonusType.ToUpper().Trim().IndexOf('Z') > -1)))
											fstorno.discont = false;
										else
											fstorno.discont = true;
									}
									else
									{
										fstorno.discont = true;
									}
									fstorno.ShowDialog();
									if (fstorno.DialogResult == DialogResult.OK)
									{
										vozvrat = fstorno.type;
										ok = true;
										_tmp = true;
									}
									else
									{
										_tmp = false;
									}
								}
								else
								{
									_tmp = true;
								}
							}
						}
						if (order.Discont != null)
						{
							if ((order.Bonus != 0) && (order.Discont.Id_dcard != 777777777))
							{
								ok = false;
								try
								{
									string key = DateTime.Now.Year.ToString("D4") +
												DateTime.Now.Month.ToString("D2") +
												DateTime.Now.Day.ToString("D2") +
												DateTime.Now.Hour.ToString("D2") +
												DateTime.Now.Minute.ToString("D2") +
												DateTime.Now.Second.ToString("D2") +
												DateTime.Now.Millisecond.ToString("D2");
									HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + prop.DiscontServerAddress + "/card.get.php?code=" + order.Discont.Code_dcard + "&key=" + key + "&format=csv");
									HttpWebResponse response = (HttpWebResponse)request.GetResponse();
									Stream resStream = response.GetResponseStream();
									byte[] buf = new byte[255];
									if (resStream.Read(buf, 0, 255) > 0)
									{
										ok = true;
									}
									else
									{
										ok = false;
										MessageBox.Show("Списание бонусов не возможно т.к. нет связи с сервером!", "Ошибка списания бонусов", MessageBoxButtons.OK, MessageBoxIcon.Error);
									}
								}
								catch
								{
									ok = false;
									MessageBox.Show("Списание бонусов не возможно т.к. нет связи с сервером!", "Ошибка списания бонусов", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
							}

						}

						if (ok)
						{
							if (!order.UpdateOrder(usr, AddedFinalPayment, UseAdvPayment, false))
							{
								MessageBox.Show("При сохранении заказа произошла ошибка!\n" + order.Err, "Ошибка",
												MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							else
							{
								if ((order.Orderno.Substring(0, 1) == prop.Order_terminal_prefics.Substring(0, 1)) && ((order.Distanation.Trim() == "100000") || (order.Distanation.Trim() == "200000")))
								{
									try
									{
										using (SqlConnection cn_tmp = new SqlConnection(prop.Connection_string))
										{
											cn_tmp.Open();
											SqlCommand cmd_tmp = new SqlCommand("INSERT INTO [dbo].[kiosk_orders_ok] ([number], [status], [exported]) VALUES ('" + order.Orderno + "', 2, 0)", cn_tmp);
											cmd_tmp.ExecuteNonQuery();
										}
									}
									catch
									{
									}
								}
								if (order.Discont != null)
								{
									if ((order.Discont.Id_dcard != 777777777) & (order.Bonus != 0))
									{
										try
										{
											string key = DateTime.Now.Year.ToString("D4") +
														DateTime.Now.Month.ToString("D2") +
														DateTime.Now.Day.ToString("D2") +
														DateTime.Now.Hour.ToString("D2") +
														DateTime.Now.Minute.ToString("D2") +
														DateTime.Now.Second.ToString("D2") +
														DateTime.Now.Millisecond.ToString("D2");
											HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + prop.DiscontServerAddress + "/card.action.php?code=" + order.Discont.Code_dcard + "&key=" + key + "&order=" + order.Orderno.Trim() + "&dep=" + prop.Order_prefics + "&action=add&bonus=-" + order.Bonus.ToString().Replace(",", "."));
											HttpWebResponse response = (HttpWebResponse)request.GetResponse();
											Stream resStream = response.GetResponseStream();
											byte[] buf = new byte[255];
											if (resStream.Read(buf, 0, 255) > 0)
											{
												if (Encoding.ASCII.GetString(buf).ToLower().Trim('\0') == "OK. Confirm, please.".ToLower())
												{
													AddEvent("Бонусы зарезервированы для списания");
													request = (HttpWebRequest)WebRequest.Create("http://" + prop.DiscontServerAddress + "/card.action.php?code=" + order.Discont.Code_dcard + "&key=" + key + "&order=" + order.Orderno.Trim() + "&dep=" + prop.Order_prefics + "&action=confirm&bonus=-" + order.Bonus.ToString().Replace(",", "."));
													response = (HttpWebResponse)request.GetResponse();
													resStream = response.GetResponseStream();
													buf = new byte[255];
													resStream.Read(buf, 0, 255);
													if (Encoding.ASCII.GetString(buf).ToLower().Trim('\0') == "OK.".ToLower())
													{
														AddEvent("Потверждено списание с карты " + order.Discont.Code_dcard + " " + order.Bonus.ToString()+ " бонусов");
													}
													else
													{
														AddEvent("Не удалось списать бонусы, повторная попытка");
														key = DateTime.Now.Year.ToString("D4") +
																DateTime.Now.Month.ToString("D2") +
																DateTime.Now.Day.ToString("D2") +
																DateTime.Now.Hour.ToString("D2") +
																DateTime.Now.Minute.ToString("D2") +
																DateTime.Now.Second.ToString("D2") +
																DateTime.Now.Millisecond.ToString("D2");
														request = (HttpWebRequest)WebRequest.Create("http://" + prop.DiscontServerAddress + "/card.action.php?code=" + order.Discont.Code_dcard + "&key=" + key + "&order=" + order.Orderno.Trim() + "&dep=" + prop.Order_prefics + "&action=add&bonus=-" + order.Bonus.ToString().Replace(",", "."));
														response = (HttpWebResponse)request.GetResponse();
														resStream = response.GetResponseStream();
														buf = new byte[255];
														if (resStream.Read(buf, 0, 255) > 0)
														{
															if (Encoding.ASCII.GetString(buf).ToLower().Trim('\0') == "OK. Confirm, please.".ToLower())
															{
																AddEvent("Бонусы зарезервированы для списания");
																request = (HttpWebRequest)WebRequest.Create("http://" + prop.DiscontServerAddress + "/card.action.php?code=" + order.Discont.Code_dcard + "&key=" + key + "&order=" + order.Orderno.Trim() + "&dep=" + prop.Order_prefics + "&action=confirm&bonus=-" + order.Bonus.ToString().Replace(",", "."));
																response = (HttpWebResponse)request.GetResponse();
																resStream = response.GetResponseStream();
																buf = new byte[255];
																if (resStream.Read(buf, 0, 255) > 0)
																{
																	if (Encoding.ASCII.GetString(buf).ToLower().Trim('\0') == "OK.".ToLower())
																	{
																		AddEvent("Потверждено списание с карты " + order.Discont.Code_dcard + " " + order.Bonus.ToString() + " бонусов");
																	}
																	else
																	{
																		AddEvent("Не удалось списать бонусы.");
																	}
																}
															}
															else
															{
																switch (Encoding.ASCII.GetString(buf))
																{
																	case "Not enough bonuses.":
																		{
																			AddEvent("При списании бонусов сервер вернул - Not enough bonuses.");
																			break;
																		}
																	case "Card not found.":
																		{
																			AddEvent("При списании бонусов сервер вернул - Card not found.");
																			break;
																		}
																	case "Action not found.":
																		{
																			AddEvent("При списании бонусов сервер вернул - Action not found.");
																			break;
																		}
																	default:
																		{
																			AddEvent("При списании бонусов сервер вернул - " + Encoding.ASCII.GetString(buf));
																			break;
																		}
																}
															}
														}
														else
														{
															ok = false;
															MessageBox.Show("Списание бонусов не возможно т.к. нет связи с сервером!", "Ошибка списания бонусов", MessageBoxButtons.OK, MessageBoxIcon.Error);
														}
													}
												}
												else
												{
													switch(Encoding.ASCII.GetString(buf))
													{
														case "Not enough bonuses.":
															{
																AddEvent("При списании бонусов сервер вернул - Not enough bonuses.");
																break;
															}
														case "Card not found.":
															{
																AddEvent("При списании бонусов сервер вернул - Card not found.");
																break;
															}
														case "Action not found.":
															{
																AddEvent("При списании бонусов сервер вернул - Action not found.");
																break;
															}
														default:
															{
																AddEvent("При списании бонусов сервер вернул - " + Encoding.ASCII.GetString(buf));
																break;
															}
													}
												}
											}
											else
											{
												ok = false;
												MessageBox.Show("Списание бонусов не возможно т.к. нет связи с сервером!", "Ошибка списания бонусов", MessageBoxButtons.OK, MessageBoxIcon.Error);
											}
										}
										catch
										{
											ok = false;
											MessageBox.Show("Списание бонусов не возможно т.к. нет связи с сервером!", "Ошибка списания бонусов", MessageBoxButtons.OK, MessageBoxIcon.Error);
										}
									}
								}
								AddEvent("Сохранены изменения в заказе (на приемке)");
								if (AddedFinalPayment)
									AddEvent("Внесена оплата за заказ " + order.FinalPayment.ToString().Replace(",", "."));
								if ((sum_vozvrat <= (-1)) && (order.Distanation.Trim() == "100000"))
								{
									// нет больше новых строчек
									for (int i = 1; i < order.OrderBody.Rows.Count; i++)
									{
										order.OrderBody.Rows[i][10] = true;
									}

									if (vozvrat == 1)
									{
										if ((order.Bonus + sum_vozvrat) < 0)
										{
											AddEvent("Сделан возврат бонусов на сумму " +
														 decimal.Round(order.Bonus, 2).ToString().Replace(",", "."));
										}
										else
										{
											AddEvent("Сделан возврат бонусов на сумму " +
														 decimal.Round(sum_vozvrat, 2).ToString().Replace(",", "."));
										}
										// списание с карты, потом наличные
										order.Bonus += sum_vozvrat;
										if (!order.UpdateOrder(usr, false, false, false))
										{
											MessageBox.Show("При сохранении возврата бонусов прозошла ошибка!\n" + order.Err, "Ошибка",
															MessageBoxButtons.OK, MessageBoxIcon.Error);
										}

										if (order.Bonus < 0)
										{
											// бонусов не хватило для возврата;
											SqlCommand cmd_dc =
												new SqlCommand(
													"INSERT INTO [dbo].[payments] ([date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way], [exported]) VALUES (getdate(), '" + DateTime.Now.ToShortTimeString() + "', " + usr.Id_user + ", '" + usr.Name.Trim() + "', '" + order.Orderno + "', " + decimal.Round(order.Bonus, 2).ToString().Replace(",", ".") + ", 1, 'Автоматический возврат средств', 1, 0); UPDATE [dbo].[order] SET [final_payment] = [final_payment] + " + decimal.Round(order.Bonus, 2).ToString().Replace(",", ".") + " WHERE [id_order] = " + order.Id.ToString(),
													db_connection);
											try
											{
												cmd_dc.CommandTimeout = 9000;
												cmd_dc.ExecuteNonQuery();
												AddEvent("Сделан возврат на сумму " +
														 decimal.Round(order.Bonus, 2).ToString().Replace(",", "."));
												order.FinalPayment += order.Bonus;
												order.Bonus = 0;
												if (!order.UpdateOrder(usr, false, false, false))
												{
													MessageBox.Show("При закрытии возвратов произошла ошибка!\n" + order.Err, "Ошибка",
																	MessageBoxButtons.OK, MessageBoxIcon.Error);
												}
												else
												{
												}
											}
											catch
											{
												MessageBox.Show(
													"Ошибка при сохрании возврата, необходимо вручную провести сумму " +
													decimal.Round(order.Bonus, 2).ToString(), "Ошибка сохранения возврата",
													MessageBoxButtons.OK, MessageBoxIcon.Error);
											}

										}

									}
									if (vozvrat == 2)
									{
										if (((order.AdvancedPayment + order.FinalPayment) + sum_vozvrat) < 0)
										{
											SqlCommand cmd_dc =
												new SqlCommand(
													"INSERT INTO [dbo].[payments] ([date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way], [exported]) VALUES (getdate(), '" + DateTime.Now.ToShortTimeString() + "', " + usr.Id_user + ", '" + usr.Name.Trim() + "', '" + order.Orderno + "', " + decimal.Round((order.AdvancedPayment + order.FinalPayment) * (-1), 2).ToString().Replace(",", ".") + ", 1, 'Автоматический возврат средств', 1, 0); UPDATE [dbo].[order] SET [final_payment] = [final_payment] + " + decimal.Round((order.AdvancedPayment + order.FinalPayment) * (-1), 2).ToString().Replace(",", ".") + " WHERE [id_order] = " + order.Id.ToString(),
													db_connection);
											try
											{
												cmd_dc.CommandTimeout = 9000;
												cmd_dc.ExecuteNonQuery();
												AddEvent("Сделан возврат на сумму " +
														 decimal.Round((order.AdvancedPayment + order.FinalPayment) * (-1), 2).ToString().Replace(",", "."));
												sum_vozvrat += (order.AdvancedPayment + order.FinalPayment);
												order.FinalPayment -= (order.AdvancedPayment + order.FinalPayment);
												if (!order.UpdateOrder(usr, false, false, false))
												{
													MessageBox.Show("При закрытии возвратов произошла ошибка!\n" + order.Err, "Ошибка",
																	MessageBoxButtons.OK, MessageBoxIcon.Error);
												}
											}
											catch
											{
												MessageBox.Show(
													"Ошибка при сохрании возврата, необходимо вручную провести сумму " +
													decimal.Round((order.AdvancedPayment + order.FinalPayment) * (-1), 2).ToString(), "Ошибка сохранения возврата",
													MessageBoxButtons.OK, MessageBoxIcon.Error);
											}

										}
										else
										{
											SqlCommand cmd_dc =
												new SqlCommand(
													"INSERT INTO [dbo].[payments] ([date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way], [exported]) VALUES (getdate(), '" + DateTime.Now.ToShortTimeString() + "', " + usr.Id_user + ", '" + usr.Name.Trim() + "', '" + order.Orderno + "', " + decimal.Round(sum_vozvrat, 2).ToString().Replace(",", ".") + ", 1, 'Автоматический возврат средств', 1, 0); UPDATE [dbo].[order] SET [final_payment] = [final_payment] + " + decimal.Round(sum_vozvrat, 2).ToString().Replace(",", ".") + " WHERE [id_order] = " + order.Id.ToString(),
													db_connection);
											try
											{
												cmd_dc.CommandTimeout = 9000;
												cmd_dc.ExecuteNonQuery();
												AddEvent("Сделан возврат на сумму " +
														 decimal.Round(sum_vozvrat, 2).ToString().Replace(",", "."));
												order.FinalPayment += sum_vozvrat;
												sum_vozvrat = 0;
												if (!order.UpdateOrder(usr, false, false, false))
												{
													MessageBox.Show("При закрытии возвратов произошла ошибка!\n" + order.Err, "Ошибка",
																	MessageBoxButtons.OK, MessageBoxIcon.Error);
												}
											}
											catch
											{
												MessageBox.Show(
													"Ошибка при сохрании возврата, необходимо вручную провести сумму " +
													decimal.Round(sum_vozvrat, 2).ToString(), "Ошибка сохранения возврата",
													MessageBoxButtons.OK, MessageBoxIcon.Error);
											}
										}

										if (sum_vozvrat < 0)
										{
											// бонусов не хватило для возврата;

											AddEvent("Сделан возврат бонусов на сумму " +
														 decimal.Round(sum_vozvrat, 2).ToString().Replace(",", "."));

											order.Bonus += sum_vozvrat;
											if (!order.UpdateOrder(usr, false, false, false))
											{
												MessageBox.Show("При сохранении возврата бонусов прозошла ошибка!\n" + order.Err, "Ошибка",
																MessageBoxButtons.OK, MessageBoxIcon.Error);
											}



										}

									}
									/*
									cmd_dc =
										new SqlCommand(
											"INSERT INTO [dbo].[payments] ([date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way], [exported]) VALUES (getdate(), '" + DateTime.Now.ToShortTimeString() + "', " + usr.Id_user + ", '" + usr.Name.Trim() + "', '" + order.Orderno + "', " + decimal.Round(sum_vozvrat, 2).ToString().Replace(",", ".") + ", 1, 'Автоматический возврат средств', 1, 0); UPDATE [dbo].[order] SET [final_payment] = [final_payment] + " + decimal.Round(sum_vozvrat, 2).ToString().Replace(",", ".") + " WHERE [id_order] = " + order.Id.ToString(),
											db_connection);
									try
									{
										cmd_dc.ExecuteNonQuery();
										AddEvent("Сделан возврат на сумму " +
												 decimal.Round(sum_vozvrat, 2).ToString().Replace(",", "."));
									}
									catch
									{
										MessageBox.Show(
											"Ошибка при сохрании возврата, необходимо вручную провести сумму " +
											(decimal.Round(sum_vozvrat, 2)).ToString(), "Ошибка сохранения возврата",
											MessageBoxButtons.OK, MessageBoxIcon.Error);
									}
									 */
								}

								// списание бонусов
								if ((order.Discont.BonusType.Trim() != "Z") && (newdcard))
								{
									SqlCommand cmd_dc =
										new SqlCommand(
											"UPDATE [dbo].[dcard] SET [bonus] = " +
											(order.Discont.Bonus - order.Bonus).ToString().Replace(",", ".") +
											" WHERE [code] = '" + order.Discont.Code_dcard.Trim() + "'", db_connection);
									try
									{
										cmd_dc.ExecuteNonQuery();
										AddEvent("Списано " + order.Bonus + " бонусов");
									}
									catch
									{
									}


									cmd_dc =
										new SqlCommand(
											"DECLARE @C int; SET @C = (SELECT COUNT(*) FROM [dbo].[dcarduse] WHERE [code] = '" +
											order.Discont.Code_dcard.Trim() +
											"'); IF(@C = 0) BEGIN INSERT INTO [dbo].[dcarduse]([code],[cnt]) VALUES ('" +
											order.Discont.Code_dcard.Trim() +
											"',1); END ELSE BEGIN DECLARE @CC int; SET @CC = (SELECT COUNT(*) FROM [dbo].[dcarduse] WHERE [code] = '" +
											order.Discont.Code_dcard.Trim() + "' AND ((lastdate >= CONVERT(DATETIME, '" +
											DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" +
											DateTime.Now.Day.ToString("D2") +
											" 00:00:00', 102)) AND (lastdate <= CONVERT(DATETIME, '" +
											DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" +
											DateTime.Now.Day.ToString("D2") +
											" 23:59:00', 102)))); IF(@CC = 0) BEGIN UPDATE [dbo].[dcarduse] SET [lastdate] = getdate(),[cnt] = 1 WHERE [code] = '" +
											order.Discont.Code_dcard.Trim() +
											"'; END ELSE BEGIN UPDATE [dbo].[dcarduse] SET [lastdate] = getdate(),[cnt] = [cnt] + 1 WHERE [code] = '" +
											order.Discont.Code_dcard.Trim() + "'; END END", db_connection);
									try
									{
										cmd_dc.ExecuteNonQuery();
									}
									catch
									{
									}

								}
								this.Close();
							}
						}
					}

					else
					{
						MessageBox.Show("Заказ не может быть сохранен!\nВероятно он уже был кем то изменен.\n" + order.Err, "Ошибка",
											MessageBoxButtons.OK, MessageBoxIcon.Error);
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("При сохранении заказа произошла ошибка!\n" + ex.Message + "\n" + ex.Source, "Ошибка",
				                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
			}
		}

		private void btnOrderEnd_Click(object sender, EventArgs e)
		{
			bool ok = true;
			try
			{
				if ((order.AdvancedPayment != 0) || (decimal.Parse(lblOrderSumFinal.Text) != 0))
				{
					if (MessageBox.Show("Выдать с оплатой?\nУбедитесь, что оплата внесена.",
						"Сохранение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
						ok = false;
				}
			}
			catch { }
			if (ok)
			{
				order.Distanation = "100000";
				SaveOrder(false);
			}
		}

		private void btnOrderEndZ_Click(object sender, EventArgs e)
		{
			order.Distanation = "200000";
			SaveOrder(false);
		}

		private void btnOrderDesigner_Click(object sender, EventArgs e)
		{
			order.Distanation = "000200";
			order.Preview = 0;
			SaveOrder(false);
		}

		private void btnOrderPrint_Click(object sender, EventArgs e)
		{
			order.Distanation = "000100";
			order.Preview = 0;
			SaveOrder(false);
		}

		private void btnToWork_Click(object sender, EventArgs e)
		{
			frmSelectWork f = new frmSelectWork();
			f.ShowDialog();
			if (f.rez > 0)
			{
				if (f.rez == 1)
				{
					order.Distanation = "000200";
					order.Preview = 0;
					SaveOrder(false);
				}

				if (f.rez == 2)
				{
					order.Distanation = "000100";
					order.Preview = 0;
					SaveOrder(false);
				}
			}
		}

		private void btnOrderCancel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Действительно отменить заказ?", "Отмена заказа", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				order.Distanation = "010000";
				SaveOrder(false);
			}
		}

        private void PrintCheck()
        {
            // Печатаем чек
            OrderInfo prnOrder = new OrderInfo(db_connection, lblOrderNo.Text.Trim(), true);
            try
            {
                if (prop.PathReportsTemplates != "")
                {
                    rep.Load(prop.PathReportsTemplates, "Check2");
                    rep.DataSource.Recordset = prnOrder.OrderBody;
                    decimal itog = 0;
                    decimal iitog = 0;
                    for (int i = 0; i < prnOrder.OrderBody.Rows.Count; i++)
                    {
                        itog += decimal.Parse(prnOrder.OrderBody.Rows[i]["price"].ToString()) *
                                decimal.Parse(prnOrder.OrderBody.Rows[i]["actual_quantity"].ToString());
                    }
					rep.Fields["advstr1"].Text = prop.CheckString1;
					rep.Fields["advstr2"].Text = prop.CheckString2;
					rep.Fields["Total"].Text = itog.ToString().Replace(",", ".");
					rep.Fields["BarCode"].Text = prnOrder.Orderno.Trim();
					rep.Fields["OrderNo"].Text = prnOrder.Orderno.Trim();
                    rep.Fields["DateOut"].Text = prnOrder.Dateout + " " + prnOrder.Timeout;
                    rep.Fields["Client"].Text = prnOrder.Client.Name.Trim();
                    rep.Fields["AddonInfo"].Text = prop.ReklamBlock1;
                    rep.Fields["Priemka"].Text = "Заказ принят: " + prnOrder.Datein + " " + prnOrder.Timein + "\nЗаказ принял: " + prnOrder.Name_accept;
                    string tp = "";
                    switch (prnOrder.Crop)
                    {
                        case 1:
                            {
                                tp = "Обрезать под формат; ";
                                break;
                            }
                        case 2:
                            {
                                tp = "Сохранить пропорции; ";
                                break;
                            }
                        case 3:
                            {
                                tp = "Реальный размер; ";
                                break;
                            }
                    }
                    if (prnOrder.Preview > 0)
                        rep.Fields["PreView"].Visible = true;
                    if (prnOrder.Type == 1) tp += "Глянцевая бумага;";
                    if (prnOrder.Type == 2) tp += "Матовая бумага;";
                    rep.Fields["TypePaper"].Text = tp;
                    if (prnOrder.Discont != null)
                    {
                        rep.Fields["Discont"].Text = prnOrder.Discont.Discserv.ToString().Replace(",", ".");
                        iitog = itog - ((itog * prnOrder.Discont.Discserv) / 100);
                    }
                    else
                    {
                        rep.Fields["Discont"].Text = "0";
                        iitog = itog;
                    }
					switch (prop.ModelRound)
					{
						case 0:
							{
								break;
							}
						case 1:
							{
								if (((iitog - ((int)iitog)) <= (decimal)0.25) && ((iitog - ((int)iitog)) > 0))
									iitog = ((int)iitog);
								else if (((iitog - ((int)iitog)) > (decimal)0.25) && ((iitog - ((int)iitog)) <= (decimal)0.75))
									iitog = ((int)iitog) + (decimal)0.5;
								else if ((iitog - ((int)iitog)) > (decimal)0.75)
									iitog = ((int)iitog) + 1;
								break;
							}
						case 2:
							{
								if (((iitog - ((int)iitog)) <= (decimal)0.45) && ((iitog - ((int)iitog)) > 0))
									iitog = ((int)iitog);
								else if (((iitog - ((int)iitog)) > (decimal)0.45) && ((iitog - ((int)iitog)) <= (decimal)0.95))
									iitog = ((int)iitog) + (decimal)0.5;
								else if ((iitog - ((int)iitog)) > (decimal)0.95)
									iitog = ((int)iitog) + 1;
								break;
							}
						case 3:
							{
								if (((iitog - ((int)iitog)) <= (decimal)0.15) && ((iitog - ((int)iitog)) > 0))
									iitog = (int)iitog;
								else if (((iitog - ((int)iitog)) > (decimal)0.15) && ((iitog - ((int)iitog)) <= (decimal)0.65))
									iitog = ((int)iitog) + (decimal)0.5;
								else if ((iitog - ((int)iitog)) > (decimal)0.65)
									iitog = ((int)iitog) + 1;
								break;
							}
						case 4:
							{
								if (((iitog - ((int)iitog)) <= (decimal)0.49) && ((iitog - ((int)iitog)) > 0))
									iitog = (int)iitog;
								else if ((iitog - ((int)iitog)) > (decimal)0.49)
									iitog = ((int)iitog) + 1;
								break;
							}
						case 5:
							{
								if (((iitog - ((int)iitog)) <= (decimal)0.5) && ((iitog - ((int)iitog)) > 0))
									iitog = ((int)iitog) + (decimal)0.5;
								else if ((iitog - ((int)iitog)) > (decimal)0.5)
									iitog = ((int)iitog) + 1;
								break;
							}
					}
					rep.Fields["Itogo"].Text = iitog.ToString().Replace(",", ".");
                    decimal p = prnOrder.FinalPayment + prnOrder.AdvancedPayment;
                    rep.Fields["Payment"].Text = p.ToString().Replace(",", ".");
                    rep.Fields["EndPayment"].Text = (iitog - p).ToString().Replace(",", ".");
                    if (prop.CheckPreview)
                    {
                        PrintPreviewDialog pd = new PrintPreviewDialog();
                        pd.ClientSize = new Size(465, 680);
                        pd.StartPosition = FormStartPosition.CenterScreen;
                        pd.PrintPreviewControl.Zoom = 1.5;
                        pd.Document = rep.Document;
                        pd.ShowDialog();
                    }
                    else
                    {
                        for (int j = 0; j < prop.CheckCount; j++)
                        {
                            rep.Document.Print();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не выбран файл шаблонов отчетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ErrorNfo.WriteErrorInfo(ex);
                MessageBox.Show("Ошибка вывода чека\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void checkPreview_CheckedChanged(object sender, EventArgs e)
		{
			//if (checkPreview.Checked)
			//	order.Preview = 1;
			//else
			//	order.Preview = 0;
		}

		private void btnBonusAdd_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			try
			{
				frmBonusPayment fPayment = new frmBonusPayment();
				if (order.Discont.Bonus > maxBonusSum)
					fPayment.max = (double)decimal.Round(maxBonusSum, 0);
				else
					fPayment.max = (double)order.Discont.Bonus;
				fPayment.lblBonusInfo.Text = BonusInfo;
				fPayment.ShowDialog();
				this.order.Bonus = (decimal)fPayment.Payment;
				btnBonusAdd.Enabled = false;
				ReBild();
				//ReCalcOrder();
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
			}
			tmr.Start();
		}

		private void btnBonusDel_Click(object sender, EventArgs e)
		{
			order.Bonus = 0;
			btnBonusAdd.Enabled = true;
			btnBonusDel.Enabled = false;
			lblBonus.Text = "";
		}

		private void btnHow_Click(object sender, EventArgs e)
		{
			MessageBox.Show(how, "Как это получилось", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

        private void btnAdvAction_Click(object sender, EventArgs e)
        {
            PSA.Lib.Interface.frmOrderAskAction f = new PSA.Lib.Interface.frmOrderAskAction(usr);
            f.Title = "Действия";
            f.ShowDialog();
            switch (f.DoAction)
            {
                case PSA.Lib.Util.OrderActions.PrintCheck:
                    {
                        PrintCheck();
                        break;
                    }
                case PSA.Lib.Util.OrderActions.MoveEdit:
                    {
                        MoveToStatus("000200");
                        break;
                    }
                case PSA.Lib.Util.OrderActions.MovePrint:
                    {
                        MoveToStatus("000100");
                        break;
                    }
                case PSA.Lib.Util.OrderActions.MovePreview:
                    {
                        MoveToStatus("000010");
                        break;
                    }
                case PSA.Lib.Util.OrderActions.MoveWaitPay:
                    {
                        MoveToStatus("000001");
                        break;
                    }
				case PSA.Lib.Util.OrderActions.MoveToAward:
					{
						MoveToStatus("000000");
						break;
					}
				case PSA.Lib.Util.OrderActions.MovePayment:
					{
						frmMovePayment frm = new frmMovePayment(usr, order.Orderno.Trim());
						frm.ShowDialog();
						break;
					}
				default:
                    {
                        break;
                    }
            }
        }

        private void MoveToStatus(string status)
        {
            if (
                (
                    (order.Distanation.Trim() != "000000")// || // на выдаче
                    //(
                    //    (order.Distanation.Trim() == "000000") && // на выдаче
                    //    (order.FinalPayment == 0) &&              // не принимались деньги
                    //    (
                    //        (order.Discont == null)? // дисконт не принимался
                    //            true:
                    //            (
                    //                ((order.Discont.Discserv == 0) && (order.Bonus == 0))? // скидка = 0 и бонусы не снимались
                    //                    true:
                    //                    false
                    //            )
                    //    )
                    //)
                ) &&
                (order.Distanation.Trim() != "000110") && // в печати
                (order.Distanation.Trim() != "000111") && // готово после печати
                (order.Distanation.Trim() != "000210") && // в обработке
                (order.Distanation.Trim() != "000212") && // готово после обработки
                (order.Distanation.Trim() != "000211") && // готово после обработки
                (order.Distanation.Trim() != "100000") && // выдано
                (order.Distanation.Trim() != "200000") && // выдано
                (order.Distanation.Trim() != "300000") && // утеряно
                (order.Distanation.Trim() != "010000")    // отменено
                )
            {
                order.Distanation = status;
				if (!order.UpdateOrder(usr, false, false, false))
                {
                    MessageBox.Show("При сохранении заказа произошла ошибка!\n" + order.Err, "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    AddEvent("Заказу присвоен новый статус");
                    initOrder(order.Id);
                    frmOrderClose_Load(this, new EventArgs());
                }

            }
            else
            {
                MessageBox.Show("Вы не можете изменить статус этого заказа!\n" + order.Err, "Внимание",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnOkWorkCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOkWorkAction_Click(object sender, EventArgs e)
        {
            if (order.Distanation.Trim() == "000212")
            {
                order.Distanation = "000010";
            }
            else
            {
                order.Distanation = "000000";
            }
			if (!order.UpdateOrder(usr, false, false, false))
            {
                MessageBox.Show("При сохранении заказа произошла ошибка!\n" + order.Err, "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                AddEvent("Работы приняты приемкой");
                initOrder(order.Id);
                frmOrderClose_Load(this, new EventArgs());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmOrderEvents f = new frmOrderEvents(order.Id);
            f.Title = "История работы с заказом " + order.Orderno.Trim();
            f.ShowDialog();
        }

        private void AddEvent(string Event)
        {
            try
            {
                string body = "";
                for (int i = 0; i < order.OrderBody.Rows.Count; i++)
                {
                    body += order.OrderBody.Rows[i][10].ToString() + "|" +
                            order.OrderBody.Rows[i][1].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][3].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][4].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][5].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][6].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][7].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][22].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][15].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][13].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][18].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][19].ToString().Trim();
                    body += "#";
                }
                body = body.Substring(0, body.Length - 1);
                body = "$$" + body + "$$" + order.AdvancedPayment + "$$" + order.FinalPayment + "$$" + order.Bonus;
                SqlCommand _cmd = new SqlCommand("INSERT INTO [dbo].[orderevent] ([id_order], [event_user], [event_status], [event_point], [event_text]) VALUES (" +
                            order.Id + ", '" + usr.Name.Trim() + "', '" + order.Distanation + "', '" + prop.Order_prefics.Trim() +
                            "', '" + Event + body + "')", db_connection);
                _cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
            }
        }

		private void btnPrintCheckGetWork_Click(object sender, EventArgs e)
		{
			PrintCheck();
		}

		private void btnPrintCheckQ_Click(object sender, EventArgs e)
		{
			PrintCheck();
		}

		private void btnGetWorker_Click(object sender, EventArgs e)
		{
			btnGetWorker.Enabled = false;
			gridOrder.Enabled = false;
			try
			{
				RemoteQuery r = new RemoteQuery();
				DataSet ds = r.GetDataSet(order.Orderno.Trim());
				if (ds.Tables.Count > 0)
				{
					if (ds.Tables["header"].Rows.Count > 0)
					{
						if ((ds.Tables["header"].Rows[0]["PRINTED_AUTHID"].ToString().Trim() != "") && 
							(ds.Tables["header"].Rows[0]["STATUS"].ToString().Trim().ToUpper() != "ОТМЕНЕН"))
						{
							for (int i = 0; i < tblOrder.Rows.Count; i++)
							{
								tblOldOrder.Rows[i]["name_work"] = ds.Tables["header"].Rows[0]["PRINTED_AUTHID"].ToString().Trim();
								try
								{
									tblOldOrder.Rows[i]["datework"] = DateTime.Parse(ds.Tables["header"].Rows[0]["PRINTED_AUTHDATE"].ToString().Trim());
								}
								catch
								{
								}
							}
							ReBild();
							if (!order.UpdateOrder(usr, false, false, true))
							{
								MessageBox.Show("При сохранении заказа произошла ошибка!\n" + order.Err, "Ошибка",
												MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							else
							{
								AddEvent("Работы приняты приемкой");
								initOrder(order.Id);
								frmOrderClose_Load(this, new EventArgs());
								ReBild();
							}
						}
						else if (ds.Tables["header"].Rows[0]["STATUS"].ToString().Trim().ToUpper() == "ОТМЕНЕН")
						{
							MessageBox.Show("Заказ отменен печатным центром");
							order.Distanation = "010000";
							for (int i = 0; i < tblOrder.Rows.Count; i++)
							{
								tblOldOrder.Rows[i]["name_work"] = "Заказ отменен";
								try
								{
									tblOldOrder.Rows[i]["datework"] = DateTime.Now;
								}
								catch
								{
								}
							}
							ReBild();
							if (!order.UpdateOrder(usr, false, false, true))
							{
								MessageBox.Show("При сохранении заказа произошла ошибка!\n" + order.Err, "Ошибка",
												MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							else
							{
								AddEvent("Заказ отменен печатным центром");
								initOrder(order.Id);
								frmOrderClose_Load(this, new EventArgs());
								ReBild();
							}
						}
						else
						{
							MessageBox.Show("Исполнитель не найден, возможно заказ еще не выполнен!");
						}
					}
					else
					{
						MessageBox.Show("Исполнитель не найден, возможно заказ еще не выполнен!");
					}
				}
				else
				{
					MessageBox.Show("Исполнитель не найден, возможно заказ еще не выполнен!");
				}
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
			gridOrder.Enabled = true;
			btnGetWorker.Enabled = true;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			order.AdvancedPayment += order.FinalPayment;
			order.FinalPayment = 0;
			SaveOrder(true);
		}

		private void btnFixDoubleClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnFixDouble_Click(object sender, EventArgs e)
		{
			try
			{
				using(SqlConnection _cn = new SqlConnection(prop.Connection_string))
				{
					_cn.Open();
					SqlCommand _cmd = new SqlCommand();
					_cmd.CommandTimeout = 9000;
					_cmd.Connection = _cn;
					_cmd.CommandText = "UPDATE [order] SET status = '010000', number = " + (order.Orderno.Substring(0, 1) == prop.Order_terminal_prefics.Substring(0, 1) ? "49" : "59") + order.Orderno.Substring(2) + ", exported = 0 WHERE id_order = " + order.Id; 
					_cmd.ExecuteNonQuery();
					AddEvent("Заказу присвоен новый номер с префиксом " + (order.Orderno.Substring(0, 1) == prop.Order_terminal_prefics.Substring(0, 1) ? "49" : "59"));
					AddEvent("Заказ был отменен как продублированный");
					this.Close();
				}
			}
			catch
			{
			}
		}

		private void btnEditPType_Click(object sender, EventArgs e)
		{
			txtPType.Enabled = true;
		}
	

	}
}
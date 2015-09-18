using System;
using System.Collections.Generic;
using System.Text;
using PSA.Lib.DAL.SqlProviders;
using System.Data;

namespace PSA.Lib.BLL
{
	public class Kiosk
	{
		private int _id;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private bool _del;
		public bool Del
		{
			get { return _del; }
			set { _del = value; }
		}

		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private string _path;
		public string Path
		{
			get { return _path; }
			set { _path = value; }
		}

		private int _code;
		public int Code
		{
			get { return _code; }
			set { _code = value; }
		}

		public Kiosk()
		{
		}

		public bool Save()
		{
			bool ret = false;

			try
			{
				KioskProvider p = new KioskProvider();
				p.update(this._name, this._path, this._code, this._id);
				ret = true;
			}
			catch
			{
				ret = false;
			}

			return ret;
		}

		public bool Delete()
		{
			bool ret = false;

			try
			{
				KioskProvider p = new KioskProvider();
				p.delete(this._id, this._del);
				ret = true;
			}
			catch
			{
				ret = false;
			}

			return ret;
		}

		public bool Add()
		{
			bool ret = false;

			try
			{
				KioskProvider p = new KioskProvider();
				if (p.add(this._name, this._path, this._code) > 0)
				{
					ret = true;
				}
				else
				{
					ret = false;
				}
			}
			catch(Exception ex)
			{
				ret = false;
			}

			return ret;
		}

		public static List<Kiosk> getList()
		{
			List<Kiosk> l = new List<Kiosk>();

			try
			{
				KioskProvider p = new KioskProvider();
				DataSet ds = p.Get();
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					Kiosk k = new Kiosk();
					k._id = int.Parse(ds.Tables[0].Rows[i]["id_kiosk"].ToString());
					k._del = bool.Parse(ds.Tables[0].Rows[i]["del"].ToString());
					k._name = ds.Tables[0].Rows[i]["name"].ToString();
					k._path = ds.Tables[0].Rows[i]["path"].ToString();
					k._code = int.Parse(ds.Tables[0].Rows[i]["code"].ToString());

					l.Add(k);
				}
			}
			catch (Exception ex)
			{
			}

			return l;
		}

		public static List<Kiosk> getListAll()
		{
			List<Kiosk> l = new List<Kiosk>();

			try
			{
				KioskProvider p = new KioskProvider();
				DataSet ds = p.GetAll();
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					Kiosk k = new Kiosk();
					k._id = int.Parse(ds.Tables[0].Rows[i]["id_kiosk"].ToString());
					k._del = bool.Parse(ds.Tables[0].Rows[i]["del"].ToString());
					k._name = ds.Tables[0].Rows[i]["name"].ToString();
					k._path = ds.Tables[0].Rows[i]["path"].ToString();
					k._code = int.Parse(ds.Tables[0].Rows[i]["code"].ToString());

					l.Add(k);
				}
			}
			catch (Exception ex)
			{
			}

			return l;
		}

		public static Kiosk getById(int id)
		{
			Kiosk k = new Kiosk();

			try
			{
				KioskProvider p = new KioskProvider();
				DataSet ds = p.getById(id);
				if (ds.Tables[0].Rows.Count > 0)
				{
					k._id = int.Parse(ds.Tables[0].Rows[0]["id_kiosk"].ToString());
					k._del = bool.Parse(ds.Tables[0].Rows[0]["del"].ToString());
					k._name = ds.Tables[0].Rows[0]["name"].ToString();
					k._path = ds.Tables[0].Rows[0]["path"].ToString();
					k._code = int.Parse(ds.Tables[0].Rows[0]["code"].ToString());
				}

			}
			catch (Exception ex)
			{
			}

			return k;
		}

		public static Kiosk getByCode(int code)
		{
			Kiosk k = new Kiosk();

			try
			{
				KioskProvider p = new KioskProvider();
				DataSet ds = p.getByCode(code);
				if (ds.Tables[0].Rows.Count > 0)
				{
					k._id = int.Parse(ds.Tables[0].Rows[0]["id_kiosk"].ToString());
					k._del = bool.Parse(ds.Tables[0].Rows[0]["del"].ToString());
					k._name = ds.Tables[0].Rows[0]["name"].ToString();
					k._path = ds.Tables[0].Rows[0]["path"].ToString();
					k._code = int.Parse(ds.Tables[0].Rows[0]["code"].ToString());
				}

			}
			catch (Exception ex)
			{
			}

			return k;
		}

	}
}

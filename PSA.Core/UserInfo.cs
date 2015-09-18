using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace Photoland.Security.User
{
	public class UserInfo
	{
		

		public UserInfo()
		{

		}

		// код пользователя
		private int _id_user;
		public int Id_user
		{
			get { return _id_user; }
			set { this._id_user = value; }
		}

		// код должности
		private int _id_post;
		public int Id_post
		{
			get { return _id_post; }
			set { this._id_post = value; }
		}

		// наименование должности
		private string _post;
		public string Post
		{
			get { return _post; }
			set { this._post = value; }
		}

		// код точки
		private int _id_point;
		public int Id_point
		{
			get { return _id_point; }
			set { this._id_point = value; }
		}

		// наименование точки
		private string _point;
		public string Point
		{
			get { return _point; }
			set { this._point = value; }
		}

		// уникальный ключ
		private string _guid;
		public string Guid
		{
			get { return _guid; }
			set { this._guid = value; }
		}

		// наименование
		private string _name;
		public string Name
		{
			get { return _name; }
			set { this._name = value; }
		}

		// контактная информация
		private string _contact;
		public string Contact
		{
			get { return _contact; }
			set { this._contact = value; }
		}

		// логин
		private string _login;
		public string Login
		{
			get { return _login; }
			set { this._login = value; }
		}

		// пароль
		private string _password;
		public string Password
		{
			get { return _password; }
			set { this._password = value; }
		}

        // код машины
        private string _id_mashine;
        public string Id_Mashine
        {
            get { return _id_mashine; }
            set { this._id_mashine = value; }
        }

        // машина
        private string _mashine;
        public string Mashine
        {
            get { return _mashine; }
            set { this._mashine = value; }
        }

        // код бумаги
        private string _id_paper;
        public string Id_Paper
        {
            get { return _id_paper; }
            set { this._id_paper = value; }
        }

        // бумага
        private string _paper;
        public string Paper
        {
            get { return _paper; }
            set { this._paper = value; }
        }

        // права доступа в числовом представлении
		private long _permission;
		public long Permission
		{
			get { return _permission; }
			set { this._permission = value; }
		}

		public bool prmCanLogin
		{
			get
			{
				if ((this.Permission & 1) == 1)
					return true;
				else
					return false;
			}
		}

		public bool prmCanLoginAcceptance
		{
			get
			{
				if ((this.Permission & 2) == 2)
					return true;
				else
					return false;
			}
		}

		public bool prmCanLoginOperator
		{
			get
			{
				if ((this.Permission & 4) == 4)
					return true;
				else
					return false;
			}
		}

		public bool prmCanLoginDesigner
		{
			get
			{
				if ((this.Permission & 8) == 8)
					return true;
				else
					return false;
			}
		}

		public bool prmCanLoginAdmin
		{
			get
			{
				if ((this.Permission & 16) == 16)
					return true;
				else
					return false;
			}
		}

		public bool prmCanSetup
		{
			get
			{
				if ((this.Permission & 32) == 32)
					return true;
				else
					return false;
			}
		}

		public bool prmCanEditPayments
		{
			get
			{
				if ((this.Permission & 128) == 128)
					return true;
				else
					return false;
			}
		}

		public bool prmCanSeeAllPayments
		{
			get
			{
				if ((this.Permission & 256) == 256)
					return true;
				else
					return false;
			}
		}

		public bool prmCanSeeMyPayments
		{
			get
			{
				if ((this.Permission & 512) == 512)
					return true;
				else
					return false;
			}
		}

		public bool prmCanDelEditBrak
		{
			get
			{
				if ((this.Permission & 1024) == 1024)
					return true;
				else
					return false;
			}
		}

        public bool prmCanInventory
        {
            get
            {
                if ((this.Permission & 2048) == 2048)
                    return true;
                else
                    return false;
            }
        }

        public bool prmCanEditInventory
        {
            get
            {
                if ((this.Permission & 4096) == 4096)
                    return true;
                else
                    return false;
            }
        }

        public bool prmCanMovePayment
        {
            get
            {
				if ((this.Permission & 8192) == 8192)
                    return true;
                else
                    return false;
            }
        }


    }
}

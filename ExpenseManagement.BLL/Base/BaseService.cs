﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ExpenseManagement.BLL.Base
{
    public class BaseService : IBaseService
    {
        public int CurrentPersonId { get; set; }
        public string CurrentCultureCode { get; set; }

        private string _connString;
        private string _key;
        protected string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connString))
                {
                    _connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                    return _connString;
                }
                else
                {
                    return _connString;
                }
            }
        }
        protected string Key
        {
            get
            {
                if (string.IsNullOrEmpty(_key))
                {
                    _key = System.Configuration.ConfigurationManager.AppSettings["TokenKey"].ToString();
                    return _key;
                }
                else
                {
                    return _key;
                }
            }
        }
    }
}

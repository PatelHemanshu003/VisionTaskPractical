using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VisionTaskPractical.Models;
using System.Web.Mvc;
using System.Reflection;

namespace VisionTaskPractical.Models
{

    public class DataAccesLayer
    {
        string connectionString = ConfigurationManager.AppSettings["DbConnection"].ToString();
        public List<SelectListItem> GetProductList()
        {
            List<SelectListItem> resultList = new List<SelectListItem>();
            SqlConnection dbConnection = null;
            SqlDataReader reader = null;
            try
            {
                dbConnection = new SqlConnection(connectionString);
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                //dbCommand.Parameters.AddWithValue("@COUNTRYID", CountryId);
                dbCommand.CommandText = "Usp_GetProductList";
                SqlDataAdapter adp = new SqlDataAdapter(dbCommand);
                DataTable dt = new DataTable();
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    SelectListItem invoiceEntity = new SelectListItem();
                    invoiceEntity.Value = (string)reader["Id"].ToString();
                    invoiceEntity.Text = (string)reader["Name"];
                    resultList.Add(invoiceEntity);
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                dbConnection.Close();
            }
            return resultList.ToList();
        }
        public List<SelectListItem> GetStateList()
        {
            List<SelectListItem> resultList = new List<SelectListItem>();
            SqlConnection dbConnection = null;
            SqlDataReader reader = null;
            try
            {
                dbConnection = new SqlConnection(connectionString);
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                //dbCommand.Parameters.AddWithValue("@COUNTRYID", CountryId);
                dbCommand.CommandText = "Usp_GetStateList";
                SqlDataAdapter adp = new SqlDataAdapter(dbCommand);
                DataTable dt = new DataTable();
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    SelectListItem invoiceEntity = new SelectListItem();
                    invoiceEntity.Value = (string)reader["Id"].ToString();
                    invoiceEntity.Text = (string)reader["Name"];
                    resultList.Add(invoiceEntity);
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                dbConnection.Close();
            }
            return resultList.ToList();
        }
        public List<SelectListItem> GetCityList(int StateId)
        {
            List<SelectListItem> resultList = new List<SelectListItem>();
            SqlConnection dbConnection = null;
            SqlDataReader reader = null;
            try
            {
                dbConnection = new SqlConnection(connectionString);
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@StateId", StateId);
                dbCommand.CommandText = "Usp_GetCityList";
                SqlDataAdapter adp = new SqlDataAdapter(dbCommand);
                DataTable dt = new DataTable();
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    SelectListItem invoiceEntity = new SelectListItem();
                    invoiceEntity.Value = (string)reader["Id"].ToString();
                    invoiceEntity.Text = (string)reader["Name"];
                    resultList.Add(invoiceEntity);
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                dbConnection.Close();
            }
            return resultList.ToList();
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public int SaveData(InvoiceGenerationModel objInvoiceGenerationModel)
        {
            int Result = 0;
            SqlConnection dbConnection = null;
            SqlDataReader reader = null;
            try
            {
                dbConnection = new SqlConnection(connectionString);
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@BillingAddress", objInvoiceGenerationModel.BillingAddress);
                dbCommand.Parameters.AddWithValue("@BillingCityId", objInvoiceGenerationModel.BillingCityId);
                dbCommand.Parameters.AddWithValue("@BillingStateId", objInvoiceGenerationModel.BillingStateId);
                dbCommand.Parameters.AddWithValue("@BillingPhoneNo", objInvoiceGenerationModel.BillingPhoneNo);
                dbCommand.Parameters.AddWithValue("@ShippingAddress", objInvoiceGenerationModel.ShippingAddress);
                dbCommand.Parameters.AddWithValue("@ShippingCityId", objInvoiceGenerationModel.ShippingCityId);
                dbCommand.Parameters.AddWithValue("@ShippingStateId", objInvoiceGenerationModel.ShippingStateId);
                dbCommand.Parameters.AddWithValue("@ShippingPhoneNo", objInvoiceGenerationModel.ShippingPhoneNo);
                dbCommand.Parameters.AddWithValue("@ProductDetails", ToDataTable(objInvoiceGenerationModel.lstInvoiceGenerationDetailModel));
                dbCommand.Parameters.AddWithValue("@InvoiceNo", objInvoiceGenerationModel.InvoiceNo);
                if (objInvoiceGenerationModel.InvoiceNo == null || objInvoiceGenerationModel.InvoiceNo == 0)
                {
                    dbCommand.Parameters.AddWithValue("@Action", "I");
                }
                else { dbCommand.Parameters.AddWithValue("@Action", "U"); }
                dbCommand.CommandText = "Usp_InsertUpdate_InvoiceDetails";
                dbConnection.Open();
                Result = dbCommand.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                dbConnection.Close();
            }
            return Result;
        }
        public List<InvoiceGenerationInvoiceList> GetInvoiceList()
        {
            List<InvoiceGenerationInvoiceList> invoiceGenerationInvoices = new List<InvoiceGenerationInvoiceList>();
            SqlConnection dbConnection = null;
            SqlDataReader reader = null;
            try
            {
                dbConnection = new SqlConnection(connectionString);
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "Usp_GetInvoiceAllDetails";
                SqlDataAdapter adp = new SqlDataAdapter(dbCommand);
                DataTable dt = new DataTable();
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    InvoiceGenerationInvoiceList invoiceEntity = new InvoiceGenerationInvoiceList();
                    invoiceEntity.InvoiceNo = (int)reader["InvoiceNo"];
                    invoiceGenerationInvoices.Add(invoiceEntity);
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                dbConnection.Close();
            }
            return invoiceGenerationInvoices.ToList();
        }
        public InvoiceGenerationModel GetInvoiceHeaderlist(int InvoiceNo)
        {
            InvoiceGenerationModel objInvoiceGenerationModel = new InvoiceGenerationModel();
            SqlConnection dbConnection = null;
            SqlDataReader reader = null;
            try
            {
                dbConnection = new SqlConnection(connectionString);
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo); ;
                dbCommand.CommandText = "Usp_GetInvoiceHeader";
                SqlDataAdapter adp = new SqlDataAdapter(dbCommand);
                DataTable dt = new DataTable();
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    objInvoiceGenerationModel.InvoiceNo = (int)reader["InvoiceNo"];
                    objInvoiceGenerationModel.BillingAddress = (string)reader["BillingAddress"];
                    objInvoiceGenerationModel.BillingCityId = (int)reader["BillingCityId"];
                    objInvoiceGenerationModel.BillingStateId = (int)reader["BillingStateId"];
                    objInvoiceGenerationModel.BillingPhoneNo = (string)reader["BillingPhoneNo"];
                    objInvoiceGenerationModel.ShippingAddress = (string)reader["ShippingAddress"];
                    objInvoiceGenerationModel.ShippingCityId = (int)reader["ShippingCityId"];
                    objInvoiceGenerationModel.ShippingStateId = (int)reader["ShippingStateId"];
                    objInvoiceGenerationModel.ShippingPhoneNo = (string)reader["ShippingPhoneNo"];
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                dbConnection.Close();
            }
            return objInvoiceGenerationModel;
        }
        public List<InvoiceGenerationDetailModel> GetInvoiceDetailList(int InvoiceNo)
        {
            List<InvoiceGenerationDetailModel> invoiceGenerationInvoices = new List<InvoiceGenerationDetailModel>();
            SqlConnection dbConnection = null;
            SqlDataReader reader = null;
            try
            {
                dbConnection = new SqlConnection(connectionString);
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo); ;
                dbCommand.CommandText = "Usp_GetInvoiceDetails";
                SqlDataAdapter adp = new SqlDataAdapter(dbCommand);
                DataTable dt = new DataTable();
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    InvoiceGenerationDetailModel invoiceEntity = new InvoiceGenerationDetailModel();
                    invoiceEntity.ProductId = (int)reader["ProductId"];
                    invoiceEntity.Qty = (int)reader["Qty"];
                    invoiceEntity.Rate = (decimal)reader["Rate"];
                    invoiceEntity.Amount = (decimal)reader["Amount"];
                    invoiceGenerationInvoices.Add(invoiceEntity);
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                dbConnection.Close();
            }
            return invoiceGenerationInvoices.ToList();
        }
    }
}
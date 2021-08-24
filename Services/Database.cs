using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace WebServiceApi.Services{
  public class SqlDatabase
  {
    public SqlConnection objConn = new SqlConnection("Data Source=(local);Initial Catalog=webserviceapi;User ID=sa;Password=password;");
    public SqlCommand objCmd = new SqlCommand();
    public SqlDataAdapter dtAdapter = new SqlDataAdapter();
    public DataSet ds = new DataSet();
    public DataTable dt = new DataTable();
    public DataTable Datatable(string strSQL)
    {
      if (this.objConn.State != ConnectionState.Open){
        this.objConn.Open();
      }else{
        this.objConn.Close();
        this.objConn.Open();
      }
        objCmd = new SqlCommand();
        dtAdapter = new SqlDataAdapter();
        ds = new DataSet();
        dt = new DataTable();
        
        objCmd = objConn.CreateCommand();
        objCmd.CommandText = strSQL;
        objCmd.CommandType = CommandType.Text;
        dtAdapter.SelectCommand = objCmd;
        dtAdapter.Fill(ds);
        dt = ds.Tables[0];
      return dt;
    }
    public int Cmd_ExecuteNonQuery(string strSQL)
    {
      if (this.objConn.State != ConnectionState.Open){
        this.objConn.Open();
      }else{
        this.objConn.Close();
        this.objConn.Open();
      }
        objCmd = new SqlCommand();
        dtAdapter = new SqlDataAdapter();
        ds = new DataSet();
        dt = new DataTable();

        objCmd = objConn.CreateCommand();
        objCmd.CommandText = strSQL;
        var NonQuery = objCmd.ExecuteNonQuery();

      return NonQuery;
    }
  }
}
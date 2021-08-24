using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebServiceApi.Models;
using WebServiceApi.Services;

namespace WebServiceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;        
        public UserController(IConfiguration config)
        {
            this.configuration = config;
        }

        [Route("")]
        [HttpGet]
        public ObjectResult Get()
        {
            var SqlDatabase = new SqlDatabase();
            List<UserList> convertedList = new List<UserList>();
            string sql = @"SELECT * FROM tbl_users";
            DataTable dt = SqlDatabase.Datatable(sql);
            DataColumnCollection coluUserAccess = dt.Columns;
            convertedList = (from rw in dt.AsEnumerable()
            select new UserList() {
                Id = Convert.ToString(rw["Id"]),
                name = Convert.ToString(rw["name"]),
            }).ToList();
                
            return Ok(convertedList);
        }

        [Route("")]
        [HttpPost]
        public ObjectResult Create(UserList user)
        {
            var SqlDatabase = new SqlDatabase();
            List<UserList> convertedList = new List<UserList>();
            user.Id = Guid.NewGuid().ToString();
            string sqlinsert = @"INSERT INTO tbl_users (Id, name) VALUES ('"+ user.Id +"','"+ user.name +"');";

            if(SqlDatabase.Cmd_ExecuteNonQuery(sqlinsert)== 1){
                convertedList = new List<UserList>{
                    new UserList{
                        Id=user.Id,
                        name=user.name
                    }
                };
            }                                        
            return Ok(convertedList);            
        }

        [Route("{name}")]
        [HttpGet]
        public ObjectResult Search(string name)
        {
            var SqlDatabase = new SqlDatabase();
            List<UserList> convertedList = new List<UserList>();
            string sql = @"SELECT * FROM tbl_users WHERE name ='"+ name +"'";
            DataTable dt = SqlDatabase.Datatable(sql);
            DataColumnCollection coluUserAccess = dt.Columns;
            convertedList = (from rw in dt.AsEnumerable()
            select new UserList() {
                Id = Convert.ToString(rw["Id"]),
                name = Convert.ToString(rw["name"]),
            }).ToList();
                
            return Ok(convertedList);
        }

        [Route("{id}")]
        [HttpPatch]
        public ObjectResult Update(string id,UserList user)
        {
            var SqlDatabase = new SqlDatabase();
            List<UserList> convertedList = new List<UserList>();
            string sqlupdate = @"UPDATE tbl_users SET name = '"+ user.name +"' WHERE Id = '"+ id +"'";
            if(SqlDatabase.Cmd_ExecuteNonQuery(sqlupdate)== 1){
                convertedList = new List<UserList>{
                    new UserList{
                        Id=id,
                        name=user.name,
                    }
                };
            }           
            return Ok(convertedList);
        }

        [Route("{id}")]
        [HttpDelete]
         public ObjectResult Delete(string id)
         {
            var SqlDatabase = new SqlDatabase();
            List<UserList> convertedList = new List<UserList>();
            string sqldelete = @"DELETE FROM tbl_users WHERE Id = '"+ id +"'"; 
                if(SqlDatabase.Cmd_ExecuteNonQuery(sqldelete)== 1){
                    convertedList = new List<UserList>{
                        new UserList{
                            Id=id
                        }
                    };
                }     
            return Ok(convertedList);
         }
    }
}
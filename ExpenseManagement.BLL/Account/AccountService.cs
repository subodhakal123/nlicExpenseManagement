using Dapper;
using ExpenseManagement.BLL.Base;
using ExpenseManagement.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Account
{
    public class AccountService : BaseService, IAccountService
    {
        public string CreateToken(string username, string password)
        {
            SymmetricSecurityKey _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(base.Key));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,username)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public UserModel ValidateUser(string username, string password)
        {
            try
            {
                UserModel model = new UserModel();
                string conString = base.ConnectionString;
                try
                {
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();
                        model = con.Query<UserModel>("[USRS].[usp_ValidateLogin]", new { username = username, password = password }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                        con.Close();

                    }
                }
                catch (Exception ex)
                {

                }
            return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

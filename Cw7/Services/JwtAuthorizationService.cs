using Cw7.DTO.Requests;
using Cw7.DTO.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cw7.Services
{
    public class JwtAuthorizationService : IJwtAuthorizationService
    {
        private IConfiguration _configuration { get; set; }
        public JwtAuthorizationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public JwtResponse LogIn(LoginRequest request)
        {
            using (var con = new SqlConnection("Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=s16870;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                com.CommandText = "SELECT pswd, salt FROM StudentAPBD WHERE IndexNumber = @index";
                com.Parameters.AddWithValue("index", request.Index);
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    var salt = (byte[])dr["salt"];
                    var pswd = dr["pswd"].ToString();
                    dr.Close();
                    if (PasswordHelper.CheckPswd(request.Password, pswd, salt))
                    {
                        var claims = new[]
                        {
                            new Claim("Index",request.Index),
                            new Claim(ClaimTypes.Role,"Student")
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken
                        (
                            issuer: "Gakko",
                            audience: "Students",
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(10),
                            signingCredentials: creds
                        );
                        var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                        com.CommandText = "UPDATE StudentAPBD SET refreshToken = @refreshToken WHERE IndexNumber = @index";
                        com.Parameters.AddWithValue("refreshToken", refreshToken);
                        com.ExecuteNonQuery();
                        tran.Commit();
                        return new JwtResponse
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            RefreshToken = refreshToken
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        public JwtResponse RefreshToken(String refreshToken)
        {
            using (var con = new SqlConnection("Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=s16870;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                com.CommandText = "SELECT IndexNumber FROM StudentAPBD WHERE refreshToken = @refreshToken";
                com.Parameters.AddWithValue("refreshToken", refreshToken);
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    var index = dr["IndexNumber"].ToString();
                    dr.Close();
                    var claims = new[]
                    {
                            new Claim("Index",index),
                            new Claim(ClaimTypes.Role,"student")
                        };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken
                    (
                        issuer: "Gakko",
                        audience: "Students",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: creds
                    );
                    var newRefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    com.Parameters.Clear();
                    com.CommandText = "UPDATE StudentAPBD SET refreshToken = @refreshToken WHERE IndexNumber = @index";
                    com.Parameters.AddWithValue("refreshToken", newRefreshToken);
                    com.Parameters.AddWithValue("index", index);
                    com.ExecuteNonQuery();

                    return new JwtResponse
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = newRefreshToken
                    };
                }
                else
                {
                    return null;
                }
            }
        }
    }
}

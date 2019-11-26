using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BasicAspNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("token")] //...//api/authentication/token
        public ActionResult GetToken()
        {
            //security key
            string securityKey = "this_is_our_long_security_key_for_token_26_11_2019$smesk.in";

            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //signing credentials
            var sigingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //create token
            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: sigingCredential
                );

            //return token
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
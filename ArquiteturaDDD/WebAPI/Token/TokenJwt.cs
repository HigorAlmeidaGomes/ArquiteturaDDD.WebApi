using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Token
{
    public class TokenJwt
    {
        private JwtSecurityToken token;

        internal TokenJwt(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime validTo => token.ValidTo;

        public string value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}

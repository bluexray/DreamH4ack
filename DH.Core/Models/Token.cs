using System;

namespace DH.Core.Models
{
    public class Token
    {
        // [Key]
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientType { get; set; }
        public string Scope { get; set; }
        public string UserName { get; set; }
        // [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string IpAddress { get; set; }
    }
}

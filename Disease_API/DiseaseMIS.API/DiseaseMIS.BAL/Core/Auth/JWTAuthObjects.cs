using System;
using System.Text.Json.Serialization;

namespace DiseaseMIS.BAL.Core
{
    // Added by Gautam
    public class JwtAuthResult
    {
        /// <summary>
        /// Property to store Access Token for Users
        /// </summary>
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Property to store Access Refresh Token for Users
        /// </summary>
        [JsonPropertyName("refreshToken")]
        public RefreshToken RefreshToken { get; set; }
    }

    public class RefreshToken
    {
        /// <summary>
        /// Property to store Phone Number to fetch the Token for User
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Property to store Access Token
        /// </summary>
        [JsonPropertyName("tokenString")]
        public string TokenString { get; set; }

        /// <summary>
        /// Property to store Expiration Date for Access Tokens
        /// </summary>
        [JsonPropertyName("expireAt")]
        public DateTime ExpireAt { get; set; }

        /// <summary>
        /// Property to store User Details
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }

}

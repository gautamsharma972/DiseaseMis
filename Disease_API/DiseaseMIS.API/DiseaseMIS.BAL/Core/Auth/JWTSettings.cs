namespace DiseaseMIS.BAL.Core
{
    public class JWTSettings
    {
        /// <summary>
        /// Property to store Secret key
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Property to store Issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Property to store Audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Property to store AccessTokenExpiration period
        /// </summary>
        public int AccessTokenExpiration { get; set; }

        /// <summary>
        /// Property to store Access Refresh Token for Users
        /// </summary>
        public int RefreshTokenExpiration { get; set; }
    }
}

using System;

namespace DiseaseMIS.BAL.Core
{
    public class LoginInputModel
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Phone Number of User
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// OTP holder
        /// </summary>
        public string OTP { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Time generated for OTP
        /// </summary>
        public DateTime TimeGenerated { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public LoginInputModel(string phoneNumber, string otp)
        {
            this.PhoneNumber = phoneNumber;
            this.OTP = otp;
        }
    }
}

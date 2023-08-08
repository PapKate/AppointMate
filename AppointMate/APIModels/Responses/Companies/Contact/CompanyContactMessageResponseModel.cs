﻿using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a company contact message
    /// </summary>
    public class CompanyContactMessageResponseModel : DateResponseModel, ICompanyIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="CompanyId"/> property
        /// </summary>
        private string? mCompanyId;

        /// <summary>
        /// The member of the <see cref="FirstName"/> property
        /// </summary>
        private string? mFirstName;

        /// <summary>
        /// The member of the <see cref="LastName"/> property
        /// </summary>
        private string? mLastName;

        /// <summary>
        /// The member of the <see cref="Message"/> property
        /// </summary>
        private string? mMessage;

        /// <summary>
        /// The member of the <see cref="Email"/> property
        /// </summary>
        private string? mEmail;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string CompanyId 
        { 
            get => mCompanyId ?? string.Empty;
            set => mCompanyId = value;
        }

        /// <summary>
        /// The first name
        /// </summary>
        public string FirstName
        {
            get => mFirstName ?? string.Empty;
            set => mFirstName = value;
        }

        /// <summary>
        /// The last name
        /// </summary>
        public string LastName
        {
            get => mLastName ?? string.Empty;
            set => mLastName = value;
        }

        /// <summary>
        /// The email
        /// </summary>
        public string Email
        {
            get => mEmail ?? string.Empty;
            set => mEmail = value;
        }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The message
        /// </summary>
        public string Message
        {
            get => mMessage ?? string.Empty;
            set => mMessage = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyContactMessageResponseModel() : base()
        {

        }

        #endregion
    }
}

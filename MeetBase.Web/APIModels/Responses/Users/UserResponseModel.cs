﻿
using Newtonsoft.Json;

using System.Drawing;

namespace MeetBase.Web
{
    /// <summary>
    /// Represents a user
    /// </summary>
    public class UserResponseModel : DateResponseModel, IImageable, IEmailable, IPhoneable, IColorable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="FirstName"/> property
        /// </summary>
        private string? mFirstName;

        /// <summary>
        /// The member of the <see cref="LastName"/> property
        /// </summary>
        private string? mLastName;

        /// <summary>
        /// The member of the <see cref="Username"/> property
        /// </summary>
        private string? mUsername;

        /// <summary>
        /// The member of the <see cref="Email"/> property
        /// </summary>
        private string? mEmail;

        /// <summary>
        /// The member of the <see cref="Color"/> property
        /// </summary>
        private string? mColor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The username
        /// </summary>
        public string Username
        {
            get => mUsername ?? string.Empty;
            set => mUsername = value;
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
        /// A flag indicating whether the email is confirmed or not
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        public string Color
        {
            get => mColor ?? string.Empty;
            set => mColor = value;
        }

        /// <summary>
        /// The birthday
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// The location information
        /// </summary>
        public Location? Location { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="UserResponseModel"/> used when embedding
    /// </summary>
    public class EmbeddedUserResponseModel : EmbeddedBaseResponseModel, IImageable, IColorable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="FirstName"/> property
        /// </summary>
        private string? mFirstName;

        /// <summary>
        /// The member of the <see cref="LastName"/> property
        /// </summary>
        private string? mLastName;

        /// <summary>
        /// The member of the <see cref="Username"/> property
        /// </summary>
        private string? mUsername;

        /// <summary>
        /// The member of the <see cref="Email"/> property
        /// </summary>
        private string? mEmail;

        /// <summary>
        /// The member of the <see cref="Color"/> property
        /// </summary>
        private string? mColor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The username
        /// </summary>
        public string Username
        {
            get => mUsername ?? string.Empty;
            set => mUsername = value;
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
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        public string Color
        {
            get => mColor ?? string.Empty;
            set => mColor = value;
        }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The location information
        /// </summary>
        public Location? Location { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedUserResponseModel() : base()
        {

        }

        #endregion
    }
}

﻿using MeetBase;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// Dialog for updating a professor
    /// </summary>
    public partial class UpdateStaffMemberDialog<T>
        where T : UpdateStaffMemberModel, new()
    {
        #region Private Members

        /// <summary>
        /// The password
        /// </summary>
        private string mPassword = string.Empty;

        /// <summary>
        /// The confirm password
        /// </summary>
        private string mConfirmPassword = string.Empty;

        /// <summary>
        /// The password
        /// </summary>
        private string mPhotoLabel = "Profile photo";

        /// <summary>
        /// The country code
        /// </summary>
        private int mCountryCode = 30;

        /// <summary>
        /// The phone number
        /// </summary>
        private string mPhoneNumber = string.Empty;

        /// <summary>
        /// The birth date
        /// </summary>
        private DateTime? mBirthDate;

        /// <summary>
        /// The location
        /// </summary>
        private Location mLocation = new Location();

        #endregion

        #region Public Properties

        /// <summary>
        /// The dialog instance
        /// </summary>
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = default!;

        /// <summary>
        /// The model
        /// </summary>
        [Parameter]
        public T Model { get; set; } = new();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateStaffMemberDialog() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            mCountryCode = Model.PhoneNumber?.CountryCode ?? 30;
            mPhoneNumber = Model.PhoneNumber?.Phone ?? string.Empty;
            mBirthDate = Model.DateOfBirth?.ToDateTime() ?? DateTime.Now;
            mLocation = Model.Location ?? new();
        }

        #endregion

        #region Private Methods

        private void Save()
        {
            Model.PhoneNumber = new PhoneNumber(mCountryCode, mPhoneNumber);
            Model.Location = mLocation;
            Model.DateOfBirth = mBirthDate?.ToDateOnly();

            if (!mPassword.IsNullOrEmpty() && mPassword == mConfirmPassword)
                Model.PasswordHash = mPassword.EncryptPassword();

            MudDialog.Close(DialogResult.Ok(Model));
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion
    }
}
﻿namespace AppointMate
{
    /// <summary>
    /// Request model used for a note
    /// </summary>
    public class CustomerNoteRequestModel : StandardRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// The type
        /// </summary>
        public CustomerNoteType? Type { get; set; }

        /// <summary>
        /// A flag indicating whether this note can be seen by the customer or not
        /// </summary>
        public bool? IsVisibleToCustomer { get; set; }

        /// <summary>
        /// A flag indicating whether the note automatically hides
        /// </summary>
        public bool? IsHidingAutomatically { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerNoteRequestModel() : base()
        {

        }

        #endregion
    }
}

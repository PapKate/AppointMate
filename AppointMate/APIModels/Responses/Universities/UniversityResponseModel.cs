﻿namespace AppointMate
{
    /// <summary>
    /// Represents a university 
    /// </summary>
    public class UniversityResponseModel : StandardResponseModel, IImageable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Fields"/> property
        /// </summary>
        private IEnumerable<string>? mFields;

        #endregion

        #region Public Properties

        /// <summary>
        /// The image
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The fields of study
        /// </summary>
        public IEnumerable<string> Fields
        {
            get => mFields ?? Enumerable.Empty<string>();
            set => mFields = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UniversityResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="UniversityResponseModel"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedUniversityResponseModel : EmbeddedStandardEntity, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The image
        /// </summary>
        public Uri? ImageUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedUniversityResponseModel() : base()
        {

        }

        #endregion
    }
}

﻿using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a label document in the MongoDB
    /// </summary>
    public class LabelEntity : StandardEntity, IDescriptable, ICompanyIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LabelEntity()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="LabelEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="companyId">The company id</param>
        /// <returns></returns>
        public static LabelEntity FromRequestModel(LabelRequestModel model, ObjectId companyId)
        {
            var entity = new LabelEntity();

            DI.Mapper.Map(model, entity);
            entity.CompanyId = companyId;
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="LabelResponseModel"/> from the current <see cref="LabelEntity"/>
        /// </summary>
        /// <returns></returns>
        public LabelResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<LabelResponseModel>(this);



        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="LabelEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedLabelEntity : EmbeddedStandardEntity
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedLabelEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;

        #endregion
    }
}

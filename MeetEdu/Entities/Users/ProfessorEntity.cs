﻿using AutoMapper;
using MeetBase;

using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents a professor document in the MongoDB
    /// </summary>
    public class ProfessorEntity : StaffMemberEntity, IEmbeddedable<EmbeddedProfessorEntity>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Field"/> property
        /// </summary>
        private string? mField;

        /// <summary>
        /// The member of the <see cref="ResearchInterests"/> property
        /// </summary>
        private string? mResearchInterests;

        /// <summary>
        /// The member of the <see cref="Websites"/> property
        /// </summary>
        private IList<Website>? mWebsites;

        /// <summary>
        /// The member of the <see cref="Lectures"/> property
        /// </summary>
        private IList<Lecture>? mLectures;

        #endregion

        #region Public Properties

        /// <summary>
        /// The rank
        /// </summary>
        public ProfessorRank Rank { get; set; }

        /// <summary>
        /// The personal websites 
        /// </summary>
        public IList<Website> Websites 
        { 
            get => mWebsites ??= new List<Website>();
            set => mWebsites = value;
        }

        /// <summary>
        /// The field of study
        /// </summary>
        public string Field
        {
            get => mField ?? string.Empty;
            set => mField = value;
        }

        /// <summary>
        /// The research interests
        /// </summary>
        public string ResearchInterests
        {
            get => mResearchInterests ?? string.Empty;
            set => mResearchInterests = value;
        }

        /// <summary>
        /// The lectures 
        /// </summary>
        public IList<Lecture> Lectures
        {
            get
            {
                if(mLectures == null)
                    mLectures = new List<Lecture>();
                return mLectures;
            }
            set => mLectures = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="ProfessorEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static async Task<ProfessorEntity> FromRequestModelAsync(ProfessorRequestModel model)
        {
            var entity = new ProfessorEntity();

            DI.Mapper.Map(model, entity);
            
            entity = await UpdateNonAutoMapperValuesAsync(model, entity);
            
            return entity;
        }

        /// <summary>
        /// Updates the values of the specified <paramref name="entity"/> with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static async Task<ProfessorEntity> UpdateNonAutoMapperValuesAsync(ProfessorRequestModel model, ProfessorEntity entity)
        {
            entity.User = !model.UserId.IsNullOrEmpty() ? await EntityHelpers.GetUserAsync(model.UserId) : null;
            entity.Department = !model.DepartmentId.IsNullOrEmpty() ? await EntityHelpers.GetDepartmetAsync(model.DepartmentId) : null;

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="ProfessorResponseModel"/> from the current <see cref="ProfessorEntity"/>
        /// </summary>
        /// <returns></returns>
        public ProfessorResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<ProfessorResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedProfessorEntity"/> from the current <see cref="ProfessorEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedProfessorEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedProfessorEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="ProfessorEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedProfessorEntity : EmbeddedStaffMemberEntity
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Field"/> property
        /// </summary>
        private string? mField;

        #endregion

        #region Public Properties

        /// <summary>
        /// The rank
        /// </summary>
        public ProfessorRank Rank { get; set; }

        /// <summary>
        /// The field of study
        /// </summary>
        public string Field
        {
            get => mField ?? string.Empty;
            set => mField = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedProfessorEntity() : base()
        {

        }

        #endregion
    }
}

﻿using AutoMapper;

namespace MeetEdu
{
    /// <summary>
    /// Represents a university document in the MongoDB
    /// </summary>
    public class UniversityEntity : StandardEntity, IImageable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Labels"/> property
        /// </summary>
        private IEnumerable<EmbeddedLabelEntity>? mLabels;

        #endregion

        #region Public Properties

        /// <summary>
        /// The image
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<EmbeddedLabelEntity> Labels
        {
            get => mLabels ?? Enumerable.Empty<EmbeddedLabelEntity>();
            set => mLabels = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UniversityEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="UniversityEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static UniversityEntity FromRequestModelAsync(UniversityRequestModel model)
        {
            var entity = new UniversityEntity();

            DI.Mapper.Map(model, entity);

            UpdateNonAutoMapperValues(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="UniversityResponseModel"/> from the current <see cref="UniversityEntity"/>
        /// </summary>
        /// <returns></returns>
        public UniversityResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<UniversityResponseModel>(this);

        /// <summary>
        /// Updates the values of the specified <paramref name="entity"/> with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static async void UpdateNonAutoMapperValues(UniversityRequestModel model, UniversityEntity entity)
        {
            if (!model.LabelIds.IsNullOrEmpty())
            {
                var formattedLabelIds = model.LabelIds.Select(l => l.ToObjectId()).ToList();
                var labels = await MeetEduDbMapper.UniversityLabels.SelectAsync(x => formattedLabelIds.Contains(x.Id));

                entity.Labels = labels.Select(l => l.ToEmbeddedEntity()).ToList();
            }
        }

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedUniversityEntity"/> from the current <see cref="UniversityEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedUniversityEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedUniversityEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="UniversityEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedUniversityEntity : EmbeddedStandardEntity, IImageable
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
        public EmbeddedUniversityEntity() : base()
        {

        }

        #endregion
    }
}

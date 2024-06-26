﻿using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents an appointment template document in the MongoDB
    /// </summary>
    public class AppointmentRuleEntity : StandardEntity, IDescriptable, INoteable, IProfessorIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Link"/> property
        /// </summary>
        private string? mLink;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        /// <summary>
        /// The member of the property <see cref="StartMinutes"/>
        /// </summary>
        private IEnumerable<int>? mStartMinutes;

        #endregion

        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public ObjectId ProfessorId { get; set; }

        /// <summary>
        /// A flag indicating whether it has a remote option or not
        /// </summary>
        public bool HasRemoteOption { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The link for the online appointment
        /// </summary>
        public string Link
        {
            get => mLink ?? string.Empty;
            set => mLink = value;
        }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// The date the rules is valid from
        /// </summary>
        public DateTimeOffset DateFrom { get; set; }

        /// <summary>
        /// The date the rules is valid to
        /// </summary>
        public DateTimeOffset DateTo { get; set; }

        /// <summary>
        /// The possible start minutes of the appointment
        /// </summary>
        public IEnumerable<int> StartMinutes 
        {
            get => mStartMinutes ?? Enumerable.Empty<int>();
            set => mStartMinutes = value; 
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentRuleEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="AppointmentRuleEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static AppointmentRuleEntity FromRequestModel(AppointmentRuleRequestModel model)
        {
            var entity = new AppointmentRuleEntity();

            DI.Mapper.Map(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="AppointmentRuleResponseModel"/> from the current <see cref="AppointmentRuleEntity"/>
        /// </summary>
        /// <returns></returns>
        public AppointmentRuleResponseModel ToResponseModel() 
            => EntityHelpers.ToResponseModel<AppointmentRuleResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedAppointmentRuleEntity"/> from the current <see cref="AppointmentRuleEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedAppointmentRuleEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedAppointmentRuleEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="AppointmentRuleEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedAppointmentRuleEntity : EmbeddedStandardEntity, IProfessorIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public ObjectId ProfessorId { get; set; }

        /// <summary>
        /// A flag indicating whether it has a remote option or not
        /// </summary>
        public bool HasRemoteOption { get; set; }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedAppointmentRuleEntity() : base()
        {

        }

        #endregion
    }
}

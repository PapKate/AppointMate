﻿using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Provides methods for managing professors
    /// </summary>
    public class ProfessorsRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="ProfessorsRepository"/>
        /// </summary>
        public static ProfessorsRepository Instance { get; } = new ProfessorsRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorsRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a professor
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorEntity>> AddProfessorAsync(ProfessorRequestModel model, CancellationToken cancellationToken = default)
        {
            return await MeetEduDbMapper.Professors.AddAsync(await ProfessorEntity.FromRequestModelAsync(model), cancellationToken);
        }

        /// <summary>
        /// Updates the professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="professor">The professor</param>
        /// <param name="user">The user</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorEntity>> UpdateProfessorAsync(ObjectId id, ProfessorRequestModel professor, UserRequestModel user, CancellationToken cancellationToken = default)
        {
            var professorEntity = await MeetEduDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the professor does not exist...
            if (professorEntity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Professors));

            professorEntity = await MeetEduDbMapper.Professors.UpdateAsync(id, professor, cancellationToken);

            var userEntity = await MeetEduDbMapper.Users.FirstAsync(professorEntity!.UserId, cancellationToken);

            userEntity = await MeetEduDbMapper.Users.UpdateAsync(userEntity.Id, user, cancellationToken);

            // If the user exists...
            if (user is not null)
                professorEntity.User = userEntity!.ToEmbeddedEntity();

            return professorEntity;
        }

        /// <summary>
        /// Deletes the professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorEntity>> DeleteProfessorAsync(ObjectId id, CancellationToken cancellationToken = default)
        {
            // Gets the professor
            var entity = await MeetEduDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the professor does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Professors));

            // Removes the professor from the database
            await MeetEduDbMapper.Professors.DeleteAsync(id, cancellationToken);
            // Removes the user from the database
            await MeetEduDbMapper.Users.DeleteAsync(entity.UserId, cancellationToken);

            return entity;
        }

        #endregion
    }
}
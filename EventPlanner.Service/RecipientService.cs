using EventPlanner.Core.Notification;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Service.Interfaces;

namespace EventPlanner.Service
{
    public class RecipientService : IRecipientService
    {
        #region Fields

        private readonly IRecipientRepository _recipientRepository;

        #endregion Fields

        #region Constructors

        public RecipientService(IRecipientRepository recipientRepository)
        {
            _recipientRepository = recipientRepository;
        }

        #endregion Constructors

        #region Private Methods



        #endregion Private Methods

        #region Public Methods

        public async Task<Recipient?> Get(Guid id)
        {
            return await _recipientRepository.Get(id);
        }

        public async Task<List<Recipient>> GetMany(List<Guid> ids)
        {
            return await _recipientRepository.GetMany(ids);
        }

        public async Task<List<Recipient>> GetAll()
        {
            return await _recipientRepository.GetAll();
        }

        public async Task<bool> Create(Recipient recipient)
        {
            return await _recipientRepository.Create(recipient);
        }

        public async Task<bool> Update(Recipient recipient)
        {
            recipient.UpdatedDate = DateTime.UtcNow;
            
            return await _recipientRepository.Update(recipient);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _recipientRepository.Delete(id);
        }

        #endregion Public Methods
    }
}

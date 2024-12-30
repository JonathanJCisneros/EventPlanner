using EventPlanner.Core;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Service.Interfaces;

namespace EventPlanner.Service
{
    public class InquiryService : IInquiryService
    {
        #region Fields

        private readonly IInquiryRepository _inquiryRepository;

        #endregion Fields

        #region Constructors

        public InquiryService(IInquiryRepository inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        #endregion Constructors

        #region Private Methods



        #endregion Private Methods

        #region Public Methods

        public async Task<Inquiry?> Get(Guid id)
        {
            return await _inquiryRepository.Get(id);
        }

        public async Task<List<Inquiry>> GetMany(List<Guid> ids)
        {
            return await _inquiryRepository.GetMany(ids);
        }

        public async Task<List<Inquiry>> GetAll()
        {
            return await _inquiryRepository.GetAll();
        }

        public async Task<bool> Create(Inquiry inquiry)
        {
            return await _inquiryRepository.Create(inquiry);
        }

        public async Task<bool> Update(Inquiry inquiry)
        {
            inquiry.UpdatedDate = DateTime.UtcNow;

            return await _inquiryRepository.Update(inquiry);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _inquiryRepository.Delete(id);
        }

        #endregion Public Methods        
    }
}

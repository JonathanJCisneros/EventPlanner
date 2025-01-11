using EventPlanner.Core.Address;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Service.Interfaces;

namespace EventPlanner.Service
{
    public class AddressService : IAddressService
    {
        #region Fields

        private readonly IAddressRepository _addressRepository;

        #endregion Fields

        #region Constructors

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        #endregion Constructors

        #region Private Methods



        #endregion Private Methods

        #region Public Methods

        public async Task<Address?> Get(Guid id)
        {
            return await _addressRepository.Get(id);
        }

        public async Task<List<Address>> GetMany(List<Guid> ids)
        {
            return await _addressRepository.GetMany(ids);
        }

        public async Task<List<Address>> GetAll()
        {
            return await _addressRepository.GetAll();
        }

        public async Task<bool> Create(Address address)
        {
            return await _addressRepository.Create(address);
        }

        public async Task<bool> Update(Address address)
        {
            address.UpdatedDate = DateTime.UtcNow;

            return await _addressRepository.Update(address);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _addressRepository.Delete(id);
        }

        #endregion Public Methods
    }
}

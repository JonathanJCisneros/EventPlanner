using EventPlanner.Core.Event;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Service.Interfaces;

namespace EventPlanner.Service
{
    public class EventService : IEventService
    {
        #region Fields

        private readonly IEventRepository _eventRepository;

        #endregion Fields

        #region Constructors

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        #endregion Constructors

        #region Private Methods



        #endregion Private Methods

        #region Public Methods

        public async Task<Event?> Get(Guid id)
        {
            return await _eventRepository.Get(id);
        }

        public async Task<List<Event>> GetMany(List<Guid> ids)
        {
            return await _eventRepository.GetMany(ids);
        }

        public async Task<List<Event>> GetAll()
        {
            return await _eventRepository.GetAll();
        }

        public async Task<bool> Create(Event entity)
        {
            return await _eventRepository.Create(entity);
        }

        public async Task<bool> Update(Event entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;

            return await _eventRepository.Update(entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _eventRepository.Delete(id);
        }

        #endregion Public Methods
    }
}
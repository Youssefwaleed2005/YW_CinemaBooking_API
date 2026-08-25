// Services/CustomerService.cs
using AutoMapper;
using CinemaTicketBooking_WebAPI.DTOs;
using CinemaTicketBooking_WebAPI.Exceptions;
using CinemaTicketBooking_WebAPI.Models;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;
using CinemaTicketBooking_WebAPI.Services.Interfaces;

namespace CinemaTicketBooking_WebAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepo customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var customers = await _customerRepo.GetAll();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetById(int id)
        {
            var customer = await _customerRepo.GetById(id);
            if (customer is null)
                throw new CustomerNotFoundException(id);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> Create(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _customerRepo.Add(customer);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> Update(int id, UpdateCustomerDto dto)
        {
            var customer = await _customerRepo.GetById(id);
            if (customer is null)
                throw new CustomerNotFoundException(id);

            customer.Name = dto.Name;
            customer.Email = dto.Email;
            customer.UpdatedAt = DateTime.UtcNow;

            await _customerRepo.Update(customer);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task Delete(int id)
        {
            var customer = await _customerRepo.GetById(id);
            if (customer is null)
                throw new CustomerNotFoundException(id);

            if (await _customerRepo.HasBookings(id))
                throw new InvalidBookingException($"Customer {id} cannot be deleted because they have bookings.");

            await _customerRepo.Delete(customer);
        }
    }
}
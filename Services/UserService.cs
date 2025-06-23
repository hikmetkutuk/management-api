using ManagementApi.DTOs;
using ManagementApi.Entities;
using ManagementApi.Messaging;
using ManagementApi.Repositories;

namespace ManagementApi.Services;

public class UserService(UserRepository userRepository, RabbitMqPublisher rabbitMqPublisher)
{
    public async Task<List<User>> GetAllAsync() => await userRepository.GetAllAsync();

    public async Task<User?> GetByIdAsync(int id) => await userRepository.GetByIdAsync(id);

    public async Task AddAsync(UserCreateDto dto)
    {
        await userRepository.AddAsync(dto);
        // Kullanıcı eklendiğinde RabbitMQ'ya mesaj gönder
        rabbitMqPublisher.PublishUserCreated(dto);
    }

    public async Task UpdateAsync(User user) => await userRepository.UpdateAsync(user);

    public async Task DeleteAsync(int id) => await userRepository.DeleteAsync(id);
}
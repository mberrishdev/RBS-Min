﻿using Common.Repository.Repository;
using RBS.Application.Services.PlatformNotifications.Models;
using RBS.Domain.PlatformNotifications;
using RBS.Domain.PlatformNotifications.Commands;

namespace RBS.Application.Services.PlatformNotifications
{
    public class PlatformNotificationService : IPlatformNotificationService
    {
        private readonly IRepository<PlatformNotification> _repository;
        private readonly IQueryRepository<PlatformNotification> _queryRepository;

        public PlatformNotificationService(IRepository<PlatformNotification> repository)
        {
            _repository = repository;
        }

        public async Task CreatePlatformNotification(CreatePlatformNotificationCommand command, CancellationToken cancellationToken)
        {
            var platformNotificaton = new PlatformNotification(command);
            await _repository.InsertAsync(platformNotificaton, cancellationToken);
        }

        public async Task<List<PlatformNotificationModel>> GetUserNotifications(int userId, CancellationToken cancellationToken)
        {
            var notifications = await _queryRepository.GetListAsync(predicate: x => x.ApplicationUserId == userId, cancellationToken: cancellationToken);
            return notifications.Select(x => new PlatformNotificationModel(x)).ToList();
        }
    }
}

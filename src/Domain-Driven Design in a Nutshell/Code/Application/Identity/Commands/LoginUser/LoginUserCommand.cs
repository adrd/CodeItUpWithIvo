﻿namespace CarRentalSystem.Application.Identity.Commands.LoginUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Domain.Dealerships.Repositories;
    using MediatR;

    public class LoginUserCommand : UserInputModel, IRequest<Result<LoginOutputModel>>
    {
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentity identity;
            private readonly IDealerDomainRepository dealerRepository;

            public LoginUserCommandHandler(
                IIdentity identity,
                IDealerDomainRepository dealerRepository)
            {
                this.identity = identity;
                this.dealerRepository = dealerRepository;
            }

            public async Task<Result<LoginOutputModel>> Handle(
                LoginUserCommand request,
                CancellationToken cancellationToken)
            {
                var result = await this.identity.Login(request);

                if (!result.Succeeded)
                {
                    return result.Errors;
                }

                var user = result.Data;

                var dealerId = await this.dealerRepository.GetDealerId(user.UserId, cancellationToken);

                return new LoginOutputModel(user.Token, dealerId);
            }
        }
    }
}

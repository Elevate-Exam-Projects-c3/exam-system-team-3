using exam_system.Common.Enums;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Register.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;

namespace exam_system.Features.Identity.Register.Handlers;

public class RegisterUserCommandHandler(
    IGenericRepository<ApplicationUser> userRepo,
    IGenericRepository<EmailVerificationOtp> otpRepo,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterUserCommand, RequestResponse<RegisterUserResponse>>
{
    public async Task<RequestResponse<RegisterUserResponse>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var emailExists = await userRepo
            .Get(u => u.Email.ToLower() == request.Email.ToLower())
            .AnyAsync(cancellationToken);

        if (emailExists)
        {
            return RequestResponse<RegisterUserResponse>.Fail(
                "Email already registered.",
                StatusCodes.Status409Conflict);
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = UserRole.Student,
            AccountStatus = AccountStatus.Pending,
            EmailConfirmed = false
        };

        await userRepo.AddAsync(user);

        var plainOtp = Random.Shared.Next(100000, 999999).ToString();
        var otpHash = BCrypt.Net.BCrypt.HashPassword(plainOtp, workFactor: 12);

        var otp = new EmailVerificationOtp
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Email = user.Email,
            OtpHash = otpHash,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10),
            AttemptCount = 0,
            IsUsed = false
        };

        await otpRepo.AddAsync(otp);

    
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // 6. TODO: إرسال الـ plainOtp بالإيميل — تأكد مين شايل Email Service في التيم

        return RequestResponse<RegisterUserResponse>.Created(
            new RegisterUserResponse(user.Id, user.Email, "Registration successful. Please verify your email."));
    }
}
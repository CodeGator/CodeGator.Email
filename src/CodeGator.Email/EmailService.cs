
namespace CodeGator.Email;

/// <summary>
/// This interface represents an object that sends emails.
/// </summary>
/// <param name="emailStrategy">The email strategy to use with this service.</param>
internal sealed class EmailService(
    [NotNull] IEmailStrategy emailStrategy
    ) : IEmailService
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods
    
    /// <inheritdoc/>
    public async Task<EmailResult> SendAsync(
        [NotNull] string fromAddress,
        [NotNull] IEnumerable<string> toAddresses,
        [NotNull] IEnumerable<string> ccAddresses,
        [NotNull] IEnumerable<string> bccAddresses,
        [NotNull] IEnumerable<string> attachments,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml = false,
        CancellationToken cancellationToken = default
        )
    {
        Guard.Instance().ThrowIfNullOrEmpty(fromAddress, nameof(fromAddress))
            .ThrowIfNull(toAddresses, nameof(toAddresses))
            .ThrowIfNull(ccAddresses, nameof(ccAddresses))
            .ThrowIfNull(bccAddresses, nameof(bccAddresses))
            .ThrowIfNull(attachments, nameof(attachments))
            .ThrowIfNull(subject, nameof(subject))
            .ThrowIfNull(body, nameof(body));

        return await emailStrategy.SendAsync(
            fromAddress,
            toAddresses,
            ccAddresses,
            bccAddresses,
            attachments,
            subject,
            body,
            bodyIsHtml,
            cancellationToken
            ).ConfigureAwait(false);
    }

    #endregion
}


namespace CodeGator.Email;

/// <summary>
/// This interface represents an object that sends emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// This method sends an email.
    /// </summary>
    /// <param name="fromAddress">The from address to use for the operation.</param>
    /// <param name="toAddresses">The to addresses to use for the operation.</param>
    /// <param name="ccAddresses">The CC addresses to use for the operation.</param>
    /// <param name="bccAddresses">The BCC addresses to use for the operation.</param>
    /// <param name="attachments">The attachments to use for the operation.</param>
    /// <param name="subject">The subject to use for the operation.</param>
    /// <param name="body">The body to use for the operation.</param>
    /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for a cancellation request throughout the lifetime of the method.</param>
    /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
    /// object, representing the results of the operation.</returns>
    /// <exception cref="ArgumentNullException">This exception is thrown whenever one or 
    /// more arguments are missing, or invalid.</exception>
    /// <exception cref="ServiceException">This exception is thrown whenever the operation
    /// fails, for any reason.</exception>
    Task<EmailResult> SendAsync(
        [NotNull] string fromAddress,
        [NotNull] IEnumerable<string> toAddresses,
        [NotNull] IEnumerable<string> ccAddresses,
        [NotNull] IEnumerable<string> bccAddresses,
        [NotNull] IEnumerable<string> attachments,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml = false,
        CancellationToken cancellationToken = default
        );
}

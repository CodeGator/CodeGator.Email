
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace CodeGator.Email;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class utility contains extension methods related to the <see cref="IEmailService"/>
/// type.
/// </summary>
public static partial class EmailServiceExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method sends an email.
    /// </summary>
    /// <param name="service">The service to use for the operation.</param>
    /// <param name="fromAddress">The from address to use for the operation.</param>
    /// <param name="toAddresses">The to addresses to use for the operation.</param>
    /// <param name="ccAddresses">The CC addresses to use for the operation.</param>
    /// <param name="bccAddresses">The BCC addresses to use for the operation.</param>
    /// <param name="attachments">The attachments to use for the operation.</param>
    /// <param name="subject">The subject to use for the operation.</param>
    /// <param name="body">The body to use for the operation.</param>
    /// <param name="bodyIsHtml">True is the body contains HTML; False otherwise.</param>
    /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
    /// object, representing the results of the operation.</returns>
    /// <exception cref="ArgumentNullException">This exception is thrown whenever one or 
    /// more arguments are missing, or invalid.</exception>
    /// <exception cref="ServiceException">This exception is thrown whenever the operation
    /// fails, for any reason.</exception>
    public static EmailResult Send(
        [NotNull] this IEmailService service,
        [NotNull] string fromAddress,
        [NotNull] IEnumerable<string> toAddresses,
        [NotNull] IEnumerable<string> ccAddresses,
        [NotNull] IEnumerable<string> bccAddresses,
        [NotNull] IEnumerable<string> attachments,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml
        )
    {
        Guard.Instance().ThrowIfNull(service, nameof(service));

        return service.SendAsync(
            fromAddress,
            toAddresses,
            ccAddresses,
            bccAddresses,
            attachments,
            subject,
            body,
            bodyIsHtml
            ).Result;
    }

    // *******************************************************************

    /// <summary>
    /// This method sends an email.
    /// </summary>
    /// <param name="service">The service object to use for the operation.</param>
    /// <param name="fromAddress">The from address to use for the operation.</param>
    /// <param name="toAddress">The to address to use for the operation.</param>
    /// <param name="subject">The subject to use for the operation.</param>
    /// <param name="body">The body to use for the operation.</param>
    /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
    /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
    /// object, representing the results of the operation.</returns>
    /// <exception cref="ArgumentNullException">This exception is thrown whenever one or 
    /// more arguments are missing, or invalid.</exception>
    /// <exception cref="ServiceException">This exception is thrown whenever the operation
    /// fails, for any reason.</exception>
    public static EmailResult Send(
        [NotNull] this IEmailService service,
        [NotNull] string fromAddress,
        [NotNull] string toAddress,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml = false
        )
    {
        Guard.Instance().ThrowIfNull(service, nameof(service))
            .ThrowIfNullOrEmpty(fromAddress, nameof(fromAddress));

        return service.Send(
            fromAddress,
            toAddress.Split(';'),
            new string[0],
            new string[0],
            new string[0],
            subject,
            body,
            bodyIsHtml
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method sends an email.
    /// </summary>
    /// <param name="service">The service object to use for the operation.</param>
    /// <param name="fromAddress">The from address to use for the operation.</param>
    /// <param name="toAddress">The to address to use for the operation.</param>
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
    public static async Task<EmailResult> SendAsync(
        [NotNull] this IEmailService service,
        [NotNull] string fromAddress,
        [NotNull] string toAddress,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml = false,
        CancellationToken cancellationToken = default(CancellationToken)
        )
    {
        Guard.Instance().ThrowIfNull(service, nameof(service))
            .ThrowIfNullOrEmpty(toAddress, nameof(toAddress));

        return await service.SendAsync(
            fromAddress,
            toAddress.Split(';'),
            new string[0],
            new string[0],
            new string[0],
            subject,
            body,
            bodyIsHtml
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method sends an email.
    /// </summary>
    /// <param name="service">The service object to use for the operation.</param>
    /// <param name="fromAddress">The from address to use for the operation.</param>
    /// <param name="toAddress">The to address to use for the operation.</param>
    /// <param name="ccAddress">The cc address to use for the operation.</param>
    /// <param name="subject">The subject to use for the operation.</param>
    /// <param name="body">The body to use for the operation.</param>
    /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
    /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
    /// object, representing the results of the operation.</returns>
    /// <exception cref="ArgumentNullException">This exception is thrown whenever one or 
    /// more arguments are missing, or invalid.</exception>
    /// <exception cref="ServiceException">This exception is thrown whenever the operation
    /// fails, for any reason.</exception>
    public static EmailResult Send(
        [NotNull] this IEmailService service,
        [NotNull] string fromAddress,
        [NotNull] string toAddress,
        [NotNull] string ccAddress,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml = false
        )
    {
        Guard.Instance().ThrowIfNull(service, nameof(service))
            .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
            .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress));

        return service.Send(
            fromAddress,
            toAddress.Split(';'),
            ccAddress.Split(';'),
            new string[0],
            new string[0],
            subject,
            body,
            bodyIsHtml
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method sends an email.
    /// </summary>
    /// <param name="service">The service object to use for the operation.</param>
    /// <param name="fromAddress">The from address to use for the operation.</param>
    /// <param name="toAddress">The to address to use for the operation.</param>
    /// <param name="ccAddress">The cc address to use for the operation.</param>
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
    public static async Task<EmailResult> SendAsync(
        [NotNull] this IEmailService service,
        [NotNull] string fromAddress,
        [NotNull] string toAddress,
        [NotNull] string ccAddress,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml = false,
        CancellationToken cancellationToken = default(CancellationToken)
        )
    {
        Guard.Instance().ThrowIfNull(service, nameof(service))
            .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
            .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress));

        return await service.SendAsync(
            fromAddress,
            toAddress.Split(';'),
            ccAddress.Split(';'),
            new string[0],
            new string[0],
            subject,
            body,
            bodyIsHtml,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method sends an email.
    /// </summary>
    /// <param name="service">The service object to use for the operation.</param>
    /// <param name="fromAddress">The from address to use for the operation.</param>
    /// <param name="toAddress">The to address to use for the operation.</param>
    /// <param name="ccAddress">The cc address to use for the operation.</param>
    /// <param name="bccAddress">The cc address to use for the operation.</param>
    /// <param name="subject">The subject to use for the operation.</param>
    /// <param name="body">The body to use for the operation.</param>
    /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
    /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
    /// object, representing the results of the operation.</returns>
    /// <exception cref="ArgumentNullException">This exception is thrown whenever one or 
    /// more arguments are missing, or invalid.</exception>
    /// <exception cref="ServiceException">This exception is thrown whenever the operation
    /// fails, for any reason.</exception>
    public static EmailResult Send(
        [NotNull] this IEmailService service,
        [NotNull] string fromAddress,
        [NotNull] string toAddress,
        [NotNull] string ccAddress,
        [NotNull] string bccAddress,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml = false
        )
    {
        Guard.Instance().ThrowIfNull(service, nameof(service))
            .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
            .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress))
            .ThrowIfNullOrEmpty(bccAddress, nameof(bccAddress));

        return service.Send(
            fromAddress,
            toAddress.Split(';'),
            ccAddress.Split(';'),
            bccAddress.Split(';'),
            new string[0],
            subject,
            body,
            bodyIsHtml
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method sends an email.
    /// </summary>
    /// <param name="service">The service object to use for the operation.</param>
    /// <param name="fromAddress">The from address to use for the operation.</param>
    /// <param name="toAddress">The to address to use for the operation.</param>
    /// <param name="ccAddress">The cc address to use for the operation.</param>
    /// <param name="bccAddress">The bcc address to use for the operation.</param>
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
    public static async Task<EmailResult> SendAsync(
        [NotNull] this IEmailService service,
        [NotNull] string fromAddress,
        [NotNull] string toAddress,
        [NotNull] string ccAddress,
        [NotNull] string bccAddress,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml = false,
        CancellationToken cancellationToken = default(CancellationToken)
        )
    {
        Guard.Instance().ThrowIfNull(service, nameof(service))
            .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
            .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress))
            .ThrowIfNullOrEmpty(bccAddress, nameof(bccAddress));

        return await service.SendAsync(
            fromAddress,
            toAddress.Split(';'),
            ccAddress.Split(';'),
            bccAddress.Split(';'),
            new string[0],
            subject,
            body,
            bodyIsHtml,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method sends an email.
    /// </summary>
    /// <param name="service">The service object to use for the operation.</param>
    /// <param name="fromAddress">The from address to use for the operation.</param>
    /// <param name="toAddress">The to address to use for the operation.</param>
    /// <param name="ccAddress">The cc address to use for the operation.</param>
    /// <param name="bccAddress">The cc address to use for the operation.</param>
    /// <param name="attachment">The attachment to use for the operation.</param>
    /// <param name="subject">The subject to use for the operation.</param>
    /// <param name="body">The body to use for the operation.</param>
    /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
    /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
    /// object, representing the results of the operation.</returns>
    /// <exception cref="ArgumentNullException">This exception is thrown whenever one or 
    /// more arguments are missing, or invalid.</exception>
    /// <exception cref="ServiceException">This exception is thrown whenever the operation
    /// fails, for any reason.</exception>
    public static EmailResult Send(
        [NotNull] this IEmailService service,
        [NotNull] string fromAddress,
        [NotNull] string toAddress,
        [NotNull] string ccAddress,
        [NotNull] string bccAddress,
        [NotNull] string attachment,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml = false
        )
    {
        Guard.Instance().ThrowIfNull(service, nameof(service))
            .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
            .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress))
            .ThrowIfNullOrEmpty(bccAddress, nameof(bccAddress))
            .ThrowIfNullOrEmpty(attachment, nameof(attachment));

        return service.Send(
            fromAddress,
            toAddress.Split(';'),
            ccAddress.Split(';'),
            bccAddress.Split(';'),
            attachment.Split(';'),
            subject,
            body,
            bodyIsHtml
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method sends an email.
    /// </summary>
    /// <param name="service">The service object to use for the operation.</param>
    /// <param name="fromAddress">The from address to use for the operation.</param>
    /// <param name="toAddress">The to address to use for the operation.</param>
    /// <param name="ccAddress">The cc address to use for the operation.</param>
    /// <param name="bccAddress">The bcc address to use for the operation.</param>
    /// <param name="attachment">The attachment to use for the operation.</param>
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
    public static async Task<EmailResult> SendAsync(
        [NotNull] this IEmailService service,
        [NotNull] string fromAddress,
        [NotNull] string toAddress,
        [NotNull] string ccAddress,
        [NotNull] string bccAddress,
        [NotNull] string attachment,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml = false,
        CancellationToken cancellationToken = default(CancellationToken)
        )
    {
        Guard.Instance().ThrowIfNull(service, nameof(service))
            .ThrowIfNullOrEmpty(fromAddress, nameof(fromAddress))
            .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
            .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress))
            .ThrowIfNullOrEmpty(bccAddress, nameof(bccAddress))
            .ThrowIfNullOrEmpty(attachment, nameof(attachment));

        return await service.SendAsync(
            fromAddress,
            toAddress.Split(';'),
            ccAddress.Split(';'),
            bccAddress.Split(';'),
            attachment.Split(';'),
            subject,
            body,
            bodyIsHtml
            ).ConfigureAwait(false);
    }

    #endregion
}

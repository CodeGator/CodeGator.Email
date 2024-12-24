
namespace CodeGator.Email.Strategies.SMTP;

/// <summary>
/// This class is an SMTP implementation of the <see cref="IEmailStrategy"/>
/// interface.
/// </summary>
/// <param name="smtpClient">The SMTP client to use with this strategy.</param>
/// <param name="logger">The logger to use with this strategy.</param>
internal sealed class SmtpEmailStrategy(
    [NotNull] SmtpClient smtpClient,
    [NotNull] ILogger<SmtpEmailStrategy> logger
    ) : IEmailStrategy
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <inheritdoc/>
    public Task<EmailResult> SendAsync(
        [NotNull] string fromAddress,
        [NotNull] IEnumerable<string> toAddresses,
        [NotNull] IEnumerable<string> ccAddresses,
        [NotNull] IEnumerable<string> bccAddresses,
        [NotNull] IEnumerable<string> attachments,
        [NotNull] string subject,
        [NotNull] string body,
        bool bodyIsHtml = false,
        CancellationToken token = default
        )
    {
        Guard.Instance().ThrowIfNullOrEmpty(fromAddress, nameof(fromAddress))
            .ThrowIfNull(toAddresses, nameof(toAddresses))
            .ThrowIfNull(ccAddresses, nameof(ccAddresses))
            .ThrowIfNull(bccAddresses, nameof(bccAddresses))
            .ThrowIfNull(attachments, nameof(attachments))
            .ThrowIfNull(subject, nameof(subject))
            .ThrowIfNull(body, nameof(body));

        logger.LogDebug(
            "The '{t1}' strategy is building a message.",
            nameof(SmtpEmailStrategy)
            );

        var message = BuildAMessage(
                fromAddress,
                toAddresses,
                ccAddresses,
                bccAddresses,
                attachments,
                subject,
                body,
                bodyIsHtml
                );

        logger.LogDebug(
            "The '{t1}' strategy is sending a message.",
            nameof(SmtpEmailStrategy)
            );

        smtpClient.Send(message);

        logger.LogDebug(
            "The '{t1}' strategy is formatting a return code.",
            nameof(SmtpEmailStrategy)
            );

        var retValue = new EmailResult()
        {
            EmailId = $"{Guid.NewGuid():N}"
        };

        return Task.FromResult(retValue);
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method builds up a <see cref="MailMessage"/> and returns it.
    /// </summary>
    /// <param name="fromAddress">The from address to use for the operation.</param>
    /// <param name="toAddresses">The to addresses to use for the operation.</param>
    /// <param name="ccAddresses">The CC addresses to use for the operation.</param>
    /// <param name="bccAddresses">The BCC addresses to use for the operation.</param>
    /// <param name="attachments">The attachments to use for the operation.</param>
    /// <param name="subject">The subject to use for the operation.</param>
    /// <param name="body">The body to use for the operation.</param>
    /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
    /// <returns>A populated <see cref="MailMessage"/> object.</returns>
    private static MailMessage BuildAMessage(
        string fromAddress,
        IEnumerable<string> toAddresses,
        IEnumerable<string> ccAddresses,
        IEnumerable<string> bccAddresses,
        IEnumerable<string> attachments,
        string subject,
        string body,
        bool bodyIsHtml
        )
    {
        var message = new MailMessage()
        {
            From = new MailAddress(fromAddress)
        };

        foreach (var toAddress in toAddresses.Select(x => x.Trim()))
        {
            if (false == string.IsNullOrEmpty(toAddress))
            {
                message.To.Add(new MailAddress(toAddress));
            }
        }

        foreach (var ccAddress in ccAddresses.Select(x => x.Trim()))
        {
            if (false == string.IsNullOrEmpty(ccAddress))
            {
                message.CC.Add(new MailAddress(ccAddress));
            }
        }

        foreach (var bccAddress in bccAddresses.Select(x => x.Trim()))
        {
            if (false == string.IsNullOrEmpty(bccAddress))
            {
                message.Bcc.Add(new MailAddress(bccAddress));
            }
        }

        foreach (string attachment in attachments.Select(x => x.Trim()))
        {
            if (false == string.IsNullOrEmpty(attachment))
            {
                message.Attachments.Add(new Attachment(attachment));
            }
        }

        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = bodyIsHtml;

        return message;
    }

    #endregion
}

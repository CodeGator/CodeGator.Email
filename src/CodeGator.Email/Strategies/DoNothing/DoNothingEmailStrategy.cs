

namespace CodeGator.Email.Strategies.DoNothing;

/// <summary>
/// This class is a "Do Nothing" implementation of the <see cref="IEmailStrategy"/>
/// interface.
/// </summary>
/// <param name="logger">The logger to use with this strategy</param>
internal class DoNothingEmailStrategy(
    [NotNull] ILogger<DoNothingEmailStrategy> logger
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
        logger.LogWarning(
            "The '{t1}' strategy is only pretending to send an email " +
            "to '{t2}' with the subject: '{t3}' because this strategy " +
            "doesn't actually do anything. To actually send an email, " +
            "configure another strategy in the '{t4}' section of the configuration.",
            nameof(DoNothingEmailStrategy),
            string.Join(';', toAddresses),
            subject,
            EmailServiceOptions.SectionPath
            );

        var retValue = new EmailResult()
        {
            EmailId = $"{Guid.NewGuid():N}"
        };

        return Task.FromResult(retValue);
    }

    #endregion
}

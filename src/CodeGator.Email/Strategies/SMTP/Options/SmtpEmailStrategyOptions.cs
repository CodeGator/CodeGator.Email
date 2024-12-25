
namespace CodeGator.Email.Strategies.SMTP.Options;

/// <summary>
/// This class contains configuration settings for the <see cref="SmtpEmailStrategy"/>
/// strategy.
/// </summary>
public class SmtpEmailStrategyOptions : EmailStrategyOptions    
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the host address for the SMTP client.
    /// </summary>
    [Required]
    public string Host { get; set; } = null!;

    /// <summary>
    /// This property contains the host post for the SMTP client.
    /// </summary>
    [Required]
    public int Port { get; set; }

    /// <summary>
    /// This property contains the user name for the SMTP client.
    /// </summary>
    [Required]
    public string UserName { get; set; } = null!;

    /// <summary>
    /// This property contains the password for the SMTP client.
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;

    /// <summary>
    /// This property contains a flag to use SSL for sending emails.
    /// </summary>
    public bool EnableSSL { get; set; }

    /// <summary>
    /// This property contains an optional timeout for the SMTP client.
    /// </summary>
    public int? Timeout { get; set; }

    #endregion
}

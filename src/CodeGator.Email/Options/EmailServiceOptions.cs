
namespace CodeGator.Email.Options;

/// <summary>
/// This class represents configuration settings for the CodeGator email service.
/// </summary>
public class EmailServiceOptions
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the path to these options in the configuration.
    /// </summary>
    public const string SectionPath = "Services:Email";

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the name of the current strategy.
    /// </summary>
    [Required]
    public string CurrentStrategyName { get; set; } = null!;

    /// <summary>
    /// This property contains the collection of email strategy options.
    /// </summary>
    public List<EmailStrategyOptions> Strategies { get; set; } = [];

    #endregion
}

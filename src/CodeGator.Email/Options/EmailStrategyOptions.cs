
namespace CodeGator.Email.Options;

/// <summary>
/// This class represents configuration settings for the CodeGator email strategy.
/// </summary>
public class EmailStrategyOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the name of the strategy.
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// This property contains the full .NET type of the strategy.
    /// </summary>
    [Required]
    public string Type { get; set; } = null!;

    #endregion
}

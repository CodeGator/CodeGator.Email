
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.Hosting;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class utility contains extension methods for the <see cref="IHostApplicationBuilder"/> 
/// interface.
/// </summary>
public static partial class HostApplicationBuilderExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This methods add the abstractions required to support the CodeGator
    /// email service to the specified <see cref="IHostApplicationBuilder"/>   
    /// </summary>
    /// <typeparam name="TBuilder">The type of associated host builder.</typeparam>
    /// <param name="hostApplicationBuilder">The host application builder to 
    /// use for the operation.</param>
    /// <param name="bootstrapLogger">An optional bootstrap logger to use 
    /// for the operation.</param>
    /// <returns>The value of the <paramref name="hostApplicationBuilder"/> parameter,
    /// for chaining method calls together, Fluent style.</returns>
    public static TBuilder AddCodeGatorEmail<TBuilder>(
        [NotNull] this TBuilder hostApplicationBuilder,
        [AllowNull] ILogger? bootstrapLogger = null
        ) where TBuilder : IHostApplicationBuilder
    {
        Guard.Instance().ThrowIfNull(hostApplicationBuilder, nameof(hostApplicationBuilder));

        bootstrapLogger?.LogDebug(
            "Registering options for the CodeGator email service"
            );

        hostApplicationBuilder.Services.AddOptions<EmailServiceOptions>()
            .BindConfiguration(EmailServiceOptions.SectionPath)
            .ValidateOnStart()
            .ValidateDataAnnotations();

        bootstrapLogger?.LogDebug(
            "Registering the CodeGator email service"
            );

        hostApplicationBuilder.Services.AddScoped<IEmailService, EmailService>();

        bootstrapLogger?.LogDebug(
            "Registering the CodeGator email strategy"
            );

        hostApplicationBuilder.Services.AddScoped<IEmailStrategy>(serviceProvider =>
        {
            bootstrapLogger?.LogDebug(
                "Fetching the email service options"
                );

            var emailServiceOptions = serviceProvider.GetRequiredService<
                IOptions<EmailServiceOptions>
                >();

            var strategyName = emailServiceOptions.Value.CurrentStrategyName;

            bootstrapLogger?.LogDebug(
                $"Fetching the email strategy options for the '{strategyName}' strategy"
                );

            var strategyOptions = emailServiceOptions.Value.Strategies.FirstOrDefault(x =>
                x.Name.Equals(strategyName, StringComparison.InvariantCultureIgnoreCase)
                );

            if (strategyOptions is null)
            {
                throw new InvalidOperationException(
                    $"The options for strategy: '{strategyName}' are missing " +
                    $"in the configuration section: {EmailServiceOptions.SectionPath}" +
                    $":Strategies"
                    );
            }

            if (string.IsNullOrEmpty(strategyOptions.Type))
            {
                throw new InvalidOperationException(
                    $"The type for strategy: '{strategyName}' is missing, or empty " +
                    $"in the configuration section: {EmailServiceOptions.SectionPath}" +
                    $":Strategies"
                    );
            }

            bootstrapLogger?.LogDebug(
                $"Resolving type: '{strategyOptions.Type}' for the '{strategyName}' strategy"
                );

            var strategyType = Type.GetType(
                strategyOptions.Type,
                true
                ); 
            
            if (strategyType is null)
            {
                throw new InvalidOperationException(
                    $"The strategy type: '{strategyOptions.Type}' for strategy: " +
                    $"'{strategyName}' could not be resolved."
                    );
            }

            bootstrapLogger?.LogDebug(
                $"Creating an instance of: '{strategyOptions.Type}' for the '{strategyName}' strategy"
                );

            var emailStrategy = ActivatorUtilities.CreateInstance(
                serviceProvider,
                strategyType
                ) as IEmailStrategy;

            if (emailStrategy is null)
            {
                throw new InvalidOperationException(
                    $"The strategy: '{strategyName}' could not be created " +
                    $"from the type: '{strategyOptions.Type}'"
                    );
            }

            return emailStrategy;
        });

        bootstrapLogger?.LogDebug(
            "Registering the CodeGator SMTP client"
            );

        hostApplicationBuilder.Services.AddScoped<SmtpClient>(serviceProvider =>
        {
            bootstrapLogger?.LogDebug(
                "Fetching the email strategy options"
                );

            var strategiesSection = hostApplicationBuilder.Configuration.GetSection(
                $"{EmailServiceOptions.SectionPath}:Strategies"
                );

            foreach (var strategySection in strategiesSection.GetChildren())
            {
                var strategyOptions = new SmtpEmailStrategyOptions();
                strategySection.Bind(strategyOptions);

                if (strategyOptions.Name.Equals("SMTP", StringComparison.InvariantCultureIgnoreCase))
                {
                    bootstrapLogger?.LogDebug(
                        "Creating an SMTP client instance"
                        );

                    var smtpClient = new SmtpClient()
                    {
                        Host = strategyOptions.ServerAddress,
                        Port = strategyOptions.ServerPort,
                        EnableSsl = strategyOptions.EnableSSL,
                        Timeout = strategyOptions.Timeout ?? -1,
                        Credentials = new NetworkCredential(
                            strategyOptions.UserName,
                            strategyOptions.Password
                            ) 
                    };

                    return smtpClient;
                }
            }

            throw new InvalidOperationException(
                "The strategy options for the 'SMTP' option are missing from the configuration " +
                $"{EmailServiceOptions.SectionPath}:Strategies section."
                );            
        });

        return hostApplicationBuilder;
    }

    #endregion
}

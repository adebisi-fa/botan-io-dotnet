## Botan IO API for .Net 

A .Net implementation of the botan.io analytics and url shortening API for Telegram Bot developers.

## Usage

### Installation

    Install-Package BotanIO.Net

### Hook up your Botan IO token

```C#
    var botan = new BotanIO.Api.Botan("<botan-io-token-goes-here>");
```

### Analytics

```C#
    //
    // Basic scenario
    //
    botan.Track("Search", new { some_metric = 100, another_metric = 100 }, "1234567890");
    
    //
    // Advance scenario
    //
    
    // Date cohorts
    botan.Track(
      "Cohorts", 
      DateTime.UtcNow /* could have been 'webhookUpdateObject.date' */,
      "1234567890"
    );
    
    // Single depth, commands order
    botan.Track(
      "CommandOrders",
      new {
        last_command = current_command
      },
      "1234567890"
    );
    
    // Double depth, commands order
    botan.Track(
      "CommandOrders",
      new {
        before_last_command = new {
          last_command = current_command
        }
      },
      "1234567890"
    );
```

### How do I get my telgr.me URL?

```C#
    var shortenedUrl = botan.ShortenUrl("http://botframework.com");
    // Use your shotenedUrl e.g. inject in response to bot user, send as SMS, etc.
```

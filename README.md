# :black_joker::bear:

**Pack Bear** (:black_joker::bear:) is a pack of cards. Cards can be added, validated, removed and drawn. Random card draws are noise driven using unique values such as seed and game / session ID.

The structure of the cards (options count, player stats etc) dictate the rules of the game and are set in `appsettings.json`. [DealerBear](https://github.com/robinlacey/DealerBear) (🎰:bear:) draws cards according to the card structure. 

Cards are added to the pack as one or many Json files. Each is checked prior to adding to ensure it follows the correct structure. Here's an example. 

```

{
    "ID":"ID1",
    "Title":"Simple Card",
    "Description":"Description",
    "ImageURL":"URL",
    "Weight":"Common",
    "Options":{
        "Health":{
            "Title":"Option 1 Title",
            "Description":"Option 2 Description",
            "Health": 1,
            "Power": -2,
            "Money": 3,
            "CardsToAdd":[]
        },
        "Option2":{
            "Title":"Option 2 Title",
            "Description":"Option 2 Description",
            "Health": 10,
            "Power": -9,
            "Money": 8,
            "CardsToAdd":[]
        }
    }
}
```

When cards are added the PackVersion is incremented. Players using a previous pack version will continue to have the same cards available and will not see new cards. If the Card ID is already in use it will be moved from the old pack into the new one.



**Warning**: 🎰:bear: is still work in progress and is missing a big chunk of functionality. 

To run:  

Pack Bear uses `MassTransit` with `RabbitMQ` by default. This could be easily changed by creating a different `IPublishMessageAdaptor `

- Set the `RABBITMQ_HOST` environment variable along with `RABBITMQ_USERNAME` and `RABBITMQ_PASSWORD` 
- Build and run the `Dockerfile `
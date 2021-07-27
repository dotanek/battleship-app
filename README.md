
#  BattleshipApp

Battleship app is an application based on ASP .Net and React that allows users to simulate game of battleship.

## Choices

One of the main ideas I had during the process of creating this application was to make it easy to extend to a fully playable game. For that reason I tried to make models in a way that makes it easy to swap computer with a human player.

#### Game representation
I decided to represent the model of two boards belonging to the players as an array of fields. This solution allowed me to easily mark fields that have already been interacted with. On the top of flag indicating if certain field has been attacked before fields also hold reference to any ship that "sits" on top of them - this way I was able to determine whether an attack was a hit way more easily as well as check whether the ship still has any hit points left.

Ship models role was to keep information of its hit points, that way determining if certain ship is sunk required way less computation as opposed to checking manually if every adjacent field containing ship has beet fired at. The most important reason the ship class has been introduced was to allow for displaying models in a correct way on client side - thanks to keeping track of ships "root", direction, and type. These informations are irrelevant when it comes to the logic of the gameplay.

Each action taken by one of the players is being kept as the instance of Move class. Because of that I was able to simulate the entire game in one go and merely animate it on the client side. In case the game was ever played step by step, for example if two human players were playing it (simulating the entire gameplay would be impossible), this class would allow to exchange the information about each new move without the need of resending the entire board model after every change.

#### Random ship placement
When the ship positions are not provided the to GameManager, a class implementing IBoardGenerator interface is used to generate the boards. I decided to make it an interface in case I'd ever want to implement it in a different way. The only present implementation of that interface is the RandomBoardGenerator which uses backtracking algorithm to place all ships of hardcoded types on the array of fields. The reason I choose to use backtracking is that If I ever decide to increase the amount of ships that need to be placed on board there can occur a situation when due to "unlucky" random numbers not all ships can fit on the remaining space - thanks to backtracking the placement of previous entities would be changed until all of them can be successfully placed.

#### Computer players
The AI making decisions regarding which coordinates to attack next are represented by IComputerPlayer interface and has 2 implementations as of now. The first one named RandomComputerPlayer is purely random and only checks if certain field was already attacked in the past. I created this class mainly as placeholder until a "smarter" one is created but it can also serve as an very easy opponent in case difficulty settings were ever introduced. A bit more efficient AI named ConsequentComputerPlayer prioritizes attacking fields around previously hit spots that belong to not yet sunk ships. While the AI could be way more efficient if the memory of their last actions was kept I decided to have them only use the board itself for their decision-making - thanks to that the instance of bot doesn't need to be kept for each active game (in case its ever done against human for example).

#### Conclusion
Some things could definitely be done more efficiently but everything seems to be working as intended. The provided solution should have been tested - and most likely would If I hadn't misjudged the amount of free time I'd get in the past few days. Another thing It's lacking is some better error handling which I was not sure how to tackle in the game like this.

## Installation

After cloning the repository in order to run the API navigate to server directory, open the solution and run it preferably using "BattleshipAPI" profile.

To run the client application navigate to client directory and use npm script to install dependencies.
```
npm i
```
After the dependencies have been installed run another npm stript to start the application.
```
npm start
```

###### Notice
Make sure that the API url found in config directory is using the same port that the server is running on (5000 by default).



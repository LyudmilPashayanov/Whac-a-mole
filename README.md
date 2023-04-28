# Whac-a-mole
Miniclip tech assignment
The "Whac-A-Mole" project was a technical assignment created for Miniclip NL (f.k.a. Gamebasics). Its aim is to assess my technical knowledge.

## Technical Description
The project was developed using C# on the Unity Engine 2021.3.23f1 version. The app was tested and working in landscape mode on an Android device or in the Unity Editor on Windows.

## Extra Plugins
For the animations in the project it was used the DoTween plugin and for persitency it was used the Microsoft LiveOps service- Playfab.

## Dependencies
Internet- The project uses non-flat persistent storage system- Playfab. Constant access to the internet is required, so that the database can be updated successfully.
## How to run?
You can run the app/game from:

- Inside the Unity Editor with version 2021.3.23f1.
OR
- Build the game on an Android device from the Unity Editor.
OR
- Download it from the following link: https://1drv.ms/u/s!Avm8s6sBTUmyrFUlWMtfy4QLepOR?e=Bd6LRN

## About the game
In the "Whac-A-Mole" game you need to whack moles which come out from holes on the screen. There are different types of Moles: Normal Mole, Diamond Mole and Bomb Mole.
You need to be careful not to hit the Bomb Moles, because they will explode and harm you(r score)!
If you whack the normal and diamond Moles consecutively without whacking a bomb Mole you increase your combo meter and you receive even more points!
Try to get as much points as possible and compare yourself to everyne in the world who played the game.

## Cool technical stuff
With the aim to make the app as optimized and flexible as possible:
### Patterns:
- MVC pattern was used to handle the UI.
- SOLID principles were taken consideration and used when applicable, so that the app can reuse its code as much as possible and allow maintainability and flexability.
- Factory Pattern to create Moles with different functionalities and Pooling technique inside the factory in order to optimize the Moles appearing on the screen.
- Singleton pattern for an Audio Manager, so that you can play sounds from anywhere in the code.
- UI Pooling technique was used in order to handle the data in the high score panel. The pooling script is done in a way to be reused on different scroll views with different styles and prefabs.

### Playfab:
A non-flat percisency storage system was used- Playfab. Some of the game rules data of the game is stored in Playfab and in JSON format.
Each player/user has its own player data, which is constantly synced with the data shown in the game/app.
The developers can track when the player opens and how the player uses the app, from the Playfab console, within their browser.
The developers can dynamically change the Title data of the game/app from the Playfab console, within their browser.
Every update you make to the Title data, the next time the user starts the app/game, he will see the new changes, as the app gets the data from the server.

## TODO:
As the game could be even more improved I would:
- Add a second page in the tutorial panel, where I would explain how the scoring works.
- I would add login, as now similar names can conflict in the global high score panel.
- I would add more UI effects while you whack moles, so that you can see better when you are doing great or bad :).

## Early design   
<img src="https://user-images.githubusercontent.com/41620452/234694279-2e259247-ed3a-4295-8868-947812d5c5c4.jpg" alt="My Image" width="500"/>
<img src="https://user-images.githubusercontent.com/41620452/234694305-51f77247-a56b-4ecb-a6eb-d4d15989503f.jpg" alt="My Image" width="500"/>

## Questions
For further questions about the game, please refer to its codebase, where almost all functions are commented and explained. For more information, please contact me.

# Wumpus-XNA-Game-Engine
A game engine written in C# using the XNA technology. Includes a 3D rendering system, physics engine, and AI implementation. Is designed specifically for Hunt the Wumpus variant games although could be adapted for other needs.
Check out the website [here](http://wumpusengine.com). **Won the best Hunt the Wumpus Game at the Microsoft Hunt the Wumpus 
competition.**

The Wumpus Game Engine is a comprehensive 3D XNA game engine. While it was designed for the Hunt the Wumpus game it can easily be changed to suite the needs of any other game due to the highly extensible and adaptable code. The Hunt the Wumpus Game code is completely abstracted from the rest of the code. 

In the engine there are four main components: rendering, physics, editing, and AI. They all work together to make an easy to use game engine. Your own scenes can easily be created through the in game editor which allows importing custom meshes and customizing all graphics settings. Game mechanics can then be implemented in code through interfacing with the Wumpus Game Engine. The engine abstracts all of the rendering, physics, and AI code so attention can be focused on implementing gameplay alone. However, if you wanted to change fundemental engine code you totally could. 

- Controls for the game can be found [here](http://wumpusengine.com/Usage.aspx)
- Pictures of the engine in action can be found [here](http://wumpusengine.com/FeaturesPage.aspx)

### Rendering
- Deferred rendering
- Screen space ambient occlusion
- Shading with specular, diffuse, and ambbient lighting  model
- Custom mesh rendering
-  Can support thousands of lights at once with the deferred rendering system
  - Point lights
  - Spot lights
  - Directional lights
- Custom texture rendering
- Customizable GPU particle systems
- Post processing effects
- Shadow mapping using the cascade shadow mapping technique. Supported for...
- Lens flares
- Water plane effects
- Mesh and light culling

### Physics
Comes equiped with a full fledged physics system. Collisions, constraints, and movement are all implemented. You can even define your own physics data.

### AI
AI are equiped to use path finding techniques, do graph navigation, have states and much more.

### In Game Editor
Change every aspect of the scene with the in game editor. Whether it be as samll as the the particle rotational velocity in a particle system or as major as post processing effects. No coding necessary!

## Using
The prerequisites for running this project are Visual Studio 2010 and the XNA 4.0 runtime. Once both of these are obtained go download the project then open the .sln file with Visual Studio 2010. Then run the project in Visual Studio 2010.



## Screenshots
Below: Beautiful scenes with complex shading which are completely customizable through the in game editor.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic0.png)

Below: A heavy bloom is applied as a post processing effect. Use post processing effects to change the mood of the scene.  Make these changes in a few simple steps. No programming required.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic1.png)

Below: Tone mapping applied to the scene to create a more gloomy atmosphere.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic2.png)

Below: A spot light is illuminating the scene.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic3.png)

Below: A sun flare can be seen shining down on the scene.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic4.png)

Below: Water in a scene.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic6.png)

Below: Natural sunlight beating down on the scene.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic9.png)

Below: Fire particle system in a scene.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic15.png)

Below: Transparent object materials are fully supported.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic14.png)

Below: Thousands of lights are supported at once.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic18.png)

Below: Dynamic show casting at work
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic20.png)

Below: Further demonstration of a shadows for a complex object.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic11.png)

Below: Spot light illuminating a part of a scene.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic12.png)

Below: Even in a dark lighting environment the scene looks great.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic13.png)

Below: Import your own custom game models.
[alt](https://github.com/ASzot/Wumpus-XNA-Game-Engine/blob/master/img/wumpus/pic19.png)

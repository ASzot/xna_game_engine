
A simple physics settings editor to create bounding box information for meshes to be used in the Wumpus Game Engine.
The steps to using this are simple:
-Give the filename of the model to load relative to the Game Content folder for THIS VISUAL STUDIO PROJECT NOT THE WUMPUS GAME
 ENGINE VISUAL STUDIO PROJECT.
-The model will now be rendered on screen. Using the bounding box editor form create and delete bounding boxes as you see fit.
 Use W/S/A/D to move through around the model with convenience. 
-Save the physics data. The file will be in the given folder from the start up of the application.
-Move the physics data to the Physics Data folder in the Wumpus Game Engine game content folder. This can then be loaded into
 the Wumpus Game Engine application. See the flare model for an example of how this works (it's configured for that model).

Controls:
	W -> Move camera forward
	A -> Move camera left
	S -> Move camera backward
	D -> Move camera right
	B -> Physics data editor form
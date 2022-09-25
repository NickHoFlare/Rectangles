# Rectangles
Rectangles are my favourite shape

## About
This project is an interactive console application built using .NET6.

## Objectives
Track the position of a collection of rectangles on a grid that supports the following actions:
 - Create a grid
 - Place rectangles on the grid
 - Find a rectangle based on a given position
 - Remove a rectangle from the grid by specifying any point within the rectangle
 - Display the grid and rectangles as ASCII art

## Constraints
 - A grid must have a width and height of no less than 5 and no greater than 25
 - Positions on the grid are non-negative integer coordinates starting at 0
 - Rectangles must not extend beyond the edge of the grid
 - Rectangles must not overlap

## Instructions
### Known Issues
Unit Tests all pass when run in isolation file-by-file or individually, 
however something holds their execution when tests are run all at once - race condition?

### How to run Rectangles
1. Pull down this repository onto your local machine
2. open the .sln file (on the top level of the repo) in Visual Studio 2022 (not sure if .NET6 works on VS2019)
3. Ensure the "Rectangles" project is set as the startup project
4. Hit F5 to start the application!

ALTERNATIVELY, build the solution on Visual Studio, then navigate to the `bin\Debug\net6.0` folder of Rectangles,
double-click the `Rectangles.exe` that should be found in that folder.

### Using Rectangles
When first launching the application, you will be faced with a welcome screen, and a prompt requesting you to provide parameters
to create your first grid. This step cannot be skipped. 
Ensure that you type in values for the length and height that are no less than 5 and no more than 25.

Following this, you should be presented with a menu of commands that you can make.

Valid commands are surrounded in brackets, as displayed on the menu. For example, 

```
Please pick an option:
[PLACE] a rectangle
[FIND] a rectangle
[REMOVE] a rectangle
[DISPLAY] the grid
[LIST] all rectangles
[CREATE] a new grid (reset)
show this [MENU]

[EXIT] rectangles
```

In the above, you can enter the `place`, `create` or `exit` etc commands to perform the actions as described on the menu.
Commands are case-insensitive.

Enjoy!

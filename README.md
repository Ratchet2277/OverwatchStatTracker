# OverwatchStatTracker
## Basic presentation
Tracker for Overwatch ranked activity.

Due to lack of API from Blizzard, all games data have to be entered manually by the user for corect tracking

At the moment, this system can track win rate depending on the role (DPS/Tank/Support of Open Queue), the heroes played. Also show average SR win and lose after a game.

More stats will be added during the development.

## Usage

There is currently no easy use options. If you want this project to run on your setup, you have to clone the depot and build it from your IDE

## Requirement for dev

* .Net 6.0 at least (pre-installed by Visual Studio)
* An MSSQL Server with a (LocalDB) Instance (usually pre-installed by Visual Studio)
* Node JS for installing external JS/CSS Library
* TypeScript Compiler (tsconfig.json include in wwwroot)
* SCSS Compiler (you can use Nodejs 'sass' package)
**Optional**
* dotnet-ef tool can be useful for migration, can be installed by the command `dotnet tool install --global dotnet-ef`

## How to build locally

1. Clone depot on your usual project location
2. Run `npm install` in the OverwatchTracker(web app)/wwwroot directory
  * `cd your/project/location/directory/OverwatchTracker/wwwroot`
  * `npm install`
3. Initialize TypeScript and SCSS Compiler (I recommend to set up a Watcher for long term dev on it)
4. Run the command `dotnet ef database update` to create database (you can skip this one if you create a `TrackerDB` base on your (LocalDB) Instance, next step should create structure too 
5. Run the Initializer console command (through Visual Studio/IDE Run option) (not required for migration, but at least for the 1st run since it create base datas un DB)
6. Once the console app finish, just run the ISS Express:Overwatch Tracker in you're IDE, then the app should run fine and you can start to create an acount and input your data

# Stationeers Structure and Base Mover

This tool allows you to move bases & other structures Stationeers by selecting
them individually adjusting their positions.

This is especially useful if the developers change the terrain format in, say
the beta development banch.  This may put your bases underground or otherwise
misalign them with the terrain.  The game will warn you with the following
dialog when this has happened:

> Failure!
> Terrain save data invalid.  This world has been modified.
> &gt;Cancel&lt; &gt;Reset Terrain&lt;

If you select "Reset Terrain" and find you base underground or otherwise
exploded, you can use this tool to move the base back to a reasonable
position.

## Status

This program is in early development and a total hack.  It worked well enough
for me to move my 6 bases after the last terrain format change in beta.  Only
two ended up underground, but my main base wes exploded and a mining outpost
completely buiried.  Thankfully, I had Beacons on everything, so I was able to
find them all and move them back to a reasonable position (up by 6 meters),
while leaving all the other bases in place (these were fine).

## Usage
1. *Launch Stationeers* - start the game.  This ensures Steam cloud-saves
   have synchronized your save files.
1. *Launch this tool* - If you haven't already, build the solution in your
   preferred IDE (e.g., Visual Studio) and run the executable.
1. *Open a save game* - use the "Open Save Game" button to select a save file.
1. *Identify structures* - Expand structures in the left panel to see
   their contents.  Look for items you have renamed with the Labeler tool,
   or just examine the structure names, to get an idea of which base each
   structure belongs to.
1. *Rename structures for reference* - Use Edit/Rename (or F2) to rename
   structures for easier identification.  This information is local-only
   and will not affect your save file, so you can use any names you like.
1. *Determine how much to move each structure* - In Stationeers, load your
   save game and use the in-game tools to determine how much you want to
   move each structure.  You can use the GPS or Deep Miner Cartridge to
   get the Y coordinate of ground level, then dig (or fly) to a frame on
   the structure you want to move, and note its Y coordinate.  Subtract
   the frame's Y coordinate from the ground level Y coordinate to get the
   offset you need to apply to the structure's Y coordinate.  Positive
   Y offsets move the structure up, negative Y offsets move it down.
1. *Exit Stationeers, _without saving_*
1. *Adjust structure positions* - In this tool, select a structure in the
   left panel, then use Edit/Move Structure... to adjust
   its position.
1. *Save your changes* - Once done, use File/Save to save the changes to the
   save file.
1. *Verify your chagnes* - You can then load the modified save in Stationeers and
   verify the changes.  
1. *Regenerate Rooms* - From with Stationeers, use the "F3" key to open
   the console and type "REGENERATEROOMS".
1. *Save your changes!* - Once you have verified that the changes are
   correct, save your game in Stationeers to ensure the changes are retained.

## How it works

Structures are built by locating things and atmospheres
within a certain proximity range, which is set to 20 meters by default; any
thing within this range is considered part of the structure.  Note: 20 meters
seems to work fine, but if there's demand this can easily be moved into a
configuration setting.  Loose items in the world grouped into the `Loose
Items` structure.

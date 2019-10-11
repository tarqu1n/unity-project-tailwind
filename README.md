# unity-project-tailwind
Tower Defence / RTS test

## Glossary
- **SpawnPoint** - Where a *Monster* appears on the map
- **TerminatePoint** - Where a *Monster* travels towards, when they reach it they are considered to have passed completed their journey.
- **Monster** - object that travels between a *SpawnPoint* and *TerminatePoint*.
a
## Layers
 - **Terrain** Contains the objects which make up the solid ground and track. 
 - **Scatter** Contains the items placed on the terrain which are there for purely aesthetic reasons
 - **Resources** Contains the objects which the player can harvest resources from, such as trees

 ## Notes
 - Builders must be assigned to buildings for them to operate, some buildings require more than one builder to operate.
 - The player must balance their resources, providing for their workers, battling the on map enemies to get resources / items they need to unlock their economy all while defending against waves of enemies coming through the track.
 - Player can only pause the game during the time between waves.
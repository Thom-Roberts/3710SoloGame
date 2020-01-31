# Game Idea

The idea is that the player is in competition with the A.I. to color more than half of the blocks. The tricky thing is that the block will move up and down randomly.

## Rules for blocks

Blocks are allowed to move up 1 space or down 1 space. Player's cannot move up to reach a space, but they can fall down if they are on one. Blocks should have some low chance of moving up or down.

## Movement

Player and enemy movement should be rather fast, but cannot be instantaneous. Should move only by pressing the right/left keys once (holding should not do anything). The opponent should move every 1.5 seconds or so. Play around with that number to what feels good. Movement should be restricted if an attempt is made to move onto a block the opposing party is on or to move to a block above them. Perhaps gravity will need to be increased in order to fall more quickly to the floor if they jump off.

## UI

Ideally a bar would be made that shows the progress that the player vs the enemy is on. More realistically these will be numbers and perhaps a total? Particle effect will be when the player wins, and maybe an opposing color if the enemy wins. Sound is more tricky. Probably a victory sound (like final fantasy's), but most likely I'll need some song looping in the background (probably just rip a videogame sound like a persona song).

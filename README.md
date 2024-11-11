<h1>Death Dealt Hand</h1>
<p>is a short playing card game that introduces a twist to the traditional italian card game of Scopa. This is done by infusing horror elements into an otherwise calming experience through fine tunned lighting and post processing effects as well as carefully selected sound effects and ambiance. Inspired by games such as Buckshot Roulette and Inscription, we aimed to create a chilling but memorable game that brings a unique vibe to the playing card table. This game was a collaborative work made in unity by a team of three programmers as a final project.</p>
<br><br>

<h2>Gameplay</h2>
<p>Death Dealt Hand's gameplay is simple and breaks down into three main systems that work in unison to deliver the final coherent experience. These three systems are:</p>
<ul>
  <li><a href="https://github.com/LorenzoPicken/Death-Dealt-Hand/blob/main/README.md#the-table">The Table</a></li>
  <li><a href="https://github.com/LorenzoPicken/Death-Dealt-Hand/blob/main/README.md#the-opponent">The Opponent</a></li>
  <li><a href="#">The Effect Deck</a></li>
</ul>
<br><br>

<h3>The Table</h3>
<p>The Table is the name that we gave to the dealer or more accurately, the game manager. Responsible for overseeing the game state and tracking important values, the table provides crutial information to various other systems which allows them to perform important procedures. Information such as score, player turns, deck size, available table slots, etc, are all accounted for by this manager. Additionally, it is also responsible for executing tasks such as distributing cards, shuffling the deck, calculating points and managing the game state. Systems such as the player controller and enemy ai rely heavily on variables that the table tracks to function properly.</p>
<br>

<h3>The Opponent</h3>
<p>The opponent is the algorythm responsible for the decision making of the enemy player and is by far the most complexe system in the game. In order to achieve a realistic and challenging opponent for the player, the algorythm will run multiple simulated senarious internally then make the appropriate decision based on which outcome yields the most points. The amount of points each individual play yields is calculated by subtracting the risk of the play from the reward. In order to find the reward of a given outcome, the simulated play is compared to a list of template outcomes with pre-set reward values. After finding the closest template to the current situation, the reward level for that outcome will be set to the default reward value of the compared template. Following this, the algorithm will calculate the risk factor. The risk can be as low as 0 or as high as 3 and directly correlates to the likelyhood that after the simulated play is executed, the player will be left in a favorable position to score points. From here the play with the best score is selected and depending on the final reward value, the algorythm will decide if the outcome is worth executing or if giving up a card to avoid potentially helping the player is more ideal in it's current position.</p>
